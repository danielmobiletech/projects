var app = new Vue({
  el: '#app',
  data: {
    username: ''
  },
  mounted () {
    // this.getStock()
    return 0
  },
  methods: {
    createUser () {
      axios
        .post('/users', { username: this.username })
        .then(res => {
          console.log(res.username)
        })
        .catch(err => {
          console.log(err)
        })
    }
  },
  compute: {}
})
