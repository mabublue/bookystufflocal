using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using bookystufflocal.domain.DomainLayer.BaseModels;
using bookystufflocal.domain.DomainLayer.Library;
using Dapper;
using Npgsql;

namespace bookystufflocal.domain.Queries.Library
{
    public class AuthorListPagedQuery : IPagedQuery<IEnumerable<Author>>
    {
        public int? Page { get; set; }
        public int NumberOfRecordsPerPage { get; set; }
    }

    public class AuthorListPagedQueryHandler : BasePagedQueryHandler<AuthorListPagedQuery, IEnumerable<Author>>
    {
        private readonly ConnectionString _connectionString;

        public AuthorListPagedQueryHandler(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public override async Task<IEnumerable<Author>> Handle(AuthorListPagedQuery query, CancellationToken cancellationToken)
        {
            await using var conn = new NpgsqlConnection(_connectionString.Db);

            conn.Open();

            var builder = new SqlBuilder();
            var selector = GeneratePagedTemplate(builder, query, @"
SELECT * FROM public.""Authors""
ORDER BY ""Id"" ASC
/**where**/
");

            return await conn.QueryAsync<Author>(selector.RawSql, selector.Parameters);
        }
    }
}
