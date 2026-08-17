import tkinter as tk
from tkinter import ttk, messagebox

from shapes.two_d.circle import Circle
from shapes.two_d.rectangle import Rectangle
from shapes.two_d.triangle import Triangle
from shapes.two_d.square import Square
from shapes.two_d.trapezoid import Trapezoid
from shapes.two_d.rhombus import Rhombus
from shapes.two_d.parallelogram import Parallelogram
from shapes.two_d.regular_polygon import RegularPolygon


class ShapeInputPanel(tk.Frame):

    def __init__(self, parent, on_shape_created):
        super().__init__(
            parent,
            bg="#ffffff",
            padx=0,
            pady=0
        )

        self.on_shape_created = on_shape_created
        self.fields = {}

        self.create_widgets()

    def create_widgets(self):
        self.shape_choice = ttk.Combobox(
            self,
            values=[
                "Circle",
                "Rectangle",
                "Square",
                "Triangle",
                "Trapezoid",
                "Rhombus",
                "Parallelogram",
                "Regular Polygon"
            ],
            state="readonly"
        )

        self.shape_choice.grid(
            row=0,
            column=0,
            sticky="w",
            pady=(0, 12)
        )

        self.shape_choice.current(0)

        self.shape_choice.bind(
            "<<ComboboxSelected>>",
            self.show_fields
        )

        self.input_frame = tk.Frame(
            self,
            bg="#ffffff"
        )

        self.input_frame.grid(
            row=1,
            column=0,
            sticky="w"
        )

        self.create_button = ttk.Button(
            self,
            text="Create Shape",
            command=self.create_shape
        )

        self.create_button.grid(
            row=2,
            column=0,
            sticky="w",
            pady=(16, 0)
        )

        self.show_fields()

    def show_fields(self, event=None):
        for widget in self.input_frame.winfo_children():
            widget.destroy()

        self.fields.clear()

        shape = self.shape_choice.get()

        if shape == "Circle":
            self.create_field("Radius")

        elif shape == "Rectangle":
            self.create_field("Width")
            self.create_field("Height")

        elif shape == "Square":
            self.create_field("Side")

        elif shape == "Triangle":
            self.create_field("Side A")
            self.create_field("Side B")
            self.create_field("Side C")

        elif shape == "Trapezoid":
            self.create_field("Base A")
            self.create_field("Base B")
            self.create_field("Height")
            self.create_field("Side A")
            self.create_field("Side B")

        elif shape == "Rhombus":
            self.create_field("Side")
            self.create_field("Diagonal A")
            self.create_field("Diagonal B")

        elif shape == "Parallelogram":
            self.create_field("Base")
            self.create_field("Side")
            self.create_field("Height")

        elif shape == "Regular Polygon":
            self.create_field("Side")
            self.create_field("Number of sides")

    def create_field(self, name):
        row = len(self.fields)

        label = ttk.Label(
            self.input_frame,
            text=name
        )

        label.grid(
            row=row,
            column=0,
            sticky="w",
            padx=(0, 10),
            pady=6
        )

        entry = ttk.Entry(
            self.input_frame
        )

        entry.grid(
            row=row,
            column=1,
            sticky="w",
            pady=6
        )

        self.fields[name] = entry

    def create_shape(self):
        try:
            shape = self.shape_choice.get()

            if shape == "Circle":
                radius = float(self.fields["Radius"].get())
                circle = Circle(radius)
                self.on_shape_created(circle)

            elif shape == "Square":
                side = float(self.fields["Side"].get())
                square = Square(side)
                self.on_shape_created(square)

            elif shape == "Rectangle":
                width = float(self.fields["Width"].get())
                height = float(self.fields["Height"].get())
                rectangle = Rectangle(width, height)
                self.on_shape_created(rectangle)

            elif shape == "Triangle":
                side_a = float(self.fields["Side A"].get())
                side_b = float(self.fields["Side B"].get())
                side_c = float(self.fields["Side C"].get())
                triangle = Triangle(side_a, side_b, side_c)
                self.on_shape_created(triangle)

            elif shape == "Trapezoid":
                base_a = float(self.fields["Base A"].get())
                base_b = float(self.fields["Base B"].get())
                height = float(self.fields["Height"].get())
                side_a = float(self.fields["Side A"].get())
                side_b = float(self.fields["Side B"].get())
                trapezoid = Trapezoid(base_a, base_b, height, side_a, side_b)
                self.on_shape_created(trapezoid)

            elif shape == "Rhombus":
                side = float(self.fields["Side"].get())
                diagonal_a = float(self.fields["Diagonal A"].get())
                diagonal_b = float(self.fields["Diagonal B"].get())
                rhombus = Rhombus(side, diagonal_a, diagonal_b)
                self.on_shape_created(rhombus)

            elif shape == "Parallelogram":
                base = float(self.fields["Base"].get())
                side = float(self.fields["Side"].get())
                height = float(self.fields["Height"].get())
                parallelogram = Parallelogram(base, side, height)
                self.on_shape_created(parallelogram)

            elif shape == "Regular Polygon":
                side = float(self.fields["Side"].get())
                number_of_sides = int(self.fields["Number of sides"].get())
                regularpolygon = RegularPolygon(side, number_of_sides)
                self.on_shape_created(regularpolygon)

        except ValueError as error:
            messagebox.showerror(
                "Invalid value",
                str(error)
            )