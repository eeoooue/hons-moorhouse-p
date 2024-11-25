
import subprocess
import os
import shutil

class WrappedAligner:
    def __init__(self, title: str, executable: str, directory: str) -> None:

        self.seed = 0
        self.title = title
        self.executable = executable
        self.directory = directory
        self.exec_path = f"{directory}/{executable}"
        self.output_folder = f"{directory}/output"
        self.prepare_output_folder()
        self.indicate_iterations = False
        self.iteration_count = 0

    def specify_iterations(self, iters: int):

        self.iteration_count = iters
        self.indicate_iterations = True

    def set_seed(self, x: int):

        self.seed = x

    def prepare_output_folder(self):

        #prepare output folder if there isn't one

        shutil.rmtree(self.output_folder, ignore_errors=True)
        os.mkdir(self.output_folder)

        pass

    def align_testcase(self, testcase, testcase_path):

        line = f"{self.exec_path} -input {testcase_path} -output {self.output_folder}/{testcase}"
        if self.seed > 0:
            line += f" -seed {self.seed}"
        if self.indicate_iterations:
            line += f" -iterations {self.iteration_count}"

        subprocess.run(line, capture_output=True)
    
    def get_alignment_path(self, testcase):

        return f"{self.output_folder}/{testcase}"

        
        