<template>
  <!-- Modal -->
  <div class="modal fade"
       id="newVaultForm"
       tabindex="-1"
       role="dialog"
       aria-labelledby="newVaultFormLabel"
       aria-hidden="true"
  >
    <form class="modal-dialog" role="document" @submit.prevent="createVault" id="newVaultFormContent">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            New Vault
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div class="form-group">
            <input type="text"
                   class="form-control"
                   v-model="state.newVault.name"
                   minlength="2"
                   required
                   placeholder="Vault Name"
            >
          </div>
          <div class="form-group">
            <textarea type="text"
                      class="form-control"
                      v-model="state.newVault.description"
                      minlength="2"
                      required
                      rows="5"
                      placeholder="Vault Description"
            />
          </div>
          <div class="form-group align-items-center">
            <label>
              <input type="checkbox" v-model="state.newVault.isPrivate">
              Mark this vault as private?</label>
          </div>
        </div>
        <div class="modal-footer">
          <button type="submit" class="btn btn-secondary">
            Create
          </button>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import { reactive } from '@vue/runtime-core'
import Pop from '../utils/Notifier'
import { vaultsService } from '../services/VaultsService'
import $ from 'jquery'

export default {
  setup() {
    const state = reactive({
      newVault: {}
    })
    return {
      state,
      async createVault() {
        try {
          await vaultsService.createVault(state.newVault)
          $('#newVaultForm').modal('toggle')
          const form = document.getElementById('newVaultFormContent')
          form.reset()
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
    }
  }
}
</script>

<style>

</style>
