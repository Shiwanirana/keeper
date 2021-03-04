using System;
using System.Collections.Generic;
using System.Linq;
using keeper_server.Models;
using keeper_server.Repositories;

namespace keeper_server.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }
     public IEnumerable<Vault> GetAll()
    {
      IEnumerable<Vault> Vaults = _repo.GetAll();
      return Vaults.ToList().FindAll(r => !r.IsPrivate);
    }

    public Vault Create(Vault newVault)
    {
      newVault.Id = _repo.Create(newVault);
      return newVault;
    }

    public Vault GetById(int id, string userId)
    {
      Vault original = _repo.Get(id);
      if (original == null || original.CreatorId != userId && original.IsPrivate == true) { throw new Exception("Invalid Id"); }
      return original;
    }
    public Vault GetById(int id)
    {
      Vault original = _repo.Get(id);
      if (original == null || original.IsPrivate == true ) { throw new Exception("Invalid Id"); }
      return original;
    }

    internal Vault Edit(Vault editData, string userId)
    {
      Vault original = GetById(editData.Id);
      if (original.CreatorId != userId)
      {
        throw new Exception("Access Denied, You cannot edit something that is not yours");
      }
      editData.Name = editData.Name == null ? original.Name : editData.Name;
      editData.Description = editData.Description == null ? original.Description : editData.Description;
      editData.IsPrivate = editData.IsPrivate? original.IsPrivate : editData.IsPrivate;
      return _repo.Edit(editData);
    }


    internal string Delete(int id, string userId)
    {
      Vault original = GetById(id, userId);
      if (original.CreatorId != userId)
      {
        throw new Exception("Access Denied, You cannot delete something that is not yours");
      }
      _repo.Remove(id);
      return "successfully deleted";
    }

    // internal IEnumerable<Vault> GetByRestaurantId(int id)
    // {
    //   return _repo.GetByRestaurantId(id).ToList().FindAll(r => r.Published);
    // }

    internal IEnumerable<Vault> GetByProfileId(string id)
    {
      return _repo.GetByOwnerId(id).ToList().FindAll(r => !r.IsPrivate);
    }

    internal IEnumerable<Vault> GetByAccountId(string id)
    {
      return _repo.GetByOwnerId(id);
    }
 }
}