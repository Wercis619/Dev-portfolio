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

from shapes.three_d.cone import Cone
from shapes.three_d.cube import Cube
from shapes.three_d.cuboid import Cuboid
from shapes.three_d.cylinder import Cylinder
from shapes.three_d.prism import Prism
from shapes.three_d.pyramid import Pyramid
from shapes.three_d.sphere import Sphere


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
        self.base_fields = {}

        self.layout_mode = "desktop"

        self.touch_start_y = None

        self.dimension_label = None
        self.dimension_choice = None
        self.shape_label = None
        self.shape_choice = None
        self.input_container = None
        self.input_canvas = None
        self.input_scrollbar = None
        self.input_frame = None
        self.input_window = None
        self.create_button = None
        self.base_section = None
        self.base_shape_choice = None
        self.base_input_frame = None

        self.create_widgets()

    def create_widgets(self):

        self.dimension_label = ttk.Label(
            self,
            text="Dimension"
        )

        self.dimension_label.grid(
            row=0,
            column=0,
            sticky="w",
            pady=(0, 6)
        )

        self.dimension_choice = ttk.Combobox(
            self,
            values=[
                "2D",
                "3D"
            ],
            state="readonly"
        )

        self.dimension_choice.grid(
            row=1,
            column=0,
            sticky="w",
            pady=(0, 12)
        )

        self.dimension_choice.bind(
            "<<ComboboxSelected>>",
            self.update_shape_choices
        )

        self.shape_label = ttk.Label(
            self,
            text="Shape"
        )

        self.shape_label.grid(
            row=2,
            column=0,
            sticky="w",
            pady=(0, 6)
        )

        self.shape_choice = ttk.Combobox(
            self,
            state="disabled"
        )

        self.shape_choice.grid(
            row=3,
            column=0,
            sticky="w",
            pady=(0, 12)
        )

        self.shape_choice.bind(
            "<<ComboboxSelected>>",
            self.show_fields
        )

        self.input_container = tk.Frame(
            self,
            bg="#ffffff"
        )

        self.input_container.grid(
            row=4,
            column=0,
            sticky="w"
        )

        self.input_canvas = tk.Canvas(
            self.input_container,
            bg="#ffffff",
            highlightthickness=0,
            bd=0
        )

        self.input_canvas.grid(
            row=0,
            column=0,
            sticky="w"
        )

        self.input_scrollbar = ttk.Scrollbar(
            self.input_container,
            orient="vertical",
            command=self.input_canvas.yview
        )

        self.input_canvas.configure(
            yscrollcommand=self.input_scrollbar.set
        )

        self.input_frame = tk.Frame(
            self.input_canvas,
            bg="#ffffff"
        )

        self.input_window = (
            self.input_canvas.create_window(
                (0, 0),
                window=self.input_frame,
                anchor="nw"
            )
        )

        self.input_frame.bind(
            "<Configure>",
            self.update_input_scroll
        )

        self.input_canvas.bind(
            "<Configure>",
            self.on_input_canvas_configure
        )

        self.input_canvas.bind(
            "<MouseWheel>",
            self.on_input_mousewheel
        )

        self.bind_all(
            "<ButtonPress-1>",
            self.on_input_touch_start,
            add="+"
        )

        self.bind_all(
            "<B1-Motion>",
            self.on_input_touch_move,
            add="+"
        )

        self.bind_all(
            "<ButtonRelease-1>",
            self.on_input_touch_end,
            add="+"
        )

        self.create_button = ttk.Button(
            self.input_frame,
            text="Create Shape",
            command=self.create_shape,
            state="disabled"
        )

        self.create_button.grid(
            row=0,
            column=0,
            columnspan=2,
            sticky="w"
        )

        self.grid_columnconfigure(
            0,
            weight=1
        )

        self.after_idle(
            self.reset_input_area,
        )

    def set_layout_mode(self, mode):

        if self.layout_mode == mode:
            return

        self.layout_mode = mode

        self.touch_start_y = None

        self.input_canvas.yview_moveto(0)

        self.input_scrollbar.grid_remove()

        self.after_idle(
            self.update_input_scroll,
            0
        )

    def is_widget_inside_input(self, widget):

        current = widget

        while current is not None:

            if current is self.input_frame:
                return True

            if current is self.input_canvas:
                return True

            if current is self.input_container:
                return True

            try:
                current = current.master
            except AttributeError:
                return False

        return False

    def input_can_scroll(self):

        bbox = self.input_canvas.bbox("all")

        if bbox is None:
            return False

        content_height = (
            bbox[3] - bbox[1]
        )

        canvas_height = (
            self.input_canvas.winfo_height()
        )

        return (
            content_height
            > canvas_height + 1
        )

    def should_capture_touch(self, widget):

        if self.layout_mode != "tablet":
            return False

        if not self.is_widget_inside_input(widget):
            return False

        return self.input_can_scroll()

    def get_available_input_height(self):

        panel_height = (
            self.winfo_height()
        )

        if panel_height <= 1:
            return 250

        top_height = (
            self.dimension_label.winfo_reqheight()
            + self.dimension_choice.winfo_reqheight()
            + self.shape_label.winfo_reqheight()
            + self.shape_choice.winfo_reqheight()
            + 60
        )

        available_height = (
            panel_height - top_height
        )

        return max(
            120,
            available_height
        )

    def update_input_scroll(
        self,
        _event=None
    ):

        self.update_idletasks()

        bbox = self.input_canvas.bbox(
            "all"
        )

        if bbox is None:
            content_height = 1

        else:
            content_height = (
                bbox[3] - bbox[1]
            )

        if self.layout_mode == "phone":

            self.input_scrollbar.grid_remove()

            self.input_canvas.yview_moveto(
                0
            )

            self.input_canvas.configure(
                height=max(
                    1,
                    content_height
                ),
                scrollregion=bbox
            )

            return

        if self.layout_mode == "tablet":

            available_height = 220

        else:

            available_height = (
                self.get_available_input_height()
            )

        if content_height > available_height:

            self.input_canvas.configure(
                height=available_height
            )

            self.input_scrollbar.grid(
                row=0,
                column=1,
                sticky="ns"
            )

        else:

            self.input_canvas.configure(
                height=max(
                    1,
                    content_height
                )
            )

            self.input_scrollbar.grid_remove()

            self.input_canvas.yview_moveto(
                0
            )

        self.input_canvas.configure(
            scrollregion=bbox
        )

    def on_input_canvas_configure(
        self,
        event
    ):

        required_width = (
            self.input_frame.winfo_reqwidth()
        )

        width = max(
            event.width,
            required_width
        )

        self.input_canvas.itemconfigure(
            self.input_window,
            width=width
        )

        self.after_idle(
            self.update_input_scroll,
            0
        )

    def on_input_mousewheel(
        self,
        event
    ) -> str | None:

        if self.layout_mode == "phone":
            return None

        if not self.input_can_scroll():
            return None

        if event.delta > 0:
            direction = -1
        else:
            direction = 1

        self.input_canvas.yview_scroll(
            direction,
            "units"
        )

        return "break"

    def on_input_touch_start(
        self,
        event
    ):

        if not self.should_capture_touch(
            event.widget
        ):
            self.touch_start_y = None
            return

        self.touch_start_y = (
            event.y_root
        )

    def on_input_touch_move(
        self,
        event
    ) -> str | None:

        if self.touch_start_y is None:
            return None

        if not self.should_capture_touch(
            event.widget
        ):
            return None

        bbox = self.input_canvas.bbox(
            "all"
        )

        if bbox is None:
            return None

        content_height = (
            bbox[3] - bbox[1]
        )

        canvas_height = (
            self.input_canvas.winfo_height()
        )

        scrollable_height = (
            content_height
            - canvas_height
        )

        if scrollable_height <= 0:
            return None

        distance = (
            self.touch_start_y
            - event.y_root
        )

        current_top, _ = (
            self.input_canvas.yview()
        )

        current_pixel = (
            current_top
            * content_height
        )

        new_pixel = (
            current_pixel
            + distance
        )

        new_pixel = max(
            0,
            min(
                new_pixel,
                scrollable_height
            )
        )

        if content_height > 0:

            self.input_canvas.yview_moveto(
                new_pixel
                / content_height
            )

        self.touch_start_y = (
            event.y_root
        )

        return "break"

    def on_input_touch_end(
        self,
        _event
    ):

        self.touch_start_y = None

    def reset_input_area(self):

        self.input_canvas.yview_moveto(
            0
        )

        self.input_scrollbar.grid_remove()

        self.input_canvas.configure(
            scrollregion=self.input_canvas.bbox(
                "all"
            )
        )

        self.after_idle(
            self.update_input_scroll,
            0
        )

    def update_shape_choices(
        self,
        _event=None
    ):

        for widget in (
            self.input_frame.winfo_children()
        ):

            if widget is not self.create_button:
                widget.destroy()

        self.fields.clear()
        self.base_fields.clear()

        self.shape_choice.set("")

        self.create_button.config(
            state="disabled"
        )

        dimension = (
            self.dimension_choice.get()
        )

        if dimension == "2D":

            shapes = [
                "Circle",
                "Rectangle",
                "Square",
                "Triangle",
                "Trapezoid",
                "Rhombus",
                "Parallelogram",
                "Regular Polygon"
            ]

        elif dimension == "3D":

            shapes = [
                "Cube",
                "Cuboid",
                "Sphere",
                "Cylinder",
                "Cone",
                "Prism",
                "Pyramid"
            ]

        else:

            shapes = []

        self.shape_choice.config(
            values=shapes,
            state="readonly"
        )

        self.create_button.grid_forget()

        self.create_button.grid(
            row=0,
            column=0,
            columnspan=2,
            sticky="w"
        )

        self.input_canvas.yview_moveto(
            0
        )

        self.input_scrollbar.grid_remove()

        self.after_idle(
            self.reset_input_area,
        )

    def show_fields(
        self,
        _event=None
    ):

        for widget in (
            self.input_frame.winfo_children()
        ):

            if widget is not self.create_button:
                widget.destroy()

        self.fields.clear()
        self.base_fields.clear()

        self.input_canvas.yview_moveto(
            0
        )

        shape = (
            self.shape_choice.get()
        )

        # 2D

        if shape == "Circle":

            self.create_field(
                "Radius"
            )

        elif shape == "Rectangle":

            self.create_field(
                "Width"
            )

            self.create_field(
                "Height"
            )

        elif shape == "Square":

            self.create_field(
                "Side"
            )

        elif shape == "Triangle":

            self.create_field(
                "Side A"
            )

            self.create_field(
                "Side B"
            )

            self.create_field(
                "Side C"
            )

        elif shape == "Trapezoid":

            self.create_field(
                "Base A"
            )

            self.create_field(
                "Base B"
            )

            self.create_field(
                "Height"
            )

            self.create_field(
                "Side A"
            )

            self.create_field(
                "Side B"
            )

        elif shape == "Rhombus":

            self.create_field(
                "Side"
            )

            self.create_field(
                "Diagonal A"
            )

            self.create_field(
                "Diagonal B"
            )

        elif shape == "Parallelogram":

            self.create_field(
                "Base"
            )

            self.create_field(
                "Side"
            )

            self.create_field(
                "Height"
            )

        elif shape == "Regular Polygon":

            self.create_field(
                "Side"
            )

            self.create_field(
                "Number of sides"
            )

        # 3D

        elif shape == "Cube":

            self.create_field(
                "Side"
            )

        elif shape == "Cuboid":

            self.create_field(
                "Length"
            )

            self.create_field(
                "Width"
            )

            self.create_field(
                "Height"
            )

        elif shape == "Sphere":

            self.create_field(
                "Radius"
            )

        elif shape == "Cylinder":

            self.create_field(
                "Radius"
            )

            self.create_field(
                "Height"
            )

        elif shape == "Cone":

            self.create_field(
                "Radius"
            )

            self.create_field(
                "Height"
            )

        elif shape == "Prism":

            self.create_base_selector()

            self.create_prism_height_field()

        elif shape == "Pyramid":

            self.create_base_selector()

            self.create_pyramid_height_field()

        if shape:

            self.create_button.config(
                state="normal"
            )

            self.place_create_button()

        else:

            self.create_button.config(
                state="disabled"
            )

            self.create_button.grid_forget()

            self.create_button.grid(
                row=0,
                column=0,
                columnspan=2,
                sticky="w"
            )

        self.after_idle(
            self.update_input_scroll,
            0,
        )

    def place_create_button(self):

        self.create_button.grid_forget()

        shape = (
            self.shape_choice.get()
        )

        if shape == "Prism":
            row = 2

        elif shape == "Pyramid":
            row = 2

        else:
            row = len(
                self.fields
            )

        self.create_button.grid(
            row=row,
            column=0,
            columnspan=2,
            sticky="w",
            pady=(16, 0)
        )

    def create_field(
        self,
        name
    ):

        row = len(
            self.fields
        )

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

    def create_base_selector(self):

        self.base_section = tk.Frame(
            self.input_frame,
            bg="#ffffff"
        )

        self.base_section.grid(
            row=0,
            column=0,
            columnspan=2,
            sticky="w"
        )

        base_label = ttk.Label(
            self.base_section,
            text="Base shape"
        )

        base_label.grid(
            row=0,
            column=0,
            sticky="w",
            padx=(0, 10),
            pady=6
        )

        self.base_shape_choice = (
            ttk.Combobox(
                self.base_section,
                values=[
                    "Square",
                    "Rectangle",
                    "Triangle",
                    "Trapezoid",
                    "Rhombus",
                    "Parallelogram",
                    "Regular Polygon"
                ],
                state="readonly"
            )
        )

        self.base_shape_choice.grid(
            row=0,
            column=1,
            sticky="w",
            pady=6
        )

        self.base_shape_choice.bind(
            "<<ComboboxSelected>>",
            self.show_base_fields
        )

        self.base_input_frame = tk.Frame(
            self.base_section,
            bg="#ffffff"
        )

        self.base_input_frame.grid(
            row=1,
            column=0,
            columnspan=2,
            sticky="w"
        )

    def show_base_fields(
        self,
        _event=None
    ):

        for widget in (
            self.base_input_frame.winfo_children()
        ):
            widget.destroy()

        self.base_fields.clear()

        base_shape = (
            self.base_shape_choice.get()
        )

        if base_shape == "Square":

            self.create_base_field(
                "Side"
            )

        elif base_shape == "Rectangle":

            self.create_base_field(
                "Width"
            )

            self.create_base_field(
                "Height"
            )

        elif base_shape == "Triangle":

            self.create_base_field(
                "Side A"
            )

            self.create_base_field(
                "Side B"
            )

            self.create_base_field(
                "Side C"
            )

        elif base_shape == "Trapezoid":

            self.create_base_field(
                "Base A"
            )

            self.create_base_field(
                "Base B"
            )

            self.create_base_field(
                "Height"
            )

            self.create_base_field(
                "Side A"
            )

            self.create_base_field(
                "Side B"
            )

        elif base_shape == "Rhombus":

            self.create_base_field(
                "Side"
            )

            self.create_base_field(
                "Diagonal A"
            )

            self.create_base_field(
                "Diagonal B"
            )

        elif base_shape == "Parallelogram":

            self.create_base_field(
                "Base"
            )

            self.create_base_field(
                "Side"
            )

            self.create_base_field(
                "Height"
            )

        elif base_shape == "Regular Polygon":

            self.create_base_field(
                "Side"
            )

            self.create_base_field(
                "Number of sides"
            )

        self.input_canvas.yview_moveto(
            0
        )

        self.after_idle(
            self.update_input_scroll,
            0
        )

    def create_base_field(
        self,
        name
    ):

        row = len(
            self.base_fields
        )

        label = ttk.Label(
            self.base_input_frame,
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
            self.base_input_frame
        )

        entry.grid(
            row=row,
            column=1,
            sticky="w",
            pady=6
        )

        self.base_fields[name] = entry

    def create_prism_height_field(self):

        label = ttk.Label(
            self.input_frame,
            text="Prism height"
        )

        label.grid(
            row=1,
            column=0,
            sticky="w",
            padx=(0, 10),
            pady=6
        )

        entry = ttk.Entry(
            self.input_frame
        )

        entry.grid(
            row=1,
            column=1,
            sticky="w",
            pady=6
        )

        self.fields["Height"] = entry

    def create_pyramid_height_field(self):

        label = ttk.Label(
            self.input_frame,
            text="Pyramid height"
        )

        label.grid(
            row=1,
            column=0,
            sticky="w",
            padx=(0, 10),
            pady=6
        )

        entry = ttk.Entry(
            self.input_frame
        )

        entry.grid(
            row=1,
            column=1,
            sticky="w",
            pady=6
        )

        self.fields["Height"] = entry

    def create_base_shape(self):

        base_shape = (
            self.base_shape_choice.get()
        )

        if base_shape == "Square":

            side = float(
                self.base_fields[
                    "Side"
                ].get()
            )

            return Square(
                side
            )

        elif base_shape == "Rectangle":

            width = float(
                self.base_fields[
                    "Width"
                ].get()
            )

            height = float(
                self.base_fields[
                    "Height"
                ].get()
            )

            return Rectangle(
                width,
                height
            )

        elif base_shape == "Triangle":

            side_a = float(
                self.base_fields[
                    "Side A"
                ].get()
            )

            side_b = float(
                self.base_fields[
                    "Side B"
                ].get()
            )

            side_c = float(
                self.base_fields[
                    "Side C"
                ].get()
            )

            return Triangle(
                side_a,
                side_b,
                side_c
            )

        elif base_shape == "Trapezoid":

            base_a = float(
                self.base_fields[
                    "Base A"
                ].get()
            )

            base_b = float(
                self.base_fields[
                    "Base B"
                ].get()
            )

            height = float(
                self.base_fields[
                    "Height"
                ].get()
            )

            side_a = float(
                self.base_fields[
                    "Side A"
                ].get()
            )

            side_b = float(
                self.base_fields[
                    "Side B"
                ].get()
            )

            return Trapezoid(
                base_a,
                base_b,
                height,
                side_a,
                side_b
            )

        elif base_shape == "Rhombus":

            side = float(
                self.base_fields[
                    "Side"
                ].get()
            )

            diagonal_a = float(
                self.base_fields[
                    "Diagonal A"
                ].get()
            )

            diagonal_b = float(
                self.base_fields[
                    "Diagonal B"
                ].get()
            )

            return Rhombus(
                side,
                diagonal_a,
                diagonal_b
            )

        elif base_shape == "Parallelogram":

            base = float(
                self.base_fields[
                    "Base"
                ].get()
            )

            side = float(
                self.base_fields[
                    "Side"
                ].get()
            )

            height = float(
                self.base_fields[
                    "Height"
                ].get()
            )

            return Parallelogram(
                base,
                side,
                height
            )

        elif base_shape == "Regular Polygon":

            side = float(
                self.base_fields[
                    "Side"
                ].get()
            )

            number_of_sides = int(
                self.base_fields[
                    "Number of sides"
                ].get()
            )

            return RegularPolygon(
                side,
                number_of_sides
            )

        raise ValueError(
            "Select base shape"
        )

    def create_shape(self):

        try:

            shape = (
                self.shape_choice.get()
            )

            # 2D

            if shape == "Circle":

                radius = float(
                    self.fields[
                        "Radius"
                    ].get()
                )

                self.on_shape_created(
                    Circle(radius)
                )

            elif shape == "Square":

                side = float(
                    self.fields[
                        "Side"
                    ].get()
                )

                self.on_shape_created(
                    Square(side)
                )

            elif shape == "Rectangle":

                width = float(
                    self.fields[
                        "Width"
                    ].get()
                )

                height = float(
                    self.fields[
                        "Height"
                    ].get()
                )

                self.on_shape_created(
                    Rectangle(
                        width,
                        height
                    )
                )

            elif shape == "Triangle":

                side_a = float(
                    self.fields[
                        "Side A"
                    ].get()
                )

                side_b = float(
                    self.fields[
                        "Side B"
                    ].get()
                )

                side_c = float(
                    self.fields[
                        "Side C"
                    ].get()
                )

                self.on_shape_created(
                    Triangle(
                        side_a,
                        side_b,
                        side_c
                    )
                )

            elif shape == "Trapezoid":

                base_a = float(
                    self.fields[
                        "Base A"
                    ].get()
                )

                base_b = float(
                    self.fields[
                        "Base B"
                    ].get()
                )

                height = float(
                    self.fields[
                        "Height"
                    ].get()
                )

                side_a = float(
                    self.fields[
                        "Side A"
                    ].get()
                )

                side_b = float(
                    self.fields[
                        "Side B"
                    ].get()
                )

                self.on_shape_created(
                    Trapezoid(
                        base_a,
                        base_b,
                        height,
                        side_a,
                        side_b
                    )
                )

            elif shape == "Rhombus":

                side = float(
                    self.fields[
                        "Side"
                    ].get()
                )

                diagonal_a = float(
                    self.fields[
                        "Diagonal A"
                    ].get()
                )

                diagonal_b = float(
                    self.fields[
                        "Diagonal B"
                    ].get()
                )

                self.on_shape_created(
                    Rhombus(
                        side,
                        diagonal_a,
                        diagonal_b
                    )
                )

            elif shape == "Parallelogram":

                base = float(
                    self.fields[
                        "Base"
                    ].get()
                )

                side = float(
                    self.fields[
                        "Side"
                    ].get()
                )

                height = float(
                    self.fields[
                        "Height"
                    ].get()
                )

                self.on_shape_created(
                    Parallelogram(
                        base,
                        side,
                        height
                    )
                )

            elif shape == "Regular Polygon":

                side = float(
                    self.fields[
                        "Side"
                    ].get()
                )

                number_of_sides = int(
                    self.fields[
                        "Number of sides"
                    ].get()
                )

                self.on_shape_created(
                    RegularPolygon(
                        side,
                        number_of_sides
                    )
                )

            # 3D

            elif shape == "Cube":

                side = float(
                    self.fields[
                        "Side"
                    ].get()
                )

                self.on_shape_created(
                    Cube(side)
                )

            elif shape == "Cuboid":

                length = float(
                    self.fields[
                        "Length"
                    ].get()
                )

                width = float(
                    self.fields[
                        "Width"
                    ].get()
                )

                height = float(
                    self.fields[
                        "Height"
                    ].get()
                )

                self.on_shape_created(
                    Cuboid(
                        length,
                        width,
                        height
                    )
                )

            elif shape == "Sphere":

                radius = float(
                    self.fields[
                        "Radius"
                    ].get()
                )

                self.on_shape_created(
                    Sphere(radius)
                )

            elif shape == "Cylinder":

                radius = float(
                    self.fields[
                        "Radius"
                    ].get()
                )

                height = float(
                    self.fields[
                        "Height"
                    ].get()
                )

                self.on_shape_created(
                    Cylinder(
                        radius,
                        height
                    )
                )

            elif shape == "Cone":

                radius = float(
                    self.fields[
                        "Radius"
                    ].get()
                )

                height = float(
                    self.fields[
                        "Height"
                    ].get()
                )

                self.on_shape_created(
                    Cone(
                        radius,
                        height
                    )
                )

            elif shape == "Prism":

                base = (
                    self.create_base_shape()
                )

                height = float(
                    self.fields[
                        "Height"
                    ].get()
                )

                self.on_shape_created(
                    Prism(
                        base,
                        height
                    )
                )

            elif shape == "Pyramid":

                base = (
                    self.create_base_shape()
                )

                height = float(
                    self.fields[
                        "Height"
                    ].get()
                )

                self.on_shape_created(
                    Pyramid(
                        base,
                        height
                    )
                )

        except ValueError as error:

            messagebox.showerror(
                "Invalid value",
                str(error)
            )