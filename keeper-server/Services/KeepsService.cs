using System;
using System.Collections.Generic;
using System.Linq;
using keeper_server.Models;
using keeper_server.Repositories;

namespace keeper_server.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
      private readonly VaultsRepository _krepo;
    public KeepsService(KeepsRepository repo, VaultsRepository krepo)
    {
      _repo = repo;
      _krepo = krepo;
    }
     public IEnumerable<Keep> GetAll()
    {
      IEnumerable<Keep> keeps = _repo.GetAll();
      return keeps;
    }

    public Keep Create(Keep newKeep)
    {
      newKeep.Id = _repo.Create(newKeep);
      return newKeep;
    }

    public Keep GetById(int id)
    {
      Keep original = _repo.Get(id);
      if (original == null) { throw new Exception("Invalid Id"); }
      return original;
    }

    internal Keep Edit(Keep editData, string userId)
    {
      Keep original = GetById(editData.Id);
      if (original.CreatorId != userId)
      {
        throw new Exception("Access Denied, You cannot edit something that is not yours");
      }
      editData.Name = editData.Name == null ? original.Name : editData.Name;
      editData.Description = editData.Description == null ? original.Description : editData.Description;
      editData.Img = editData.Img == null ? original.Img : editData.Img;
      return _repo.Edit(editData);
    }


    internal string Delete(int id, string userId)
    {
      Keep original = GetById(id);
      if (original.CreatorId != userId)
      {
        throw new Exception("Access Denied, You cannot delete something that is not yours");
      }
      _repo.Remove(id);
      return "successfully deleted";
    }

    // internal IEnumerable<Keep> GetByRestaurantId(int id)
    // {
    //   return _repo.GetByRestaurantId(id).ToList().FindAll(r => r.Published);
    // }

    internal IEnumerable<Keep> GetByProfileId(string id)
    {
      return _repo.GetByOwnerId(id);
    }

    internal IEnumerable<Keep> GetByAccountId(string id)
    {
      return _repo.GetByOwnerId(id);
    }

     public void increaseCount(int keepId)
    {
      Keep foundKeep = _repo.Get(keepId);
      foundKeep.Keeps++;
      _repo.changeKeepCount(foundKeep);
    }
     public void ViewCount(int keepId)
    {
      Keep viewKeep = _repo.Get(keepId);
      viewKeep.Views++;
      _repo.changeViewCount(viewKeep);
    }
    // public void viewsCount(int keepId)
    // {
    //   Keep viewKeep = _repo;
    // }
    public void decreaseCount(int keepId)
    {
      Keep foundKeep = _repo.Get(keepId);
      foundKeep.Keeps--;
      _repo.changeKeepCount(foundKeep);
    }
     internal IEnumerable<Keep> GetKeepsByVaultId(int vaultId)
    {
      Vault exists = _krepo.Get(vaultId);
      if (exists == null || exists.IsPrivate == true)
      {
        throw new Exception("Invalid Id");
      }
      return _repo.GetKeepsByVaultId(vaultId);
    }
 }
}