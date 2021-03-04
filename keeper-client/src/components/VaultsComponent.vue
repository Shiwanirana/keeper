<template>
  <div class="card col-4 ml-5 pl-5 m-2 p-3">
    <div class="row d-flex">
      <div class="ml-5">
        <router-link class="text-info" :to="{name: 'Vault', params: {id: vaultProp.id}}">
          <h5>{{ vaultProp.name }}</h5>
          <p>{{ vaultProp.isPrivate }}</p>
        </router-link>
        <i class="fa fa-trash-o text-danger" aria-hidden="true" v-if="vaultProp.creatorId == state.account.id" @click="deleteVault"></i>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import Swal from 'sweetalert2'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'

export default {
  name: 'VaultsComponent',
  props: {
    vaultProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.profile),
      user: computed(() => AppState.user)
    })
    return {
      state,
      deleteVault() {
        // eslint-disable-next-line no-undef
        Swal.fire({
          title: 'Are you sure?',
          text: "You won't be able to revert this!",
          icon: 'warning',
          showCancelButton: true,
          confirmButtonColor: '#3085d6',
          cancelButtonColor: '#d33',
          confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
          if (result.isConfirmed) {
            try {
              vaultsService.deleteVault(props.vaultProp.id)
              // eslint-disable-next-line no-undef
              // swal('Your Vault is permanently deleted', 'WITH ALL YOUR KEEPS', 'error')
            } catch (error) {
              logger.error(error)
            }
            // eslint-disable-next-line no-undef
            Swal.fire(
              'Deleted!',
              'Your file has been deleted.',
              'success'
            )
          }
        })
      }
    }
  }
}
</script>

<style scoped>
.card{
  background: rgb(192, 186, 186);
background: linear-gradient(90deg, rgb(202, 193, 193) 25%, rgb(218, 213, 213) 55%, rgba(194, 190, 190, 0.981) 85%);
 border-radius: 4%;
  box-shadow: 3px 3px , -1em 0 .4em rgb(152, 148, 156);
}
</style>
