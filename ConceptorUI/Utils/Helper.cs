using System.Globalization;

namespace ConceptorUI.Utils;

public class Helper
{

    public static double ConvertToDouble(string value)
    {
        double.TryParse(value.Replace(',', '.'), NumberStyles.Any, new CultureInfo("en-US"), out var newValue);
        return newValue;
    }
    
}