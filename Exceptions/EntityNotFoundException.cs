using Core.Properties;

namespace Core.Exceptions;

public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
      : base(Resources.StrErrorEntityNotFound)
    {
    }

    public EntityNotFoundException(string entityType, string entityId)
      : base(!string.IsNullOrWhiteSpace(entityId) || !string.IsNullOrWhiteSpace(entityType) ? (string.IsNullOrWhiteSpace(entityType) ? string.Format(Resources.StrFmtErrorEntityNotFound, (object)entityId) : (string.IsNullOrWhiteSpace(entityId) ? string.Format(Resources.StrFmtErrorEntityWithTypeNotFound, (object)entityType) : string.Format(Resources.StrFmtErrorEntityWithTypeAndIdNotFound, (object)entityType, (object)entityId))) : Resources.StrErrorEntityNotFound)
    {
        EntityType = entityType;
        EntityId = entityId;
    }

    public string EntityId { get; }

    public string EntityType { get; }
}
