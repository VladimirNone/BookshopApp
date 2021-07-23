namespace BookshopApp.Models
{
    public interface IEntity<TId>
    {
        public TId Id { get; set; }
    }

    public interface IEntity : IEntity<int>
    {

    }
}