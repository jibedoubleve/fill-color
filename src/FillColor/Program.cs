using FillColor;
using Spectre.Console;

AnsiConsole.WriteLine();
var rule = new Rule("Select the starting point") { Justification = Justify.Left };
AnsiConsole.Write(rule);

var prompt = GetPrompt("Specify [green]X[/]:");
var x = AnsiConsole.Prompt(prompt);

prompt = GetPrompt("Specify [green]Y[/]:");
var y = AnsiConsole.Prompt(prompt);

AnsiConsole.WriteLine();
var start = new Rule("Display the image") { Justification = Justify.Left };
AnsiConsole.Write(start);
ChangeColor.Execute(x, y);

return;

TextPrompt<int> GetPrompt(string question)
{
    return new TextPrompt<int>(question)
           .PromptStyle("green")
           .ValidationErrorMessage("[red]Invalid coordinate[/]")
           .Validate(c =>
           {
               return c switch
               {
                   < 0 => ValidationResult.Error("[red]Coordinate should be above 0[/]"),
                   >= 5 => ValidationResult.Error("[red]Coordinate should ba below 5[/]"),
                   _ => ValidationResult.Success(),
               };
           });
}