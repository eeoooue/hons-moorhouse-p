
import subprocess

class WrappedScorer:
    def __init__(self, executable: str) -> None:
        
        self.executable = executable

    def crop_line_at_start_of(self, line: str, prefix: str):

        i = line.index(prefix)
        return line[i:]
    
    def crop_line_to_end_before(self, line: str, suffix: str):

        j = line.find(suffix)
        return line[:j]

    def extract_q_score(self, line: str):
        
        line = self.crop_line_at_start_of(line, "Q=")
        line = self.crop_line_to_end_before(line, ";")
        clean_line = line.strip()
        arr = clean_line.split("=")
        return float(arr[-1])
    
    def extract_tc_score(self, line: str):

        line = self.crop_line_at_start_of(line, "TC=")
        clean_line = line.strip()
        arr = clean_line.split("=")
        return float(arr[-1])

    def score_testcase(self, test_path, ref_path):

        # print(f"scoring {test_path} against {ref_path}")

        command = f"{self.executable} -test {test_path} -ref {ref_path}"
        process = subprocess.run(command, capture_output=True, text=True)
        output = process.stdout
        if output == "":
            return (0, 0)
        
        q_score = self.extract_q_score(str(process.stdout))
        tc_score = self.extract_tc_score(str(process.stdout))
        # print(f"scores were: Q={q_score} | TC={tc_score}")

        return (q_score, tc_score)