using MediatR;

namespace Demo.API.CQRS
{
    public interface ICommand<TCommandResut> : IRequest<TCommandResut>
    {
    }

}
