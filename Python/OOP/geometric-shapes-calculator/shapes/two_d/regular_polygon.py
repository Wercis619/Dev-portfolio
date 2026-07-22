import math
from shapes.shape_2d import Shape2D

class RegularPolygon(Shape2D):
    def __init__(self, side, number_of_sides):
        super().__init__("Regular Polygon")
        self.validate_positive(side,number_of_sides)

        if not isinstance(number_of_sides, int):
            raise ValueError("Number of sides must be an integer")

        if number_of_sides < 3:
            raise ValueError("Polygon must have at least 3 sides")
        if number_of_sides == 3:
            raise ValueError("Use Triangle class")
        if number_of_sides == 4:
            raise ValueError("Use Square class")
        if number_of_sides > 10:
            raise ValueError("Maximum supported polygon is Decagon")

        self.side = side
        self.number_of_sides = number_of_sides

    def area(self):
        return (self.number_of_sides * self.side ** 2) / (4 * math.tan(math.pi / self.number_of_sides))

    def perimeter(self):
        return self.number_of_sides * self.side

    def shape_type(self):
        match self.number_of_sides:
            case 5:
                return "Pentagon"
            case 6:
                return "Hexagon"
            case 7:
                return "Heptagon"
            case 8:
                return "Octagon"
            case 9:
                return "Nonagon"
            case 10:
                return "Decagon"
            case _:
                return "Unknown polygon"

    def parameters(self):
        return (
            f"side = {self.side}, "
            f"number of sides = {self.number_of_sides}"
        )