using System.Collections.Generic;

namespace RESTfulClient.Data
{
    public interface IClientAccessLayer
    {
        IEnumerable<Models.Client> GetAllClients();
        Models.Client GetClient(long id);
    }
}
