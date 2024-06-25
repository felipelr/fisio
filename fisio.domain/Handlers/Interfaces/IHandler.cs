using fisio.domain.Commands.Interfaces;

namespace fisio.domain.Handlers.Interfaces
{
    public interface IHandler<T> where T : ICommand
    {
        Task<ICommandResult> Handle(T command, CancellationToken cancellationToke);
    }
}