namespace Core.Exceptions;

public sealed class BusinessException(string message, Exception innerException = null) : Exception(message, innerException)
{
}
