<template>
  <div class="QuickModal">
    <!-- Modal -->
    <div class="modal"
         id="vaultId"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              New Vault
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form class="container-fluid" @submit.prevent="createVault()">
              <!-- <div class="form-group"> -->
              <label for="name">Name</label>
              <input type="text"
                     name="name"
                     id="name"
                     class="form-control"
                     placeholder="Name.."
                     aria-describedby="helpId"
                     v-model="state.newVault.name"
                     required
              >
              <!-- </div> -->

              <!-- <div class="form-group"> -->
              <label for="description">Description</label>
              <input type="text"
                     name="description"
                     id="description"
                     class="form-control"
                     placeholder="Description..."
                     aria-describedby="helpId"
                     v-model="state.newVault.description"
                     required
              >
              <!-- </div> -->

              <!-- <div class="form-group"> -->
              <input type="checkbox" v-model="state.newVault.isPrivate">
              <label for="checkbox">Private?</label>
              <!-- </div> -->
              <button class="btn btn-secondary btn-block mt-2" type="submit">
                Create
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { logger } from '../utils/Logger'
import $ from 'jquery'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
export default {
  name: 'VaultsModal',
  setup() {
    const state = reactive({
      vaults: computed(() => AppState.vaults),
      newVault: {}
    })
    return {
      state,
      async createVault() {
        try {
          logger.log(state.newVault)
          await vaultsService.createVault(state.newVault)
          logger.log(state.newVault)
          $('#vaultId').modal('hide')
          state.newVault = ''
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>

<style>

</style>
