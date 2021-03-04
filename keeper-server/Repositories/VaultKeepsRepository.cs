using System;
using System.Collections.Generic;
using System.Data;
using keeper_server.Models;
using Dapper;

namespace keeper_server.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int Create(VaultKeep newVK)
    {
      string sql = @"
      INSERT INTO vaultKeeps
      (vaultId, keepId, creatorId)
      VALUES
      (@VaultId, @KeepId, @CreatorId);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVK);
    }

    internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      string sql = @"
      SELECT k.*,
      vk.id as VaultKeepId,
      p.*
      FROM vaultkeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN profiles p ON p.id = vk.creatorId
      WHERE vaultId = @VaultId;
      ";
      return _db.Query<VaultKeepsViewModel, Profile, VaultKeepsViewModel>(sql, (keep, profile) => { keep.Creator = profile; return keep; }, new { vaultId }, splitOn: "id");
    }

    internal void Delete(int id)
    {
      string sql = "DELETE FROM vaultKeeps WHERE id = @Id";
       _db.Execute(sql, new { id });
      
    }

    internal VaultKeep Get(int id)
    {
      string sql = "SELECT * FROM vaultKeeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
  }
}