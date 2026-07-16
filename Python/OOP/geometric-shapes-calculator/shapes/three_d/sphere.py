import math
from shapes.shape_3d import Shape3D

class Sphere(Shape3D):
    def __init__(self, radius):
        super().__init__("Sphere")
        self.validate_positive(radius)
        self.radius = radius

    def shape_type(self):
        return "Sphere"

    def volume(self):
        return (4/3) * math.pi * self.radius ** 3

    def surface_area(self):
        return 4 * math.pi * self.radius ** 2

    def parameters(self):
        return f"radius = {self.radius}"