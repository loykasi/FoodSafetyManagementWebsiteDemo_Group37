using System.Text;
using System.Text.RegularExpressions;

public static class StringExtension
{
    public static string ConvertNoSymbol(string s)
    {
        Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
        string temp = s.Normalize(NormalizationForm.FormD);
        return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
    }  
}