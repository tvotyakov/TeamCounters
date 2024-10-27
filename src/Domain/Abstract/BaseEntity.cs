namespace TeamCounters.Domain.Abstract;
public abstract class BaseEntity<TKey> where TKey : struct
{
    public TKey Id { get; set; }

    protected BaseEntity() { }

    protected BaseEntity(TKey id) { Id = id; }

    protected bool Equals(BaseEntity<TKey>? other) => other is not null && Id.Equals(other.Id);

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((BaseEntity<TKey>)obj);
    }

    // ReSharper disable once NonReadonlyMemberInGetHashCode
    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(BaseEntity<TKey>? left, BaseEntity<TKey>? right) =>
        Equals(left, right);

    public static bool operator !=(BaseEntity<TKey>? left, BaseEntity<TKey>? right) =>
        !Equals(left, right);
}
