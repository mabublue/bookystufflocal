using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace bookystufflocal.domain.Queries
{
    public interface IPagedQuery<out TResponse> : IQuery<TResponse>
    {
        int? Page { get; set; }
        int NumberOfRecordsPerPage { get; set; }
    }

    public abstract class BasePagedQueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse> where TQuery : IPagedQuery<TResponse>
    {
        public abstract Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken);
        
        protected SqlBuilder.Template GeneratePagedTemplate(SqlBuilder builder, IPagedQuery<TResponse> query, string sql)
        {
            var offsetRow = 0;

            if (query.NumberOfRecordsPerPage <= 0)
                query.NumberOfRecordsPerPage = 100;

            if (query.Page.HasValue && query.Page > 1)
                offsetRow = (query.Page.Value - 1) * query.NumberOfRecordsPerPage;

            builder.AddParameters(new
            {
                OffsetRowNumber = offsetRow,
                FetchNumberOfRows = query.NumberOfRecordsPerPage + 1
            });
            
            return builder.AddTemplate(sql + @"
LIMIT @FetchNumberOfRows
OFFSET @OffsetRowNumber ROWS
");
        }
    }
}
