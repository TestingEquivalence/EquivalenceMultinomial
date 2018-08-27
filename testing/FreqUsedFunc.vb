Module FreqUsedFunc
    Public l22 As Func(Of Double(), Double(), Double) =
        Function(p As Double(), q() As Double)
            Dim l(p.GetUpperBound(0)) As Double
            For i As Integer = 0 To p.GetUpperBound(0)
                l(i) = Math.Pow(p(i) - q(i), 2)
            Next

            Return sum(l)
        End Function


    Public l2 As Func(Of Double(), Double(), Double) =
        Function(p As Double(), q() As Double)
            Return Math.Sqrt(l22(p, q))
        End Function

    Public l1 As Func(Of Double(), Double(), Double) =
        Function(p As Double(), q() As Double) As Double
            Dim l As Double = 0
            For i As Integer = 0 To p.GetUpperBound(0)
                l = l + Math.Abs(p(i) - q(i))
            Next

            Return l
        End Function

    Public sum As Func(Of Double(), Double) =
        Function(vec As Double())

            Dim s As Double = 0
            For Each v In vec
                s = s + v
            Next
            Return s
        End Function

    Public normiert As Func(Of Double(), Double()) =
        Function(vec As Double())
            Dim ls As New List(Of Double)
            Dim s = sum(vec)

            For Each v In vec
                ls.Add(v / s)
            Next

            Return ls.ToArray
        End Function


    Public sumInteger As Func(Of Integer(), Integer) =
        Function(vec As Integer()) As Integer
            Dim s As Double = 0
            For Each v In vec
                s = s + v
            Next
            Return s
        End Function

        
    Public sign As Func(Of Double, Double) =
        Function(v As Double) As Double
            Dim s As Double
            If v < 0 Then
                s = -1
            ElseIf v = 0 Then
                s = 0
            Else
                s = 1
            End If
            Return s
        End Function

    
    Public smooth_abs As Func(Of Double, Double, Double) =
        Function(x As Double, a As Double) As Double
            Dim v As Double
            v = x ^ 2 + a ^ 2
            v = Math.Sqrt(v)
            Return v
        End Function

    Function smooth_l1(a As Double) As Func(Of Double(), Double(), Double)
        Dim f As Func(Of Double(), Double(), Double)
        f = Function(x, y)
                Dim l As Double = 0
                For i As Integer = 0 To x.GetUpperBound(0)
                    l = l + smooth_abs(x(i) - y(i), a)
                Next
                Return l
            End Function
        Return f
    End Function

    
    
    Public l22_derivative As Func(Of Double(), Double(), Double()) =
        Function(p() As Double, q() As Double)
            Dim y(p.Length - 1) As Double

            For i As Integer = 0 To p.Length - 1
                y(i) = 2 * (p(i) - q(i))
            Next
            Return y
        End Function

    Public l1_derivative As Func(Of Double(), Double(), Double()) =
    Function(p As Double(), q As Double()) As Double()
        Dim y(p.Length - 1) As Double

        For i As Integer = 0 To p.Length - 1
            y(i) = sign(p(i) - q(i))
        Next
        Return y
    End Function

    Private Function diff_smt_abs(x As Double, a As Double) As Double
        Return x / Math.Sqrt(x ^ 2 + a ^ 2)
    End Function


    Public Function smooth_L1_derivative(a As Double) As Func(Of Double(), Double(), Double())
        Return Function(p As Double(), q As Double()) As Double()
                   Dim y(p.Length - 1) As Double

                   For i As Integer = 0 To p.Length - 1
                       Dim diff = p(i) - q(i)
                       y(i) = diff_smt_abs(diff, a)
                   Next
                   Return y
               End Function
    End Function
End Module
