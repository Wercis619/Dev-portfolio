import math

from shapes.shape_3d import Shape3D
from shapes.shape_2d import Shape2D

from shapes.two_d.rectangle import Rectangle
from shapes.two_d.triangle import Triangle
from shapes.two_d.square import Square
from shapes.two_d.trapezoid import Trapezoid
from shapes.two_d.rhombus import Rhombus
from shapes.two_d.parallelogram import Parallelogram
from shapes.two_d.regular_polygon import RegularPolygon


class Pyramid(Shape3D):

    def __init__(self, base, height):
        super().__init__("Pyramid")

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
            "Parallelogram": "Parallelogram-based",
        }

        return names.get(
            self.base.name,
            self.base.name
        )

    def shape_type(self):
        return (
            f"{self.base_type_name()} pyramid"
        )

    def volume(self):
        return (
            (1 / 3)
            * self.base.area()
            * self.height
        )

    def surface_area(self):
        points = self.get_base_points()

        apex = (
            0,
            0,
            self.height
        )

        lateral_area = 0

        for i in range(len(points)):
            next_i = (
                i + 1
            ) % len(points)

            x1, y1 = points[i]
            x2, y2 = points[next_i]

            vector_a = (
                x1 - apex[0],
                y1 - apex[1],
                -apex[2]
            )

            vector_b = (
                x2 - apex[0],
                y2 - apex[1],
                -apex[2]
            )

            cross_x = (
                vector_a[1] * vector_b[2]
                - vector_a[2] * vector_b[1]
            )

            cross_y = (
                vector_a[2] * vector_b[0]
                - vector_a[0] * vector_b[2]
            )

            cross_z = (
                vector_a[0] * vector_b[1]
                - vector_a[1] * vector_b[0]
            )

            triangle_area = (
                0.5
                * math.sqrt(
                    cross_x ** 2
                    + cross_y ** 2
                    + cross_z ** 2
                )
            )

            lateral_area += triangle_area

        return (
            self.base.area()
            + lateral_area
        )

    def get_base_points(self):

        if isinstance(self.base, Square):
            half = self.base.side / 2

            return [
                (-half, -half),
                (half, -half),
                (half, half),
                (-half, half)
            ]

        elif isinstance(self.base, Rectangle):
            half_width = self.base.width / 2
            half_height = self.base.height / 2

            return [
                (-half_width, -half_height),
                (half_width, -half_height),
                (half_width, half_height),
                (-half_width, half_height)
            ]

        elif isinstance(self.base, Triangle):
            a = self.base.side_a
            b = self.base.side_b
            c = self.base.side_c

            x = (
                b ** 2
                + c ** 2
                - a ** 2
            ) / (2 * c)

            y = math.sqrt(
                b ** 2
                - x ** 2
            )

            points = [
                (0, 0),
                (c, 0),
                (x, y)
            ]

            center_x = sum(
                point[0]
                for point in points
            ) / 3

            center_y = sum(
                point[1]
                for point in points
            ) / 3

            return [
                (
                    point_x - center_x,
                    point_y - center_y
                )
                for point_x, point_y in points
            ]

        elif isinstance(self.base, Trapezoid):
            base_a = self.base.base_a
            base_b = self.base.base_b
            height = self.base.height
            side_a = self.base.side_a
            side_b = self.base.side_b

            difference = (
                base_a - base_b
            )

            x1 = math.sqrt(
                side_a ** 2
                - height ** 2
            )

            x2 = math.sqrt(
                side_b ** 2
                - height ** 2
            )

            offset = (
                difference
                + x1
                - x2
            ) / 2

            points = [
                (0, 0),
                (base_a, 0),
                (
                    offset + base_b,
                    height
                ),
                (
                    offset,
                    height
                )
            ]

            area_twice = 0
            center_x = 0
            center_y = 0

            for i in range(len(points)):
                x_current, y_current = (
                    points[i]
                )

                x_next, y_next = points[
                    (i + 1)
                    % len(points)
                ]

                cross = (
                    x_current * y_next
                    - x_next * y_current
                )

                area_twice += cross

                center_x += (
                    x_current + x_next
                ) * cross

                center_y += (
                    y_current + y_next
                ) * cross

            center_x /= (
                3 * area_twice
            )

            center_y /= (
                3 * area_twice
            )

            return [
                (
                    x - center_x,
                    y - center_y
                )
                for x, y in points
            ]

        elif isinstance(self.base, Rhombus):
            diagonal_a = (
                self.base.diagonal_a
            )

            diagonal_b = (
                self.base.diagonal_b
            )

            half_a = diagonal_a / 2
            half_b = diagonal_b / 2

            points = [
                (0, half_a),
                (half_b, 0),
                (0, -half_a),
                (-half_b, 0)
            ]

            x1, y1 = points[2]
            x2, y2 = points[1]

            side_angle = math.atan2(
                y2 - y1,
                x2 - x1
            )

            rotation = -side_angle

            rotated_points = []

            for x, y in points:
                rotated_x = (
                    x * math.cos(rotation)
                    - y * math.sin(rotation)
                )

                rotated_y = (
                    x * math.sin(rotation)
                    + y * math.cos(rotation)
                )

                rotated_points.append(
                    (
                        rotated_x,
                        rotated_y
                    )
                )

            return rotated_points

        elif isinstance(
            self.base,
            Parallelogram
        ):
            base_length = self.base.base
            side = self.base.side
            height = self.base.height

            offset = math.sqrt(
                side ** 2
                - height ** 2
            )

            center_x = (
                base_length
                + offset
            ) / 2

            center_y = (
                height / 2
            )

            return [
                (
                    -center_x,
                    -center_y
                ),
                (
                    base_length
                    - center_x,
                    -center_y
                ),
                (
                    base_length
                    + offset
                    - center_x,
                    height
                    - center_y
                ),
                (
                    offset
                    - center_x,
                    height
                    - center_y
                )
            ]

        elif isinstance(
            self.base,
            RegularPolygon
        ):
            side = self.base.side
            n = self.base.number_of_sides

            radius = side / (
                2
                * math.sin(
                    math.pi / n
                )
            )

            points = []

            for i in range(n):
                angle = (
                    2
                    * math.pi
                    * i
                    / n
                )

                x = (
                    radius
                    * math.cos(angle)
                )

                y = (
                    radius
                    * math.sin(angle)
                )

                points.append(
                    (
                        x,
                        y
                    )
                )

            return points

        raise ValueError(
            "Unsupported pyramid base shape"
        )

    def parameters(self):
        return (
            f"base = {self.base.shape_type()}, "
            f"height = {self.height}"
        )