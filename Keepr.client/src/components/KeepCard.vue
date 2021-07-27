<template>
  <div class="keep-container hoverable grow">
    <img :src="keep.img"
         :alt="keep.name"
         class="keep-img "
         @click="setActiveKeep()"
         data-toggle="modal"
         data-target=".bd-example-modal-lg"
    >
    <div class="content">
      <img src="../assets/img/whitecan.png" alt="" class="grow hoverable shadow vault-delete" v-if="route.name == 'Vault' && activeVault.creatorId == account.id" @click="deleteVaultKeep(keep.id, route.params.id)">
      <img :src="keep.creator.picture" alt="" class="creator-pic rounded shadow">
      <p>{{ keep.name }}</p>
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { vaultKeepsService } from '../services/VaultKeepsService'
export default {
  props: {
    keep: { type: Object, required: true }
  },
  setup(props) {
    const route = useRoute()
    return {
      route,
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault),
      setActiveKeep() {
        keepsService.updateKeepViews(props.keep)
      },
      async deleteVaultKeep(kId, vId) {
        try {
          if (await Pop.confirm()) {
            await vaultKeepsService.deleteVaultKeep(kId, vId)
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }
}
</script>

<style>

.keep-img{
  max-width: 100%;
}
.hoverable{
  cursor: pointer;
}
.grow{
  transition: all .25s linear;
}
.grow:hover{
  transform: scale(1.1);
}
.keep-container{
  position: relative;
  text-align: center;
  padding: 0.25rem;
}
.keep-container .content{
  position: absolute;
  text-shadow: black;
  bottom: -30px;
  right: -15px;
  color: #f7f7f7;
  width: 100%;
  padding: 20px;
  font-size: 1.2rem;
  text-shadow: -1px 1px 0 #000,
                1px 1px 0 #000,
                1px -1px 0 #000,
               -1px -1px 0 #000;
}
.creator-pic{
  height: 2.5rem;
  position: absolute;
  bottom: 60px;
  right: 30px;
}
.vault-delete{
  top: -4px;
  position: absolute;
  left: -5px;
  height: 2rem;
}
</style>
