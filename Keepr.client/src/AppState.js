import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  userVaults: [],
  activeVaultKeeps: [],
  account: {},
  activeProfile: null,
  keeps: [],
  activeKeep: null,
  vaults: [],
  activeVault: null

})
