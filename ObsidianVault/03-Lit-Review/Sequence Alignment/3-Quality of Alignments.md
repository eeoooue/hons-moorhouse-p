

Many bioinformatics analysis processes are sensitive to the alignment of sequences on which they depend. e.g. phylogenetic trees depend on the specific alignment given; if aligned differently the same sequences produce a different tree

There is inherently some variance in bioinformatics analysis as a result of MSA being an optimization problem; we must treat alignment as a random variable

However, we can distinguish between alignments of high quality & those of low quality; though perhaps not in all cases.

We use principle of parsimony - go with the truth that takes the least explaining / the alignment which shows the strongest set of relationships for the minimum amount of manipulation of the sequences. Based on our understanding of evolutionary biology, this is a good heuristic for aligning sequences.

We can use structural benchmarking to assess and compare multiple sequence alignment software. A structural benchmark is a 'gold standard' alignment that can be used as a reference; these alignments are the work of subject experts who align the protein sequences using the known 3d structure of the proteins involved to define 'core blocks' (columns) within these alignments against which the output of other alignment software can be compared.

One of the most popular structural benchmarking datasets is BAliBASE (Thompson etc.)

