using System.Text.Json;

namespace AdmissionCommittee.Api.Middleware;

/// <summary>
/// Represents the details of an error, including the status code and a message.
/// </summary>
public class ErrorDetails
{
    /// <summary>
    /// Gets or sets the HTTP status code of the error.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the message associated with the error.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Converts the error details to a JSON string.
    /// </summary>
    /// <returns>A JSON string representing the error details.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
