from shapes.two_d.rectangle import Rectangle
class Square(Rectangle):
    def __init__(self, side):
        super().__init__(side, side)
        self.validate_positive(side)
        if side > 600:
            raise ValueError(
                "Side cannot be greater than 600"
            )
        self.name = "Square"
        self.side = side

    def shape_type(self):
        return "Square"

    def parameters(self):
        return f"side = {self.side}"
