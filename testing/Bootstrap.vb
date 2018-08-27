Public Class diskr_bootstrap
    Dim distr As clsMultinomialDistribution
    Dim n As Integer
    Dim SimNumber As Integer

    Dim rnd As Random = New MathNet.Numerics.Random.MersenneTwister(10071977)
    Dim T As Func(Of Double(), Double)
    Dim QuickT As Func(Of Double(), Boolean)
    Public v() As Double
    Public vpr()() As Double
    Public bv() As Double

    Function EmpProb() As Double
        Dim i As Integer = 0
        For Each res In bv
            If res Then i = i + 1
        Next

        Return i / SimNumber
    End Function

    Sub New(distr As clsMultinomialDistribution, _
            n As Integer, _
            T As Func(Of Double(), Boolean), _
            reset As Boolean, _
            SimNumber As Integer)
        Me.distr = distr
        Me.n = n
        Me.QuickT = T
        Me.SimNumber = SimNumber
        ReDim bv(SimNumber - 1)
        ReDim vpr(SimNumber - 1)

        If reset Then
            rnd = New MathNet.Numerics.Random.MersenneTwister()
        End If

        'simulate bst sample

        For i As Integer = 1 To SimNumber
            'gen multinomial
            Dim rp As Double()
            rp = distr.dbl_simulate(n, rnd)
            Me.vpr(i - 1) = rp
            bv(i - 1) = Me.QuickT(rp)
        Next

    End Sub



    Sub New(distr As clsMultinomialDistribution, _
            n As Integer, _
            T As Func(Of Double(), Double), _
            reset As Boolean, _
            SimNumber As Integer)
        Me.distr = distr
        Me.n = n
        Me.T = T
        Me.SimNumber = SimNumber
        ReDim v(SimNumber - 1)
        ReDim vpr(SimNumber - 1)

        If reset Then
            rnd = New MathNet.Numerics.Random.MersenneTwister()
        End If

        'simulate bst sample

        For i As Integer = 1 To SimNumber
            'gen multinomial
            Dim rp As Double()
            rp = distr.dbl_simulate(n, rnd)
            Me.vpr(i - 1) = rp
            v(i - 1) = Me.T(rp)
        Next

    End Sub

    Sub New(distr As clsMultinomialDistribution, _
            n_sampling As Integer, _
            n_subsampling As Integer, _
            T As Func(Of Double(), Double), _
            reset As Boolean, _
            SimNumber As Integer)
        Me.distr = distr
        Me.n = n_sampling
        Me.T = T
        Me.SimNumber = SimNumber
        ReDim v(SimNumber - 1)
        ReDim vpr(SimNumber - 1)

        If reset Then
            rnd = New MathNet.Numerics.Random.MersenneTwister()
        End If

        'simulate bst sample

        For i As Integer = 1 To SimNumber
            'gen subsampling multinomial
            Dim rpsub, rp As Double()
            rpsub = distr.dbl_simulate(n_subsampling, rnd)

            'gen sampling multionmial
            Dim sub_distr As New clsMultinomialDistribution(rpsub)
            rp = sub_distr.dbl_simulate(n, rnd)

            Me.vpr(i - 1) = rp
            v(i - 1) = Me.T(rp)
        Next

    End Sub

End Class
