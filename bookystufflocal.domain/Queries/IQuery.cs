using MediatR;

namespace bookystufflocal.domain.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }

    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IRequest<TResponse>
    {
    }
}
