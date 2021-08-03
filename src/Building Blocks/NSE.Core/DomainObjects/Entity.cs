using System;

namespace NSE.Core.DomainObjects
{
    public abstract class Entity : IAggregateRoot
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Entity entity && Id.Equals(entity.Id) && CreatedAt == entity.CreatedAt;
        }

        public override int GetHashCode() => GetType().GetHashCode() * 907 + Id.GetHashCode();
        public override string ToString() => $"{this.GetType().Name} {Id}";

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }
    }
}
