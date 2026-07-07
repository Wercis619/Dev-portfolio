class Shape:
    def __init__(self, name):
        self.name = name

    def info(self):
        return f"Shape: {self.name}"

    def __str__(self):
        return self.info()