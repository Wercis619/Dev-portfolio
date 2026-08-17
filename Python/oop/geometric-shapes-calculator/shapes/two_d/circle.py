import math
from shapes.shape_2d import Shape2D

class Circle(Shape2D):
    def __init__(self, radius):
        super().__init__("Circle")
        self.validate_positive(radius)
        if radius > 600:
            raise ValueError(
                "Radius cannot be greater than 600"
            )
        self.radius = radius

    def shape_type(self):
        return "Circle"

    def parameters(self):
        return f"radius = {self.radius}"

    def area(self):
        return math.pi * self.radius ** 2

    def perimeter(self):
        return 2 * math.pi * self.radius

