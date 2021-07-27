<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
    <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-lg modal-dialog-centered">
        <div class="modal-content">
          <div class="container-fluid" v-if="activeKeep">
            <div class="row">
              <div class="col-md-6 my-2">
                <img :src="activeKeep.img" alt="" class="modal-img">
              </div>
              <div class="col-md-6 my-2 text-center justify-content-between d-flex flex-column">
                <div class="row">
                  <div class="col-12 ">
                    <div class="row justify-content-center ">
                      <div class="col-3">
                        <img src="./assets/img/keepicon.png" alt="Keeps Count">
                        <span class="icount">

                          {{ activeVaultKeeps.length }}
                        </span>
                      </div>
                      <div class="col-3">
                        <img src="./assets/img/viewicon.png" alt="Views Count">
                        <span class="icount">
                          {{ activeKeep.views }}
                        </span>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-12">
                    <p class="keep-title">
                      {{ activeKeep.name }}
                    </p>
                  </div>
                </div>
                <div class="row">
                  <div class="col-12 justify-content-start">
                    <div class="row">
                      <p class="text-left keep-desc">
                        {{ activeKeep.description }}
                      </p>
                    </div>
                    <div class="row modal-footer">
                      <div class="dropdown">
                        <button class="btn btn-secondary dropdown-toggle"
                                type="button"
                                id="dropdownMenuButton"
                                data-toggle="dropdown"
                                aria-haspopup="true"
                                aria-expanded="false"
                                v-if="user.isAuthenticated && userVaults.length > 0 && route.name != 'Vault' "
                        >
                          Add to Vault
                        </button>
                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                          <a class="dropdown-item hoverable" v-for="v in userVaults" :key="v.id" @click="createVaultKeep(v.id, activeKeep.id, v.name)">{{ v.name }}</a>
                        </div>
                      </div>
                      <img src="./assets/img/trash-can.png" alt="Delete Keep" v-if="account.id == activeKeep.creatorId" class="grow hoverable" @click="deleteKeep(activeKeep.id)">
                      <img :src="activeKeep.creator.picture" :alt="activeKeep.creator.name" class="user-img hoverable grow" @click="goToProfile(activeKeep.creator.id)" data-dismiss="modal">
                      <span>{{ activeKeep.creator.name }}</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </main>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import { vaultKeepsService } from './services/VaultKeepsService'
import Pop from './utils/Notifier'
import { router } from './router'
import { keepsService } from './services/KeepsService'
import $ from 'jquery'
import { useRoute } from 'vue-router'

export default {
  name: 'App',
  setup() {
    const route = useRoute()
    return {
      route,
      appState: computed(() => AppState),
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep),
      activeVaultKeeps: computed(() => AppState.activeVaultKeeps),
      user: computed(() => AppState.user),
      userVaults: computed(() => AppState.userVaults),
      createVaultKeep(vId, kId, vName) {
        try {
          $('.bd-example-modal-lg').modal('toggle')
          vaultKeepsService.createVaultKeep(vId, kId)
          Pop.toast('Successfully added to ' + vName, 'success')
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      goToProfile(id) {
        AppState.vaults = []
        AppState.keeps = []
        router.push({ name: 'Profile', params: { id: id } })
      },
      async deleteKeep(id) {
        $('.bd-example-modal-lg').modal('toggle')
        try {
          if (await Pop.confirm()) {
            await keepsService.deleteKeep(id)
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

.mdisplay{
  width: 50vw;
}
.modal-img{
  max-width:100% ;
}
.keep-title{
font-size: 2rem;}
.keep-desc{
  font-size: 1.25rem;
  padding-bottom: 7rem;
  padding-left: 1rem;
}
.icount{
  font-size: 1.5rem;
  padding-left: 0.5rem;
}

.user-img{
  height:3rem;
}
main{
  background-color: #fffae9;
}

</style>
