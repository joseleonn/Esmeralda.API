using Application.Common;
using System.Net;
public static class NotificationCodes
{
    public const string Success = "SUCCESS";
    public const string NotFound = "NOT_FOUND";
    public const string BadRequest = "BAD_REQUEST";
    public const string InternalError = "INTERNAL_ERROR";
    public const string Created = "CREATED";
    public const string NoContent = "NO_CONTENT";

}

public class Response<T>
{
    public T? Content { get; private set; }
    public HttpStatusCode StatusCode { get; private set; } = HttpStatusCode.OK;
    public List<Notify> Notifications { get; }
    public bool IsValid => !Notifications.Any();
    public Dictionary<string, string> Headers { get; private set; }

    public Response()
    {
        Notifications = new List<Notify>();
        Headers = new Dictionary<string, string>();
    }

    // Métodos de fábrica estáticos para casos comunes
    public static Response<T> Success(T content = default!)
    {
        var response = new Response<T>();

        if (!EqualityComparer<T>.Default.Equals(content, default!))
        {
            response.SetContent(content);
        }
        response.SetStatusCode(HttpStatusCode.OK);
        response.AddNotification(NotificationCodes.Success, "Resource", "Solicitud Realizada Con Exito");
        return response;

    }

    public static Response<T> Created(T content = default!) 
    {
        var response = new Response<T>();

        if (!EqualityComparer<T>.Default.Equals(content, default!))
        {
            response.SetContent(content);
        }

        response.SetStatusCode(HttpStatusCode.Created);
        response.AddNotification(NotificationCodes.Created, "Resource", "Elemento Creado");
        return response;
    }


    public static Response<T> NotFound(string message = "Elemento no encontrado")
    {
        var response = new Response<T>();
        response.SetStatusCode(HttpStatusCode.NotFound);
        response.AddNotification(NotificationCodes.NotFound, "Resource", message);
        return response;
    }

    public static Response<T> BadRequest(string message)
    {
        var response = new Response<T>();
        response.SetStatusCode(HttpStatusCode.BadRequest);
        response.AddNotification(NotificationCodes.BadRequest, "Validation", message);
        return response;
    }

    public static Response<T> InternalError(string message = "Un error inesperado a ocurrido")
    {
        var response = new Response<T>();
        response.SetStatusCode(HttpStatusCode.InternalServerError);
        response.AddNotification(NotificationCodes.InternalError, "Server", message);
        return response;
    }

    // Métodos para modificar el estado
    public void SetStatusCode(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public void SetContent(T content)
    {
        Content = content;
    }

    public void AddHeader(string key, string value)
    {
        Headers[key] = value;
    }

    public void AddNotifications(IEnumerable<Notify> notifications)
    {
        Notifications.AddRange(notifications);
    }

    public void AddNotification(Notify notification)
    {
        Notifications.Add(notification);
    }

    public void AddNotification(string code, string property, string message)
    {
        Notifications.Add(new Notify
        {
            Code = code,
            Message = message,
            Property = property
        });
    }

    // Método para combinar notificaciones de múltiples responses
    public void AddNotificationsFromResponse<TOther>(Response<TOther> response)
    {
        AddNotifications(response.Notifications);
    }
}