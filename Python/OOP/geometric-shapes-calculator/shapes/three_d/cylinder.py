import math
from shapes.shape_3d import Shape3D

class Cylinder(Shape3D):
    def __init__(self, radius, height):
        super().__init__("Cylinder")
        self.validate_positive(radius, height)
        self.radius = radius
        self.height = height

    def shape_type(self):
        return "Cylinder"

    def volume(self):
        return math.pi * self.radius ** 2 * self.height

    def surface_area(self):
        return 2 * math.pi * self.radius * (self.radius + self.height)

    def parameters(self):
        return (
            f"radius = {self.radius}, "
            f"height = {self.height}"
        )