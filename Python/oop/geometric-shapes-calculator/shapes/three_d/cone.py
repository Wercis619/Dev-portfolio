import math
from shapes.shape_3d import Shape3D

class Cone(Shape3D):
    def __init__(self, radius, height):
        super().__init__("Cone")
        self.validate_positive(radius, height)
        self.radius = radius
        self.height = height

    def shape_type(self):
        return "Cone"

    def volume(self):
        return (1/3) * math.pi * self.radius ** 2 * self.height

    def surface_area(self):
        l = math.sqrt(self.radius ** 2 + self.height ** 2)
        return math.pi * self.radius * (self.radius + l)

    def parameters(self):
        return (
            f"radius = {self.radius}, "
            f"height = {self.height}"
        )