Public Class TestParameter
    Sub New()

    End Sub
    Public p As clsMultinomialDistribution
    Public q As clsMultinomialDistribution
    Public ex_point As New List(Of Double())
    Public n As Integer
    Public reset As Boolean
    Public eps As Double
    Public ntoss As Long
    Public nBstSamples As Integer
    Public LogPath As String
    Public alpha As Double
    Public low_n As Integer
    Public a As Double
    Public key As String
    Public adj_eps As Double
    Public opt As DistanceMinimizer
    Public eps_min As Double
    Public rnd As Random
    Public derivative As Func(Of Double(), Double(), Double())
    Public test_statistic As Func(Of Double(), Double(), Double)
End Class
