
import os
from batch_scorer import BatchScorer
from wrapped_subset import WrappedSubset
from wrapped_aligner import WrappedAligner

subset = WrappedSubset("PREFAB-I")

testcases = subset.get_testcases()

for test in testcases:
    input_path = subset.get_input_path(test)
    print(f"input @ {input_path}")
    ref_path = subset.get_reference_path(test)
    print(f"ref @ {ref_path}")


aligner = WrappedAligner("MAli Candidate", "MAli", "MAli-candidate")

for testcase in testcases:
    testcase_path = subset.get_input_path(testcase)
    aligner.align_testcase(testcase, testcase_path)