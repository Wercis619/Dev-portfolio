from shapes.three_d.cuboid import Cuboid

class Cube(Cuboid):
    def __init__(self, side):
        super().__init__(side, side, side)
        self.side = side
        self.name = "Cube"

    def shape_type(self):
        return "Cube"

    def parameters(self):
        return f"side = {self.side}"