namespace OnlineCourse.Common.Extensions;

public static class HelperExtension
{
    public static string AsString(this object? value)
    {
        if (value is null) return string.Empty;

        return value is string str ? str : value.ToString()!;
    }
}
