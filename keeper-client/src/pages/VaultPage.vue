<template>
  <div class="container mt-5 pt-5" v-if="state.vault.isPrivate==false">
    <div class="row mt-5 pt-5">
      <div class="col d-flex">
        <h2 class="text-danger">
          Selected Vault: {{ state.vault.name }}
        </h2> <i class="fa fa-trash-o fa-2x ml-3 mt-1 text-danger" v-if="state.vault.creatorId == state.account.id" aria-hidden="true" @click="deleteVault"></i>
      </div>
    </div>
    <div class="row">
      <div class="col">
        Description: {{ state.vault.description }}
      </div>
    </div>

    <div class="row my-2 mt-5">
      <div class="col">
        <h3> Total Keeps: {{ state.vaultKeeps.length }}</h3>
      </div>
    </div>
    <div class="row">
      <keeps-vault-component v-for="k in state.vaultKeeps" :key="k.id" :keeps-vault-prop="k" />
    </div>
  </div>
  <div class="m-5 p-5 " v-else>
    <button class="btn btn-danger btn-lg m-5 p-5 d-flex justify-content-center" @click="privateVault">
      Want to access a PRIVATE Valut
    </button>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { useRoute, useRouter } from 'vue-router'
// import KeepsComponent from '../components/KeepsComponent.vue'
import { logger } from '../utils/Logger'
import Swal from 'sweetalert2'
import KeepsVaultComponent from '../components/KeepsVaultComponent.vue'
export default {
  components: { KeepsVaultComponent },
  name: 'VaultPage',
  setup() {
    const route = useRoute()
    const router = useRouter()
    const state = reactive({
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.vaultKeeps),
      account: computed(() => AppState.account),
      vaultKeeps: computed(() => AppState.vaultKeeps)
    })
    onMounted(async() => {
      // logger.log(state.vault.name)
      // logger.log(state.profile.id)
      // logger.log(state.vaults.creatorId)
      // logger.log(props.keepProp.vaultKeepsId)
      await vaultsService.getVaultById(route.params.id)
      await vaultsService.getKeepsByVaultId(route.params.id)
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
              vaultsService.deleteVault(route.params.id)
              router.push({ name: 'Home' })
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
      },
      privateVault() {
        Swal.fire(' This Vault is PRIVATE...if you want to see whats inside...make it PUBLIC first!!')
      }
    }
  }
}
</script>

<style>

</style>
