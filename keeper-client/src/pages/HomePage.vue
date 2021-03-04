<template>
  <div class="container-fluid m-5 p-5">
    <div class="row m-5 ">
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

/* The Masonry Container */
.masonry {
  margin: 1.5em auto;
  max-width: 768px;
  column-gap: 1.5em;
}

/* The Masonry Brick */
.item {
  background: #fff;
  padding: 1em;
  margin: 0 0 1.5em;
}

/* Masonry on large screens */
@media only screen and (min-width: 1024px) {
  .masonry {
    column-count: 4;
  }
}

/* Masonry on medium-sized screens */
@media only screen and (max-width: 1023px) and (min-width: 768px) {
  .masonry {
    column-count: 3;
  }
}

/* Masonry on small screens */
@media only screen and (max-width: 767px) and (min-width: 540px) {
  .masonry {
    column-count: 2;
  }
}
</style>
