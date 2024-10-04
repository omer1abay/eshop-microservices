using MediatR;
namespace BuildingBlocks.CQRS;

public interface ICommand : ICommand<Unit> //doesn't return a response
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
