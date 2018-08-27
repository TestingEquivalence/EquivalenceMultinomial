Public Class constants
    Public WMultDistr As New Dictionary(Of String, Double())
    Public FMultDistr As New Dictionary(Of String, Integer())
    Public qdic As New Dictionary(Of String, clsMultinomialDistribution)
    Public ProdDict As New Dictionary(Of String, Double(,))
    Public TwoWayCls As New Dictionary(Of String, Double())

    Sub InitQdic()
        Dim key As String
        Dim q As clsMultinomialDistribution

        key = "w_p_1"
        q = New clsMultinomialDistribution(1 / 3, 12)
        qdic.Add(key, q)

        key = "w_p_2"
        qdic.Add(key, q)

        key = "w_p_3"
        q = New clsMultinomialDistribution(1 / 6, 12)
        qdic.Add(key, q)

        key = "w_p_4"
        q = New clsMultinomialDistribution(1 / 2, 12)
        qdic.Add(key, q)

        key = "pi_1"
        q = New clsMultinomialDistribution(10)
        qdic.Add(key, q)

        key = "pi_2"
        qdic.Add(key, q)

        key = "pi_3"
        qdic.Add(key, q)

        key = "pi_4"
        qdic.Add(key, q)

        key = "pi_5"
        qdic.Add(key, q)

        key = "mendel"
        q = New clsMultinomialDistribution({9, 3, 3, 1})
        qdic.Add(key, q)

        key = "benford"
        Dim q_pr(8) As Double
        For i As Integer = 1 To 9
            q_pr(i - 1) = Math.Log10(1 + 1 / i)
        Next
        q = New clsMultinomialDistribution(q_pr)
        qdic.Add(key, q)
    End Sub

    Sub New()
        'old values of the first version
        'WMultDistr.Add("w_4_1", {0.28879, 0.13, 0.25, 0.33121})
        'WMultDistr.Add("w_4_2", {0.29655, 0.15, 0.21, 0.34345})
        'WMultDistr.Add("w_4_3", {0.21029, 0.2, 0.21, 0.37971})
        'WMultDistr.Add("w_4_4", {0.25388, 0.22, 0.16, 0.36612})
        'WMultDistr.Add("w_4_5", {0.14393, 0.25, 0.25, 0.35607})
        'WMultDistr.Add("w_4_6", {0.175, 0.325, 0.175, 0.325})
        'WMultDistr.Add("w_6_1", {0.21057, 0.05, 0.15, 0.15, 0.2, 0.23943})
        'WMultDistr.Add("w_6_2", {0.12296, 0.05, 0.2, 0.2, 0.2, 0.22704})
        'WMultDistr.Add("w_6_3", {0.17296, 0.1, 0.1, 0.15, 0.2, 0.27704})
        'WMultDistr.Add("w_6_4", {0.10283, 0.15, 0.1, 0.15, 0.25, 0.24717})
        'WMultDistr.Add("w_6_5", {0.10211, 0.15, 0.15, 0.15, 0.15, 0.29789})
        'WMultDistr.Add("w_6_6", {0.10543, 0.2279, 0.10543, 0.2279, 0.10543, 0.2279})

        WMultDistr.Add("w_4_1", {0.282325, 0.150514, 0.25, 0.317161})
        WMultDistr.Add("w_4_2", {0.28325, 0.178595, 0.221429, 0.316726})
        WMultDistr.Add("w_4_3", {0.219386, 0.211478, 0.219162, 0.349975})
        WMultDistr.Add("w_4_4", {0.253233, 0.225, 0.175091, 0.346675})
        WMultDistr.Add("w_4_5", {0.151005, 0.25, 0.25, 0.348995})
        WMultDistr.Add("w_4_6", {0.20001, 0.20001, 0.3, 0.29999})
        WMultDistr.Add("w_6_1", {0.195936, 0.088889, 0.155556, 0.155635, 0.18, 0.223985})
        WMultDistr.Add("w_6_2", {0.139469, 0.09392, 0.187452, 0.187452, 0.187452, 0.204257})
        WMultDistr.Add("w_6_3", {0.170862, 0.122329, 0.122222, 0.155556, 0.188889, 0.240142})
        WMultDistr.Add("w_6_4", {0.127775, 0.156494, 0.125976, 0.156494, 0.21753, 0.215731})
        WMultDistr.Add("w_6_5", {0.117557, 0.153966, 0.153966, 0.153966, 0.153966, 0.26658})
        WMultDistr.Add("w_6_6", {0.133348, 0.199985, 0.133348, 0.199985, 0.133348, 0.199985})
        WMultDistr.Add("w_6_7", {0.166667, 0.122329, 0.122222, 0.155998, 0.188889, 0.243895})

        FMultDistr.Add("w_p_1", {185, 1149, 3265, 5475, 6114, 5194, 3067, 1331, 403, 105, 14, 4, 0})
        FMultDistr.Add("w_p_2", {45, 327, 886, 1475, 1571, 1404, 787, 367, 112, 29, 2, 1, 0})
        FMultDistr.Add("w_p_3", {447, 1145, 1181, 796, 380, 115, 24, 8, 0, 0, 0, 0, 0})
        FMultDistr.Add("w_p_4", {0, 7, 60, 198, 430, 731, 948, 847, 536, 257, 71, 11, 0})

        FMultDistr.Add("pi_1", {8, 8, 12, 11, 10, 8, 9, 8, 12, 14})
        FMultDistr.Add("pi_2", {93, 116, 103, 102, 93, 97, 94, 95, 101, 106})
        FMultDistr.Add("pi_3", {968, 1026, 1021, 974, 1012, 1046, 1021, 970, 948, 1014})
        FMultDistr.Add("pi_4", {9999, 10137, 9908, 10025, 9971, 10026, 10029, 10025, 9978, 9902})
        FMultDistr.Add("pi_5", {99959, 99758, 100026, 100229, 100230, 100359, 99548, 99800, 99985, 100106})

        FMultDistr.Add("mendel", {315, 101, 108, 32})
        FMultDistr.Add("emp_compet", {1, 6, 19, 4,
                                      0, 4, 11, 8})
        FMultDistr.Add("madoff", {83, 29, 20, 13, 11, 17, 14, 15, 12})
        FMultDistr.Add("apple", {2001, 1359, 872, 625, 468, 417, 306, 251, 208})
        FMultDistr.Add("census", {5738, 3540, 2342, 1847, 1559, 1370, 1166, 1043, 904})
        InitQdic()
    End Sub

End Class
