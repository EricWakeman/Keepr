<template>
  <!-- Modal -->
  <div class="modal fade"
       id="newKeepForm"
       tabindex="-1"
       role="dialog"
       aria-labelledby="newKeepFormLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document" @submit.prevent="createKeep">
      <form class="modal-content" id="newKeepFormContent">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            New Keep
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div class="form-group">
            <input type="text"
                   name="Keep Name"
                   class="form-control"
                   placeholder="Keep Name"
                   v-model="state.newKeep.name"
                   minlength="2"
                   required
            >
          </div>
          <div class="form-group">
            <input type="text"
                   name="Keep Image URL"
                   class="form-control"
                   placeholder="Keep Image URL"
                   v-model="state.newKeep.img"
                   minlength="2"
                   required
            >
          </div>
          <div class="form-group">
            <textarea type="text"
                      name="Keep Description"
                      class="form-control"
                      placeholder="Keep Description"
                      v-model="state.newKeep.description"
                      rows="5"
                      minlength="2"
                      required
            />
          </div>
        </div>
        <button type="submit" class="btn btn-secondary">
          Create
        </button>
      </form>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/runtime-core'
import { logger } from '../utils/Logger'
import $ from 'jquery'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'
export default {
  setup() {
    const state = reactive({
      newKeep: {}
    })
    return {
      state,
      async createKeep() {
        try {
          await keepsService.createKeep(state.newKeep)
          $('#newKeepForm').modal('toggle')
          const form = document.getElementById('newKeepFormContent')
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
