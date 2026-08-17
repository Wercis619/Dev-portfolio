import tkinter as tk
from tkinter import ttk

from gui.panels.shape_input import ShapeInputPanel
from gui.renderer import ShapeRenderer


class GeometryApp:

    def __init__(self):
        self.root = tk.Tk()

        self.root.title("Geometry Calculator")
        self.root.geometry("1200x780")
        self.root.minsize(420, 680)
        self.root.configure(bg="#f4f6f9")

        self.configure_style()
        self.create_layout()

        self.current_layout_mode = None
        self.root.bind("<Configure>", self.check_responsive_layout)

    def configure_style(self):
        style = ttk.Style()
        style.theme_use("clam")

        style.configure(".", background="#f4f6f9", foreground="#1e293b")

        style.configure("Card.TFrame", background="#ffffff")
        style.configure("TLabel", font=("Segoe UI", 11), background="#ffffff", foreground="#475569")
        self.root.option_add("*TCombobox*Listbox.font", ("Segoe UI", 11))
        self.root.option_add("*TCombobox*font", ("Segoe UI", 11))
        style.configure("TEntry", font=("Segoe UI", 11))
        style.configure("TCombobox", font=("Segoe UI", 11))
        self.root.option_add("*TCombobox*Listbox.font", ("Segoe UI", 11))
        style.map("TCombobox", fieldbackground=[("readonly", "#ffffff")])
        style.map("TButton",
                  background=[("active", "#15578a")],
                  focuscolor=[("active", "none"), ("focus", "none")])

        style.map("TCombobox",
                  fieldbackground=[("readonly", "#ffffff")],
                  selectbackground=[("readonly", "#ffffff")],
                  selectforeground=[("readonly", "#1e293b")],
                  focuscolor=[("active", "none"), ("focus", "none")])

        style.configure(
            "TButton",
            font=("Segoe UI", 11, "bold"),
            padding=6,
            background="#1f77b4",
            foreground="white",
            borderwidth=0,
            focuscolor="none"
        )
        style.map("TButton", background=[("active", "#15578a")])

    def create_layout(self):
        self.main_container = ttk.Frame(self.root, padding=36)
        self.main_container.pack(fill="both", expand=True)

        self.left_container = ttk.Frame(self.main_container)
        self.left_container.columnconfigure(0, weight=1)
        self.left_container.rowconfigure(0, weight=1)
        self.left_container.rowconfigure(1, weight=1)

        self.input_card = ttk.Frame(self.left_container, padding=16, style="Card.TFrame")
        self.input_card.grid(row=0, column=0, sticky="nsew", pady=(0, 25))

        self.input_title = tk.Label(
            self.input_card,
            text="Choose shape",
            font=("Segoe UI", 12, "bold"),
            bg="#ffffff",
            fg="#1e293b",
            anchor="w"
        )
        self.input_title.pack(anchor="w", pady=(0, 12))

        self.input_panel = ShapeInputPanel(
            self.input_card,
            self.shape_created
        )
        self.input_panel.pack(fill="both", expand=True, anchor="w")


        self.info_card = ttk.Frame(self.left_container, padding=16, style="Card.TFrame")
        self.info_card.grid(row=1, column=0, sticky="nsew", pady=(20, 0))

        self.info_title = tk.Label(
            self.info_card,
            text="Shape Information",
            font=("Segoe UI", 12, "bold"),
            bg="#ffffff",
            fg="#1e293b",
            anchor="w"
        )
        self.info_title.pack(anchor="w", pady=(0, 12))

        labels_config = [
            ("shape_name_label", "Name: —"),
            ("shape_type_label", "Type: —"),
            ("parameters_label", "Parameters: —"),
            ("area_label", "Area: —"),
            ("perimeter_label", "Perimeter: —")
        ]

        for attr_name, text in labels_config:
            lbl = tk.Label(
                self.info_card,
                text=text,
                font=("Segoe UI", 11),
                bg="#ffffff",
                fg="#475569",
                anchor="w",
                justify="left"
            )
            lbl.pack(anchor="w", pady=6)
            setattr(self, attr_name, lbl)

        self.right_container = ttk.Frame(self.main_container, padding=16, style="Card.TFrame")
        self.right_container.columnconfigure(0, weight=1)
        self.right_container.rowconfigure(0, weight=1)
        self.right_container.grid(row=0, column=1, sticky="nsew", padx=(0, 0), pady=0)

        self.renderer = ShapeRenderer(self.right_container)
        try:
            self.renderer.grid(row=0, column=0, sticky="nsew")
        except AttributeError:
            pass

        self.main_container.columnconfigure(0, weight=1, minsize=420)
        self.main_container.columnconfigure(1, weight=2)
        self.main_container.rowconfigure(0, weight=1)

    def check_responsive_layout(self, event):
        if event.widget != self.root:
            return

        width = event.width
        new_mode = "vertical" if width < 950 else "horizontal"

        if new_mode != self.current_layout_mode:
            self.current_layout_mode = new_mode

            self.left_container.grid_forget()
            self.right_container.grid_forget()
            self.main_container.columnconfigure(0, weight=0)
            self.main_container.columnconfigure(1, weight=0)
            self.main_container.rowconfigure(0, weight=0)
            self.main_container.rowconfigure(1, weight=0)

            if new_mode == "vertical":
                self.main_container.columnconfigure(0, weight=1)
                self.main_container.rowconfigure(0, weight=1)
                self.main_container.rowconfigure(1, weight=1)

                self.left_container.grid(row=0, column=0, sticky="nsew", padx=0, pady=(0, 24))
                self.right_container.grid(row=1, column=0, sticky="nsew", padx=0, pady=0)
            else:
                self.main_container.columnconfigure(0, weight=1, minsize=420)
                self.main_container.columnconfigure(1, weight=2)
                self.main_container.rowconfigure(0, weight=1)

                self.left_container.grid(row=0, column=0, sticky="nsew", padx=(50, 50), pady=25)
                self.right_container.grid(row=0, column=1, sticky="nsew", padx=(0, 50), pady=25)

    def update_shape_info(self, shape):
        self.shape_name_label.config(text=f"Name: {shape.name}")
        self.shape_type_label.config(text=f"Type: {shape.shape_type()}")
        self.parameters_label.config(text=f"Parameters: {shape.parameters()}")
        self.area_label.config(text=f"Area: {shape.area():.2f}")
        self.perimeter_label.config(text=f"Perimeter: {shape.perimeter():.2f}")

    def shape_created(self, shape):
        self.renderer.draw(shape)
        self.update_shape_info(shape)

    def run(self):
        self.root.mainloop()
