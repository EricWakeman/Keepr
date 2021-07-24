import { AppState } from '../AppState'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('/api/keeps')
    AppState.keeps = res.data
  }

  async getProfileKeeps(id) {
    const res = await api.get('/api/profiles/' + id + '/keeps')
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

  async getKeepById(id) {
    const res = await api.get('/api/keeps/' + id)
    AppState.activeKeep = res.data
  }

  async updateKeep(id, keepData) {
    const res = await api.put('/api/keeps/' + id, keepData)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)
    AppState.keeps.push(res.data)
  }

  async deleteKeep(id) {
    await api.delete('/api/keeps/' + id)
    AppState.keeps = AppState.keeps.filter(k => k.id !== id)
  }
}
export const keepsService = new KeepsService()
