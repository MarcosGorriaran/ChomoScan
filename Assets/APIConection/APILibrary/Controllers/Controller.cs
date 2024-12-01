

using System.Net.Http;
using System.Text.Json;

namespace APILibraryDaltonismo.Controllers
{
    public abstract class Controller
    {
        protected HttpClient client { get; private set;}
        protected JsonSerializerOptions serializerOptions  = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        public Controller(HttpClient client)
        {
            this.client = client;
        }

    }
}
