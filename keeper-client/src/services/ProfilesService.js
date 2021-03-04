import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class ProfilesService {
  async getProfileById(id) {
    const res = await api.get('api/profiles/' + id)
    // logger.log(res.data)
    AppState.activeProfile = res.data
  }

  async getVaultsByProfileId(id) {
    const res = await api.get('api/profiles/' + id + '/vaults')
    logger.log(res.data)
    AppState.profileVaults = res.data
  }

  async getKeepsByProfileId(id) {
    const res = await api.get('api/profiles/' + id + '/keeps')
    logger.log(res.data)
    AppState.profileKeeps = res.data
  }
}
export const profilesService = new ProfilesService()
