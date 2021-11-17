using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulClient.Data
{
    public interface IClientRepository : IClientReadOnly
    {
        Task<int> AddClient(Models.Client client);
        Task<int> UpdateClient(Models.Client client);
        Task<int> DeleteClient(long id);
    }
}
