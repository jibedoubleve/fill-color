using Spectre.Console;

namespace FillColor;

public static class ChangeColor
{
    #region Fields

    private static readonly Logger Logger = new();

    #endregion Fields

    #region Methods

    private static void ColorizeWithBfs(Image image, int x, int y, Color newColor)
    {
        var firstPixel = image.GetPixel(x, y);
        var visitor = new Visitor(firstPixel.Color, Logger);
        var queue = new Queue<Pixel>();
        queue.Enqueue(firstPixel);
        Logger.Append($"[yellow]Starting...[/]", firstPixel);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            Logger.Append($"[hotpink3_1]Dequeue.[/]", current);
            visitor.Visit(current, p => { p.Color = newColor; });

            image.TryGoLeft(current, pix => visitor.TryScheduleVisit(pix, queue.Enqueue));
            image.TryGoRight(current, pix => visitor.TryScheduleVisit(pix, queue.Enqueue));
            image.TryGoDown(current, pix => visitor.TryScheduleVisit(pix, queue.Enqueue));
            image.TryGoUp(current, pix => visitor.TryScheduleVisit(pix, queue.Enqueue));
        }
    }

    private static Canvas Draw(Image image)
    {
        Canvas canvas = image;
        foreach (var pixel in image) canvas.SetPixel(pixel.X, pixel.Y, pixel.Color);

        return canvas;
    }

    public static void Execute(int x, int y)
    {
        var pixels = new List<Pixel>()
        {
            new(0, 0, Color.Gold3),
            new(0, 1, Color.Gold3),
            new(0, 2, Color.Gold3),
            new(0, 3, Color.White),
            new(0, 4, Color.Gold3),

            new(1, 0, Color.Gold3),
            new(1, 1, Color.White),
            new(1, 2, Color.Gold3),
            new(1, 3, Color.White),
            new(1, 4, Color.Gold3),

            new(2, 0, Color.Gold3),
            new(2, 1, Color.Gold3),
            new(2, 2, Color.Gold3),
            new(2, 3, Color.Gold3),
            new(2, 4, Color.White),

            new(3, 0, Color.Gold3),
            new(3, 1, Color.Gold3),
            new(3, 2, Color.Gold3),
            new(3, 3, Color.Gold3),
            new(3, 4, Color.Gold3),

            new(4, 0, Color.White),
            new(4, 1, Color.White),
            new(4, 2, Color.White),
            new(4, 3, Color.Gold3),
            new(4, 4, Color.White)
        };
        var image = new Image(5, 5, pixels);

        AnsiConsole.MarkupLine("\t[Yellow]Based image. [/]");
        var canvas = Draw(image);
        AnsiConsole.Write(canvas);

        AnsiConsole.MarkupLine($"\t[Yellow]Fill zone that contains pixel ({x}, {y}) in red. [/]");
        ColorizeWithBfs(image, x, y, Color.Red);
        AnsiConsole.Write((Canvas)image);

        AnsiConsole.WriteLine();
        if (AnsiConsole.Confirm("Display logs?", false))
        {
            Logger.Dump();
        }
    }

    #endregion Methods
}