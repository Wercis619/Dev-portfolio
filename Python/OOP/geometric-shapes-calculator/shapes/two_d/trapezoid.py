import math
from shapes.shape_2d import Shape2D


class Trapezoid(Shape2D):

    def __init__(self, base_a, base_b, height, side_a, side_b):
        super().__init__("Trapezoid")

        self.validate_positive(
            base_a,
            base_b,
            height,
            side_a,
            side_b
        )

        if height > side_a or height > side_b:
            raise ValueError("Impossible trapezoid")

        difference = abs(base_a - base_b)

        x1 = math.sqrt(side_a ** 2 - height ** 2)
        x2 = math.sqrt(side_b ** 2 - height ** 2)

        if difference > x1 + x2:
            raise ValueError("Impossible trapezoid")

        self.base_a = base_a
        self.base_b = base_b
        self.height = height
        self.side_a = side_a
        self.side_b = side_b

    def shape_type(self):
        if math.isclose(self.side_a, self.side_b):
            return "Isosceles trapezoid"

        return "Scalene trapezoid"

    def parameters(self):
        return (
            f"base a = {self.base_a}, "
            f"base b = {self.base_b}, "
            f"height = {self.height}, "
            f"side a = {self.side_a}, "
            f"side b = {self.side_b}"
        )

    def area(self):
        return ((self.base_a + self.base_b) * self.height) / 2

    def perimeter(self):
        return self.base_a + self.base_b + self.side_a + self.side_b
