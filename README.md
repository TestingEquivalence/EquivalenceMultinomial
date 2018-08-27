# EquivalenceMultinomial

This package provides two tests to show equivalence of observed counting frequencies to a fully specified multinomial distribution.  Thus, it is possible to show equivalence of the observed empirical data to the theoretical distribution. 
The package is based on the published article:

Vladimir Ostrovski, “Testing equivalence of multinomial distributions”, Statistics and Probability Letters 124 (2017), 77–82.

The package is written in VB.NET. Tree examples are available in the module "StartMod.vb“, sub main, which can be run immediately.
The program is rewritten and optimized for better performance and understanding. Particularly the optimization in the bootstrap
test is reworked considerable for a stable performance. Hence the results for real data sets deviate slightly from these, 
published in the article. However, the deviations are very small and do not matter for any application.

Let d denote the total variation distance and let q be the fully specified multinomial distribution.
Let p be the true underlying distribution of the observed data. 
The distribution p is equivalent to q if d(p,q)<e, where e>0 is a tolerance parameter.
The equivalence test problem is formally stated by H0={d(p,q) >=e } against H1={d(p,q)<e}.
The true distribution p is unknown. Instead we observe the counting frequencies p_n, where n is the number of observations. 

The goal is to reject the hypothesis of the non-equivalency H0 at a significance level alpha based on the counting frequencies p_n.

The package provides two tests for that purpose: the asymptotic test and the bootstrap test. 
Both tests are available as functions in the module “tests_equivalence.”, which return the class “TestResult” as result.

The class “TestResult” contains two public fields only:
• Field “result” is Boolean. The value is true if the test rejects H0 and false otherwise. 
• Field “minEps” is double. This is the smallest tolerance parameter, for which the test can reject H0.

The asymptotic test is based on the asymptotic distribution of the test statistic.
Therefore, the asymptotic test need some sufficiently large number of the observations. 
It should be used carefully because the test is approximate and may be anti-conservative at some points. 
To obtain a conservative test reducing of alpha (usually halving) or 
slight shrinkage of the tolerance parameter e may be appropriate. 

The asymptotic test is realized as the function “asymptoticTest”, which has the following parameters:

•	p is an array of integer. It should contain counts of events.  The number of observations will be also derived from this vector.
•	q is an array of double and should contain the probability vector of the theoretical distribution,
to which the equivalence should be shown.
•	b is a smoothing parameter for the total variation distance, see article for more information. 
We recommend setting b=0.01/sqrt(n), where n is the number of observations. 
•	alpha is the significance level. 
•	epsilon is the tolerance parameter e.

The bootstrap test is based on the re-sampling method called bootstrap. 
The bootstrap test is more precise and reliable than the asymptotic test. 
However, it should be used carefully because the test is approximate and may be anti-conservative at some points. 
To obtain a conservative test reducing of alpha (usually halving) 
or slight shrinkage of the tolerance parameter e may be appropriate. 
We prefer the slight shrinkage of the tolerance parameter because it is more effective, and the significance level remains unchanged.

The bootstrap test is realized as the function “bootstrapTest”, which has the following parameters:

•	p is an array of integer. It should contain counts of events.  The number of observations will be also derived from this vector.
•	q is an array of double and should contain the probability vector of the theoretical distribution, to which the equivalence should be shown.
•	b is a smoothing parameter for the total variation distance, see article for more information. We recommend setting b=0.01/sqrt(n), where n is the number of observations. 
•	alpha is the significance level. 
•	epsilon is the tolerance parameter e.
•	nDirections is the number of random directions to search for a boundary point of H0. The number of random directions has a negative impact on the computation time. The number should be set empirically. You can increase it gradually (100, 200, ...) until the minimum tolerance parameter “minEps” does not change anymore. We would recommend using 200*dim(p) directions.
•	nBootstrapSamples is the number of bootstrap samples. The parameter should be at least 1000. However, higher values lead to the better approximation generally. Usually it is not necessary to generate more than 10.000 bootstrap samples.

The bootstrap test needs considerable computation time. For example, it may need few minutes on the usual office computer.
