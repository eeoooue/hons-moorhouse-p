
from Bio.Align import substitution_matrices

blosum_62 = substitution_matrices.load('BLOSUM62') 

score = blosum_62.get(('A', 'R'))

print(score)

POSSIBLE_CHARACTERS = "CSTAGPDEQNHRKMILVWYF"

# POSSIBLE_CHARACTERS = "CSTAGPDEQNHRKMILVWYF" + "XBZ"

n = len(POSSIBLE_CHARACTERS)

lines = []

for i in range(n):
    x = POSSIBLE_CHARACTERS[i]
    for j in range(i, n):
        y = POSSIBLE_CHARACTERS[j]
        score = blosum_62.get((x, y))
        line = f"[DataRow('{x}', '{y}', {score})]"
        print(line)
        lines.append(line)

with open(f"blosum_tests.txt", "w") as file:
    for line in lines:
        file.write(line)
        file.write("\n")

lines = []
for i in range(n):
    x = POSSIBLE_CHARACTERS[i]
    for y in "Z":
        score = blosum_62.get((x, y))
        line = f"[DataRow('{x}', '{y}', {score})]"
        print(line)
        lines.append(line)

with open(f"blosum_tests_2.txt", "w") as file:
    for line in lines:
        file.write(line)
        file.write("\n")