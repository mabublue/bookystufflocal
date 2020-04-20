namespace bookystufflocal.domain.DomainLayer.BaseModels
{
    public class ConnectionString
    {
        public ConnectionString(string db)
        {
            Db = db;
        }

        public string Db { get; private set; }
    }
}
