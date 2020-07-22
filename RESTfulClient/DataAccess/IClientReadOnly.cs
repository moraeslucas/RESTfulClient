using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTfulClient.DataAccess
{
    public interface IClientReadOnly
    {
        Task<IEnumerable<Models.Client>> GetAllClients();
        Task<Models.Client> GetClient(long id);
    }
}
