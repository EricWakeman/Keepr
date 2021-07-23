using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
      var sql = @"
      INSERT INTO 
      vaultkeeps
      (creatorId, vaultId, keepId)
      VALUES
      (@CreatorId, @VaultId, @KeepId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
      vaultKeepData.Id = id;
      return vaultKeepData;
    }

    internal List<VaultKeepViewModel> GetVKeepsByVId(int id)
    {
      var sql = @"
      SELECT
      vk.*,
      a.*, 
      k.*
      FROM vaultkeeps vk
      JOIN accounts a ON vk.creatorId = a.id
      JOIN keeps k ON vk.keepId = k.id
      WHERE vk.vaultId =@id;
      ";
      List<VaultKeep> vkeeps = _db.Query<VaultKeep, Account, VaultKeepViewModel, VaultKeep>(sql, (vk, a, k) =>
            {
              vk.Keep = k;
              vk.Keep.Creator = a;
              return vk;
            }, new { id }, splitOn: "id"
            ).ToList();

      List<VaultKeepViewModel> keeps = new List<VaultKeepViewModel>();
      vkeeps.ForEach(v =>
      {
        v.Keep.VaultKeepId = v.Id;
        keeps.Add(v.Keep);
      }
      );
      return keeps;

    }

    internal int Delete(int vkId)
    {
      var sql = @"
      DELETE FROM 
      vaultkeeps 
      WHERE id = @vkId;";
      return _db.Execute(sql, new { vkId });
    }

    internal VaultKeep GetOne(int vkId)
    {
      var sql = @"
      SELECT * FROM
      vaultkeeps 
      WHERE id = @vkId;";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { vkId });
    }
  }
}