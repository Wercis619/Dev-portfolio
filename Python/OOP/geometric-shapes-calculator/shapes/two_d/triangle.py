import math
from shapes.shape_2d import Shape2D

class Triangle(Shape2D):

    def __init__(self, side_a, side_b, side_c):

        if (
            side_a + side_b <= side_c
            or side_a + side_c <= side_b
            or side_b + side_c <= side_a
        ):
            raise ValueError("Impossible triangle")

        super().__init__("Triangle")

        self.validate_positive(
            side_a,
            side_b,
            side_c
        )

        self.side_a = side_a
        self.side_b = side_b
        self.side_c = side_c

    def triangle_type(self):

        if self.side_a == self.side_b and self.side_b == self.side_c:
            return "Equilateral triangle"

        elif (
            self.side_a == self.side_b
            or self.side_a == self.side_c
            or self.side_b == self.side_c
        ):
            return "Isosceles triangle"

        else:
            return "Scalene triangle"

    def angle_type(self):
        sides = sorted(
            [self.side_a, self.side_b, self.side_c]
        )

        a = sides[0]
        b = sides[1]
        c = sides[2]

        if a ** 2 + b ** 2 == c ** 2:
            return "Right triangle"

        elif a ** 2 + b ** 2 > c ** 2:
            return "Acute triangle"

        else:
            return "Obtuse triangle"

    def shape_type(self):
        return (
            f"{self.triangle_type()}\n"
            f"Angle type: {self.angle_type()}"
        )

    def parameters(self):
        return (
            f"side a = {self.side_a}, "
            f"side b = {self.side_b}, "
            f"side c = {self.side_c}"
        )


    def area(self):
        s = (self.side_a + self.side_b + self.side_c)/2
        return math.sqrt(s*(s-self.side_a)*(s-self.side_b)*(s-self.side_c))

    def perimeter(self):
        return self.side_a + self.side_b + self.side_c
