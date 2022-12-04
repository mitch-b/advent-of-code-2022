using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode.Solutions.Utils;

public static class RegexUtils
{
    public static IEnumerable<T> ExtractFromString<T>(this string str, string regexPattern, bool ignoreFirstCaptureGroup = true)
    {
        var matches = Regex.Match(str, regexPattern);
        var extractedData = new List<T>();
        var iterator = 0;
        foreach (var match in matches.Groups)
        {
            if (++iterator <= 1 && ignoreFirstCaptureGroup) continue;
            extractedData.Add(match.ToString().ConvertTo<T>());
        }
        return extractedData;
    }
}
