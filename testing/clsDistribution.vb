Public MustInherit Class clsDistribution
    'Dichtefunction:
    Public MustOverride Function PDF() As Func(Of Double, Double)
    'Verteilungsfunktion:
    Public MustOverride Function CDF() As Func(Of Double, Double)
    'Quantilfunktion
    Public MustOverride Function InvCDF() As Func(Of Double, Double)
    Public MustOverride Function simulate(rnd As Random) As Double
End Class

