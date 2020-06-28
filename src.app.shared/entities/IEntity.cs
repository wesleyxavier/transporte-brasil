using System;

namespace src.app.shared.entities
{
    public interface IEntity
    {
        int Id { get; set; }
    }

    public class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
