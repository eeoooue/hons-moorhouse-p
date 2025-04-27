

class ScorefileReader:
    def __init__(self):

        pass
        
    def get_table_of_scores(self, filename):

        lines = self.read_lines_from_csv(filename)
        table = self.get_key_value_table(lines)
        
        return table

    def read_lines_from_csv(self, filename):
    
        lines = []
        with open(filename, "r") as file:
            for line in file:
                line = line.strip()
                if len(line):
                    lines.append(line)
    
        return lines

    def get_key_value_table(self, lines):
    
        lines = lines[1:]
        table = {}
    
        for line in lines:
            (key, value) = line.split(',')
            table[key] = value
    
        return table