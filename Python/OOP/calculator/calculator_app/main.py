import tkinter as tk
from calculator_engine import CalculatorEngine

engine = CalculatorEngine()

root = tk.Tk()
root.title("Calculator")
root.geometry("360x520")
root.minsize(320, 480)
root.configure(bg="#f2f2f2")

history_var = tk.StringVar()

history_label = tk.Label(
    root,
    textvariable=history_var,
    font=("Segoe UI", 12),
    bg="#f2f2f2",
    fg="#666",
    anchor="e",
    justify="right"
)
history_label.pack(fill="x", padx=12)

display_var = tk.StringVar()

display = tk.Label(
    root,
    textvariable=display_var,
    font=("Segoe UI", 30),
    bg="#f2f2f2",
    fg="#111",
    anchor="e",
    padx=12,
    pady=20
)
display.pack(fill="x")

def update():
    display_var.set(engine.get_display())
    history_var.set(engine.get_history())

def num(n):
    engine.input_number(n)
    update()


def op(o):
    engine.input_operator(o)
    update()


def eq():
    engine.calculate()
    update()


def clear():
    engine.reset()
    update()

def backspace():
    engine.backspace()
    update()
def percent():
    engine.input_percent()
    update()

BTN_BG = "#ffffff"
BTN_HOVER = "#e6e6e6"


def make_btn(parent, text, cmd):
    b = tk.Button(
        parent,
        text=text,
        command=cmd,
        font=("Segoe UI", 16),
        bg=BTN_BG,
        fg="#111",
        relief="flat",
        activebackground=BTN_HOVER,
        bd=0
    )

    def enter(_):
        b.config(bg=BTN_HOVER)

    def leave(_):
        b.config(bg=BTN_BG)

    b.bind("<Enter>", enter)
    b.bind("<Leave>", leave)

    return b

frame = tk.Frame(root, bg="#f2f2f2")
frame.pack(expand=True, fill="both", padx=10, pady=10)

buttons = [
    ("C", clear), ("%", percent), ("^", lambda: op("^")), ("/", lambda: op("/")),
    ("7", lambda: num("7")), ("8", lambda: num("8")), ("9", lambda: num("9")), ("*", lambda: op("*")),
    ("4", lambda: num("4")), ("5", lambda: num("5")), ("6", lambda: num("6")), ("-", lambda: op("-")),
    ("1", lambda: num("1")), ("2", lambda: num("2")), ("3", lambda: num("3")), ("+", lambda: op("+")),
    ("⌫", backspace), ("0", lambda: num("0")),(".", lambda: engine.input_dot() or update()), ("=", eq)
]

row = 0
col = 0

for text, cmd in buttons:
    if text == "":
        col += 1
        continue

    make_btn(frame, text, cmd).grid(
        row=row,
        column=col,
        sticky="nsew",
        padx=4,
        pady=4,
        ipady=12
    )

    col += 1
    if col > 3:
        col = 0
        row += 1

for i in range(6):
    frame.grid_rowconfigure(i, weight=1)

for i in range(4):
    frame.grid_columnconfigure(i, weight=1)

update()
root.mainloop()