using Domain.Exceptions;

namespace Domain;

public record TypedIdValueBase
{
    public Guid Value { get; }
    protected TypedIdValueBase(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
        {
            throw new DomainException("Id cannot be empty.");
        }

        Value = value;
    }
}
