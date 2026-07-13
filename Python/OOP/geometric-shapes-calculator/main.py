from shapes.two_d.circle import Circle
from shapes.two_d.rectangle import Rectangle
from shapes.two_d.rhombus import Rhombus
from shapes.two_d.square import Square
from shapes.two_d.trapezoid import Trapezoid
from shapes.two_d.triangle import Triangle
from shapes.two_d.regular_polygon import RegularPolygon

circle = Circle(5)
rectangle = Rectangle(5, 3)
square = Square(4)
triangle = Triangle(3, 3,3)
trapezoid = Trapezoid(5, 5,5,5,5)
rhombus= Rhombus(5,8,6 )
regular_polygon = RegularPolygon(5,8)


shapes = [circle, rectangle, square, triangle, trapezoid, rhombus, regular_polygon]


for shape in shapes:
    print(shape)
    print("-------------------")