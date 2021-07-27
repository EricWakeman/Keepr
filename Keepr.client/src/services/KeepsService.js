import Masonry from 'masonry-layout'
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('/api/keeps')
    AppState.keeps = res.data
    logger.log(AppState.keeps)
  }

  async getProfileKeeps(id) {
    const res = await api.get('/api/profiles/' + id + '/keeps')
    logger.log(res.data)
    AppState.keeps = res.data
  }

  async getKeepsByVaultId(id) {
    const res = await api.get('/api/vaults/' + id)
    AppState.keeps = res.data
  }

  async createKeep(keepData) {
    const res = await api.post('/api/keeps', keepData)
    AppState.keeps.push(res.data)
  }

  async updateKeep(id, keepData) {
    const res = await api.put('/api/keeps/' + id, keepData)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)
    AppState.keeps.push(res.data)
  }

  async updateKeepViews(keepdata) {
    keepdata.views += 1
    const res = await api.put('api/keeps', keepdata)
    logger.log(res)
    AppState.activeKeep = res.data
  }

  async deleteKeep(id) {
    await api.delete('/api/keeps/' + id)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)
  }
}
export const keepsService = new KeepsService()
