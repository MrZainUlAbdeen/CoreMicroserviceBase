using EntityFrameworkCore.Triggers;

namespace BaseTriggers
{
    public abstract class Trackable
    {
        static Trackable()
        {
            Triggers<Trackable>.Inserting += entry =>
            {
                entry.Entity.CreatedAt = DateTime.Now;
                entry.Entity.UpdatedAt = DateTime.Now;
            };
            Triggers<Trackable>.Updating += entry =>
            {
                entry.Entity.UpdatedAt = DateTime.Now;
            };
        }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}