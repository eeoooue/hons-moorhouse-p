

In examples:

Lots of the projects have about 7 objectives, lowest seen was 4

roughly seemed to be ~4 primary & ~3 secondary

----

## Primary Objectives

#### Perform Multiple Sequence Alignment in a Time-Efficient Manner
The produced software should be able to align a typical testcase of 6 protein sequences within 10 seconds on a university desktop computer. The resulting alignment of sequences must conserve the original sequence content and identifiers given as input.

#### Assess the Viability of a Single-State Approach for Iterative Alignment
TODO: specify this

#### Support Established Bioinformatics File Formats
The alignment tool should be able to read biological sequences from an established file format such as FASTA. Likewise, the tool should support an established file format for outputting sequence alignments, such as FASTA, PHYLIP or NEXUS. The user should be able to specify in the input source and output destination as command-line arguments.

#### Consistently Produce High-Quality Alignments of Sequences
As assessed in a case study using structural benchmarking, the alignment tool should demonstrate the ability to consistently produce alignments of a comparable quality to established software packages such as Clustal Omega and MUSCLE.



-----

## Secondary Objectives

#### ((MULTI OBJECTIVE APPROACH))
TODO: specify this

#### Support Batch Alignment
The alignment tool should support the alignment of a series of input files from a specified directory. The user should be able to specify a source and destination directory using command-line arguments. The software should work through each input in sequence and output the resulting alignments to the destination directory.

#### Indicate Progress in Aligning Sequences
As an iterative aligner that may process a set of sequences for multiple seconds at a time, the software should display a clear indicator of how much progress has been made on the current alignment. For example, the program could present a progress bar using ASCII characters.