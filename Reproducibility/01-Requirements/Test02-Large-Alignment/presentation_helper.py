
from Bio import AlignIO
from Bio import SeqIO
import matplotlib.pyplot as plt

class PresentationHelper:
    def __init__(self):

        pass

    def present_unaligned_fasta(self, filepath):

        print(f"Displaying Sequences from {filepath}: " + "\n")
        
        for record in SeqIO.parse(filepath, "fasta"):
            print(f">{record.id}") # printing identifier
            print(record.seq + "\n") # printing sequence content

    def present_aligned_clustalw(self, filepath):
        
        alignment = AlignIO.read(filepath, "clustal")
        print(f"Displaying ClustalW format alignment from {filepath}: " + "\n")
        
        block_width = 100
        num_seqs = len(alignment)
        alignment_length = alignment.get_alignment_length()

        for block_start in range(0, alignment_length, block_width):
            self.present_interleaved_block(alignment, block_start, block_width)

    def present_interleaved_aligned_fasta(self, filepath):

        alignment = AlignIO.read(filepath, "fasta")
        print(f"Displaying interleaved alignment from '{filepath}: " + "\n")
        
        block_width = 60
        num_seqs = len(alignment)
        alignment_length = alignment.get_alignment_length()
        
        for block_start in range(0, alignment_length, block_width):
            self.present_interleaved_block(alignment, block_start, block_width)

    def present_interleaved_block(self, alignment, block_start, block_width):

        block_end = block_start + block_width
        for record in alignment:
            name = record.id[:15].ljust(16)
            segment = record.seq[block_start:block_end]
            print(f"{name}{segment}")
            
        print()

    def present_score(self, testcase_name: str, score: float):

        LABELS = ["MAli v1.31", "Structural Reference"]
        VALUES = [score, 1.0]
        COLOURS = ["turquoise", "goldenrod"]

        plt.figure(figsize=(5, 4))
        plt.bar(LABELS, VALUES, color=COLOURS)
        plt.title(f"Scoring alignment of {testcase_name}")

        PADDING = 0.05

        plt.ylabel("Score")
        plt.ylim(0, 1 + PADDING)

        plt.show()
