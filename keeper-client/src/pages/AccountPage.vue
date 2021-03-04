<template>
  <div class=" container about text-center mt-5 pt-5">
    <div class="row mt-5">
      <div class="col">
        <h1>Welcome {{ state.account.name }}</h1>
        <img class="rounded" :src="state.account.picture" alt="" />
        <p>{{ state.account.email }}</p>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <h3> {{ state.vaults.length }} Vaults  <i class="fa fa-plus text-info" data-toggle="modal" data-target="#vaultId"></i></h3>
        <vaults-model />
        <div class="row ml-5 pl-5">
          <vaults-component v-for="v in state.vaults" :key="v.id" :vault-prop="v" />
        </div>
      </div>
    </div>
    <div class="row">
      <div class="col">
        <h3> {{ state.keeps.length }} Keeps:  <i class="fa fa-plus text-info" data-toggle="modal" data-target="#keepId"></i></h3>
        <keeps-modal />
        <div class="row ml-5 pl-5">
          <keeps-component v-for="k in state.keeps" :key="k.id" :keep-prop="k" />
        </div>
      </div>
    </div>
    <div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { accountService } from '../services/AccountService'
import { logger } from '../utils/Logger'
import VaultsModel from '../components/VaultsModel.vue'
export default {
  components: { VaultsModel },
  name: 'Account',
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async() => {
      try {
        await accountService.getVaultsByAccount()
        await accountService.getKeepsByAccount()
      } catch (error) {
        logger.error(error)
      }
    })
    return {
      state
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
