<template>
  <div class="container-fluid mt-5 pt-5">
    <div class="row mt-3">
      <div class="col text-center">
        <h1 class="display-2">
          <b>Welcome</b>
        </h1>
      </div>
    </div>
    <div class="row">
      <div class="col d-flex">
        <img :src="state.profile.picture">
        <div class="col-2">
          <h1 class="display ml-2">
            {{ state.profile.name }}
          </h1><h3>Vaults:  {{ state.vaults.length }}</h3><h3>Keeps:  {{ state.keeps.length }}</h3>
        </div>
      </div>
    </div>
    <div class="row mt-4">
      <h2>Vaults: <i class="fa fa-plus text-info" data-toggle="modal" data-target="#vaultId"></i></h2>
      <vaults-component v-for="v in state.vaults" :key="v.id" :vault-prop="v" />
      <vaults-model />
    </div>
    <div class="row mt-4">
      <h2>Keeps: <i class="fa fa-plus text-info" data-toggle="modal" data-target="#keepId"></i></h2>
      <keeps-modal />
      <keeps-component v-for="k in state.keeps" :key="k.id" :keep-prop="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { profilesService } from '../services/ProfilesService'
import { logger } from '../utils/Logger'
// import { keepsService } from '../services/KeepsService'
import { useRoute } from 'vue-router'
export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    const state = reactive({
      profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.profileVaults),
      keeps: computed(() => AppState.profileKeeps)
    })
    onMounted(async() => {
      try {
        logger.log(route.params.id)
        logger.log(state.vaults.length)

        await profilesService.getProfileById(route.params.id)
        await profilesService.getKeepsByProfileId(route.params.id)
        await profilesService.getVaultsByProfileId(route.params.id)
      } catch (error) {
        logger.error(error)
      }
    })
    return { state }
  }
}
</script>

<style>

</style>
