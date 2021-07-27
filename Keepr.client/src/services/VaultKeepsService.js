import { AppState } from '../AppState'
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
}
export const vaultKeepsService = new VaultKeepsService()
