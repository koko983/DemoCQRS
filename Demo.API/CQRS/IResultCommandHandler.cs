namespace Demo.API.CQRS
{
    public interface IResultCommandHandler<TCommand> : ICommandHandler<TCommand, CommandResult>
    where TCommand : IResultCommand
    {
    }

}
