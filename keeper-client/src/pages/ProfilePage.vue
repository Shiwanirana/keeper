<template>
  <div class="container-fluid m-5 p-5">
    <div class="row mt-3">
      <div class="col text-center">
        <h1 class="display-2 greet">
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
      <h2>Vaults: </h2>
      <vaults-component v-for="v in state.vaults" :key="v.id" :vault-prop="v" />
      <vaults-model />
    </div>
    <div class="row mt-4">
      <h2>Keeps: </h2>
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

<style scoped>
.greet{
   text-shadow: 4px 8px 6px rgb(7, 2, 2);
   font-size: 10rem;
   color: rgb(88, 194, 180);
  /* background: rgba(11, 75, 55, 0.925); */
}
</style>
