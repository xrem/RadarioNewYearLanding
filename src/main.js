import Vue from 'vue'
import App from './App.vue'
import VueScrollTo from 'vue-scrollto'

Vue.use(VueScrollTo)

Vue.use(VueScrollTo, {
    duration: 200,
    easing: "ease",
})

Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')
