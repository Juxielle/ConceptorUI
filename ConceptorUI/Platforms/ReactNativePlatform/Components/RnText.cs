using System.Linq;
using ConceptorUI.Models;
using ConceptorUI.Platforms.ReactNativePlatform.Operations;
using ConceptorUI.Utils;

namespace ConceptorUI.Platforms.ReactNativePlatform.Components;

public class RnText : RnComponent
{
    private string Text { get; set; }
    private string FontSize { get; set; }
    private string FontFamily { get; set; }
    private string FontWeight { get; set; }
    private string FontStyle { get; set; }
    private string TextAlignment { get; set; }
    private string TextDecoration { get; set; }
    private string TextWrapping { get; set; }
    private string TextTrimming { get; set; }
    private string LineSpacing { get; set; }
    private string Foreground { get; set; }
    private bool IsTextSingle { get; set; }

    public RnText(CompSerializer compSerializer, int index, bool isTextSingle = false)
    {
        IsTextSingle = isTextSingle;
        RnStyle.Name = $"text-style{index}";
        Package = "react-native";
        
        if (isTextSingle)
        {
            Text = compSerializer.GetGroup(GroupNames.Text).GetValue(PropertyNames.Text);

            Foreground = compSerializer.GetGroup(GroupNames.Text).GetValue(PropertyNames.Color);
            RnStyle.KeyValues.Add(new PlatformProperty { Key = "color", Value = Foreground });

            var fontSize = compSerializer.GetGroup(GroupNames.Text).GetValue(PropertyNames.FontSize);
            FontSize = Helper.FormatString(fontSize);
            RnStyle.KeyValues.Add(new PlatformProperty { Key = "fontSize", Value = FontSize });

            var fontWeight = compSerializer.GetGroup(GroupNames.Text).GetValue(PropertyNames.FontWeight);
            if (fontWeight == "1")
            {
                FontWeight = "bold";
                RnStyle.KeyValues.Add(new PlatformProperty { Key = "fontWeight", Value = FontWeight });
            }

            var fontStyle = compSerializer.GetGroup(GroupNames.Text).GetValue(PropertyNames.FontStyle);
            if (fontStyle == "1")
            {
                FontStyle = "bold";
                RnStyle.KeyValues.Add(new PlatformProperty { Key = "fontStyle", Value = FontStyle });
            }

            var textLeft = compSerializer.GetGroup(GroupNames.Text).GetValue(PropertyNames.AlignLeft);
            var textCenter = compSerializer.GetGroup(GroupNames.Text).GetValue(PropertyNames.AlignCenter);
            var textRight = compSerializer.GetGroup(GroupNames.Text).GetValue(PropertyNames.AlignRight);
            var textJustify = compSerializer.GetGroup(GroupNames.Text).GetValue(PropertyNames.AlignJustify);

            var textAlign = textLeft == "1" ? "left" :
                textCenter == "1" ? "center" :
                textRight == "1" ? "right" :
                textJustify == "1" ? "justify" : "left";

            if (textAlign != "left")
            {
                TextAlignment = textAlign;
                RnStyle.KeyValues.Add(new PlatformProperty { Key = "textAlign", Value = TextAlignment });
            }

            var textUnderline = compSerializer.GetGroup(GroupNames.Text).GetValue(PropertyNames.TextUnderline);
            var textOverline = compSerializer.GetGroup(GroupNames.Text).GetValue(PropertyNames.TextOverline);
            var textThrough = compSerializer.GetGroup(GroupNames.Text).GetValue(PropertyNames.TextThrough);

            var textDecoration = textUnderline == "1" ? "underline" :
                textThrough == "1" ? "line-through" :
                textOverline == "1" ? "underline line-through" : "none";

            if (textDecoration != "none")
            {
                TextDecoration = textDecoration;
                RnStyle.KeyValues.Add(new PlatformProperty { Key = "textDecorationLine", Value = TextDecoration });
            }
        }
        else
        {
            var lineSpacing = compSerializer.GetGroup(GroupNames.Text).GetValue(PropertyNames.LineSpacing);
            LineSpacing = Helper.FormatString(lineSpacing);
            RnStyle.KeyValues.Add(new PlatformProperty { Key = "lineHeight", Value = LineSpacing });
            
            this.SetMargin(compSerializer);
        }
    }

    protected override string ConvertToString(string space)
    {
        var styles = string.Join(",", Styles);
        var content = IsTextSingle
            ? $"{space}{Platform.WhiteSpace}{Text}"
            : string.Join("\n", Children.Select(c => c.ToString($"{space}{Platform.WhiteSpace}")));

        return space + @$"<Text style=""[{styles}]"">" + "\n" +
               $"{content}\n" +
               space + "</Text>";
    }
}