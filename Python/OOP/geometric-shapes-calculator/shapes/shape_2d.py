from abc import ABC, abstractmethod
from shapes.shape import Shape

class Shape2D(Shape, ABC):
    def __init__(self, name):
        super().__init__(name)

    def info(self):
        return (
                super().info()
                + f"\nArea: {self.area()}"
                + f"\nPerimeter: {self.perimeter()}"
        )

    @abstractmethod
    def area(self):
        pass

    @abstractmethod
    def perimeter(self):
        pass