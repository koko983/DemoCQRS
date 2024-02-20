using MediatR;

namespace Demo.API.CQRS
{
    public interface IQuery<TQueryResult> : IRequest<TQueryResult>
    {
    }

}
