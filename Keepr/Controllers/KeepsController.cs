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
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;
    private readonly VaultKeepsService _vks;

    public KeepsController(KeepsService ks, VaultKeepsService vks)
    {
      _ks = ks;
      _vks = vks;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        keepData.CreatorId = userInfo.Id;
        Keep keep = _ks.CreateKeep(keepData);
        return Ok(keep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet]
    public ActionResult<List<Keep>> GetAllKeeps()
    {
      try
      {
        List<Keep> keeps = _ks.GetAllKeeps();
        return Ok(keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Keep> GetKeepById(int id)
    {
      try
      {
        Keep keep = _ks.GetOne(id);
        return Ok(keep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/count")]
    public ActionResult<List<VaultKeep>> GetKeepCount(int id)
    {
      try
      {
        List<VaultKeep> vkeeps = _vks.GetActiveVKeeps(id);
        return Ok(vkeeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{id}/vaultkeeps/{vid}")]
    public ActionResult<VaultKeep> GetVkByKeepAndVaultId(int id, int vid)
    {
      try
      {
        VaultKeep vaultK = _vks.GetVkByKeepAndVaultId(id, vid);
        return Ok(vaultK);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut]
    public ActionResult<Keep> UpdateKeepViews([FromBody] Keep keepData)
    {
      try
      {
        Keep keep = _ks.UpdateKeepViews(keepData);
        return Ok(keep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> UpdateKeep([FromBody] Keep keepdata, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        keepdata.CreatorId = userInfo.Id;
        keepdata.Id = id;
        Keep keep = _ks.UpdateKeep(keepdata);
        return Ok(keep);

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> DeleteKeep(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        string result = _ks.DeleteKeep(id, userInfo.Id);
        return Ok(result);

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}