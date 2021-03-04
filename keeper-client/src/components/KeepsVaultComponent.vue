<template>
  <div class="card m-1">
    <i class="fa fa-trash-o fa-2x text-danger" v-if="state.activeVault.creatorId == state.account.id" aria-hidden="true" @click="deleteVaultKeep"></i>
    <div class="keepcomponent card col-3 m-3 brighten" data-toggle="modal" :data-target="'#k' +keepsVaultProp.id">
      <div class="row" data-masonry="{&quot;percentPosition&quot;: true }">
        <img class="img-fluid mainKeep" :src="keepsVaultProp.img" />
        <div class="card-img-overlay row justify-content-between align-items-end text-white align-items-end ">
          <h3 class="card-title px-2">
            {{ keepsVaultProp.name }}
          </h3>
          <div>
            <div class="row mr-3">
              <div class="text-info">
                <i class="fa fa-user-circle"></i>
              </div>
            </div>
          </div>
        </div>
      <!-- <keep-modal :keep-modal-prop="keepsVaultProp.id" /> -->
      </div>
    </div>
  </div>
  <div class="modal fade"
       :id="'k' + keepsVaultProp.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalCenterTitle"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
      <div class="modal-content">
        <!-- <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLongTitle">
            Modal title
          </h5>
        </div> -->
        <div class="modal-body">
          <div class="row">
            <div class="col-6">
              <img class="img-fluid imgModal" :src="keepsVaultProp.img" />
            </div>
            <div class="col-6">
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
              <span class=" mr-2"><i class="fa fa-eye mt-3"></i> {{ keepsVaultProp.views }} </span>
              <span> <i class="fa fa-window-restore"></i> {{ keepsVaultProp.keeps }} </span>
              <h2 class="text-center mt-5 mb-4">
                {{ keepsVaultProp.name }}
              </h2>
              <p class="des">
                {{ keepsVaultProp.description }}
              </p>
              <hr>
              <div class="d-flex justify-content-center mt-5 pt-5">
                <div class="dropdown">
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
                    <!-- <vaults-component class="dropdown-item" v-for="v in state.vaults" :key="v.id" :vault-prop="v" @click="addToVault(vId)" /> -->
                  </div>
                </div>
                <i class="fa fa-trash-o fa-2x text-danger ml-5 p-0" v-if="state.account.id==keepsVaultProp.creatorId" aria-hidden="true" @click="deleteKeep"></i>
                <div class="d-flex" @click="currentProfile()">
                  <img :src="state.profile.picture" class="creatorPic mr-1 ml-5"><p>{{ state.profile.email }}</p>
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

// import { accountService } from '../services/AccountService'
export default {
  components: { VaultKeepComponent },
  name: 'KeepsVaultComponent',
  props: {
    keepsVaultProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault),
      profile: computed(() => AppState.activeProfile),
      user: computed(() => AppState.user),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      newVaultKeep: {},
      showForm: false
    })
    onMounted(async() => {
      logger.log(state.activeVault.id)
      // logger.log(state.account.id)
      // logger.log(state.activeVault.creatorId)
      await profilesService.getProfileById(props.keepsVaultProp.creatorId)
      await keepsService.getKeepById(props.keepsVaultProp.id)
    })
    return {
      state,
      currentProfile() {
        $(`#k${props.keepsVaultProp.id}`).modal('hide')
        router.push({ name: 'Profile', params: { id: props.keepsVaultProp.creatorId } })
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
              keepsService.deleteKeep(props.keepsVaultProp.id)
              // eslint-disable-next-line no-undef
              // swal('Your Vault is permanently deleted', 'WITH ALL YOUR KEEPS', 'error')
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
      async deleteVaultKeep() {
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
              logger.log(props.keepsVaultProp.vaultKeepId)
              vaultsService.deleteVaultKeep(props.keepsVaultProp.vaultKeepId, state.activeVault.id)
              // eslint-disable-next-line no-undef
              // swal('Your Vault is permanently deleted', 'WITH ALL YOUR KEEPS', 'error')
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
        state.newVaultKeep = { KeepId: props.keepsVaultProp.id, VaultId: vId }
        logger.log(state.newVaultKeep)
        vaultsService.addToVault(state.newVaultKeep)
      }
    }
  }
}
</script>

<style scoped>
.mainKeep{
  border-radius: 4%;
}
.imgModal{
  border-radius: 1%;
}
.card{
  background: rgb(231, 226, 226);
background: linear-gradient(90deg, rgb(250, 250, 250) 25%, rgb(219, 208, 208) 55%, rgba(226, 192, 192, 0.981) 85%);
 border-radius: 4%;
}
.des{
  height: 12vh;
}
  .zoom {
  padding: 50px;
  transition: transform .4s;
  width: 250px;
  height: 250px;
  margin: 0 auto;
}
.zoom:hover {
  -webkit-transform: scale(2.5);
  transform: scale(1.3);
}
.brighten{
  border-radius: 4%;
-webkit-filter: brightness(70%);
/* -webkit-transition: all 1s ease; */
/* -moz-transition: all 1s ease;
-o-transition: all 1s ease;
-ms-transition: all 1s ease; */
transition: all 1s ease;
}

.brighten:hover {
-webkit-filter: brightness(100%);
}
.creatorPic{
  height: 30px;
  width: 30px;
}
</style>
