using System;
using System.Collections.Generic;
using keeper_server.Models;
using keeper_server.Repositories;

namespace keeper_server.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;

    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }

    internal VaultKeep Create(VaultKeep newVK)
    {
      newVK.Id = _repo.Create(newVK);
      return newVK;
    }

    public IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      return _repo.GetKeepsByVaultId(vaultId);
    }

    internal string Delete(int id, string userId)
    {
      VaultKeep original = GetVaultKeepById(id);
      if (original == null || original.CreatorId != userId)
      {
        throw new Exception("Invalid Request/ You are not the authorized person!!");
      }
      _repo.Delete(id);
        return "Successfully Deleted!";
    }

    internal VaultKeep GetVaultKeepById(int id)
    {
      VaultKeep original = _repo.Get(id);
      if (original == null)
      {

        throw new Exception("Invalid Request");
      }
        return original;
    }
  }
}