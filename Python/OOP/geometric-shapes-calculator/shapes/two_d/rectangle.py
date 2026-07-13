from shapes.shape_2d import Shape2D

class Rectangle(Shape2D):
    def __init__(self, width, height):
        super().__init__("Rectangle")
        self.validate_positive(width, height)
        self.width = width
        self.height = height

    def shape_type(self):
        return "Rectangle"

    def parameters(self):
        return (
            f"width = {self.width}, "
            f"height = {self.height}"
        )

    def area(self):
        return self.width * self.height

    def perimeter(self):
        return 2 * (self.width+self.height)

