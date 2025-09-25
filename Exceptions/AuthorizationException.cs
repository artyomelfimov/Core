using Core.Properties;

namespace Core.Exceptions;

public sealed class AuthorizationException : Exception
{
    public AuthorizationException(string userName, string resourceName, Exception innerException = null)
      : base(!string.IsNullOrWhiteSpace(userName) || !string.IsNullOrWhiteSpace(resourceName) ? (string.IsNullOrWhiteSpace(userName) ? string.Format(Resources.StrFmtErrorUserDoesntHaveAccessToConcreteResource, (object)resourceName) : string.Format(Resources.StrFmtErrorConcreteUserDoesntHaveAccessToConcreteResource, (object)userName, (object)resourceName)) : Resources.StrErrorUserDoesntHaveAccess, innerException)
    {
        UserName = userName;
        ResourceName = resourceName;
    }

    public AuthorizationException(string resourceName, Exception innerException = null)
      : base(string.IsNullOrWhiteSpace(resourceName) ? Resources.StrErrorUserDoesntHaveAccess : string.Format(Resources.StrFmtErrorUserDoesntHaveAccessToConcreteResource, (object)resourceName), innerException)
    {
        ResourceName = resourceName;
    }

    public AuthorizationException(Exception? innerException = null)
      : base(Resources.StrErrorUserDoesntHaveAccess, innerException)
    {
    }

    public string ResourceName { get; }

    public string UserName { get; }
}
