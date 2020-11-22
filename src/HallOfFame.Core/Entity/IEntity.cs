using System;

namespace HallOfFame.Core.Entity
{
    public interface IEntity
    {
        bool IsDelete { get; set; }
    }

    public interface IEntity<TId> : IEntity where TId : IEquatable<TId>
    {
        TId Id { get; set; }
    }
}
