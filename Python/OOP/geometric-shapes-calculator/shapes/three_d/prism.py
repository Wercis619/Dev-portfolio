from shapes.shape_3d import Shape3D
from shapes.shape_2d import Shape2D

class Prism(Shape3D):

    def __init__(self, base, height):
        super().__init__("Prism")

        if not isinstance(base, Shape2D):
            raise ValueError(
                "Base must be 2D shape"
            )

        self.validate_positive(height)
        self.base = base
        self.height = height

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
        }

        return names.get(self.base.name, self.base.name)

    def shape_type(self):
        return f"{self.base_type_name()} prism"

    def volume(self):
        return self.base.area() * self.height

    def surface_area(self):
        return 2 * self.base.area() + self.base.perimeter() * self.height

    def parameters(self):
        return (
            f"base = {self.base.shape_type()}, "
            f"height = {self.height}"
        )
