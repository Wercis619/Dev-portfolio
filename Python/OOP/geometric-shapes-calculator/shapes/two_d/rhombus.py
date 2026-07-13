from shapes.shape_2d import Shape2D

class Rhombus(Shape2D):
    def __init__(self, side, diagonal_a, diagonal_b):
        super().__init__("Rhombus")
        self.validate_positive(side,diagonal_a,diagonal_b)

        if diagonal_a ** 2 + diagonal_b ** 2 != 4 * side ** 2:
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
