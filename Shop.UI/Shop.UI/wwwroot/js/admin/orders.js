var app = new Vue({
  el: '#app',
  data: {
    status: 0,
    loading: false,
    orders: [],
    selectOrder: null
  },
  /* stockModel: {
        productId: 0,
        name: 'axios',
        description: 'axios'
      },*/

  mounted () {
    this.getOrders()
  },
  watch:{
      status:function()
      {
          this.getOrders();
      }
  },
  methods: {
    getOrders () {
      this.loading = true
      axios.get('/orders?status=' + this.status).then(res => {
        this.orders = res.data
        this.status = false
      })
    },
    selectOrder(id)
    { this.loading=true;
        axios.get("/orders/"+id)
        .then(res=>{
            this.selectOrder=res.data;
            this.loading=false;
        })
    },
    updatedOrder() {
        this.loading=true;
        axios.put('/orders/'+this.selectOrder.id,null)
        .then(res=>
            {
                this.loading=false;
                this.exitOrders();
                this.getOrders();
                
            })
    },
    exitOrders()
    {
        this.selectOrder=null;
    },
  },
  compute: {}
})
