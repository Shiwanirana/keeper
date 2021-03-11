<template>
  <div class=" container-fluid mt-5 pt-5">
    <div class=" row mt-5 pt-5 ">
      <keeps-component v-for="k in state.keeps" :key="k.id" :keep-prop="k" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import swal from 'sweetalert2'
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
      // eslint-disable-next-line no-undef
      swal.fire({
        title: 'To the Amazing site "Keeper"',
        text: 'Please Sign-in to make your Vaults and save your Keeps!!',
        imageUrl: 'https://i.pinimg.com/originals/da/d0/31/dad0312b7d04cc741c5fadd2eac3e0a6.jpg',
        imageSize: '200x200',
        // background: '#f0f0f0',
        background: '#fff url(https://pub-static.fotor.com/static/web/site/features/backgrounds/images/Fotor-wall-background-image-6.jpg)',
        // background: '#fff url(https://image.shutterstock.com/z/stock-vector--exclamation-mark-exclamation-mark-hazard-warning-symbol-flat-design-style-vector-eps-444778462.jpg)',
        timer: 4000
      })
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
#back {
  /* width: 100px;
  height: 50px;
  background-color: red; */
  font-weight: bold;
  position: relative;
  animation: mymove 40s infinite;
}
#back {animation-timing-function: linear;}

@keyframes mymove {
  from {top: 0px;}
  to {top: 4000px;}
}

</style>
