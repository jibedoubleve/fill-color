using Spectre.Console;

namespace FillColor;

public class Pixel(int x, int y, Color color)
{
    #region Properties

    public Color Color
    {
        get => color;
        set => color = value;
    }

    public int X => x;

    public int Y => y;

    #endregion Properties

    #region Methods

    public override string ToString() => $"({X}, {Y})";

    #endregion Methods
}