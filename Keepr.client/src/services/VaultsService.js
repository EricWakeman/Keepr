import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultsService {
  async createVault(vaultData) {
    const res = await api.post('/api/vaults', vaultData)
    AppState.vaults.push(res.data)
  }

  async getVaultById(id) {
    const res = await api.get('/api/vaults/' + id)
    AppState.activeVault = res.data
  }

  async getProfileVaults(id) {
    const res = await api.get('/api/profiles/' + id + '/vaults')
    AppState.vaults = res.data
  }

  async updateVault(id, vaultdata) {
    const res = await api.put('/api/vaults' + id, vaultdata)
    AppState.vaults = AppState.vaults.filter(v => v.id !== id)
    AppState.vaults.push(res.data)
  }

  async deleteVault(id) {
    await api.delete('/api/vaults' + id)
    AppState.vaults = AppState.vaults.filter(v => v.id !== id)
  }
}
export const vaultsService = new VaultsService()
