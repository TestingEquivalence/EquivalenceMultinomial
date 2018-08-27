Public Module tests_equivalence
    Public Class TestResult
        Public result As Boolean
        Public minEps As Double

        Sub New(result As Boolean, minEps As Double)
            Me.result = result
            Me.minEps = minEps
        End Sub
    End Class

    ''' <summary>
    ''' The asymptotic test is based on the asymptotic distribution of the test statistic. 
    ''' Therefore the asymptotic test need some sufficiently large number of the observations.
    '''  It should be used carefully because the test is approximate 
    ''' and may be anti conservative at some points. 
    ''' In order to obtain a conservative test reducing of alpha  (usually halving) or
    '''  slight shrinkage of the tolerance parameter epsilon may be appropriate. 
    ''' </summary>
    ''' <param name="p">counts of events</param>
    ''' <param name="q">theoretical probability vector</param>
    ''' <param name="b">smoothing parameter for the total variation distance</param>
    ''' <param name="alpha">significance level</param>
    ''' <param name="epsilon">tolerance parameter</param>
    ''' <returns>
    ''' It returns the test result, which is true if the test can reject H_0. 
    ''' Additionally it returns the smallest epsilon 
    ''' for which test can reject H_0.
    ''' </returns>
    ''' <remarks></remarks>

    Public Function asymptoticTest(p As Integer(), q As Double(), b As Double, alpha As Double, epsilon As Double) As TestResult
        Dim parameter As New TestParameter

        With parameter
            .test_statistic = smooth_l1(b)
            .derivative = smooth_L1_derivative(b)
            .p = New clsMultinomialDistribution(p)
            .q = New clsMultinomialDistribution(q)
            .eps = epsilon * 2
            .n = 0
            For Each pr In p
                .n = .n + pr
            Next
            .nBstSamples = 0
            .alpha = alpha
            .opt = Nothing
            .a = b
        End With

        Dim res = universalAsymptPointTest(parameter, parameter.p.get_pr)
        res.minEps = res.minEps / 2
        Return res
    End Function

    ''' <summary>
    ''' The bootstrap test is based on the re-sampling method called bootstrap. 
    ''' The bootstrap test is more precise and reliable than the asymptotic test. 
    ''' However, it should be used carefully because the test is approximate 
    ''' and may be anti conservative. 
    ''' In order to obtain a conservative test reducing of alpha
    ''' (usually halving) or slight shrinkage of the tolerance parameter epsilon
    ''' may be appropriate. We prefer the slight shrinkage of the tolerance parameter 
    ''' because it is more effective and the significance level remains unchanged.
    ''' </summary>
    ''' <param name="p">counts of events</param>
    ''' <param name="q">theoretical probability vector</param>
    ''' <param name="b">smoothing parameter for the total variation distance</param>
    ''' <param name="alpha">significance level</param>
    ''' <param name="epsilon">tolerance parameter</param>
    ''' <param name="nDirections">
    ''' number of random directions to search for a boundary point
    ''' the number of random directions has a negative impact on the computation time
    ''' </param>
    ''' <param name="nBootstrapSamples">number of bootstrap samples</param>
    ''' <returns>
    ''' It returns the test result, which is true if test can reject H_0. 
    ''' Additionally if the test result is true, it returns the smallest epsilon 
    ''' for which test can reject H_0.
    ''' </returns>
    ''' <remarks></remarks>
    Public Function bootstrapTest(p() As Integer, q() As Double, b As Double, alpha As Double, epsilon As Double,
                                  nDirections As Integer, nBootstrapSamples As Integer) As TestResult
        Dim parameter As New TestParameter

        With parameter
            .test_statistic = smooth_l1(b)
            .derivative = smooth_L1_derivative(b)
            .opt = New DistanceMinimizer(smooth_l1(b), q)
            .n = 0
            For Each pr In p
                .n = .n + pr
            Next

            .p = New clsMultinomialDistribution(p)
            .q = New clsMultinomialDistribution(q)

            .eps = epsilon / 2
            .nBstSamples = nBootstrapSamples
            .alpha = alpha
            .rnd = New MathNet.Numerics.Random.MersenneTwister(10071977)
        End With

        'generate directions
        For i = 1 To nDirections
            With parameter
                Dim ex_point = .opt.RandomOuterPoint(.eps * 1.1, p.Length, .rnd)
                .ex_point.Add(ex_point)
            End With
        Next

        Dim res = universalBstPointTest(parameter, parameter.p.get_pr)

        'calculate minimum epsilon

        'start value
        Dim asymptRes = universalAsymptPointTest(parameter, parameter.p.get_pr)
        Dim eps = asymptRes.minEps

        Dim f As Func(Of Double, Double)
        f = Function(_eps As Double) As Double
                parameter.eps = _eps
                Dim pValue = universalBstPointTest(parameter, parameter.p.get_pr).minEps
                Return pValue - alpha
            End Function

        Try
            res.minEps = MathNet.Numerics.RootFinding.Brent.FindRoot(f, 0, eps) / 2
        Catch
            res.minEps = 1 / 0
        End Try

        Return res
    End Function
End Module
