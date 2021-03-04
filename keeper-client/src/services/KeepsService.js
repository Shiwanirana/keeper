import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    // logger.log(res.data)
    AppState.keeps = res.data
  }

  async getKeepById(id) {
    const res = await api.get('api/keeps/' + id)
    // logger.log(res.data)
    AppState.activeKeep = res.data
  }

  async createKeep(data) {
    const res = await api.post('api/keeps', data)
    logger.log(res.data)
    AppState.keeps.push(res.data)
  }

  async deleteKeep(id) {
    await api.delete('api/keeps/' + id)
    const index = AppState.keeps.findIndex(k => k.id === id)
    logger.log('delete')
    AppState.keeps.splice(index, 1)
  }
}
export const keepsService = new KeepsService()
