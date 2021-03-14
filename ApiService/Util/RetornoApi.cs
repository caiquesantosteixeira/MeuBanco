namespace ApiService.Util
{
    public class RetornoApi<T>
    {
        public bool Sucesso { get; set; }
        public int StatusCode { get; set; }
        public string Mensagem { get; set; }
        public T Data { get; set; }
    }
}
