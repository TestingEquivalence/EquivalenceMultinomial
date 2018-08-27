
''' <summary>
''' small log file class
''' writes log file with unified interface 
''' </summary>
''' <remarks></remarks>
Public Class log
    Dim FileStreamer As IO.StreamWriter
    Dim toFile As Boolean
    ''' <summary>
    ''' constructer
    ''' overwrite or create file under given path
    ''' </summary>
    ''' <param name="path">full path to log file</param>
    ''' <remarks></remarks>
    Sub New(path As String, append As Boolean)
        FileStreamer = New IO.StreamWriter(path, append)
        toFile = True
    End Sub

    Sub New()
        toFile = False
    End Sub
    ''' <summary>
    ''' write line in log file
    ''' </summary>
    ''' <param name="row">string to output</param>
    ''' <remarks></remarks>
    Sub WriteLn(row As String)
        If toFile Then
            Me.FileStreamer.WriteLine(row)
        Else
            Console.WriteLine(row)
        End If
    End Sub
    ''' <summary>
    ''' write in log file
    ''' </summary>
    ''' <param name="row">string to output</param>
    ''' <remarks></remarks>
    Sub Write(row As String)
        If toFile Then
            Me.FileStreamer.Write(row)
        Else
            Console.Write(row)
        End If
    End Sub
    ''' <summary>
    ''' close log file
    ''' </summary>
    ''' <remarks></remarks>
    Sub close()
        If toFile Then
            Me.FileStreamer.Close()
        End If
    End Sub

    Sub WriteTestReport(prm As TestParameter, res As Double)
        With prm
            Me.WriteLn("--------------------------")
            Me.WriteLn(.key)
            Me.WriteLn("probability dense of P:")

            'Dim p As Double() = .p.get_pr
            'For i As Integer = 0 To p.GetUpperBound(0)
            '    Me.Write(p(i) & ";")
            'Next

            Me.WriteLn("")
            Me.WriteLn("probability dense of Q:")

            If Not IsNothing(.q) Then
                'Dim q As Double() = .q.get_pr
                'For i As Integer = 0 To q.GetUpperBound(0)
                '    Me.Write(q(i) & ";")
                'Next
                WriteLn("l1=" & l1(.p.get_pr, .q.get_pr))
                WriteLn("l2=" & l2(.p.get_pr, .q.get_pr))
                If .a > 0 Then
                    WriteLn("a=" & .a.ToString)
                    WriteLn("smth_l1=" & smooth_l1(.a)(.p.get_pr, .q.get_pr))
                End If
            End If

            If Not IsNothing(.ex_point) Then
                WriteLn("n of direction=" & .ex_point.Count)
            End If

            WriteLn("reset=" & .reset)
            WriteLn("n=" & .n)
            WriteLn("n_toss=" & .ntoss)
            WriteLn("n_bst=" & .nBstSamples)
            WriteLn("alpha=" & .alpha)
            WriteLn("eps=" & .eps)
            WriteLn("eps_adj=" & .adj_eps)
            WriteLn("power=" & res)
        End With
    End Sub
    
    Sub WriteMatrix(q As Double(), n1 As Integer, n2 As Integer)
        Dim k As Integer = 0
        For i = 0 To n1 - 1
            For j = 0 To n2 - 1
                Me.Write(q(k).ToString(".000000") + ";")
                k = k + 1
            Next
            Me.WriteLn("")
        Next
    End Sub
    Sub write(result As TestResult)
        Me.WriteLn("result: " & result.result)
        Me.WriteLn("min epsilon: " & result.minEps)
    End Sub

    Sub write(p As clsMultinomialDistribution)
        For Each e In p.p
            Me.Write(e.ToString("0.0000000000") + ";")
        Next
    End Sub
End Class
