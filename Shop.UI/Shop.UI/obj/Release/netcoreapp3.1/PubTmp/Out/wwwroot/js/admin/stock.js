var app = new Vue({
  el: '#app',
  data: {
    menu: 0,
    editing: false,
    loading: false,
    objectIndex: 0,
    stockModel: {
      productId: 0,
      name: 'axios',
      description: 'axios'
    },
    products: [],
    selectedProduct: null,
    newStock: {
      productId: 0,
      description: 'stocks',
      qty: 10
    }
  },

  mounted () {
    this.getStock()
  },
  methods: {
    addStock () {
      this.loading = true
      axios
        .post('/Admin/stocks', this.newStock)
        .then(res => {
          this.selectedProduct.stock.push(res.data) 
          console.log(res)
        })
        .catch(err => {
          console.log(err)
        })
        .then(() => {
          this.loading = false
        })
    },
    

    selectProduct (product) {
      this.selectedProduct = product
      this.newStock.productId=product.id

    },

    createStock () {
      this.loading = true
      axios
        .post('/Admin/stocks', this.stockModel)
        .then(res => {
          //this.stocks = res.data;
          console.log(res.data)
          this.stocks.push(res.data)
        })
        .catch(err => {
          console.log(err)
        })
        .then(() => {
          this.loading = false
          this.editing = false
        })
    },

    getStock () {
      this.loading = true
      axios
        .get('/Admin/stocks')
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

    editStock (id, index) {
      this.objectIndex = index
      this.getStock(id)
      this.editing = true
    },

    updateStock () {
      this.loading = true
     
      axios
        .put('/Admin/stocks', {
          stock:this.selectedProduct.stock.map(m=>{
            return {
              id:m.id,
              description:m.description,
              qty:m.qty,
              productId: this.selectedProduct.productId
            }
          })
        
        
        
        })
        .then(res => {
          //this.stocks = res.data;
          console.log(res.data)
          this.selectedProduct.stock.splice(Index,1)
        })
        .catch(err => {
          console.log(err)
        })
        .then(() => {
          this.loading = false
          //this.editing = false
        })
    },
    deleteStock (id,index) {
      this.loading = true
      axios
        .delete('/Admin/stocks/' + id)
        .then(res => {
          this.selectedProduct.stock.splice(index, 1)
          console.log(res)
        })
        .catch(err => {
          console.log(err)
        })
        .then(() => {
          this.loading = false
        })
    },
    newStock () {
      this.editing = true
      this.stockModel.id = 0
    }
  },
  compute: {}
})
