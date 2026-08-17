from calculator import Calculator

class CalculatorEngine:
    def __init__(self):
        self.a = None
        self.b = ""
        self.op = None
        self.history = []
        self.after_equal = False
        self.calc = Calculator()

    def fmt(self, x):
        if isinstance(x, float) and x.is_integer():
            return str(int(x))
        return str(x)

    def reset(self):
        self.a = None
        self.b = ""
        self.op = None
        self.after_equal = False
        self.history = []

    def get_display(self):
        if self.a is None and self.b == "":
            return "0"

        if self.op is None:
            if self.b != "":
                return self.b
            if self.a is not None:
                return self.fmt(self.a)
            return "0"

        return f"{self.fmt(self.a)}{self.op}{self.b}"

    def apply(self, a, b):
        if self.op == "+":
            return self.calc.add(a, b)
        if self.op == "-":
            return self.calc.subtract(a, b)
        if self.op == "*":
            return self.calc.multiply(a, b)
        if self.op == "/":
            return self.calc.divide(a, b)
        if self.op == "^":
            return self.calc.power(a, b)

    def input_dot(self):

        # reset po "="
        if self.after_equal:
            self.a = None
            self.op = None
            self.b = ""
            self.after_equal = False

        target = self.b

        if "." in target:
            return

        if target == "":
            self.b = "0."
        elif target == "-":
            self.b = "-0."
        else:
            self.b += "."

    def input_number(self, d):

        if self.after_equal:
            self.a = None
            self.op = None
            self.b = ""
            self.after_equal = False

        self.b += d

    def input_operator(self, op):

        if self.a is None and self.b == "":
            if op == "-":
                self.b = "-"
                return
            return

        if op == "-":

            if self.op in ["*", "/"] and self.b == "":
                self.b = "-"
                return

            if self.b not in ["", "-"]:
                if self.a is None:
                    self.a = float(self.b)
                else:
                    self.a = self.apply(self.a, float(self.b))

                self.b = ""

            self.op = op
            return

        if self.b not in ["", "-"]:
            if self.a is None:
                self.a = float(self.b)
            else:
                self.a = self.apply(self.a, float(self.b))

            self.b = ""

        self.op = op
        self.after_equal = False

    def input_percent(self):

        if self.after_equal:
            self.a = None
            self.op = None
            self.after_equal = False

        if self.b in ["", "-"]:
            return

        try:
            value = float(self.b) / 100
            self.b = self.fmt(value)
        except:
            pass

    def calculate(self):

        if self.a is None or self.b in ["", "-"]:
            return

        result = self.apply(self.a, float(self.b))
        self.add_history(result)

        self.a = result
        self.b = ""
        self.op = None

        self.after_equal = True

    def add_history(self, result):
        expr = self.get_display()
        self.history.append(f"{expr} = {self.fmt(result)}")

        if len(self.history) > 3:
            self.history.pop(0)

    def get_history(self):
        return "\n".join(self.history[::-1])

    def backspace(self):

        if self.after_equal:
            self.after_equal = False
            self.b = self.fmt(self.a) if self.a is not None else ""
            self.a = None
            self.op = None

        if self.b:
            self.b = self.b[:-1]

            if self.b == "-":
                self.b = ""
            return

        if self.op is not None:
            self.op = None