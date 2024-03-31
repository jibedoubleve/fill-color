using Spectre.Console;

namespace FillColor;

public class Visitor
{
    #region Fields

    private readonly Logger _logger;
    private readonly HashSet<string> _visitRegistry = [];
    private readonly Color _zoneColor;

    #endregion Fields

    #region Constructors

    public Visitor(Color zoneColor, Logger logger)
    {
        _zoneColor = zoneColor;
        _logger = logger;
    }

    #endregion Constructors

    #region Methods

    public void TryScheduleVisit(Pixel pixel, Action<Pixel> visit)
    {
        if (pixel.Color != _zoneColor)
        {
            _logger.Append($"[maroon]Not the color to update.[/]", pixel);
            _visitRegistry.Add(pixel.ToString());
        }
        else if (_visitRegistry.Contains(pixel.ToString()))
        {
            _logger.Append($"[red3_1]Already visited.[/]", pixel);
        }
        else
        {
            _logger.Append($"[aqua]Enqueue.[/]", pixel);
            visit(pixel);
        }
    }

    public void Visit(Pixel pixel, Action<Pixel> action)
    {
        _visitRegistry.Add(pixel.ToString());
        action(pixel);
        _logger.Append($"[lime]Update color.[/]", pixel);
    }

    #endregion Methods
}