using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
      var sql = @"
      INSERT INTO vaults
      (creatorId, name, description, isPrivate)
      VALUES(@CreatorId, @Name, @Description, @isPrivate);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      var sqlJoin = @"
      SELECT
      v.*,
      a.*
      FROM vaults v 
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.id = @id;";
      List<Vault> vault = _db.Query<Vault, Account, Vault>(sqlJoin, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { id }, splitOn: "id").ToList();
      return vault[0];

    }

    internal Vault GetOne(int id)
    {
      var sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v 
      JOIN accounts a ON v.creatorId = a.id
      WHERE v.id = @id;";
      List<Vault> vault = _db.Query<Vault, Account, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new { id }, splitOn: "id").ToList();
      return vault[0];
    }

    internal int UpdateVault(Vault vaultData)
    {
      var sql = @"
      UPDATE vaults 
      SET
      name = @Name,
      description = @Description
      WHERE id = @id;";
      return _db.Execute(sql, vaultData);
    }

    internal List<Vault> GetProfileVaults(string id)
    {
      var sql = @"
      SELECT * FROM
      vaults 
      WHERE creatorId = @id AND isPrivate = false;";
      List<Vault> vaults = _db.Query<Vault>(sql, new { id }).ToList();
      return vaults;
    }

    internal List<Vault> GetUserVaults(string id)
    {
      var sql = @"
      SELECT * FROM
      vaults 
      WHERE creatorId = @id;";
      List<Vault> vaults = _db.Query<Vault>(sql, new { id }).ToList();
      return vaults;
    }

    internal int DeleteVault(int vaultId)
    {
      var sql = @"
      DELETE FROM
      vaults
      WHERE id = @vaultId;";
      return _db.Execute(sql, new { vaultId });
    }
  }
}