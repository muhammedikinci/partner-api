using System;

namespace Domain.Interfaces
{
    public interface IEntity
    {
    }

    public interface IEntity<out T> : IEntity where T : IEquatable<T>
    {
        T Id { get; }
        DateTime CreatedAt { get; set; }
    }
}