<template>
  <div class="masonry container-fluid mt-5 pt-5">
    <div class=" item row mt-5 pt-5 ">
      <keeps-component v-for="k in state.keeps" :key="k.id" :keep-prop="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
export default {
  name: 'Home',
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      profile: computed(() => AppState.profile)
    })
    onMounted(async() => {
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        logger.error(error)
      }
    })
    return { state }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
html {
  box-sizing: border-box;
}

*,
*:before,
*:after {
  box-sizing: inherit;
}

.masonry { /* Masonry container */
  column-count: 4;
  display: inline-block;
  // grid-gap: 2em;
  column-gap: 0.1em;
}

.item { /* Masonry bricks or child elements */
  // background-color: rgb(206, 205, 205);
  // display: inline-block;
  margin: 0 0 0em;
  width: 100%;
}

</style>
