using bookystufflocal.domain.DomainLayer.BaseModels;

namespace bookystufflocal.domain.Queries
{
    public abstract class BaseQuery<TParameters, TItem>
    {
        protected BaseQuery(ConnectionString connectionString)
        {
            ConnectionString = connectionString;
        }

        protected ConnectionString ConnectionString { get; private set; }

        public abstract TItem Execute(TParameters parameters);
    }
}
