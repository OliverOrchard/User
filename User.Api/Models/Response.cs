namespace User.Api.Models
{
    public class Response<T>
    {
        public T Data { get; set; }

        public Response()
        {
        }

        public Response(T data)
        {
            this.Data = data;
        }
    }
}
