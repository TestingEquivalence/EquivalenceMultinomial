Module tests
 #Region "universal point test"
    Public universalAsymptPointTest As Func(Of TestParameter, Double(), TestResult) =
        Function(prm As TestParameter, rp As Double()) As TestResult
            Dim vol As Double
            Dim res As New TestResult(False, 1)

            With prm
                vol = clsMultinomialDistribution.getAsymptoticVolatility(
                    rp, .q.get_pr, .derivative) / Math.Sqrt(.n)

                Dim qt = MathNet.Numerics.Distributions.Normal.InvCDF(0, 1, 1 - .alpha)
                Dim crit As Double = .eps - qt * vol

                Dim T As Double
                T = prm.test_statistic(rp, prm.q.get_pr)

                res.minEps = T + qt * vol
                If T < crit Then
                    res.result = True
                Else
                    res.result = False
                End If
            End With
            Return res
        End Function

    Public universalBstPointTest As Func(Of TestParameter, Double(), TestResult) =
        Function(prm As TestParameter, rp() As Double) As TestResult
            Dim res As New TestResult(False, 1)

            'check if the value outside of the H0
            Dim bound As Double
            bound = prm.opt.dst(rp)

            If bound > prm.eps Then
                Return res
            End If

            'find linear nearest boundary point
            Dim p_lin_est As Double() = Nothing

            'go trought all external points
            Dim min_dist As Double = 1
            For Each exp In prm.ex_point
                'bisection, do not fail and  also quick
                Dim lin_p = prm.opt.LinearEpsPointBis(rp, exp, prm.eps)
                'closer rand point is found
                'reset the values
                If prm.opt._dst(lin_p, rp) < min_dist Then
                    min_dist = prm.opt._dst(lin_p, rp)
                    p_lin_est = lin_p
                End If
            Next


            'define test statistics
            Dim BoolT As Func(Of Double(), Boolean)

            BoolT = Function(a() As Double)
                        Return prm.opt.dst(a) <= bound
                    End Function

            'calculate bootstrap p value
            Dim bst As New diskr_bootstrap(New clsMultinomialDistribution(p_lin_est),
                                           prm.n, BoolT, prm.reset, prm.nBstSamples)
            Dim p_val As Double
            p_val = bst.EmpProb

            res.minEps = p_val
            res.result = (p_val <= prm.alpha)
            Return res
        End Function

#End Region
End Module
