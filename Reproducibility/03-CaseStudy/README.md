
## Case Study

This directory contains notebooks for conducting structural benchmarking of a selection of aligners using BALIS-1.

This is intended to place the performances of MAli for iterative alignment and iterative refinement in context of existing alignment software.

The selected external aligners included for comparison in this study are:

1. [ClustalW v2.1](http://www.clustal.org/clustal2/)

> Larkin, M.A., Blackshields, G., Brown, N.P., Chenna, R., McGettigan, P.A., McWilliam, H., Valentin, F., Wallace, I.M., Wilm, A., Lopez, R., Thompson, J.D., Gibson, T.J. & Higgins, D.G. (2007) Clustal W and Clustal X version 2.0. Bioinformatics, 23(21), p. 2947–2948. Available online: https://doi.org/10.1093/bioinformatics/btm404.

2. [ClustalOmega v1.2.2](http://www.clustal.org/omega/)

> Sievers, F. & Higgins, D.G. (2018) Clustal Omega for making accurate alignments of many protein sequences. Protein Science, 27(1), p. 135–145. Available online: https://doi.org/10.1002/pro.3290.

3. [MUSCLE v5.3](https://www.drive5.com/muscle/)

> Edgar, R.C. (2004) MUSCLE: multiple sequence alignment with high accuracy and high throughput. Nucleic Acids Research, 32(5), p. 1792–1797. Available online: https://doi.org/10.1093/nar/gkh340.


Tests for a specific aligner can be reperformed by running the associated notebook to completion. The two notebooks used for data visualisation would then also need to be run to completion for the changes to be displayed.

#### Prerequisites

- Windows 10 or later
- Python 3.10+
- Jupyter Notebook or JupyterLab
- .NET 8.0

Note: As this study makes use of external alignment software, results may vary between runs and across different devices. The original set of results obtained can be viewed in the ```02a - DataVis-Performance``` and ```02b - DataVis-Quality.pdf``` .pdf files.


