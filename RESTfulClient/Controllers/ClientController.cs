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
        Data.IClientRepository _iClient;
        Models.Client _client;

        //Dependecy Injection via constructor
        public ClientController(Data.IClientRepository iClient)
        {
            _iClient = iClient;
        }

        [HttpGet]
        /*'Task' represents an asynchronous operation. More precisely, it's
         * an object that encapsulates the state of an asynchronous operation
         */
        public async Task<ActionResult<IEnumerable<Models.Client>>> GetAll()
        {
            try
            {
                //'await' means it's going to return the control to the caller
                return Ok(await _iClient.GetAllClients());
            }
            catch (Exception)
            {
                //The exception is logged inside _client's implementation 
                
                return StatusCode(500);//'500' means Internal Server Error 
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Client>> Get(long id)
        {
            try
            {
                _client = await _iClient.GetClient(id);

                if (_client == null)
                    return NotFound();

                return _client;
            }
            catch (Exception)
            {
                //'500' means Internal Server Error 
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Models.Client client)
        {
            try
            {
                /*The default model adds errors for type conversion
                 *(like passing a character to something which is 'int')*/
                if (ModelState.IsValid)
                    return await _iClient.AddClient(client);

                /*I could put the reason inside BadRequest's parenthesis 
                 *(keep this string to a minimum) */
                return BadRequest();
            }
            catch (Exception)
            {
                //'500' means Internal Server Error 
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update([FromBody] Models.Client client)
        {
            try
            {
                if (ModelState.IsValid)
                    return await _iClient.UpdateClient(client);

                return BadRequest();
            }
            catch (Exception)
            {
                //'500' means Internal Server Error 
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                _client = await _iClient.GetClient(id);

                if (_client == null)
                    return NotFound();

                await _iClient.DeleteClient(id);
                return NoContent();
            }
            catch (Exception)
            {
                //'500' means Internal Server Error 
                return StatusCode(500);
            }
        }
    }
}
