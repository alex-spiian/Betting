using UnityEngine;

public static class StringExtensions
{
    public static int ParseBetAmount(this string optionText)
    {
        var parts = optionText.Split(' ');
        if (parts.Length >= 2 && int.TryParse(parts[1], out int result))
        {
            return result;
        }

        Debug.LogWarning($"Failed to parse bet amount from option: {optionText}");
        return 0;
    }
}