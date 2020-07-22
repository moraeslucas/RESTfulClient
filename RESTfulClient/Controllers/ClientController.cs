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
    /* The tokens [controller] and [action] are replaced with the values of the
     * controller name (in this case Client) and action name, which is 'GetAll' and so on
     */
    [Route("api/[controller]/[action]")]
    public class ClientController : ControllerBase
    {
        DataAccess.IClientFullAccess _iClient;
        Models.Client _client;

        //Dependecy Injection via constructor
        public ClientController(DataAccess.IClientFullAccess iClient)
        {
            _iClient = iClient;
        }

        [HttpGet]
        /*'Task' represents an asynchronous operation. More precisely, it's
         * an object that encapsulates the state of an asynchronous operation
         */
        public async Task<IEnumerable<Models.Client>> GetAll()
        {
            //'await' means it's going to return the control to the caller
            return await _iClient.GetAllClients();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Client>> Get(long id)
        {
            _client = await _iClient.GetClient(id);

            if (_client == null)
                return NotFound();

            return _client;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Models.Client client)
        {
            if (ModelState.IsValid)
                return await _iClient.AddClient(client);

            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update([FromBody] Models.Client client)
        {
            if (ModelState.IsValid)
                return await _iClient.UpdateClient(client);

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            _client = await _iClient.GetClient(id);

            if (_client == null)
                return NotFound();

            await _iClient.DeleteClient(id);
            return NoContent();
        }
    }
}
