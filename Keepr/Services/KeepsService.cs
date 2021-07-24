using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{

  public class KeepsService
  {
    private readonly KeepsRepository _kr;

    public KeepsService(KeepsRepository kr)
    {
      _kr = kr;
    }

    internal Keep CreateKeep(Keep keepData)
    {
      Keep newKeep = _kr.CreateKeep(keepData);
      return newKeep;
    }

    internal List<Keep> GetAllKeeps()
    {
      List<Keep> keeps = _kr.GetAllKeeps();
      return keeps;
    }

    internal Keep GetOne(int id)
    {
      Keep keep = _kr.GetOne(id);
      return keep;
    }

    internal Keep UpdateKeep(Keep keepdata)
    {
      Keep original = GetOne(keepdata.Id);
      if (original == null)
      {
        throw new Exception("Invalid Id");
      }
      if (keepdata.CreatorId != original.CreatorId)
      {
        throw new UnauthorizedAccessException("You do not have permission to edit this Keep.");
      }
      keepdata.Views = original.Views;
      keepdata.Keeps = original.Keeps;
      keepdata.Name = keepdata.Name ?? original.Name;
      keepdata.Description = keepdata.Description ?? original.Description;
      int updated = _kr.UpdateKeep(keepdata);
      if (updated != 1)
      {
        throw new Exception("Failed to update.");
      }
      return keepdata;
    }

    internal List<Keep> GetProfileKeeps(string id)
    {
      List<Keep> keeps = _kr.GetProfileKeeps(id);
      return keeps;
    }

    internal string DeleteKeep(int keepId, string userId)
    {
      Keep keep = GetOne(keepId);
      if (keep.CreatorId != userId)
      {
        throw new UnauthorizedAccessException("You do not have permission to delete this keep.");
      }
      int result = _kr.DeleteKeep(keepId);
      if (result != 1)
      {
        throw new Exception("Failed to delete.");
      }
      return "Successfully deleted.";
    }
  }
}