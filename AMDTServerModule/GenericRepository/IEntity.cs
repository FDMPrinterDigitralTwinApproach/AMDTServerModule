namespace AMDTServerModule.GenericRepository
{
    public interface IEntity
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
