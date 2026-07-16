from shapes.three_d.pyramid import Pyramid
from shapes.two_d.circle import Circle
from shapes.two_d.rectangle import Rectangle
from shapes.two_d.rhombus import Rhombus
from shapes.two_d.square import Square
from shapes.two_d.trapezoid import Trapezoid
from shapes.two_d.triangle import Triangle
from shapes.two_d.regular_polygon import RegularPolygon
from shapes.three_d.cone import Cone
from shapes.three_d.cylinder import Cylinder
from shapes.three_d.sphere import Sphere
from shapes.three_d.cuboid import Cuboid
from shapes.three_d.cube import Cube
from shapes.three_d.prism import Prism


# 2D Shapes
circle = Circle(5)
rectangle = Rectangle(5, 3)
square = Square(4)
triangle = Triangle(3, 3, 3)
trapezoid = Trapezoid(5, 5, 5, 5, 5)
rhombus = Rhombus(5, 8, 6)
regular_polygon = RegularPolygon(5, 8)

shapes_2d = [
    circle,
    rectangle,
    square,
    triangle,
    trapezoid,
    rhombus,
    regular_polygon,
]


# 3D Shapes
cone = Cone(5, 8)
cylinder = Cylinder(4, 7)
sphere = Sphere(6)
cuboid = Cuboid(4, 5, 6)
cube = Cube(3)
triangle_base = Triangle(3,4,5)
trapezoid_base = trapezoid

triangular_prism = Prism(
    triangle_base,
    10
)

trapezoid_pyramid = Pyramid(
    trapezoid_base,
    10,
    7
)

shapes_3d = [
    cone,
    cylinder,
    sphere,
    cuboid,
    cube,
    triangular_prism,
    trapezoid_pyramid,
]


print("========== 2D SHAPES ==========\n")

for shape in shapes_2d:
    print(shape)
    print("-------------------")


print("\n========== 3D SHAPES ==========\n")

for shape in shapes_3d:
    print(shape)
    print("-------------------")