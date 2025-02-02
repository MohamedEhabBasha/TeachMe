namespace Domain;

public record UserId(Guid Value) : TypedIdValueBase(Value)
{
}
