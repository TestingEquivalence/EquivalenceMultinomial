Imports System.Threading
Imports System.Threading.Tasks
Module StartMod
    Sub main()
        'examples of apllication of the equivalence test to the real data sets
        Console.WriteLine("Examples of apllication of the equivalence test to the real data sets")
        Console.WriteLine("-----------------------------")

        'counts of events
        Dim p() As Integer
        'theoretical distribution
        Dim q() As Double
        'smoothing parameter
        Dim b As Double
        'test result
        Dim res As TestResult
        'significance level
        Dim alpha As Double = 0.05
        'equivalence parameter
        Dim epsilon As Double = 0.1
        'number of bootstrap samples
        Dim nBstSamples As Integer = 10000
        'number of search directions
        Dim nDir As Integer

        'Mendels Law
        Console.WriteLine("Mendel's Law")
        p = {315, 101, 108, 32}
        q = {9 / 16, 3 / 16, 3 / 16, 1 / 16}
        b = 0.01 / Math.Sqrt(315 + 101 + 108 + 32)
        nDir = 200 * p.Length
        res = tests_equivalence.asymptoticTest(p, q, b, alpha, epsilon)
        Console.WriteLine("asymptotic test, min epsilon: " & res.minEps)

        res = tests_equivalence.bootstrapTest(p, q, b, alpha, epsilon, nDir, nBstSamples)
        Console.WriteLine("bootstrap test, min epsilon: " & res.minEps)

        Console.ReadKey()
        Console.WriteLine("-----------------------------")

        'Benfords Law
        Console.WriteLine("Benford's Law for Apple daily returns")
        p = {2001, 1359, 872, 625, 468, 417, 306, 251, 208}
        ReDim q(8)
        For i As Integer = 1 To 9
            q(i - 1) = Math.Log10(1 + 1 / i)
        Next
        b = 0.01 / Math.Sqrt(6507)
        nDir = 200 * p.Length
        res = tests_equivalence.asymptoticTest(p, q, b, alpha, epsilon)
        Console.WriteLine("asymptotic test, min epsilon: " & res.minEps)

        res = tests_equivalence.bootstrapTest(p, q, b, alpha, epsilon, nDir, nBstSamples)
        Console.WriteLine("bootstrap test, min epsilon: " & res.minEps)

        Console.ReadKey()
        Console.WriteLine("-----------------------------")

        'Decimal digits of pi
        Console.WriteLine("Decimal digits of pi")
        p = {93, 116, 103, 102, 93, 97, 94, 95, 101, 106}
        ReDim q(9)
        For i As Integer = 1 To 10
            q(i - 1) = 1 / 10
        Next
        b = 0.01 / Math.Sqrt(1000)
        nDir = 200 * p.Length
        res = tests_equivalence.asymptoticTest(p, q, b, alpha, epsilon)
        Console.WriteLine("asymptotic test, min epsilon: " & res.minEps)

        res = tests_equivalence.bootstrapTest(p, q, b, alpha, epsilon, nDir, nBstSamples)
        Console.WriteLine("bootstrap test, min epsilon: " & res.minEps)
        Console.ReadKey()


    End Sub
End Module
