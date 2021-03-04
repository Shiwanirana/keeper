<template>
  <nav class="navbar navbar-expand-lg navbar-dark navBar fixed-top ">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center ml-2 m-3">
        <img
          alt="logo"
          src="https://p1.hiclipart.com/preview/920/29/225/k-logo.jpg"
          height="45"
        />
      </div>
    </router-link>
    <!-- <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button> -->
    <!-- <div class="collapse navbar-collapse" id="navbarText"> -->
    <!-- <ul class="navbar-nav mr-auto">
        <li class="nav-item"> -->
    <!-- <router-link :to="{ name: 'Home' }" class="nav-link">
            Home
          </router-link> -->
    <div class="input-group ml-5 d-flex justify-content-center">
      <div class="form-inline">
        <input type="search" id="form1" class="form-control" placeholder="Search..." />
        <label class="form-label" for="form1"></label>
      </div>
      <button type="button" class="btn btn-light">
        <i class="fa fa-search text-info"></i>
      </button>
    </div>
    <!-- </li>
        <li class="nav-item">
          <router-link :to="{ name: 'About' }" class="nav-link">
            About
          </router-link>
        </li>
      </ul> -->
    <div class="collapse navbar-collapse" id="navbarText">
      <span class="navbar-text text-dark">
        <button
          class="btn btn-primary text-uppercase"
          @click="login"
          v-if="!user.isAuthenticated"
        >
          Login
        </button>

        <div class="dropdown" v-else>
          <div
            class="dropdown-toggle"
            @click="state.dropOpen = !state.dropOpen"
          >
            <img
              :src="user.picture"
              alt="user photo"
              height="40"
              class="rounded"
            />
            <span class="mx-3">{{ user.name }}</span>
          </div>
          <div
            class="dropdown-menu p-0 list-group w-100"
            :class="{ show: state.dropOpen }"
            @click="state.dropOpen = false"
          >
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item list-group-item-action hoverable">
                Account
              </div>
            </router-link>
            <div
              class="list-group-item list-group-item-action hoverable"
              @click="logout"
            >
              logout
            </div>
          </div>
        </div>
      </span>
    </div>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
export default {
  name: 'Navbar',
  setup() {
    const state = reactive({
      dropOpen: false
    })
    return {
      state,
      user: computed(() => AppState.user),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        await AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link{
  text-transform: uppercase;
}
.nav-item .nav-link.router-link-exact-active{
  color: var(--primary);
}
.navBar{
  box-shadow: 1px 4px 4px grey;
  background: rgba(84, 240, 190, 0.925);
}
</style>
