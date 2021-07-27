<template>
  <div class="container-fluid">
    <div class="row" v-if="activeVault">
      <div class="col-8">
        <h1>{{ activeVault.name }}</h1>
        <h4 v-if="keeps.length >0">
          Keeps : {{ keeps.length }}
        </h4>
      </div>
      <div class="col-4">
        <img src="../assets/img/trash-can.png" alt="Delete Vault" class="grow hoverable" @click="deleteVault" v-if="account.id == activeVault.creatorId">
      </div>
    </div>
    <div class="row p-5" v-if="keeps.length>0">
      <div class="masonry-with-columns">
        <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { router } from '../router'
export default {
  setup() {
    const route = useRoute()
    onMounted(async() => {
      vaultsService.getVaultById(route.params.id)
      keepsService.getKeepsByVaultId(route.params.id)
    })
    return {
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      async deleteVault() {
        if (await Pop.confirm()) {
          router.push({ name: 'Profile', params: { id: AppState.activeVault.creatorId } })
          await vaultsService.deleteVault(AppState.activeVault.id)
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    width: 150px;
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
     img{
    height: 200px;
    width: 200px;
  }
  }
}
.keep-img{
  height: 12rem;
  width: 12rem;
  margin: 1rem
}
</style>
