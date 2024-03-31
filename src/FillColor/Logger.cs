using Spectre.Console;

namespace FillColor;

public class Logger
{
    #region Fields

    private IDictionary<DateTime, List<object>> _logs = new Dictionary<DateTime, List<object>>();

    #endregion Fields

    #region Methods

    public void Append(string message, Pixel pixel) => _logs.Add(DateTime.Now, [message, pixel]);

    public void Dump()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule("Logs") { Justification = Justify.Left });
        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddRow("Time", "Pixel", "Log");
        foreach (var line in _logs)
        {
            if (line.Value.Count != 2) continue;

            var row = new List<string>()
            {
                line.Key.ToString("HH:mm:ss.fff"),
                $"[deepskyblue1]{line.Value[1].ToString() ?? ""}[/]",
                line.Value[0]?.ToString() ?? "",
            }.ToArray();
            grid.AddRow(row);
        }
        AnsiConsole.Write(grid);
    }

    #endregion Methods
}