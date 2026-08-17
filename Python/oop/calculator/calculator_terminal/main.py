from calculator import Calculator

calc = Calculator()
is_running = True

print("Calculator")
print("Operations: +, -, *, /, ^")
print()

while is_running:

    try:
        a = float(input("Enter first number: "))
        op = input("Enter operation (+, -, *, /, ^): ")
        b = float(input("Enter second number: "))

    except ValueError:
        print("Error: You must enter valid numbers!")
        print()
        continue

    if op not in ["+", "-", "*", "/", "^"]:
        print("Error: Invalid operation!")
        print()
        continue

    match op:
        case "+":
            result = calc.add(a, b)
        case "-":
            result = calc.subtract(a, b)
        case "*":
            result = calc.multiply(a, b)
        case "/":
            result = calc.divide(a, b)
        case "^":
            result = calc.power(a, b)

    print("Result:", result)
    print()

    choice = input("Do you want to continue? (y/n): ")

    if choice.lower() == "n":
        is_running = False

print("Goodbye!")