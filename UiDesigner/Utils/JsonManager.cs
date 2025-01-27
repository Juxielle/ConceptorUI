using System.Collections.Generic;
namespace UiDesigner.Utils;

public class JsonManager
{
    public static string FormatJson(string code, string space)
    {
        var newCode = string.Empty;
        var newSpace = $"{space}  ";
        if (IsObject(code))
        {
            newCode += space + "{\n";
            var listItem = ToObject(code);
            for (var i = 0; i < listItem.Count; i++)
            {
                newCode += newSpace + GetKey(listItem[i]) + " : " + FormatJson(GetValue(listItem[i]), newSpace);
                newCode += i == listItem.Count - 1 ? "\n" : ",\n";
            }
            newCode += space + "}\n";
        }
        else if (IsArray(code))
        {
            newCode += space + "[\n";
            var listItem = ToList(code);
            for (var i = 0; i < listItem.Count; i++)
            {
                newCode += newSpace + FormatJson(listItem[i], newSpace);
                newCode += i == listItem.Count - 1 ? "\n" : ",\n";
            }
            newCode += space + "]\n";
        }
        else newCode += space + code + "\n";
        
        return newCode;
    }

    private static List<string> ToObject(string code)
    {
        return null!;
    }

    private static List<string> ToList(string code)
    {
        var extractedCode = code.Substring(1, code.Length - 1);
        return null!;
    }

    private static bool IsObject(string code)
    {
        var count = code.Length;
        return count > 0 && code[0] == '{' && code[count - 1] == '}';
    }

    private static bool IsArray(string code)
    {
        var count = code.Length;
        return count > 0 && code[0] == '[' && code[count - 1] == ']';
    }

    private static string GetKey(string code)
    {
        var key = string.Empty;
        foreach (var c in code)
        {
            if(c == ':') break;
            key += c;
        }
        return key;
    }

    private static string GetValue(string code)
    {
        var value = string.Empty;
        var found = false;
        foreach (var c in code)
        {
            if (c == ':')
            {
                found = true;
                continue;
            }
            if(!found) continue;
            value += c;
        }
        return value;
    }

    private static string GetContent(string code) //Fonctionne Bien pour les items d'un Tableau
    {
        var content = string.Empty;
        
        var count = 1;
        var open = false;
        for (var k = 0; k < code.Length; k++)
        {
            var isSpecialSymbol = code[k] == '[' || code[k] == '{' || code[k] == ']' || code[k] == ']' || code[k] == '"';
            var inString = isSpecialSymbol && FoundInString(code, code[k], k);
            if (!inString)
            {
                open = (code[k] == '"' && !open) || (code[k] != '"' || open);
                if (code[k] == '[' || code[k] == '{' || (code[k] == '"' && open)) count++;
                else if (code[k] == ']' || code[k] == ']' || (code[k] == '"' && !open)) count--;
            }
            content += code[k];
            if(count == 0) break;
        }
        
        return content;
    }

    private static bool FoundInString(string code, char c, int i)
    {
        var open = false;
        for (var k = 0; k < code.Length; k++)
        {
            if(code[k] == c && (k == 0 || (k > 0 && code[k - 1] != '\\')))
                open = !open;
            if (k != i) continue;
            break;
        }
        return open;
    }
}