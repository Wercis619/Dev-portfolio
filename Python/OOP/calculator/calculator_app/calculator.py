import math
class Calculator:
    def add(self,a,b):
        return a+b
    def subtract(self,a,b):
        return a-b
    def multiply(self,a,b):
        return a*b
    def divide(self,a,b):
        if b == 0:
            return "You can't divide by zero"
        return a/b

    def power(self, a, b):
        return a ** b