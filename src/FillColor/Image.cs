using System.Collections;
using Spectre.Console;

namespace FillColor;

public class Image : IEnumerable<Pixel>
{
    #region Fields

    private readonly IEnumerable<Pixel> _pixels;

    #endregion Fields

    #region Constructors

    public Image(int length, int width, IEnumerable<Pixel> pixels)
    {
        pixels = pixels.ToArray();
        if (Size > pixels.Count())
            throw new ArgumentOutOfRangeException(nameof(pixels), "Too many pixels for the image");

        Length = length;
        Width = width;
        _pixels = pixels;
    }

    #endregion Constructors

    #region Properties

    private int Length { get; }
    private int Size => Length * Width;
    private int Width { get; }

    #endregion Properties

    #region Methods

    private Pixel Down(Pixel pixel) => Go(pixel, 0, 1);

    private Pixel Go(Pixel pixel, int xOffset, int yOffset)
    {
        var x = pixel.X;
        var y = pixel.Y;
        return (from p in _pixels
                where p.X == x + xOffset
                      && p.Y == y + yOffset
                select p).Single();
    }

    private Pixel Left(Pixel pixel) => Go(pixel, -1, 0);

    private Pixel Right(Pixel pixel) => Go(pixel, 1, 0);

    private Pixel Up(Pixel pixel) => Go(pixel, 0, -1);

    public static implicit operator Canvas(Image image)
    {
        var canvas = new Canvas(image.Length, image.Width);
        foreach (var pixel in image) canvas.SetPixel(pixel.X, pixel.Y, pixel.Color);

        return canvas;
    }

    public IEnumerator<Pixel> GetEnumerator() => _pixels.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public Pixel GetPixel(int x, int y) => (from p in _pixels
                                            where p.X == x
                                                  && p.Y == y
                                            select p).Single();

    public void TryGoDown(Pixel pixel, Action<Pixel> action)
    {
        if (pixel.Y < Width - 1) action(Down(pixel));
    }

    public void TryGoLeft(Pixel pixel, Action<Pixel> action)
    {
        if (pixel.X > 1) action(Left(pixel));
    }

    public void TryGoRight(Pixel pixel, Action<Pixel> action)
    {
        if (pixel.X < Length - 1) action(Right(pixel));
    }

    public void TryGoUp(Pixel pixel, Action<Pixel> action)
    {
        if (pixel.Y > 1) action(Up(pixel));
    }

    #endregion Methods
}