<template>
  <div class="QuickModal">
    <!-- Modal -->
    <div class="modal"
         id="keepId"
         tabindex="-1"
         role="dialog"
         aria-labelledby="modelTitleId"
         aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              New Keep
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <form class="container-fluid" @submit.prevent="createKeep()">
              <!-- <div class="form-group"> -->
              <label for="name">Name</label>
              <input type="text"
                     name="name"
                     id="name"
                     class="form-control"
                     placeholder="Name.."
                     aria-describedby="helpId"
                     v-model="state.newKeep.name"
                     required
              >
              <!-- </div> -->
              <label for="img">Image</label>
              <input type="text"
                     name="img"
                     id="img"
                     class="form-control"
                     placeholder="Image..."
                     aria-describedby="helpId"
                     v-model="state.newKeep.img"
                     required
              >
              <!-- <div class="form-group"> -->
              <label for="description">Description</label>
              <input type="text"
                     name="description"
                     id="description"
                     class="form-control"
                     placeholder="Description..."
                     aria-describedby="helpId"
                     v-model="state.newKeep.description"
                     required
              >
              <!-- </div> -->

              <!-- <div class="form-group"> -->
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
import { keepsService } from '../services/KeepsService'
export default {
  name: 'KeepsModal',
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      newKeep: {}
    })
    return {
      state,
      async createKeep() {
        try {
          // logger.log(state.newKeep)
          await keepsService.createKeep(state.newKeep)
          $('#keepId').modal('hide')
          state.newKeep = {}
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
