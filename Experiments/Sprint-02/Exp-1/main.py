
import os
from batch_scorer import BatchScorer
from test_subset import TestSubset

subset = TestSubset("PREFAB-I")

testcases = subset.get_testcases()

for test in testcases:
    input_path = subset.get_input_path(test)
    print(f"input @ {input_path}")
    ref_path = subset.get_reference_path(test)
    print(f"ref @ {ref_path}")
