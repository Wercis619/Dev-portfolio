from shapes.two_d.circle import Circle
from shapes.two_d.rectangle import Rectangle
from shapes.two_d.regular_polygon import RegularPolygon
from shapes.two_d.rhombus import Rhombus
from shapes.two_d.square import Square
from shapes.two_d.trapezoid import Trapezoid
from shapes.two_d.triangle import Triangle


def run_terminal():
    running = True

    while running:
        running = main_menu()

def main_menu():
    print(
        """
========== Geometry Calculator ==========

1. 2D Shapes
2. 3D Shapes
0. Exit
"""
    )

    choice = get_menu_choice([0, 1, 2])

    match choice:
        case 1:
            shapes_2d_menu()

        case 2:
            shapes_3d_menu()

        case 0:
            print("Goodbye!")
            return False

    return True

def get_menu_choice(options):
    while True:
        choice = input("Choose option: ")

        if choice.isdigit():
            choice = int(choice)

            if choice in options:
                return choice

        print("Invalid option. Try again.")

def shapes_2d_menu():
    while True:
        print(
            """
========== 2D Shapes ==========

1. Circle
2. Rectangle
3. Square
4. Triangle
5. Trapezoid
6. Rhombus
7. Regular Polygon
0. Back
"""
        )

        choice = get_menu_choice([0, 1, 2, 3, 4, 5, 6, 7])

        match choice:
            case 1:
                create_circle()

            case 2:
                create_rectangle()

            case 3:
                create_square()

            case 4:
                create_triangle()

            case 5:
                create_trapezoid()

            case 6:
                create_rhombus()

            case 7:
                create_regular_polygon()

            case 0:
                return

def shapes_3d_menu():
    print(
        """
========== 3D Shapes ==========

1. Cone
2. Cylinder
3. Sphere
4. Cuboid
5. Cube
6. Prism
7. Pyramid
0. Back
"""
    )

    choice = get_menu_choice([0, 1, 2, 3, 4, 5, 6, 7])

    match choice:
        case 1:
            create_cone()

        case 2:
            create_cylinder()

        case 3:
            create_sphere()

        case 4:
            create_cuboid()

        case 5:
            create_cube()

        case 6:
            create_prism()

        case 7:
            create_pyramid()

        case 0:
            return

def ask_continue():
    print(
        """
Create another one?
1. Yes
0. Back
"""
    )

    return get_menu_choice([0,1])

#2Ds
def create_circle():
    while True:
        try:
            radius = float(input("Enter radius: "))

            circle = Circle(radius)

            print(circle)

            choice = ask_continue()
            if choice == 0:
                return

        except ValueError as error:
            print(f"Error: {error}")
            print("Try again.")

def create_rectangle():
    while True:
        try:
            width = float(input("Enter width: "))
            height = float(input("Enter height: "))

            rectangle = Rectangle(width, height)

            print(rectangle)

            choice = ask_continue()

            if choice == 0:
                return

        except ValueError as error:
            print(f"Error: {error}")
            print("Try again.")

def create_square():
    while True:
        try:
            side = float(input("Enter side: "))

            square = Square(side)

            print(square)

            choice = ask_continue()

            if choice == 0:
                return

        except ValueError as error:
            print(f"Error: {error}")
            print("Try again.")

def create_triangle():
    while True:
        try:
            side_a = float(input("Enter side a: "))
            side_b = float(input("Enter side b: "))
            side_c = float(input("Enter side c: "))

            triangle = Triangle(side_a, side_b, side_c)

            print(triangle)

            choice = ask_continue()

            if choice == 0:
                return

        except ValueError as error:
            print(f"Error: {error}")
            print("Try again.")

def create_trapezoid():
    while True:
        try:
            base_a = float(input("Enter base a: "))
            base_b = float(input("Enter base b: "))
            height = float(input("Enter height: "))
            side_a = float(input("Enter side a: "))
            side_b = float(input("Enter side b: "))

            trapezoid = Trapezoid(base_a, base_b, height, side_a, side_b)

            print(trapezoid)

            choice = ask_continue()

            if choice == 0:
                return

        except ValueError as error:
            print(f"Error: {error}")
            print("Try again.")

def create_rhombus():
    while True:
        try:
            side = float(input("Enter side: "))
            diagonal_a = float(input("Enter diagonal a: "))
            diagonal_b = float(input("Enter diagonal b: "))

            rhombus = Rhombus(side, diagonal_a, diagonal_b)

            print(rhombus)

            choice = ask_continue()

            if choice == 0:
                return

        except ValueError as error:
            print(f"Error: {error}")
            print("Try again.")

def create_regular_polygon():
    while True:
        try:
            side = float(input("Enter side: "))
            number_of_sides = int(input("Enter number of sides: "))

            polygon = RegularPolygon(
                side,
                number_of_sides
            )

            print(polygon)

            choice = ask_continue()

            if choice == 0:
                return

        except ValueError as error:
            print(f"Error: {error}")
            print("Try again.")

#3Ds
def create_cone():
    pass

def create_cylinder():
    pass

def create_sphere():
    pass

def create_cuboid():
    pass

def create_cube():
    pass

def create_prism():
    pass

def create_pyramid():
    pass