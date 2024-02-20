using MediatR;

namespace Demo.API.CQRS
{
    public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }

}
