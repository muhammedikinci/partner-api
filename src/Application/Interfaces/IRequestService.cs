using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRequestService
    {
        Task Post<T>(string url, T data);
    }
}