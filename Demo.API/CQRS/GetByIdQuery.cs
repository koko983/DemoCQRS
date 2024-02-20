namespace Demo.API.CQRS
{
    public class GetByIdQuery<T> : IQuery<T> where T : class
    {
        public GetByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

}
