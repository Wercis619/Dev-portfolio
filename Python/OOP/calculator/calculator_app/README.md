Calculator App (Python + Tkinter)

A simple calculator application built with Python and Tkinter, featuring a clean separation between UI, logic engine, and math operations.

Features

- Basic arithmetic operations (+, -, *, /, ^)
- Percentage support
- Backspace functionality
- Clear/reset button
- Calculation history (last 3 operations)
- Floating point handling with clean integer output
- Negative number support (e.g. `-6 * -2`)
- Windows/Android-style input behavior

Architecture

The project is split into 3 layers:

- **Calculator** → basic math operations
- **CalculatorEngine** → input handling + expression logic
- **GUI (Tkinter)** → user interface
