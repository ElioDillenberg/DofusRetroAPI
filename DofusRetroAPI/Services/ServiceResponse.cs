using System.Net;

namespace DofusRetroAPI.Services;

public class ServiceResponse<T>
{
    public T? Data { get; set; }

    public string Message { get; set; } = string.Empty;
    
    public HttpStatusCode? StatusCode { get; set; } = HttpStatusCode.OK; 
}