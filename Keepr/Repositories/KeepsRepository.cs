using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
      var sql = @"
      INSERT INTO keeps
      (name, description, img, creatorId)
      VALUES(@Name, @Description, @Img, @CreatorId);
      SELECT LAST_INSERT_ID();";
      var sqlJoin = @"
      SELECT 
      k.*,
      a.*
      FROM keeps k 
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @id;";
      int id = _db.ExecuteScalar<int>(sql, keepData);
      List<Keep> keep = _db.Query<Keep, Account, Keep>(sqlJoin, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { id }, splitOn: "id").ToList();
      return keep[0];

    }

    internal Keep GetOne(int id)
    {
      var sql = @"
      SELECT 
      k.*,
      a.*
      FROM keeps k 
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @id;";
      List<Keep> keep = _db.Query<Keep, Account, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { id }, splitOn: "id").ToList();
      return keep[0];
    }

    internal int UpdateKeep(Keep keepdata)
    {
      var sql = @"
      UPDATE keeps
      SET
      name = @Name,
      description = @Description
      WHERE id = @id;";
      return _db.Execute(sql, keepdata);
    }

    internal List<Keep> GetProfileKeeps(string id)
    {
      var sql = @"
      SELECT 
      k.*,
      a.*
      FROM keeps k 
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.creatorId = @id; ";
      List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new { id }, splitOn: "id").ToList();
      return keeps;
    }

    internal List<Keep> GetAllKeeps()
    {
      var sql = @"
      SELECT 
      k.*,
      a.*
      FROM keeps k 
      JOIN accounts a ON k.creatorId = a.id;";
      List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, splitOn: "id").ToList();
      return keeps;
    }

    internal int UpdateKeepViews(Keep keepData)
    {
      var sql = @"
      UPDATE keeps
      SET 
      views = @Views
      WHERE id = @Id;";
      return _db.Execute(sql, keepData);

    }

    internal int DeleteKeep(int keepId)
    {
      var sql = @"
      DELETE FROM 
      keeps
      WHERE id = @keepId;";
      int deleted = _db.Execute(sql, new { keepId });
      return deleted;

    }
  }
}