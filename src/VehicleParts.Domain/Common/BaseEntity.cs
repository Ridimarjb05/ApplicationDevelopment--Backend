namespace VehicleParts.Domain.Common;


/// Base class for all domain entities providing a Guid primary key.

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}
