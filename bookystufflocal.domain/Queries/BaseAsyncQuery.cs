using System.Threading.Tasks;
using bookystufflocal.domain.DomainLayer.BaseModels;

namespace bookystufflocal.domain.Queries
{
    public abstract class BaseAsyncQuery<TParameters, TItem>
    {
        protected BaseAsyncQuery(ConnectionString connectionString)
        {
            ConnectionString = connectionString;
        }

        protected ConnectionString ConnectionString { get; private set; }

        public abstract Task<TItem> Execute(TParameters parameters);
    }
}
