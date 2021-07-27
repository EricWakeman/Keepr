<template>
  <!-- Button trigger modal -->
  <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#newVaultForm">
    Launch demo modal
  </button>

  <!-- Modal -->
  <div class="modal fade"
       id="newVaultForm"
       tabindex="-1"
       role="dialog"
       aria-labelledby="newVaultFormLabel"
       aria-hidden="true"
  >
    <form class="modal-dialog" role="document" @submit="createVault">
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
          ...
        </div>
        <div class="modal-footer">
          <button type="submit" class="btn btn-secondary" data-dismiss="modal">
            Create
          </button>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import { reactive } from '@vue/runtime-core'

export default {
  setup() {
    const state = reactive({
      newVault: {}
    })
    return {
      state
    }
  }
}
</script>

<style>

</style>
