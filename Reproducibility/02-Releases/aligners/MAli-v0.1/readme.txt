
################################################################# 

Metaheuristic Aligner - MAli (v0.1)

#################################################################

'MAli' is a tool for performing Multiple Sequence Alignment.
The current version (v0.1) is a proof-of-concept only.
This version of the tool is not intended for professional use.

Functionality:

	- Reads a set of biological sequences from an input file
	  given in the FASTA format.

	- Outputs an alignment of these sequences using the FASTA
	  format also.
	
	- Currently produces a random alignment state for the
	  set of sequences provided. i.e. a low quality alignment.

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
