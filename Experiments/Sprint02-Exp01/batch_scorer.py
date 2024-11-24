
from wrapped_subset import WrappedSubset
from wrapped_aligner import WrappedAligner
from wrapped_scorer import WrappedScorer
import os
import time
import math

# tests an aligner on a test subset using a scoring tool

class BatchScorer:
    def __init__(self, aligner: WrappedAligner, subset: WrappedSubset, scorer: WrappedScorer) -> None:
        
        self.aligner = aligner
        self.subset = subset
        self.scorer = scorer

    def record_scores(self, filename: str):

        self.records = []

        tests = self.subset.get_testcases()
        n = len(tests)

        for i in range(n):
            self.assess_testcase(tests[i])
            print(f"performed {i+1} of {n} tests | {self.records[-1]}")

        with open(filename, "w") as file:
            file.write("testcase,alignment_performed,q_score,tc_score,ms_elapsed\n")
            for line in self.records:
                file.write(line + "\n")

    def record_success(self, testcase, q_score, tc_score, ms_elapsed):

        self.record_line(testcase, True, q_score, tc_score, ms_elapsed)
    
    def record_failure(self, testcase):

        self.record_line(testcase, False, 0, 0, 0)
    
    def record_line(self, testcase, success, q_score, tc_score, ms_elapsed):

        verdict = "pass" if success else "fail"
        line = f"{testcase},{verdict},{q_score},{tc_score},{math.ceil(ms_elapsed)}"
        self.records.append(line)

    def start_timer(self):

        self.start_time = time.time()

    def stop_timer(self):

        self.end_time = time.time()
        elapsed_time = self.end_time - self.start_time
        return elapsed_time * 1000

    def assess_testcase(self, testcase):

        testcase_path = self.subset.get_input_path(testcase)
        self.start_timer()
        self.aligner.align_testcase(testcase, testcase_path)
        ms_elapsed = self.stop_timer()

        reference_path = self.subset.get_reference_path(testcase)
        alignment_path = self.aligner.get_alignment_path(testcase)

        if os.path.exists(alignment_path):
            (q_score, tc_score) = self.scorer.score_testcase(alignment_path, reference_path)
            self.record_success(testcase, q_score, tc_score, ms_elapsed)
        else:
            self.record_failure(testcase)

