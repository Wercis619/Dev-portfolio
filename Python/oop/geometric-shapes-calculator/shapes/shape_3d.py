from abc import ABC, abstractmethod
from shapes.shape import Shape

class Shape3D(Shape, ABC):
    def __init__(self, name):
        super().__init__(name)

    def info(self):
        return (
                super().info()
                + f"\nVolume: {self.volume()}"
                + f"\nSurface area: {self.surface_area()}"
        )

    @abstractmethod
    def volume(self):
        pass

    @abstractmethod
    def surface_area(self):
        pass