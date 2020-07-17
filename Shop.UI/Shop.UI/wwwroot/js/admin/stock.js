var app = new Vue({
  el: '#app',
  data: {
    menu: 0,
    editing: false,
    loading: false,
    objectIndex: 0,
   /* stockModel: {
      productId: 0,
      name: 'axios',
      description: 'axios'
    },*/
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
        .post('/stocks', this.newStock)
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
      this.newStock.productId = product.id
      console.log("selected new")
      console.log(this.newStock.productId)
      console.log("selected product")
      console.log(this.selectedProduct.id)

    },

    /*createStock () {
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
    },*/

    getStock () {
      this.loading = true
      axios
        .get('/stocks')
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

    /*
    editStock (id, index) {
      this.objectIndex = index
      this.getStock(id)
      this.editing = true
      console.log(index)
    },
    */

    updateStock () {
      this.loading = true
      console.log("stts")
      console.log(this.selectedProduct.stock)

      var vm=this.selectedProduct.stock.map(m => {
        return {
          id: m.id,
          description: m.description,
          qty: m.qty,
          productId: this.selectedProduct.id
        }
      });
      

        console.log(vm);
        console.log(this.selectedProduct.Id)
        //return;

      axios
        .put('/stocks', {
          stock: this.selectedProduct.stock.map(m => {
            return {
              id: m.id,
              description: m.description,
              qty: m.qty,
              productId: this.selectedProduct.id
            }
          })
          })
        
        .then(res => {
          //this.stocks = res.data;
          console.log(res.data)
          this.selectedProduct.stock.splice(Index, 1)
        })
        .catch(err => {
          console.log(err)
          console.log()
        })
        .then(() => {
          this.loading = false
          //this.editing = false
        })
    },
    deleteStock (id, index) {
      this.loading = true
      axios
        .delete('/stocks/' + id)
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
    }
    /* newStock () {
      this.editing = true
      this.stockModel.id = 0
    }
    */
  },
  compute: {}
})
