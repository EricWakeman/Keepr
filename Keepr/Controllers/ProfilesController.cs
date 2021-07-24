using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[Controller]")]

  public class ProfilesController : ControllerBase
  {
    private readonly AccountService _aserv;
    private readonly KeepsService _ks;
    private readonly VaultsService _vs;

    public ProfilesController(AccountService aserv, KeepsService ks, VaultsService vs)
    {
      _aserv = aserv;
      _ks = ks;
      _vs = vs;
    }

    [HttpGet("{id}")]
    public ActionResult<Account> GetProfile(string id)
    {
      try
      {
        string email = _aserv.GetProfileEmailById(id);
        Account profile = _aserv.GetProfileByEmail(email);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetProfileKeeps(string id)
    {
      try
      {
        List<Keep> keeps = _ks.GetProfileKeeps(id);
        return Ok(keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetProfileVaults(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        if (userInfo == null)
        {
          return _vs.GetProfileVaults(id);
        }
        return _vs.GetUserVaults(id, userInfo.Id);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}