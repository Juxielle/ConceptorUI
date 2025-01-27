namespace UiDesigner.Domain.ValueObjects;

public class Error(string code, string message)
{
    private string Code { get; } = code;
    private string Message { get; } = message;
    
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "La valeur spécifiée est null.");
    public static readonly Error NotFound = new("Error.NotFound", "Aucune donnée n'a été trouvé.");
    public static readonly Error FailedOperation = new("Error.FailedOperation", "Oups !!! Cette opération a échoué");

    public override string ToString()
    {
        return $"Error code : {Code} => {Message}";
    }
}