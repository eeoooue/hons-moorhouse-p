
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt

import os

class ResultsFinder:
    def __init__(self, directory, subfolder):

        self.dir = directory
        self.subfolder = subfolder

    def construct_fullpath(self):

        return f"{self.dir}/{self.subfolder}"

    def get_filepaths(self):

        result = []
        PATH = self.construct_fullpath()

        for filepath in os.listdir(PATH):
            result.append(f"{PATH}/{filepath}")

        return result

    

class ResultsReader:
    def __init__(self, directory):


        self.dir = directory

    def get_dataset_for_version(self, version):

        finder = ResultsFinder(self.dir, version)

        scorings = []
        timings = []

        PATHS = finder.get_filepaths()
        for filepath in PATHS:
            (quality, ms_elapsed) = self.get_score_timing_pair(filepath)
            scorings.append(quality)
            timings.append(ms_elapsed)

        # print(f"avg. q_scores = {scorings}")
        # print(f"avg. ms_elapsed = {timings}")

        dataset = {
            "mean_q_score": scorings,
            "mean_secs_elapsed": timings,
        }

        df = pd.DataFrame(dataset)
        df = df.sort_values(by="mean_secs_elapsed", ascending=True)
        df = df.reset_index(drop=True)

        return df


    def get_score_timing_pair(self, filepath):

        df = pd.read_csv(filepath)

        quality = df["Q_score"].mean()
        quality = round(quality, 5)

        ms_elapsed = df["time_elapsed_ms"].mean()
        ms_elapsed = round(ms_elapsed / 1000, 5)

        # print(f"avg. q_score = {quality} | avg. ms_elapsed = {ms_elapsed}")

        return (quality, ms_elapsed)




class ExperimentReader:
    def __init__(self, directory, versions):
        
        self.dir = directory
        self.versions = versions
        self.reader = ResultsReader(self.dir)

    def get_dataset_for_version(self, version):

        return self.reader.get_dataset_for_version(version)

    def get_version_datasets(self):

        result = []
        for version in self.versions:
            df = self.reader.get_dataset_for_version(version)
            result.append(df)

        return result
    
    def get_combined_dataset(self):

        dfs = self.get_version_datasets()
        versions = self.versions

        n = len(versions)

        dataset = {
            "version": [],
            "mean_q_score": [],
            "mean_secs_elapsed": [],
        }

        for i in range(n):
            df = dfs[i]
            version = versions[i]
            vals_quality = list(df["mean_q_score"].values)
            vals_timing = list(df["mean_secs_elapsed"].values)
            vals_version = [version for _ in range(len(vals_quality))]
            
            dataset["mean_q_score"] += vals_quality
            dataset["mean_secs_elapsed"] += vals_timing
            dataset["version"] += vals_version
        
        df = pd.DataFrame(dataset)
        
        return df
    

if __name__ == "__main__":

    reader = ResultsFinder("data", "v1.1")
    files = reader.get_filepaths()
    print(files)

    FILEPATH = "data/v1.1/MAli v1.1_seed_260125_iters_100.csv"
    reader = ResultsReader("data")
    df = reader.get_dataset_for_version("v1.0")
    print(df.head())

    versions = ["v0.2", "v1.0", "v1.1", "v1.2"]
    reader = ExperimentReader("data", versions)
    reader.get_version_datasets()
    df = reader.get_combined_dataset()
    print(df.head())
