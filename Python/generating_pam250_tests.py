
from Bio.Align import substitution_matrices

matrix = substitution_matrices.load('PAM250') 

POSSIBLE_CHARACTERS = "CSTAGPDEQNHRKMILVWYF"

n = len(POSSIBLE_CHARACTERS)

lines = []

for i in range(n):
    x = POSSIBLE_CHARACTERS[i]
    for j in range(i, n):
        y = POSSIBLE_CHARACTERS[j]
        score = matrix.get((x, y))
        line = f"[DataRow('{x}', '{y}', {score})]"
        print(line)
        lines.append(line)

with open(f"pam_tests.txt", "w") as file:
    for line in lines:
        file.write(line)
        file.write("\n")

lines = []
for i in range(n):
    x = POSSIBLE_CHARACTERS[i]
    for y in "Z":
        score = matrix.get((x, y))
        line = f"[DataRow('{x}', '{y}', {score})]"
        print(line)
        lines.append(line)

with open(f"pam_tests_2.txt", "w") as file:
    for line in lines:
        file.write(line)
        file.write("\n")