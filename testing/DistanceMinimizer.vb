Public Class DistanceMinimizer
    'p is always the probability measure
    'distance to which should be minimized
    Public _dst As Func(Of Double(), Double(), Double)
    Private p() As Double
    Private q() As Double

    Friend eps As Double
    Friend lp, lq As Double()

    Friend bound As Double


    Sub New(dst As Func(Of Double(), Double(), Double), q() As Double)
        Me._dst = dst
        Me.q = q
    End Sub

#Region "helper"

    Function Symplex2Space(p() As Double) As Double()
        Dim i, n As Integer
        n = p.Length
        Dim x(n - 1) As Double

        For i = 0 To n - 1
            x(i) = Math.Log(p(i) + 1)
        Next
        Return x
    End Function

    Function Space2Symplex(x() As Double) As Double()
        Dim i, n As Integer
        n = x.Length
        Dim p(n - 1) As Double

        For i = 0 To n - 1
            p(i) = Math.Max(Math.Exp(x(i)) - 1, 0)
        Next

        Dim s As Double = sum(p)
        For i = 0 To n - 1
            p(i) = p(i) / s
        Next
        Return p
    End Function

    Function RandomPoint(d As Integer,
                       rnd As Random) As Double()
        Dim x(d - 1) As Double
        For i = 0 To d - 1
            x(i) = rnd.NextDouble
        Next

        Return Me.Space2Symplex(x)
    End Function

    Function LinComb(v1 As Double(), v2 As Double(), alpha As Double) As Double()
        Dim lc(v1.Length - 1) As Double

        For i As Integer = 0 To v1.Length - 1
            lc(i) = v1(i) * alpha + v2(i) * (1 - alpha)
            If lc(i) < 0 Then lc(i) = 0
        Next

        Dim s As Double = sum(lc)
        For i As Integer = 0 To v1.Length - 1
            lc(i) = lc(i) / s
        Next

        Return lc
    End Function

#End Region


    Function dst(p() As Double) As Double
        Return Me._dst(p, q)
    End Function

#Region "special points"

    Function linearBoundaryPoint(p As Double(), q As Double(), eps As Double) As Double()
        Dim f As Func(Of Double, Double)
        f = Function(alpha)
                Dim lc = Me.LinComb(q, p, alpha)
                Dim dst = Me.dst(lc)
                Return dst - eps
            End Function

        Dim s As Double
        s = MathNet.Numerics.RootFinding.Brent.FindRoot(f, 0, 1, , 1000)
        Return LinComb(q, p, s)
    End Function

    Sub aim_lin_est_point(x() As Double, ByRef res As Double, obj As Object)
        Dim lc() As Double
        Dim s As Double
        lc = LinComb(Me.lp, Me.lq, x(0))

        s = Me.dst(lc)

        res = Math.Pow((Me.eps - s), 2)
    End Sub

    Function RandomOuterPoint(lowBound As Double,
                              d As Integer,
                              rnd As Random) As Double()
        Dim res() As Double
        Dim max As Double = 0

        Do
            res = Me.RandomPoint(d, rnd)
        Loop Until (Me.dst(res) > lowBound)

        Return res
    End Function

    Function RandomBoundaryPoint(eps As Double,
                                 d As Integer,
                                 rnd As Random) As Double()
        Dim p = Me.RandomOuterPoint(eps, d, rnd)
        Return Me.LinearEpsPointBis(p, q, eps)
    End Function


    Public Function RandomLinBoundary(eps As Double,
                                      p() As Double,
                                   rnd As Random,
                                   dst As Func(Of Double(), Double(), Double)) As Double()
        Dim q As Double()
        Dim d As Integer = p.Length

        q = Me.RandomPoint(d, rnd)

        Do While Me.dst(q) <= eps
            q = Me.RandomPoint(d, rnd)
        Loop

        Dim f As Func(Of Double, Double)
        f = Function(alpha)
                Dim lc As Double()
                lc = Me.LinComb(q, p, alpha)
                Return Me.dst(lc) - eps
            End Function

        Dim s As Double
        s = MathNet.Numerics.RootFinding.Brent.FindRoot(f, 0, 1)
        q = LinComb(q, p, s)
        Return q
    End Function

    Function LinearEpsPointBis(low As Double(), high As Double(), eps As Double) As Double()
        Dim f As Func(Of Double, Double)
        f = Function(a As Double)
                Dim gues = LinComb(high, low, a)
                Return Me.dst(gues) - eps
            End Function

        Dim alpha = MathNet.Numerics.RootFinding.Brent.FindRoot(f, 0, 1)
        Return LinComb(high, low, alpha)
    End Function

#End Region

End Class
