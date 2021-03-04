<template>
  <div class="card col-4 ml-5 pl-5 m-2 p-3">
    <div class="row d-flex">
      <div class="ml-5">
        <!-- <router-link class="text-light" :to="{name: 'Vault', params: {id: vaultProp.id}}"> -->
        <h3>{{ vaultKeepProp.name }}</h3>
        <!-- <p>{{ vaultKeepProp.isPrivate }}</p> -->
        <!-- </router-link> -->
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'VaultKeepComponent',
  props: {
    vaultKeepProp: {
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
    return { state }
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
