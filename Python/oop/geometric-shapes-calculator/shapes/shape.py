from abc import ABC, abstractmethod
class Shape(ABC):
    def __init__(self, name):
        if not name:
            raise ValueError("Shape must have a name")
        self.name = name

    def validate_positive(self, *values):
        for value in values:
            if value <= 0:
                raise ValueError("Values must be positive")

    def info(self):
        return (
            f"Name: {self.name}\n"
            f"Type: {self.shape_type()}\n"
            f"Parameters: {self.parameters()}"
        )

    @abstractmethod
    def shape_type(self):
        pass

    @abstractmethod
    def parameters(self):
        pass

    def __str__(self):
        return self.info()