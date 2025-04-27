
import pandas as pd
from scorefile_reader import ScorefileReader
from os import listdir

class ScorefileAggregator:

    def __init__(self):

        self.reader = ScorefileReader()

    def aggregate_scores_from_directory(self, directory):

        tables = self.get_tables_from_directory(directory)
        dataset = self.build_dataset_from_tables(tables)
        return self.create_dataframe_from_dataset(dataset)

    def create_dataframe_from_dataset(self, dataset):

        df = pd.DataFrame(dataset)
        df = df.apply(pd.to_numeric, errors='coerce')

        return df
        
    def build_dataset_from_tables(self, tables):

        dataset = {x: [] for x in tables[0]}

        for table in tables:
            for x in dataset:
                value = table[x]
                dataset[x].append(value)

        return dataset

    def get_tables_from_directory(self, directory):

        result = []
        for file in listdir(directory):
            path = f"{directory}/{file}"
            table = self.reader.get_table_of_scores(path)
            result.append(table)

        return result
                        