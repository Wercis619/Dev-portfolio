from shapes.shape_3d import Shape3D
from shapes.shape_2d import Shape2D

class Pyramid(Shape3D):

    def __init__(self, base, height, slant_height):
        super().__init__("Pyramid")

        if not isinstance(base, Shape2D):
            raise ValueError(
                "Base must be 2D shape"
            )

        self.validate_positive(height, slant_height)
        self.base = base
        self.height = height
        self.slant_height = slant_height

    def base_type_name(self):
        names = {
            "Triangle": "Triangular",
            "Square": "Square",
            "Rectangle": "Rectangular",
            "Pentagon": "Pentagonal",
            "Hexagon": "Hexagonal",
            "Heptagon": "Heptagonal",
            "Octagon": "Octagonal",
            "Nonagon": "Nonagonal",
            "Decagon": "Decagonal",
            "Trapezoid": "Trapezoidal",
            "Rhombus": "Rhombic",
            "Parallelogram": "Parallelogram-based",
        }

        return names.get(self.base.name, self.base.name)

    def shape_type(self):
        return f"{self.base_type_name()} pyramid"

    def volume(self):
        return (1/3) * self.base.area() * self.height

    def surface_area(self):
        return self.base.area() + (1/2) * self.base.perimeter() * self.slant_height

    def parameters(self):
        return (
            f"base = {self.base.shape_type()}, "
            f"height = {self.height}, "
            f"slant_height = {self.slant_height}"
        )
