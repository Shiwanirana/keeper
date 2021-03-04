using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeper_server.Models;

namespace keeper_server.Repositories
{
  public class KeepsRepository
  {

    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Keep> GetAll()
    {
      string sql = @"
       SELECT 
       keep.*,
       profile.* 
       FROM keeps keep 
       JOIN profiles profile ON keep.creatorId = profile.id;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, splitOn: "id");
    }

    internal Keep Get(int id)
    {
      string sql = @"
       SELECT 
       keep.*,
       profile.* 
       FROM keeps keep 
       JOIN profiles profile ON keep.creatorId = profile.id
       WHERE keep.id = @id;";

      // _db.Execute(sql, viewsCount);
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { id }, splitOn: "id").FirstOrDefault();
    }

    internal int Create(Keep KeepData)
    {
      string sql = @"
      INSERT INTO keeps
      (creatorId, name, description, img, views, shares, keeps)
      VALUES
      (@CreatorId, @Name, @Description, @Img, @Views, @Shares, @Keeps);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, KeepData);
    }

    internal Keep Edit(Keep editKeep)
    {
      string sql = @"
     UPDATE keeps
     SET
     name = @Name,
     description = @Description,
     img = @Img
     WHERE id = @Id;";
      _db.Execute(sql, editKeep);
      return editKeep;
    }

    internal void Remove(int id)
    {
      string sql = "DELETE FROM Keeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

    internal IEnumerable<Keep> GetByProfileId(int id)
    {
      string sql = @"
       SELECT 
       keep.*,
       profile.* 
       FROM keeps keep 
       JOIN profiles profile ON keep.creatorId = profile.id
       WHERE keep.creatorId = @id;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { id }, splitOn: "id");
    }

    internal IEnumerable<Keep> GetByOwnerId(string id)
    {
      string sql = @"
       SELECT 
       keep.*,
       profile.* 
       FROM keeps keep 
       JOIN profiles profile ON keep.creatorId = profile.id
       WHERE keep.creatorId = @id;";
      return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { id }, splitOn: "id");

    }

     internal void changeKeepCount(Keep foundKeep)
    {
      string sql = @"
      UPDATE keeps
      SET 
      keeps = @Keeps
      WHERE id = @Id";
      _db.Execute(sql, foundKeep);
    }
    internal void changeViewCount(Keep foundKeep)
    {
      string sql = @"
      UPDATE keeps
      SET 
      views = @Views
      WHERE id = @Id";
      _db.Execute(sql, foundKeep);
    }
     internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
      SELECT k.*,
      vk.id as vaultkeepId 
      FROM vaultkeeps vk
      JOIN keeps k ON k.id = vk.keepId
      WHERE vaultId = @vaultId";

      return _db.Query<VaultKeepsViewModel>(sql, new { vaultId });
    }
  }
}