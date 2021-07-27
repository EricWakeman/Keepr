using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkr;
    private readonly KeepsRepository _kr;
    private readonly VaultsRepository _vr;

    public VaultKeepsService(VaultKeepsRepository vkr, KeepsRepository kr, VaultsRepository vr)
    {
      _vkr = vkr;
      _kr = kr;
      _vr = vr;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {

      Vault vault = _vr.GetOne(vaultKeepData.VaultId);
      if (vault.CreatorId != vaultKeepData.CreatorId)
      {
        throw new Exception("You cannot add to this vault.");
      }
      VaultKeep newVaultKeep = _vkr.CreateVaultKeep(vaultKeepData);
      return newVaultKeep;

    }

    internal List<VaultKeep> GetActiveVKeeps(int id)
    {
      List<VaultKeep> vkeeps = _vkr.GetActiveVKeeps(id);
      return vkeeps;
    }

    internal List<VaultKeepViewModel> GetVKeeps(int id, string userId)
    {
      Vault vault = _vr.GetOne(id);
      if (vault.IsPrivate == true)
      {
        if (vault.CreatorId != userId)
        {
          throw new Exception("This vault is set to private.");
        }
      }
      List<VaultKeepViewModel> vkeeps = _vkr.GetVKeepsByVId(id);
      return vkeeps;
    }

    internal string Delete(int vkId, string userId)
    {
      VaultKeep vk = _vkr.GetOne(vkId);
      if (vk.CreatorId != userId)
      {
        throw new Exception("You do not have permission to remove this keep.");
      }
      int deleted = _vkr.Delete(vkId);
      if (deleted != 1)
      {
        throw new Exception("Failed to delete.");
      }
      return "Successfully deleted.";
    }

    internal ActionResult<List<VaultKeepViewModel>> GetVKeepsPublic(int id)
    {
      Vault vault = _vr.GetOne(id);
      if (vault.IsPrivate == true)
      {
        throw new Exception("This vault is set to private.");
      }

      List<VaultKeepViewModel> vkeeps = _vkr.GetVKeepsByVId(id);
      return vkeeps;
    }
  }
}