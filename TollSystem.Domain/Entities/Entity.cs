namespace TollSystem.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Entity other = (Entity)obj;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
