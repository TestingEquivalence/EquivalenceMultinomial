Public Class clsMultinomialDistribution
    Friend p() As Double

    Sub New(p As Double, n As Integer)
        ReDim Me.p(n)
        For k As Integer = 0 To n
            Me.p(k) = MathNet.Numerics.Distributions.Binomial.PMF(p, n, k)
        Next
    End Sub


    Sub New(pr As Double())
        Dim s = sum(pr)
        ReDim Me.p(pr.Length - 1)

        For i As Integer = 0 To pr.Length - 1
            Me.p(i) = pr(i) / s
        Next
    End Sub

    Sub New(d As Integer)
        ReDim Me.p(d - 1)

        For i As Integer = 0 To d - 1
            Me.p(i) = 1 / d
        Next
    End Sub

    Function get_pr() As Double()
        Return Me.p
    End Function

    Sub New(pr As Integer())
        Dim count As Double = 0
        For Each s In pr
            count = count + s
        Next
        ReDim Me.p(pr.Length - 1)

        For i As Integer = 0 To pr.Length - 1
            Me.p(i) = pr(i) / count
        Next
    End Sub

 
    Public Function dblComulatedDenseFunction(ByVal dblX As Double) As Double
        Dim intX As Integer
        Dim dblSum As Double
        Dim i As Integer

        dblSum = 0
        intX = Convert.ToInt32(dblX)

        For i = 0 To intX - 1
            dblSum = dblSum + Me.p(intX)
        Next i
        Return dblSum
    End Function

    Public Function dblDenseFunction(ByVal dblX As Double) As Double
        Dim intX As Integer
        intX = Convert.ToInt32(dblX)
        Return Me.p(intX)
    End Function

    Public Function dblQuantileFunction(ByVal dblX As Double) As Double
        Dim i As Integer
        Dim dblSum As Double
        dblSum = 0
        'berechne den "Quantil"
        For i = 0 To Me.p.GetUpperBound(0)
            dblSum = dblSum + Me.p(i)
            If dblX <= dblSum Then
                Return i
                Exit Function
            End If
        Next i
        Return Me.p.GetUpperBound(0)
    End Function
    Public Function intQuantileFunction(ByVal dblX As Double) As Integer
        Dim i As Integer
        Dim dblSum As Double
        dblSum = 0

        'berechne den "Quantil"
        For i = 0 To Me.p.GetUpperBound(0)
            dblSum = dblSum + Me.p(i)
            If dblX <= dblSum Then
                Return i
                Exit Function
            End If
        Next i

        Return Me.p.GetUpperBound(0)
    End Function

    Function CheckConsistency() As Boolean
        Dim sum As Double = 0
        For Each v In p
            sum = sum + v
        Next

        Return sum = 1
    End Function

    Function int_simulate(n As Integer, rnd As Random) As Integer()
        Dim res(p.GetUpperBound(0)) As Integer

        For i As Integer = 1 To n
            'simulate
            Dim toss As Integer
            toss = Me.intQuantileFunction(rnd.NextDouble)
            res(toss) = res(toss) + 1
        Next

        Return res
    End Function

    Function dbl_simulate(n As Integer, rnd As Random) As Double()
        Dim int_res() As Integer
        int_res = int_simulate(n, rnd)

        Dim dbl_res(int_res.GetUpperBound(0)) As Double
        For i As Integer = 0 To int_res.GetUpperBound(0)
            dbl_res(i) = int_res(i) / n
        Next

        Return dbl_res
    End Function

    Function dbl_simulate_numerics(n As Integer, r As System.Random) As Double()
        Dim res(n - 1) As Integer
        MathNet.Numerics.Distributions.Categorical.Samples(r, res, Me.get_pr)

        Dim length = p.GetUpperBound(0)
        Dim dbl_res(length) As Double
        For Each t In res
            dbl_res(t) = dbl_res(t) + 1
        Next

        For i As Integer = 0 To length
            dbl_res(i) = dbl_res(i) / n
        Next
        Return dbl_res
    End Function

    Function dbl_simulate_numerics_large(n As Integer, r As System.Random) As Double()
        
        Dim length = p.GetUpperBound(0)
        Dim dbl_res(length) As Double
        Dim lng_res(length) As Long
        Dim _step As Integer = 1000 * 1000

        For i As Integer = 1 To n Step _step
            Dim res(_step - 1) As Integer
            MathNet.Numerics.Distributions.Categorical.Samples(r, res, Me.get_pr)
            For Each t In res
                lng_res(t) = lng_res(t) + 1
            Next
        Next

        
        For i As Integer = 0 To length
            dbl_res(i) = lng_res(i) / n
        Next
        Return dbl_res
    End Function


    Public Function simulate(rnd As Random) As Double
        Return -1
    End Function

    Shared Function getAsymptoticVolatility(p() As Double,
        q() As Double,
        dp As Func(Of Double(), Double(), Double())) As Double

        Dim vec = dp(p, q)

        Dim vnsq_1 As Double = 0
        For j As Integer = 0 To p.Length - 1
            vnsq_1 = vnsq_1 + p(j) * vec(j) * vec(j)
        Next

        Dim vnsq_2 As Double = 0
        For j1 As Integer = 0 To p.Length - 1
            For j2 As Integer = 0 To p.Length - 1
                vnsq_2 = vnsq_2 + vec(j1) * vec(j2) * p(j1) * p(j2)
            Next
        Next

        Dim vnsq As Double = (vnsq_1 - vnsq_2)
        Return Math.Sqrt(vnsq)
    End Function
End Class
