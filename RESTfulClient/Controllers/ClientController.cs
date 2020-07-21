using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RESTfulClient.Controllers
{
    /// <summary>
    /// The controller accepts input and converts it to commands for the model or view
    /// </summary>

    //TODO: This application can be secured by uncommenting this "Authorize"
    //[Authorize]
    [ApiController]
    public class ClientController : ControllerBase
    {
        Data.IClientAccessLayer _client;

        //Dependecy Injection via constructor
        public ClientController(Data.IClientAccessLayer client)
        {
            _client = client;
        }

        [HttpGet]
        [Route("api/Client/AllClients")]
        public IEnumerable<Models.Client> AllClients()
        {
            return _client.GetAllClients();
        }

        [HttpGet]
        [Route("api/Client/GetClient/{id}")]
        public Models.Client GetClient(int id)
        {
            return _client.GetClient(id);
        }
    }
}
