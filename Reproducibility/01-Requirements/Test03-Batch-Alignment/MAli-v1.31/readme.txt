
################################################################# 

Metaheuristic Aligner - MAli (v1.31)

#################################################################

'MAli' is an iterative alignment tool for performing Multiple Sequence Alignment.
More specifically, MAli performs global alignment of protein sequences.
The current version (v1.31) is not intended for professional use.

Functionality:

	- Reads a set of biological sequences from an input file
	  given in the FASTA or ClustalW format.

	- Outputs an alignment of these sequences using the FASTA
	  format or ClustalW also (specify using -format).
	
	- Number of iterations can be specified using '-iterations <count>'

	- Can specify a seed value using '-seed <value>'

	- Can produce up to 30 tradeoff alignments using '-pareto <tradeoff_count>'

Example usage:

	> MAli.exe -input BB11001 -output my_alignment.faa

	The above command sees sequences read from BB11001, and
	output as a valid alignment 'my_alignment.faa' using the
	FASTA file format.

	> MAli.exe -input BB11001 -output my_alignment.faa -seed 101

	In the above example, a seed value of 101 is used to ensure
	that the same seed is used for the source of randomness when
	alignment is performed - this means results can be reproduced
	given that the version of MAli used is the same.

Dependencies:

	In order to use MAli, the .NET 8.0 runtime must be installed.
