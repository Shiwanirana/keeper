using System.Threading.Tasks;
using keeper_server.Models;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using keeper_server.Services;

namespace keeper_server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _service;
    private readonly KeepsService _ks;

    public VaultKeepsController(VaultKeepsService service, KeepsService ks)
    {
      _service = service;
      _ks = ks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVS)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVS.CreatorId = userInfo.Id;
        // _ks.viewsCount(newVS.KeepId);
        _ks.increaseCount(newVS.KeepId);
        VaultKeep created = _service.Create(newVS);
        return Ok(created);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    //  Delete will go in here, get by Id will got somewhere else
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        // _ks.decreaseCount(id);
        if (userInfo == null)
        {
          throw new System.Exception("Invalid User");
        }
        return Ok(_service.Delete(id, userInfo.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> GetById(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_service.GetKeepsByVaultId(id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }

}