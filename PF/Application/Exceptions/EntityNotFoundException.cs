namespace PF.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Guid entityId, string entityType)
        : base($"Could not find {entityType} of ID {entityId}")
        {
        }
    }
}
