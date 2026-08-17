from shapes.shape_3d import Shape3D

class Cuboid(Shape3D):
    def __init__(self, length, width, height):
        super().__init__("Cuboid")
        self.validate_positive(length, width, height)
        self.length = length
        self.width = width
        self.height = height

    def shape_type(self):
        return "Cuboid"

    def volume(self):
        return self.length * self.width * self.height

    def surface_area(self):
        return 2 * (self.length * self.width + self.length * self.height + self.width * self.height)

    def parameters(self):
        return (
            f"length = {self.length}, "
            f"width = {self.width}, "
            f"height = {self.height}"
        )