import tkinter as tk
from tkinter import ttk

from gui.panels.shape_input import ShapeInputPanel
from gui.renderer import ShapeRenderer
from shapes.shape_2d import Shape2D
from shapes.shape_3d import Shape3D


class GeometryApp:

    def __init__(self):
        self.root = tk.Tk()

        self.root.title(
            "Geometry Calculator"
        )

        self.root.geometry(
            "1200x780"
        )

        self.root.minsize(
            300,
            600
        )

        self.root.configure(
            bg="#f4f6f9"
        )

        self.current_layout_mode = None
        self.touch_start_y = None

        self.phone_canvas = None
        self.main_container = None
        self.left_container = None

        self.input_card = None
        self.input_title = None
        self.input_panel = None

        self.info_card = None
        self.info_title = None

        self.shape_name_label = None
        self.shape_type_label = None
        self.parameters_label = None
        self.area_label = None
        self.perimeter_label = None

        self.right_container = None
        self.renderer = None

        self.phone_window = None

        self.configure_style()
        self.create_scroll_system()
        self.create_layout()

        self.root.bind(
            "<Configure>",
            self.check_responsive_layout
        )

        self.root.bind_all(
            "<MouseWheel>",
            self.on_mousewheel,
            add="+"
        )

        self.root.bind_all(
            "<ButtonPress-1>",
            self.start_touch_scroll,
            add="+"
        )

        self.root.bind_all(
            "<B1-Motion>",
            self.drag_touch_scroll,
            add="+"
        )

        self.root.bind_all(
            "<ButtonRelease-1>",
            self.end_touch_scroll,
            add="+"
        )

    def configure_style(self):
        style = ttk.Style()

        style.theme_use(
            "clam"
        )

        style.configure(
            ".",
            background="#f4f6f9",
            foreground="#1e293b"
        )

        style.configure(
            "Card.TFrame",
            background="#ffffff"
        )

        style.configure(
            "TLabel",
            font=("Segoe UI", 11),
            background="#ffffff",
            foreground="#475569"
        )

        style.configure(
            "TEntry",
            font=("Segoe UI", 11)
        )

        style.configure(
            "TCombobox",
            font=("Segoe UI", 11)
        )

        style.configure(
            "TButton",
            font=("Segoe UI", 11, "bold"),
            padding=6,
            background="#1f77b4",
            foreground="white",
            borderwidth=0,
            focuscolor="none"
        )

        style.map(
            "TButton",
            background=[
                (
                    "active",
                    "#15578a"
                )
            ]
        )

        style.map(
            "TCombobox",
            fieldbackground=[
                (
                    "readonly",
                    "#ffffff"
                )
            ],
            selectbackground=[
                (
                    "readonly",
                    "#ffffff"
                )
            ],
            selectforeground=[
                (
                    "readonly",
                    "#1e293b"
                )
            ],
            focuscolor=[
                (
                    "active",
                    "none"
                ),
                (
                    "focus",
                    "none"
                )
            ]
        )

        self.root.option_add(
            "*TCombobox*Listbox.font",
            ("Segoe UI", 11)
        )

        self.root.option_add(
            "*TCombobox*font",
            ("Segoe UI", 11)
        )

    def create_scroll_system(self):
        self.phone_canvas = tk.Canvas(
            self.root,
            bg="#f4f6f9",
            highlightthickness=0,
            bd=0
        )

        self.phone_canvas.bind(
            "<Configure>",
            self.update_phone_canvas_width
        )

    def create_layout(self):
        self.main_container = ttk.Frame(
            self.root,
            padding=36
        )

        self.main_container.pack(
            fill="both",
            expand=True
        )

        self.left_container = ttk.Frame(
            self.main_container
        )

        self.left_container.columnconfigure(
            0,
            weight=1
        )

        self.left_container.rowconfigure(
            0,
            weight=1
        )

        self.left_container.rowconfigure(
            1,
            weight=1
        )

        self.input_card = ttk.Frame(
            self.left_container,
            padding=16,
            style="Card.TFrame"
        )

        self.input_card.grid(
            row=0,
            column=0,
            sticky="nsew",
            pady=(0, 25)
        )

        self.input_title = tk.Label(
            self.input_card,
            text="Choose shape",
            font=(
                "Segoe UI",
                12,
                "bold"
            ),
            bg="#ffffff",
            fg="#1e293b",
            anchor="w"
        )

        self.input_title.pack(
            anchor="w",
            pady=(0, 12)
        )

        self.input_panel = ShapeInputPanel(
            self.input_card,
            self.shape_created
        )

        self.input_panel.pack(
            fill="both",
            expand=True,
            anchor="w"
        )

        self.info_card = ttk.Frame(
            self.left_container,
            padding=16,
            style="Card.TFrame"
        )

        self.info_card.grid(
            row=1,
            column=0,
            sticky="nsew",
            pady=(20, 0)
        )

        self.info_title = tk.Label(
            self.info_card,
            text="Shape Information",
            font=(
                "Segoe UI",
                12,
                "bold"
            ),
            bg="#ffffff",
            fg="#1e293b",
            anchor="w"
        )

        self.info_title.pack(
            anchor="w",
            pady=(0, 12)
        )

        self.shape_name_label = tk.Label(
            self.info_card,
            text="Name: —",
            font=(
                "Segoe UI",
                11
            ),
            bg="#ffffff",
            fg="#475569",
            anchor="w",
            justify="left",
            wraplength=320
        )

        self.shape_name_label.pack(
            anchor="w",
            pady=6
        )

        self.shape_type_label = tk.Label(
            self.info_card,
            text="Type: —",
            font=(
                "Segoe UI",
                11
            ),
            bg="#ffffff",
            fg="#475569",
            anchor="w",
            justify="left",
            wraplength=320
        )

        self.shape_type_label.pack(
            anchor="w",
            pady=6
        )

        self.parameters_label = tk.Label(
            self.info_card,
            text="Parameters: —",
            font=(
                "Segoe UI",
                11
            ),
            bg="#ffffff",
            fg="#475569",
            anchor="w",
            justify="left",
            wraplength=320
        )

        self.parameters_label.pack(
            anchor="w",
            pady=6
        )

        self.area_label = tk.Label(
            self.info_card,
            text="Area: —",
            font=(
                "Segoe UI",
                11
            ),
            bg="#ffffff",
            fg="#475569",
            anchor="w",
            justify="left",
            wraplength=320
        )

        self.area_label.pack(
            anchor="w",
            pady=6
        )

        self.perimeter_label = tk.Label(
            self.info_card,
            text="Perimeter: —",
            font=(
                "Segoe UI",
                11
            ),
            bg="#ffffff",
            fg="#475569",
            anchor="w",
            justify="left",
            wraplength=320
        )

        self.perimeter_label.pack(
            anchor="w",
            pady=6
        )

        self.right_container = ttk.Frame(
            self.main_container,
            padding=16,
            style="Card.TFrame"
        )

        self.right_container.columnconfigure(
            0,
            weight=1
        )

        self.right_container.rowconfigure(
            0,
            weight=1
        )

        self.renderer = ShapeRenderer(
            self.right_container
        )

        self.main_container.columnconfigure(
            0,
            weight=1,
            minsize=300
        )

        self.main_container.columnconfigure(
            1,
            weight=2
        )

        self.main_container.rowconfigure(
            0,
            weight=1
        )

    def check_responsive_layout(
        self,
        event
    ):
        if event.widget != self.root:
            return

        width = event.width

        if width >= 950:
            new_mode = "desktop"

        elif width >= 600:
            new_mode = "tablet"

        else:
            new_mode = "phone"

        if (
            new_mode
            == self.current_layout_mode
        ):
            return

        self.current_layout_mode = (
            new_mode
        )

        self.input_panel.set_layout_mode(
            new_mode
        )

        self.touch_start_y = None

        self.reset_main_grid()

        if new_mode == "desktop":

            self.hide_phone_scroll()

            self.set_desktop_layout()

        elif new_mode == "tablet":

            self.show_phone_scroll()

            self.set_tablet_layout()

        else:

            self.show_phone_scroll()

            self.set_phone_layout()

        self.root.after(
            20,
            self.update_phone_scroll_region,
            None
        )

    def set_desktop_layout(self):
        self.main_container.configure(
            padding=20
        )

        self.left_container.columnconfigure(
            0,
            weight=1
        )

        self.left_container.columnconfigure(
            1,
            weight=0
        )

        self.left_container.rowconfigure(
            0,
            weight=1
        )

        self.left_container.rowconfigure(
            1,
            weight=1
        )

        self.input_card.grid_configure(
            row=0,
            column=0,
            sticky="nsew",
            padx=0,
            pady=(0, 20)
        )

        self.info_card.grid_configure(
            row=1,
            column=0,
            sticky="nsew",
            padx=0,
            pady=(20, 0)
        )

        self.main_container.columnconfigure(
            0,
            weight=1,
            minsize=300
        )

        self.main_container.columnconfigure(
            1,
            weight=2
        )

        self.main_container.rowconfigure(
            0,
            weight=1
        )

        self.left_container.grid(
            row=0,
            column=0,
            sticky="nsew",
            padx=(40, 40),
            pady=20
        )

        self.right_container.grid(
            row=0,
            column=1,
            sticky="nsew",
            padx=(0, 40),
            pady=20
        )

    def set_tablet_layout(self):
        self.main_container.configure(
            padding=(12, 24)
        )

        self.left_container.columnconfigure(
            0,
            weight=1
        )

        self.left_container.columnconfigure(
            1,
            weight=1
        )

        self.left_container.rowconfigure(
            0,
            weight=1
        )

        self.left_container.rowconfigure(
            1,
            weight=0
        )

        self.input_card.grid_configure(
            row=0,
            column=0,
            sticky="nsew",
            padx=(0, 6),
            pady=0
        )

        self.info_card.grid_configure(
            row=0,
            column=1,
            sticky="nsew",
            padx=(6, 0),
            pady=0
        )

        self.main_container.columnconfigure(
            0,
            weight=1
        )

        self.left_container.grid(
            row=0,
            column=0,
            sticky="ew",
            padx=12,
            pady=(0, 12)
        )

        self.right_container.grid(
            row=1,
            column=0,
            sticky="ew",
            padx=12,
            pady=(12, 0)
        )

    def set_phone_layout(self):
        self.main_container.configure(
            padding=(7, 14)
        )

        self.left_container.columnconfigure(
            0,
            weight=1
        )

        self.left_container.columnconfigure(
            1,
            weight=0
        )

        self.left_container.rowconfigure(
            0,
            weight=0
        )

        self.left_container.rowconfigure(
            1,
            weight=0
        )

        self.input_card.grid_configure(
            row=0,
            column=0,
            sticky="ew",
            padx=0,
            pady=(0, 14)
        )

        self.info_card.grid_configure(
            row=1,
            column=0,
            sticky="ew",
            padx=0,
            pady=(0, 0)
        )

        self.main_container.columnconfigure(
            0,
            weight=1
        )

        self.left_container.grid(
            row=0,
            column=0,
            sticky="ew",
            padx=7,
            pady=(0, 7)
        )

        self.right_container.grid(
            row=1,
            column=0,
            sticky="ew",
            padx=7,
            pady=(7, 0)
        )

    def reset_main_grid(self):
        self.left_container.grid_forget()

        self.right_container.grid_forget()

        self.main_container.columnconfigure(
            0,
            weight=0,
            minsize=0
        )

        self.main_container.columnconfigure(
            1,
            weight=0,
            minsize=0
        )

        self.main_container.rowconfigure(
            0,
            weight=0
        )

        self.main_container.rowconfigure(
            1,
            weight=0
        )

    def show_phone_scroll(self):
        if self.phone_window is not None:
            return

        self.main_container.pack_forget()

        self.phone_canvas.pack(
            fill="both",
            expand=True
        )

        self.phone_window = (
            self.phone_canvas.create_window(
                0,
                0,
                window=self.main_container,
                anchor="nw"
            )
        )

        self.main_container.bind(
            "<Configure>",
            self.update_phone_scroll_region
        )

    def hide_phone_scroll(self):
        if self.phone_window is None:
            return

        self.phone_canvas.delete(
            self.phone_window
        )

        self.phone_window = None

        self.phone_canvas.pack_forget()

        self.main_container.pack(
            fill="both",
            expand=True
        )

        self.touch_start_y = None

    def update_phone_canvas_width(
        self,
        event
    ):
        if self.phone_window is not None:

            self.phone_canvas.itemconfig(
                self.phone_window,
                width=event.width
            )

    def update_phone_scroll_region(
        self,
        _event=None
    ):
        if self.phone_window is not None:

            self.phone_canvas.configure(
                scrollregion=(
                    self.phone_canvas.bbox(
                        "all"
                    )
                )
            )

    def on_mousewheel(
        self,
        event
    ) -> None:
        if self.current_layout_mode not in (
            "phone",
            "tablet"
        ):
            return

        if (
            self.current_layout_mode
            == "tablet"
            and self.input_panel.should_capture_touch(
                event.widget
            )
        ):
            return

        self.phone_canvas.yview_scroll(
            int(
                -event.delta / 120
            ),
            "units"
        )

    def start_touch_scroll(
        self,
        event
    ) -> None:

        if self.current_layout_mode not in (
            "phone",
            "tablet"
        ):
            self.touch_start_y = None
            return

        if (
            self.current_layout_mode
            == "tablet"
            and self.input_panel.should_capture_touch(
                event.widget
            )
        ):
            self.touch_start_y = None
            return

        self.touch_start_y = (
            event.y_root
        )

    def drag_touch_scroll(
        self,
        event
    ) -> str | None:

        if self.current_layout_mode not in (
            "phone",
            "tablet"
        ):
            return None

        if (
            self.current_layout_mode
            == "tablet"
            and self.input_panel.should_capture_touch(
                event.widget
            )
        ):
            return None

        if self.touch_start_y is None:
            return None

        bbox = self.phone_canvas.bbox(
            "all"
        )

        if bbox is None:
            return None

        content_height = (
            bbox[3] - bbox[1]
        )

        canvas_height = (
            self.phone_canvas.winfo_height()
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
            self.phone_canvas.yview()
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

            self.phone_canvas.yview_moveto(
                new_pixel
                / content_height
            )

        self.touch_start_y = (
            event.y_root
        )

        return "break"

    def end_touch_scroll(
        self,
        _event
    ) -> None:

        self.touch_start_y = None

    def update_shape_info(
        self,
        shape
    ):

        self.shape_name_label.config(
            text=f"Name: {shape.name}"
        )

        self.shape_type_label.config(
            text=(
                f"Type: "
                f"{shape.shape_type()}"
            )
        )

        self.parameters_label.config(
            text=(
                f"Parameters: "
                f"{shape.parameters()}"
            )
        )

        # 2D

        if isinstance(
            shape,
            Shape2D
        ):

            self.area_label.config(
                text=(
                    f"Area: "
                    f"{shape.area():.2f}"
                )
            )

            self.perimeter_label.config(
                text=(
                    f"Perimeter: "
                    f"{shape.perimeter():.2f}"
                )
            )

        # 3D

        elif isinstance(
            shape,
            Shape3D
        ):

            self.area_label.config(
                text=(
                    f"Surface area: "
                    f"{shape.surface_area():.2f}"
                )
            )

            self.perimeter_label.config(
                text=(
                    f"Volume: "
                    f"{shape.volume():.2f}"
                )
            )

    def shape_created(
        self,
        shape
    ):

        self.renderer.draw(
            shape
        )

        self.update_shape_info(
            shape
        )

    def run(self):
        self.root.mainloop()