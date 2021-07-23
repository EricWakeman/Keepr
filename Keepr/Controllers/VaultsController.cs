using System.Collections.Generic;
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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;

    public VaultsController(VaultsService vs, VaultKeepsService vks)
    {
      _vs = vs;
      _vks = vks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = userInfo.Id;
        Vault vault = _vs.CreateVault(vaultData);
        return Ok(vault);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]

    public async Task<ActionResult<Vault>> GetVaultById(int id)
    {
      try
      {
        var userInfo = await HttpContext.GetUserInfoAsync<Account>();
        if (userInfo == null)
        {
          return Ok(_vs.GetOnePublic(id));
        }
        Vault vault = _vs.GetOne(id, userInfo.Id);
        return Ok(vault);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault vaultData, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = userInfo.Id;
        vaultData.Id = id;
        Vault vault = _vs.UpdateVault(vaultData);
        return Ok(vault);

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteVault(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        string result = _vs.DeleteVault(id, userInfo.Id);
        return Ok(result);


      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<VaultKeepViewModel>>> GetVaultKeeps(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        if (userInfo == null)
        {
          return (_vks.GetVKeepsPublic(id));
        }
        List<VaultKeepViewModel> vkeeps = _vks.GetVKeeps(id, userInfo.Id);
        return Ok(vkeeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}