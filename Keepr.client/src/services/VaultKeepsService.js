import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class VaultKeepsService {
  async createVaultKeep(vId, kId) {
    const vaultKeep = {}
    vaultKeep.vaultId = vId
    vaultKeep.keepId = kId
    const res = await api.post('/api/vaultkeeps', vaultKeep)
    AppState.userVaultKeeps.push(res.data)
  }

  async getVaultKeeps(kId) {
    const res = await api.get('/api/vaultkeeps/' + kId)
    AppState.activeVaultKeeps = res.data
  }

  async deleteVaultKeep(kId, vId) {
    const res = await api.get('/api/keeps/' + kId + '/vaultkeeps/' + vId)
    logger.log(res.data)
    await api.delete('/api/vaultkeeps/' + res.data.id)

    AppState.keeps = AppState.keeps.filter(k => k.id !== kId)
  }
}
export const vaultKeepsService = new VaultKeepsService()
