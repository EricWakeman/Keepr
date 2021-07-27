<template>
  <div class="container-fluid">
    <div class="row" v-if="activeProfile">
      <div class="col-3 my-2">
        <img :src="activeProfile.picture" :alt="activeProfile.name" class="profile-img">
      </div>
      <div class="col-4">
        <span class="profile-name">{{ activeProfile.name }}</span>
        <p class="vk-count">
          Keeps:{{ vaults.length }}
        </p>
        <p class="vk-count">
          Vaults: {{ keeps.length }}
        </p>
      </div>
    </div>
    <div class="row pt-5 pl-3">
      <p class="vk-count">
        Vaults
      </p>
    </div>
    <div class="row">
      <VaultCard v-for="v in vaults" :key="v.id" :vault="v" />
    </div>
    <div class="row pt-5 pl-3">
      <p class="vk-count">
        Keeps
      </p>
      <img src=".assets/img/plus.png" alt="">
    </div>
    <div class="row">
      <div class="masonry-with-columns pt-3">
        <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { accountService } from '../services/AccountService'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
export default {
  setup() {
    const route = useRoute()

    onMounted(() => {
      accountService.getProfile(route.params.id)
      vaultsService.getProfileVaults(route.params.id)
      keepsService.getProfileKeeps(route.params.id)
    })
    return {
      route,
      activeProfile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      createVault() {

      }
    }
  }
}
</script>

<style scoped lang="scss">
.profile-img{
  height: 12rem;
}
.profile-name{
  font-size: 3rem;
  padding-bottom: 0.1rem;
}
.vk-count{
  font-size: 2rem;
}
.keep-img{
  height: 12rem;
  width: 12rem;
  margin: 1rem

}
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
    > img{
    height: 200px;
    width: 200px;
  }
  }

}
</style>
