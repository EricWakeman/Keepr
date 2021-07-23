using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[Controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vs;

    public VaultKeepsController(VaultKeepsService vs)
    {
      _vs = vs;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultKeepData.CreatorId = userInfo.Id;
        VaultKeep vaultKeep = _vs.CreateVaultKeep(vaultKeepData);
        return Ok(vaultKeep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteVK(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        string result = _vs.Delete(id, userInfo.Id);
        return Ok(result);

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}