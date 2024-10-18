namespace Data.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public int? UpdatedById { get; set; }

        public void SetCreatedTime()
        {
            CreatedTime = DateTime.UtcNow;
            UpdatedTime = DateTime.UtcNow;
        }

        public void SetUpdatedTime()
        {
            UpdatedTime = DateTime.UtcNow;
        }
    }
}
