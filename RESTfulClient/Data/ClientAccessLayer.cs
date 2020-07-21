using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace RESTfulClient.Data
{
    public class ClientAccessLayer : IClientAccessLayer
    {
        private Models.petshopdbContext _context;
        
        public ClientAccessLayer(Models.petshopdbContext context)
        {
            _context = context;
        }

        //Get all Client(s)
        public IEnumerable<Models.Client> GetAllClients()
        {
            try
            {
                return _context.Client.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        //Get the details of a particular Client    
        public Models.Client GetClient(long id)
        {
            try
            {
                Models.Client employee = _context.Client.Find(id);
                return employee;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
