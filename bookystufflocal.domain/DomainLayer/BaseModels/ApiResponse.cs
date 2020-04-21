namespace bookystufflocal.domain.DomainLayer.BaseModels
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public int StatusErrorCode { get; set; }
        public string Exception { get; set; }
        public string InnerException { get; set; }
    }
}
