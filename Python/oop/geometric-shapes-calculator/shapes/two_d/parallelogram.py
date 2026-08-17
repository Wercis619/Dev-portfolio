from shapes.shape_2d import Shape2D


class Parallelogram(Shape2D):

    MAX_SIZE = 600

    def __init__(self, base, side, height):
        super().__init__("Parallelogram")

        self.validate_positive(
            base,
            side,
            height
        )

        if (
            base > self.MAX_SIZE
            or side > self.MAX_SIZE
            or height > self.MAX_SIZE
        ):
            raise ValueError(
                "Parallelogram dimensions cannot be greater than 600"
            )

        if height > side:
            raise ValueError(
                "Impossible parallelogram"
            )

        self.base = base
        self.side = side
        self.height = height

    def shape_type(self):
        return "Parallelogram"

    def parameters(self):
        return (
            f"base = {self.base}, "
            f"side = {self.side}, "
            f"height = {self.height}"
        )

    def area(self):
        return self.base * self.height

    def perimeter(self):
        return 2 * (self.base + self.side)