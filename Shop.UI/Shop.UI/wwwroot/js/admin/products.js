var app = new Vue({
  el: '#app',
  data: {
    menu: 0,
    editing: false,
    loading: false,
    objectIndex: 0,
    productModel: {
      id: 0,
      name: 'axios',
      description: 'axios',
      value: 1.67
    },
    products: []
  },

  mounted () {
    this.getProducts()
  },
  methods: {
    getProduct (id) {
      this.loading = true
      axios
        .get('/products/' + id)
        .then(res => {
          var product = res.data
          this.productModel = {
            id: product.id,
            name: product.name,
            value: product.value,
            description: product.description
          }
          console.log(res)
        })
        .catch(err => {
          console.log(err)
        })
        .then(() => {
          this.loading = false
        })
    },

    createProduct () {
      this.loading = true
      axios
        .post('/products', this.productModel)
        .then(res => {
          //this.products = res.data;
          console.log(res.data)
          this.products.push(res.data)
        })
        .catch(err => {
          console.log(err)
        })
        .then(() => {
          this.loading = false
          this.editing = false
        })
    },

    getProducts () {
      this.loading = true
      axios
        .get('/products')
        .then(res => {
          this.products = res.data
          console.log(res)
        })
        .catch(err => {
          console.log(err)
        })
        .then(() => {
          this.loading = false
        })
    },

    cancel () {},
    editProduct (id, index) {
      this.objectIndex = index
      this.getProduct(id)
      this.editing = true
    },

    updateProduct () {
      this.loading = true
      axios
        .put('/products', this.productModel)
        .then(res => {
          //this.products = res.data;
          console.log(res.data)
          this.products.splice(this.objectIndex, 1, res.data)
        })
        .catch(err => {
          console.log(err)
        })
        .then(() => {
          this.loading = false
          this.editing = false
        })
    },
    deleteProduct (id, index) {
      this.loading = true
      axios
        .delete('/products/' + id)
        .then(res => {
          this.products.splice(this.objectIndex, 1)
          console.log(res)
        })
        .catch(err => {
          console.log(err)
        })
        .then(() => {
          this.loading = false
        })
    },
    newProduct () {
      this.editing = true
      this.productModel.id = 0
    }
  },
  compute: {}
})
