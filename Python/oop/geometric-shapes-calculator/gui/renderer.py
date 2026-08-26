import math
import numpy as np

from matplotlib.figure import Figure
from matplotlib.backends.backend_tkagg import FigureCanvasTkAgg
from matplotlib.patches import Circle as CirclePatch
from matplotlib.patches import Rectangle as RectanglePatch
from matplotlib.patches import Polygon as PolygonPatch
from mpl_toolkits.mplot3d.art3d import Poly3DCollection
from matplotlib.axes import Axes
from mpl_toolkits.mplot3d.axes3d import Axes3D

from shapes.two_d.circle import Circle
from shapes.two_d.rectangle import Rectangle
from shapes.two_d.triangle import Triangle
from shapes.two_d.square import Square
from shapes.two_d.trapezoid import Trapezoid
from shapes.two_d.rhombus import Rhombus
from shapes.two_d.parallelogram import Parallelogram
from shapes.two_d.regular_polygon import RegularPolygon

from shapes.three_d.cube import Cube
from shapes.three_d.cuboid import Cuboid
from shapes.three_d.sphere import Sphere
from shapes.three_d.cylinder import Cylinder
from shapes.three_d.cone import Cone
from shapes.three_d.prism import Prism
from shapes.three_d.pyramid import Pyramid


class ShapeRenderer:

    def __init__(self, parent):
        self.figure = Figure(figsize=(5, 5))

        self.axes: Axes | Axes3D = (
            self.figure.add_subplot(111)
        )

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

        self.axes.set_xlim(-10, 10)
        self.axes.set_ylim(-10, 10)

        self.canvas.draw()

        parent.grid_columnconfigure(
            1,
            weight=1
        )

        parent.grid_rowconfigure(
            0,
            weight=1
        )

    def draw(self, shape):
        # 3D

        if isinstance(shape, Cube):
            self.prepare_3d_axes()
            self.draw_cube(shape)

        elif isinstance(shape, Cuboid):
            self.prepare_3d_axes()
            self.draw_cuboid(shape)

        elif isinstance(shape, Sphere):
            self.prepare_3d_axes()
            self.draw_sphere(shape)

        elif isinstance(shape, Cylinder):
            self.prepare_3d_axes()
            self.draw_cylinder(shape)

        elif isinstance(shape, Cone):
            self.prepare_3d_axes()
            self.draw_cone(shape)

        elif isinstance(shape, Prism):
            self.prepare_3d_axes()
            self.draw_prism(shape)

        elif isinstance(shape, Pyramid):
            self.prepare_3d_axes()
            self.draw_pyramid(shape)

        # 2D

        else:
            self.prepare_2d_axes()

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

    def prepare_2d_axes(self):
        self.figure.clear()

        self.axes = self.figure.add_subplot(111)

        self.axes.set_aspect("equal")

    def prepare_3d_axes(self):
        self.figure.clear()

        self.axes = self.figure.add_subplot(
            111,
            projection="3d"
        )

        self.axes.set_box_aspect(
            (1, 1, 1)
        )

        self.axes.view_init(
            elev=25,
            azim=35
        )

    # 2D

    def draw_circle(self, circle):
        patch = CirclePatch(
            (0, 0),
            circle.radius
        )

        self.axes.add_patch(patch)

        limit = self.get_scale(
            circle.radius
        )

        self.axes.set_xlim(
            -limit,
            limit
        )

        self.axes.set_ylim(
            -limit,
            limit
        )

    def draw_square(self, square):
        side = square.side
        half = side / 2

        patch = RectanglePatch(
            (-half, -half),
            side,
            side
        )

        self.axes.add_patch(patch)

        limit = self.get_scale(
            side
        )

        self.axes.set_xlim(
            -limit,
            limit
        )

        self.axes.set_ylim(
            -limit,
            limit
        )

    def draw_rectangle(self, rectangle):
        width = rectangle.width
        height = rectangle.height

        patch = RectanglePatch(
            (
                -width / 2,
                -height / 2
            ),
            width,
            height
        )

        self.axes.add_patch(patch)

        limit = self.get_scale(
            max(
                width,
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

    def draw_triangle(self, triangle):
        a = triangle.side_a
        b = triangle.side_b
        c = triangle.side_c

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
            max(
                a,
                b,
                c
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

    def draw_trapezoid(self, trapezoid):
        base_a = trapezoid.base_a
        base_b = trapezoid.base_b
        height = trapezoid.height
        side_a = trapezoid.side_a
        side_b = trapezoid.side_b

        difference = (
            base_a
            - base_b
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

        for i in range(
            len(points)
        ):
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
                x_current
                + x_next
            ) * cross

            center_y += (
                y_current
                + y_next
            ) * cross

        center_x /= (
            3 * area_twice
        )

        center_y /= (
            3 * area_twice
        )

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

        self.axes.set_xlim(
            -limit,
            limit
        )

        self.axes.set_ylim(
            -limit,
            limit
        )

    def draw_rhombus(self, rhombus):
        diagonal_a = (
            rhombus.diagonal_a
        )

        diagonal_b = (
            rhombus.diagonal_b
        )

        half_a = (
            diagonal_a / 2
        )

        half_b = (
            diagonal_b / 2
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
                (
                    rotated_x,
                    rotated_y
                )
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

        self.axes.set_xlim(
            -limit,
            limit
        )

        self.axes.set_ylim(
            -limit,
            limit
        )

    def draw_parallelogram(
        self,
        parallelogram
    ):
        base = parallelogram.base
        side = parallelogram.side
        height = parallelogram.height

        offset = math.sqrt(
            side ** 2
            - height ** 2
        )

        center_x = (
            base + offset
        ) / 2

        center_y = (
            height / 2
        )

        points = [
            (
                -center_x,
                -center_y
            ),
            (
                base - center_x,
                -center_y
            ),
            (
                base
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

    def draw_regular_polygon(
        self,
        polygon
    ):
        side = polygon.side
        n = polygon.number_of_sides

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

    # 3D

    def draw_cube(self, cube):
        side = cube.side
        half = side / 2

        vertices = [
            (
                -half,
                -half,
                -half
            ),
            (
                half,
                -half,
                -half
            ),
            (
                half,
                half,
                -half
            ),
            (
                -half,
                half,
                -half
            ),
            (
                -half,
                -half,
                half
            ),
            (
                half,
                -half,
                half
            ),
            (
                half,
                half,
                half
            ),
            (
                -half,
                half,
                half
            )
        ]

        faces = [
            [
                vertices[0],
                vertices[1],
                vertices[2],
                vertices[3]
            ],
            [
                vertices[4],
                vertices[5],
                vertices[6],
                vertices[7]
            ],
            [
                vertices[0],
                vertices[1],
                vertices[5],
                vertices[4]
            ],
            [
                vertices[2],
                vertices[3],
                vertices[7],
                vertices[6]
            ],
            [
                vertices[1],
                vertices[2],
                vertices[6],
                vertices[5]
            ],
            [
                vertices[0],
                vertices[3],
                vertices[7],
                vertices[4]
            ]
        ]

        cube_collection = Poly3DCollection(
            faces,
            alpha=0.6,
            edgecolor="black"
        )

        self.axes.add_collection3d(
            cube_collection
        )

        limit = self.get_scale(
            side
        )

        self.axes.set_xlim(
            -limit,
            limit
        )

        self.axes.set_ylim(
            -limit,
            limit
        )

        self.axes.set_zlim(
            -limit,
            limit
        )


        self.axes.set_xlabel("X")
        self.axes.set_ylabel("Y")
        self.axes.set_zlabel("Z")



    def draw_cuboid(self, cuboid):
        length = cuboid.length
        width = cuboid.width
        height = cuboid.height

        half_length = length / 2
        half_width = width / 2
        half_height = height / 2

        vertices = [
            (-half_length, -half_width, -half_height),
            (half_length, -half_width, -half_height),
            (half_length, half_width, -half_height),
            (-half_length, half_width, -half_height),

            (-half_length, -half_width, half_height),
            (half_length, -half_width, half_height),
            (half_length, half_width, half_height),
            (-half_length, half_width, half_height)
        ]

        faces = [
            [
                vertices[0],
                vertices[1],
                vertices[2],
                vertices[3]
            ],
            [
                vertices[4],
                vertices[5],
                vertices[6],
                vertices[7]
            ],
            [
                vertices[0],
                vertices[1],
                vertices[5],
                vertices[4]
            ],
            [
                vertices[2],
                vertices[3],
                vertices[7],
                vertices[6]
            ],
            [
                vertices[1],
                vertices[2],
                vertices[6],
                vertices[5]
            ],
            [
                vertices[0],
                vertices[3],
                vertices[7],
                vertices[4]
            ]
        ]

        cuboid_collection = Poly3DCollection(
            faces,
            alpha=0.6,
            edgecolor="black"
        )

        self.axes.add_collection3d(
            cuboid_collection
        )

        limit = self.get_scale(
            max(
                length,
                width,
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

        self.axes.set_zlim(
            -limit,
            limit
        )

        self.axes.set_xlabel("X")
        self.axes.set_ylabel("Y")
        self.axes.set_zlabel("Z")



    def draw_sphere(self, sphere):
        radius = sphere.radius

        u = np.linspace(
            0,
            2 * np.pi,
            50
        )

        v = np.linspace(
            0,
            np.pi,
            30
        )

        x = (
                radius
                * np.outer(
            np.cos(u),
            np.sin(v)
        )
        )

        y = (
                radius
                * np.outer(
            np.sin(u),
            np.sin(v)
        )
        )

        z = (
                radius
                * np.outer(
            np.ones(
                np.size(u)
            ),
            np.cos(v)
        )
        )

        self.axes.plot_surface(
            x,
            y,
            z,
            alpha=0.6
        )

        limit = self.get_scale(
            radius * 2
        )

        self.axes.set_xlim(
            -limit,
            limit
        )

        self.axes.set_ylim(
            -limit,
            limit
        )

        self.axes.set_zlim(
            -limit,
            limit
        )


        self.axes.set_xlabel("X")
        self.axes.set_ylabel("Y")
        self.axes.set_zlabel("Z")

    def draw_cylinder(self, cylinder):
        radius = cylinder.radius
        height = cylinder.height

        theta = np.linspace(
            0,
            2 * np.pi,
            50
        )

        z = np.linspace(
            -height / 2,
            height / 2,
            30
        )

        theta_grid, z_grid = np.meshgrid(
            theta,
            z
        )

        x = radius * np.cos(theta_grid)
        y = radius * np.sin(theta_grid)

        self.axes.plot_surface(
            x,
            y,
            z_grid,
            alpha=0.6
        )

        circle_x = radius * np.cos(theta)
        circle_y = radius * np.sin(theta)

        top_z = np.full_like(
            theta,
            height / 2
        )

        bottom_z = np.full_like(
            theta,
            -height / 2
        )

        self.axes.plot_trisurf(
            circle_x,
            circle_y,
            top_z,
            alpha=0.6
        )

        self.axes.plot_trisurf(
            circle_x,
            circle_y,
            bottom_z,
            alpha=0.6
        )

        limit = self.get_scale(
            max(
                radius * 2,
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

        self.axes.set_zlim(
            -limit,
            limit
        )


        self.axes.set_xlabel("X")
        self.axes.set_ylabel("Y")
        self.axes.set_zlabel("Z")

    def draw_cone(self, cone):
        radius = cone.radius
        height = cone.height

        theta = np.linspace(
            0,
            2 * np.pi,
            50
        )

        z = np.linspace(
            -height / 2,
            height / 2,
            30
        )

        theta_grid, z_grid = np.meshgrid(
            theta,
            z
        )

        current_radius = (
                radius
                * (
                        height / 2 - z_grid
                )
                / height
        )

        x = (
                current_radius
                * np.cos(theta_grid)
        )

        y = (
                current_radius
                * np.sin(theta_grid)
        )

        self.axes.plot_surface(
            x,
            y,
            z_grid,
            alpha=0.6
        )

        circle_x = (
                radius
                * np.cos(theta)
        )

        circle_y = (
                radius
                * np.sin(theta)
        )

        bottom_z = np.full_like(
            theta,
            -height / 2
        )

        self.axes.plot_trisurf(
            circle_x,
            circle_y,
            bottom_z,
            alpha=0.6
        )

        limit = self.get_scale(
            max(
                radius * 2,
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

        self.axes.set_zlim(
            -limit,
            limit
        )

        self.axes.set_xlabel("X")
        self.axes.set_ylabel("Y")
        self.axes.set_zlabel("Z")

    @staticmethod
    def get_base_points(base):

        if isinstance(base, Square):
            half = base.side / 2

            return [
                (-half, -half),
                (half, -half),
                (half, half),
                (-half, half)
            ]

        elif isinstance(base, Rectangle):
            half_width = base.width / 2
            half_height = base.height / 2

            return [
                (-half_width, -half_height),
                (half_width, -half_height),
                (half_width, half_height),
                (-half_width, half_height)
            ]

        elif isinstance(base, Triangle):
            a = base.side_a
            b = base.side_b
            c = base.side_c

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

        elif isinstance(base, Trapezoid):
            base_a = base.base_a
            base_b = base.base_b
            height = base.height
            side_a = base.side_a
            side_b = base.side_b

            difference = (
                    base_a
                    - base_b
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

            for i in range(
                    len(points)
            ):
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
                                    x_current
                                    + x_next
                            ) * cross

                center_y += (
                                    y_current
                                    + y_next
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

        elif isinstance(base, Rhombus):
            diagonal_a = base.diagonal_a
            diagonal_b = base.diagonal_b

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

        elif isinstance(base, Parallelogram):
            base_length = base.base
            side = base.side
            height = base.height

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

        elif isinstance(base, RegularPolygon):
            side = base.side
            n = base.number_of_sides

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
            "Unsupported prism base shape"
        )

    def draw_prism(self, prism):
        base_points = self.get_base_points(
            prism.base
        )

        height = prism.height
        half_height = height / 2

        bottom = [
            (
                x,
                y,
                -half_height
            )
            for x, y in base_points
        ]

        top = [
            (
                x,
                y,
                half_height
            )
            for x, y in base_points
        ]

        faces = [
            bottom,
            top
        ]

        for i in range(
                len(base_points)
        ):
            next_i = (
                             i + 1
                     ) % len(base_points)

            faces.append(
                [
                    bottom[i],
                    bottom[next_i],
                    top[next_i],
                    top[i]
                ]
            )

        prism_collection = Poly3DCollection(
            faces,
            alpha=0.6,
            edgecolor="black"
        )

        self.axes.add_collection3d(
            prism_collection
        )

        max_x = max(
            abs(x)
            for x, y in base_points
        )

        max_y = max(
            abs(y)
            for x, y in base_points
        )

        base_size = max(
            max_x * 2,
            max_y * 2
        )

        limit = self.get_scale(
            max(
                base_size,
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

        self.axes.set_zlim(
            -limit,
            limit
        )


        self.axes.set_xlabel("X")
        self.axes.set_ylabel("Y")
        self.axes.set_zlabel("Z")

    def draw_pyramid(self, pyramid):
        base_points = self.get_base_points(
            pyramid.base
        )

        height = pyramid.height
        half_height = height / 2

        bottom = [
            (
                x,
                y,
                -half_height
            )
            for x, y in base_points
        ]

        top = (
            0,
            0,
            half_height
        )

        faces = [
            bottom
        ]

        for i in range(
                len(base_points)
        ):
            next_i = (
                             i + 1
                     ) % len(base_points)

            faces.append(
                [
                    bottom[i],
                    bottom[next_i],
                    top
                ]
            )

        pyramid_collection = Poly3DCollection(
            faces,
            alpha=0.6,
            edgecolor="black"
        )

        self.axes.add_collection3d(
            pyramid_collection
        )

        max_x = max(
            abs(x)
            for x, y in base_points
        )

        max_y = max(
            abs(y)
            for x, y in base_points
        )

        base_size = max(
            max_x * 2,
            max_y * 2
        )

        limit = self.get_scale(
            max(
                base_size,
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

        self.axes.set_zlim(
            -limit,
            limit
        )

        self.axes.set_xlabel("X")
        self.axes.set_ylabel("Y")
        self.axes.set_zlabel("Z")

    # SCALE

    @staticmethod
    def get_scale(value):

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

        raise ValueError(
            "Value cannot be greater than 600"
        )

