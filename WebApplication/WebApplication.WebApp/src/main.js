import Vue from 'vue'
import Vuetify from 'vuetify'
import App from './App'
import router from '@/router'
import store from '@/store'
import api from './utils/backend-api'

import VueProgressBar from 'vue-progressbar'

const options = {
  color: '#2196f3',
  failedColor: '#874b4b',
  thickness: '5px',
  transition: {
    speed: '0.1s',
    opacity: '0.5s',
    termination: 400
  },
  autoRevert: true,
  location: 'top',
  inverse: false
}

Vue.use(VueProgressBar, options)

Vue.use(Vuetify)
Vue.config.productionTip = false

window.Store = store
Vue.prototype.api = api

new Vue({
  el: '#app',
  router,
  store,
  render: h => h(App)
})
