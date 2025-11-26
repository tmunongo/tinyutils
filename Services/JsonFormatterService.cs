using System.Text.Json;

namespace tinyutils.Services;

public class JsonFormatterService
{
    public async Task<JsonFormatterResult> FormatJsonAsync(string input)
    {
        var result = new JsonFormatterResult();

        if (string.IsNullOrWhiteSpace(input))
        {
            result.IsValid = false;
            result.Message = "Please enter some JSON to format.";
            return result;
        }

        try
        {
            var jsonDocument = JsonDocument.Parse(input);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            result.FormattedJson = JsonSerializer.Serialize(
                jsonDocument,
                options
            );

            result.IsValid = true;
            result.Message = "JSON is valid and formatted!";
        }
        catch (JsonException ex)
        {
            result.IsValid = false;
            result.ErrorMessage = ex.Message;
            result.Message = "Invalid JSON!";
        }

        return await Task.FromResult(result);
    }
}

public class JsonFormatterResult
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? FormattedJson { get; set; }
    public string? ErrorMessage { get; set; }
}