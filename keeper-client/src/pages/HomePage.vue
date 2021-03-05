<template>
  <div class="container-fluid">
    <div id="masonry">
      <div class=" item row mt-5 pt-5 ">
        <keeps-component v-for="k in state.keeps" :key="k.id" :keep-prop="k" />
      </div>
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
div#masonry {
display: -ms-flexbox;
-ms-flex-direction: column;
-ms-flex-wrap: wrap;
display: flex;
flex-direction: column;
flex-wrap: wrap;
height: 100vw;
font-size: 0;
}
div#masonry keepsComponent {
width: 33.3%;
transition: .8s opacity;
}
div#masonry:hover keepsComponent {
opacity: 0.3;
}
div#masonry:hover keepsComponent:hover {
opacity: 1;
}

// .masonry { /* Masonry container */
//   column-count: 4;
//   display: inline-block;
//   // grid-gap: 2em;
//   column-gap: 1em;
// }

// .item { /* Masonry bricks or child elements */
//   // background-color: rgb(206, 205, 205);
//   // display: inline-block;
//   margin: 0 0 0em;
//   width: 100%;
// }
/* The Masonry Container */
// .masonry {
//   margin: 1.5em auto;
//   max-width: 768px;
//   column-gap: 1.5em;
// }

// /* The Masonry Brick */
// .item {
//   background: #fff;
//   padding: 1em;
//   margin: 0 0 1.5em;
// }

// /* Masonry on large screens */
// @media only screen and (min-width: 1024px) {
//   .masonry {
//     column-count: 4;
//   }
// }

// /* Masonry on medium-sized screens */
// @media only screen and (max-width: 1023px) and (min-width: 768px) {
//   .masonry {
//     column-count: 3;
//   }
// }

// /* Masonry on small screens */
// @media only screen and (max-width: 767px) and (min-width: 540px) {
//   .masonry {
//     column-count: 2;
//   }
// }
</style>
