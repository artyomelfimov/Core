using Core.Properties;

namespace Core.Exceptions;

public sealed class InfrastructureException(string message, Exception innerException = null) :
  Exception(string.Format(Resources.StrFmtErrorInfrastructureError, message), innerException)
{
}
