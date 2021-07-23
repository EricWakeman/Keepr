using System;
using Keepr.Models;
using Keepr.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Services
{
  public class VaultsService
  {
    internal readonly VaultsRepository _vr;

    public VaultsService(VaultsRepository vr)
    {
      _vr = vr;
    }

    internal Vault CreateVault(Vault vaultData)
    {
      Vault vault = _vr.CreateVault(vaultData);
      return vault;
    }

    internal Vault GetOne(int id, string userId)
    {
      Vault vault = _vr.GetOne(id);
      if (vault.IsPrivate == true)
      {
        if (vault.CreatorId != userId)
        {
          throw new Exception("This vault is set to private.");
        }
        return vault;
      }
      return vault;
    }

    internal Vault UpdateVault(Vault vaultData)
    {
      Vault original = _vr.GetOne(vaultData.Id);
      if (original == null)
      {
        throw new Exception("Invalid Id.");
      }
      if (vaultData.CreatorId != original.CreatorId)
      {
        throw new UnauthorizedAccessException("You do not have permission to edit this vault.");
      }
      vaultData.Name = vaultData.Name ?? original.Name;
      vaultData.Description = vaultData.Description ?? original.Description;
      int updated = _vr.UpdateVault(vaultData);
      if (updated != 1)
      {
        throw new Exception("Failed to update.");
      }
      return vaultData;
    }

    internal Vault GetOnePublic(int id)
    {
      Vault vault = _vr.GetOne(id);
      if (vault.IsPrivate == true)
      {
        throw new Exception("This vault is set to private.");
      }
      return vault;
    }

    internal string DeleteVault(int vaultId, string userId)
    {
      Vault original = _vr.GetOne(vaultId);
      if (original.CreatorId != userId)
      {
        throw new UnauthorizedAccessException("You do not have permission to delete this vault.");
      }
      int deleted = _vr.DeleteVault(vaultId);
      if (deleted != 1)
      {
        throw new Exception("Failed to delete.");
      }
      return "Successfully deleted.";
    }
  }
}