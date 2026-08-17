import math
from shapes.shape_2d import Shape2D


class Rhombus(Shape2D):
    MAX_SIZE = 600
    def __init__(self, side, diagonal_a, diagonal_b):
        super().__init__("Rhombus")
        self.validate_positive(side,diagonal_a,diagonal_b)

        if (
                side > self.MAX_SIZE
                or diagonal_a > self.MAX_SIZE
                or diagonal_b > self.MAX_SIZE
        ):
            raise ValueError(
                "Rhombus dimensions cannot be greater than 600"
            )

        if not math.isclose(
                diagonal_a ** 2 + diagonal_b ** 2,
                4 * side ** 2
        ):
            raise ValueError("Impossible rhombus")

        self.side = side
        self.diagonal_a = diagonal_a
        self.diagonal_b = diagonal_b

    def shape_type(self):
        return "Rhombus"

    def parameters(self):
        return (
            f"side = {self.side}, "
            f"diagonal a = {self.diagonal_a}, "
            f"diagonal b = {self.diagonal_b}"
        )

    def area(self):
        return (self.diagonal_a * self.diagonal_b) / 2

    def perimeter(self):
        return 4 * self.side
