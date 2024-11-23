
import os

class TestSubset:
    def __init__(self, directory: str) -> None:
        
        self.directory = directory

    def list_files_in_subfolder(self, subfolder: str):

        return os.listdir(f"{self.directory}/{subfolder}")
    
    def get_testcases(self):

        result = []
        for filename in self.list_files_in_subfolder("in"):
            result.append(filename)
        return result
    
    def get_input_path(self, testcase):

        return f"{self.directory}/in/{testcase}"
    
    def get_reference_path(self, testcase):

        return f"{self.directory}/ref/{testcase}"

