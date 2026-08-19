import math

from matplotlib.figure import Figure
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
from matplotlib.patches import Circle as CirclePatch
from matplotlib.patches import Rectangle as RectanglePatch
from matplotlib.patches import Polygon as PolygonPatch

from shapes.two_d.circle import Circle
from shapes.two_d.rectangle import Rectangle
from shapes.two_d.triangle import Triangle
from shapes.two_d.square import Square
from shapes.two_d.trapezoid import Trapezoid
from shapes.two_d.rhombus import Rhombus
from shapes.two_d.parallelogram import Parallelogram
from shapes.two_d.regular_polygon import RegularPolygon


class ShapeRenderer:

    def __init__(self, parent):
        self.figure = Figure(figsize=(5, 5))
        self.axes = self.figure.add_subplot(111)

        self.canvas = FigureCanvasTkAgg(
            self.figure,
            master=parent
        )

        self.canvas.get_tk_widget().grid(
            row=0,
            column=0,
            columnspan=2,
            padx=0,
            pady=0,
            sticky="nsew"
        )

        self.axes.set_aspect("equal")
        # Default scale
        self.axes.set_xlim(-10, 10)
        self.axes.set_ylim(-10, 10)

        self.canvas.draw()

        parent.grid_columnconfigure(1, weight=1)
        parent.grid_rowconfigure(0, weight=1)

    def draw(self, shape):
        self.axes.clear()
        self.axes.set_aspect("equal")


        if isinstance(shape, Circle):
            self.draw_circle(shape)

        elif isinstance(shape, Square):
            self.draw_square(shape)

        elif isinstance(shape, Rectangle):
            self.draw_rectangle(shape)

        elif isinstance(shape, Triangle):
            self.draw_triangle(shape)

        elif isinstance(shape, Trapezoid):
            self.draw_trapezoid(shape)

        elif isinstance(shape, Rhombus):
            self.draw_rhombus(shape)

        elif isinstance(shape, Parallelogram):
            self.draw_parallelogram(shape)

        elif isinstance(shape, RegularPolygon):
            self.draw_regular_polygon(shape)

        self.canvas.draw()

    def draw_circle(self, circle):
        patch = CirclePatch(
            (0, 0),
            circle.radius
        )

        self.axes.add_patch(patch)

        limit = self.get_scale(circle.radius)

        self.axes.set_xlim(-limit, limit)
        self.axes.set_ylim(-limit, limit)

    def draw_square(self, square):
        side = square.side
        half = side / 2

        patch = RectanglePatch(
            (-half, -half),
            side,
            side
        )

        self.axes.add_patch(patch)

        limit = self.get_scale(side)

        self.axes.set_xlim(-limit, limit)
        self.axes.set_ylim(-limit, limit)

    def draw_rectangle(self, rectangle):
        width = rectangle.width
        height = rectangle.height

        patch = RectanglePatch(
            (-width / 2, -height / 2),
            width,
            height
        )

        self.axes.add_patch(patch)

        limit = self.get_scale(
            max(width, height)
        )

        self.axes.set_xlim(-limit, limit)
        self.axes.set_ylim(-limit, limit)

    def draw_triangle(self, triangle):
        a = triangle.side_a
        b = triangle.side_b
        c = triangle.side_c

        x = (b ** 2 + c ** 2 - a ** 2) / (2 * c)
        y = math.sqrt(b ** 2 - x ** 2)

        points = [
            (0, 0),
            (c, 0),
            (x, y)
        ]

        center_x = sum(
            point[0] for point in points
        ) / 3

        center_y = sum(
            point[1] for point in points
        ) / 3

        # Move the triangle so that
        # its centroid is at (0, 0)
        centered_points = [
            (
                point_x - center_x,
                point_y - center_y
            )
            for point_x, point_y in points
        ]

        patch = PolygonPatch(
            centered_points,
            closed=True
        )

        self.axes.add_patch(patch)

        limit = self.get_scale(
            max(a, b, c)
        )

        self.axes.set_xlim(-limit, limit)
        self.axes.set_ylim(-limit, limit)

    def draw_trapezoid(self, trapezoid):
        base_a = trapezoid.base_a
        base_b = trapezoid.base_b
        height = trapezoid.height
        side_a = trapezoid.side_a
        side_b = trapezoid.side_b

        difference = base_a - base_b

        x1 = math.sqrt(
            side_a ** 2 - height ** 2
        )

        x2 = math.sqrt(
            side_b ** 2 - height ** 2
        )

        offset = (difference + x1 - x2) / 2

        points = [
            (0, 0),
            (base_a, 0),
            (offset + base_b, height),
            (offset, height)
        ]

        area_twice = 0
        center_x = 0
        center_y = 0

        for i in range(len(points)):
            x_current, y_current = points[i]
            x_next, y_next = points[
                (i + 1) % len(points)
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

        center_x /= 3 * area_twice
        center_y /= 3 * area_twice

        centered_points = [
            (
                x - center_x,
                y - center_y
            )
            for x, y in points
        ]

        patch = PolygonPatch(
            centered_points,
            closed=True
        )

        self.axes.add_patch(patch)

        limit = self.get_scale(
            max(
                base_a,
                base_b,
                height,
                side_a,
                side_b
            )
        )

        self.axes.set_xlim(-limit, limit)
        self.axes.set_ylim(-limit, limit)

    def draw_rhombus(self, rhombus):
        diagonal_a = rhombus.diagonal_a
        diagonal_b = rhombus.diagonal_b

        half_a = diagonal_a / 2
        half_b = diagonal_b / 2

        angle = math.atan2(
            half_a,
            half_b
        )

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
                (rotated_x, rotated_y)
            )

        patch = PolygonPatch(
            rotated_points,
            closed=True
        )

        self.axes.add_patch(patch)

        limit = self.get_scale(
            max(
                rhombus.side,
                diagonal_a,
                diagonal_b
            )
        )

        self.axes.set_xlim(-limit, limit)
        self.axes.set_ylim(-limit, limit)

    def draw_parallelogram(self, parallelogram):
        base = parallelogram.base
        side = parallelogram.side
        height = parallelogram.height

        offset = math.sqrt(
            side ** 2 - height ** 2
        )

        center_x = (base + offset) / 2
        center_y = height / 2

        points = [
            (0 - center_x, 0 - center_y),
            (base - center_x, 0 - center_y),
            (base + offset - center_x, height - center_y),
            (offset - center_x, height - center_y)
        ]

        patch = PolygonPatch(
            points,
            closed=True
        )

        self.axes.add_patch(patch)

        limit = self.get_scale(
            max(
                base,
                side,
                height
            )
        )

        self.axes.set_xlim(
            -limit,
            limit
        )

        self.axes.set_ylim(
            -limit,
            limit
        )

    def draw_regular_polygon(self, polygon):
        side = polygon.side
        n = polygon.number_of_sides

        radius = side / (
                2 * math.sin(math.pi / n)
        )

        points = []

        for i in range(n):
            angle = 2 * math.pi * i / n

            x = radius * math.cos(angle)
            y = radius * math.sin(angle)

            points.append((x, y))

        patch = PolygonPatch(
            points,
            closed=True
        )

        self.axes.add_patch(patch)

        limit = self.get_scale(
            max(
                side,
                radius
            )
        )

        self.axes.set_xlim(
            -limit,
            limit
        )

        self.axes.set_ylim(
            -limit,
            limit
        )

    def get_scale(self, value):

        if value <= 10:
            return 10

        if value <= 20:
            return 20

        if value <= 60:
            return 60

        if value <= 100:
            return 100

        if value <= 300:
            return 300


        if value <= 600:
            return 600


        raise ValueError("Value cannot be greater than 600")
