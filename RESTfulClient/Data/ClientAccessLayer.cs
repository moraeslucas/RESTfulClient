using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RESTfulClient.Data
{
    public class ClientAccessLayer : IClientFullAccess
    {
        private Models.petshopdbContext _context;
        private readonly ILogger<ClientAccessLayer> _logger;

        public ClientAccessLayer(Models.petshopdbContext context, ILogger<ClientAccessLayer> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Get all Client(s)
        public async Task<IEnumerable<Models.Client>> GetAllClients()
        {
            try
            {
                return await _context.Client.ToListAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }
        
        //Get the details of a particular Client    
        public async Task<Models.Client> GetClient(long id)
        {
            try
            {   
                return await _context.Client.FindAsync(id);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        //Add a new Client record     
        public async Task<int> AddClient(Models.Client client)
        {
            try
            {
                _context.Client.Add(client);
                return await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        //Updates the records of a particluar client    
        public async Task<int> UpdateClient(Models.Client client)
        {
            try
            {
                _context.Entry(client).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        //Deletes the record of a particular client    
        public async Task<int> DeleteClient(long id)
        {
            try
            {
                Task<Models.Client> client;

                //TODO: Use Dapper instead. This way, the 'GetClient' won't be needed
                client = GetClient(id);
                
                _context.Client.Remove(client.Result);
                return await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }
    }
}
