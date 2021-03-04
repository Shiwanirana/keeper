import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultsService {
  async createVault(data) {
    const res = await api.post('api/vaults', data)
    logger.log(res.data)
    AppState.vaults.push(res.data)
  }

  async getVaultById(id) {
    const res = await api.get('api/vaults/' + id)
    logger.log(res.data)
    AppState.activeVault = res.data
  }

  async getKeepsByVaultId(id) {
    const res = await api.get('api/vaults/' + id + '/keeps')
    logger.log(res.data)
    AppState.vaultKeeps = res.data
  }

  async addToVault(data) {
    const res = await api.post('api/vaultkeeps', data)
    this.getKeepsByVaultId(data.VaultId)
    logger.log(res.data)
  }

  async deleteVault(id) {
    await api.delete('api/vaults/' + id)
    const index = AppState.vaults.findIndex(v => v.id === id)
    logger.log('delete')
    AppState.vaults.splice(index, 1)
  }

  async deleteVaultKeep(id, vaultId) {
    await api.delete('api/vaultkeeps/' + id)
    this.getKeepsByVaultId(vaultId)
    // const index = AppState.vaultKeeps.findIndex(v => v.id === id)
    logger.log('delete')
    AppState.vaultKeeps = AppState.vaultKeeps.filter(vk => vk.id !== id)
  }
}
export const vaultsService = new VaultsService()
