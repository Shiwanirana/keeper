<template>
  <div class="masonry keepcomponent card  m-5 brighten zoom" data-toggle="modal" :data-target="'#k' +keepProp.id">
    <div class=" item row" data-masonry="{&quot;percentPosition&quot;: true }">
      <img class="img-fluid mainKeep" :src="keepProp.img" />
      <div class="card-img-overlay row justify-content-between align-items-end text-white align-items-end ">
        <h3 class="card-title px-2">
          <i class="fa fa-trash-o fa-2x text-white" v-if="state.vaultKeeps.vaultKeepId && state.account.id === state.vaults.creatorId" aria-hidden="true"></i>
          {{ keepProp.name }}
        </h3>
        <div>
          <div class="row mr-3">
            <div class="text-info">
              <i class="fa fa-user-circle"></i>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="modal fade"
       :id="'k' + keepProp.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalCenterTitle"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <div class="row">
            <div class="col-6">
              <img class="img-fluid imgModal" :src="keepProp.img" />
            </div>
            <div class="col-6">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
              <span class=" mr-2"><i class="fa fa-eye mt-3"></i> {{ keepProp.views }} </span>
              <span> <i class="fa fa-window-restore"></i> {{ keepProp.keeps }} </span>
              <h2 class="text-center mt-5 mb-4">
                {{ keepProp.name }}
              </h2>
              <p class="des">
                {{ keepProp.description }}
              </p>
              <hr>
              <div class="d-flex justify-content-center mt-5 pt-5">
                <div class="dropdown" v-if="state.user.isAuthenticated">
                  <button class="btn btn-outline-success dropdown-toggle btn-sm"
                          type="button"
                          id="dropdownMenuButton"
                          data-toggle="dropdown"
                          aria-haspopup="true"
                          aria-expanded="false"
                  >
                    ADD TO VAULT
                  </button>
                  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <vault-keep-component class="dropdown-item" v-for="v in state.vaults" :key="v.id" :vault-keep-prop="v" @click="addToVault(v.id)" />
                  </div>
                </div>
                <i class="fa fa-trash-o fa-2x text-danger ml-5 p-0" v-if="state.account.id==keepProp.creatorId" aria-hidden="true" @click="deleteKeep"></i>
                <div class="d-flex" @click="currentProfile()">
                  <img :src="keepProp.creator.picture" class="creatorPic mr-1 ml-5"><p>{{ keepProp.creator.email }}</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import $ from 'jquery'
import { profilesService } from '../services/ProfilesService'
import VaultKeepComponent from './VaultKeepComponent.vue'
import Swal from 'sweetalert2'
export default {
  components: { VaultKeepComponent },
  name: 'KeepsComponent',
  props: {
    keepProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      activeVault: computed(() => AppState.activeVault),
      profile: computed(() => AppState.activeProfile),
      user: computed(() => AppState.user),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      newVaultKeep: {},
      showForm: false
    })
    onMounted(async() => {
      await profilesService.getProfileById(props.keepProp.creatorId)
      await keepsService.getKeepById(props.keepProp.id)
    })
    return {
      state,
      currentProfile() {
        $(`#k${props.keepProp.id}`).modal('hide')
        router.push({ name: 'Profile', params: { id: props.keepProp.creatorId } })
      },
      async deleteKeep() {
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
              $(`#k${props.keepProp.id}`).modal('hide')
              keepsService.deleteKeep(props.keepProp.id)
            } catch (error) {
              logger.error(error)
            }
            // eslint-disable-next-line no-undef
            Swal.fire(
              'Deleted!',
              'Your Keep has been deleted.',
              'success'
            )
          }
        })
      },
      addToVault(vId) {
        state.newVaultKeep = { KeepId: props.keepProp.id, VaultId: vId }
        logger.log(state.newVaultKeep)
        $(`#k${props.keepProp.id}`).modal('hide')

        vaultsService.addToVault(state.newVaultKeep)
        // eslint-disable-next-line no-undef
        swal('Add this Keep successfully to your vault', 'Congratulation', 'success')
      }
    }
  }
}
</script>

<style  scoped lang="scss">
.mainKeep{
  border-radius: 6%;
  // height: 300px !important;
  // width: 300px !important;
}
.imgModal{
  border-radius: 6%;
}
.card{
  height: 200px !important;
  width: 200px !important;
  background: rgb(30,29,29);
background: linear-gradient(90deg, rgba(30,29,29,1) 25%, rgba(162,162,162,1) 55%, rgba(35,35,35,0.981127485173757) 85%);
 border-radius: 6%;
 box-shadow: 3px 8px , -1em 0 .4em rgb(107, 105, 109);
}
.des{
  height: 12vh;
}
  .zoom {
  padding: 8px;
  transition: transform .4s;
  width: 400px;
  height: 400px;
  margin: 0 auto;
}
.zoom:hover {
  -webkit-transform: scale(2.5);
  transform: scale(1.05);
}
.brighten{
  border-radius: 4%;
-webkit-filter: brightness(70%);
transition: all 1s ease;
}
.brighten:hover {
-webkit-filter: brightness(100%);
}
.creatorPic{
  height: 30px;
  width: 30px;
}
.masonry { /* Masonry container */
  column-count: 3;
  column-gap: 1em;
}

.item { /* Masonry bricks or child elements */
  // background-color: rgb(206, 205, 205);
  // display: inline-block;
  margin: 0 0 0em;
  width: 100%;
}
</style>
