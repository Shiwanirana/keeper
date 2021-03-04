using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keeper_server.Models;
using keeper_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keeper_server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _service;
    private readonly VaultsService _vs;
    private readonly KeepsService _ks;

    public ProfilesController(ProfilesService service, VaultsService vs, KeepsService ks)
    {
      _service = service;
      _vs = vs;
      _ks = ks;
    }

   [HttpGet]
    public async Task<ActionResult<Profile>> Get()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_service.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        Profile profile = _service.GetProfileById(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<IEnumerable<Vault>>> GetVaultsByProfileId(string id)
    {
       Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
      try
      {
        IEnumerable<Vault> vaults = _vs.GetByProfileId(id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

     [HttpGet("{id}/keeps")]
    public ActionResult<IEnumerable<Keep>> GetKeepsByProfileId(string id)
    {
      try
      {
        IEnumerable<Keep> keeps = _ks.GetByProfileId(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


  }
}