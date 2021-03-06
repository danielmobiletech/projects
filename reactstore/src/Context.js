import React, { Component } from 'react'
import { storeProducts, detailProduct } from './data'
const ProductContext = React.createContext()
//This Context use to pass down products to other components props
class ProductProvider extends Component {
  constructor (props) {
    super(props);
    this.state = {
      products: [],
      detailProduct: detailProduct,
      cart: [],
      modalOpen: false,
      modalProduct: detailProduct,
      cartSubTotal: 0,
      cartTax: 0,
      cartTotal: 0
    }
  }

  handleDetail = (id) => {
    console.log('hello from detail')
    const product = this.getItem(id)
    this.setState(() => {
      return { detailProduct: product }
    })
  }
// When product is added to the cart cart totalis recalculated when state is updated
  addToCart =( id) => {
    let tempProducts = [...this.state.products]
    const index = tempProducts.indexOf(this.getItem(id))
    const product = tempProducts[index]
    product.inCart = true
    product.count = 1
    const price = product.price
    product.total = price

    this.setState(
      () => {
        return {
          products: tempProducts,
          cart: [...this.state.cart, product]
        }
      },
      () => {
        this.addTotals()
      }
    )
  }

  //Get Product by ID
  getItem = id => {
    const product = this.state.products.find(item => item.id === id)
    return product
  }
  /**
   * Grabs Product From Data 
   * Assign products to tempProducts
   * It assign Products to the state's product property
   * 
   */
  setProducts = () => {
    let tempProducts = []
    storeProducts.forEach(item => {
      const singleItem = { ...item }
      tempProducts = [...tempProducts, singleItem]

    })
    this.setState(() => {
      return { products: tempProducts }
    })
  }
  componentWillMount()
  {
    this.setProducts();
  }
  closeModal = () => {
    this.setState(() => {
      return { modalOpen: false }
    })
  }
  openModal = id => {
    const product = this.getItem(id)
    this.setState(() => {
      return { modalProduct: product, modalOpen: true }
    })
  }
  increment = id => {
    let tempCart = [...this.state.cart]
    console.log(id+'this id')
    let selectProduct = tempCart.find(x => x.id === id)
   // console.log(selectProduct+"this index")
    const index = tempCart.indexOf(selectProduct)
    const product = tempCart[index]
    product.count += 1
    product.total = product.count * product.price
    this.setState(
      () => {
        return { cart: [...tempCart] }
      },
      () => this.addTotals()
    )
  }

  decrement = id => {
    let tempCart = [...this.state.cart]
    let selectProduct = tempCart.find(x => x.id === id)
    const index = tempCart.indexOf(selectProduct)
    const product = tempCart[index]
    product.count -= 1
    if (product.count <= 0) this.removeItem(id)
    else {
      product.total = product.count * product.price
    }
    this.setState(
      () => {
        return { cart: [...tempCart] }
      },
      () => this.addTotals()
    )
  }

  removeItem = id => {
    let tempProducts = [...this.state.products]
    let tempCart = [...this.state.cart]
    tempCart = tempCart.filter(x => {
      return x.id !== id
    })
    const index = tempProducts.indexOf(this.getItem(id))
    let removedProduct = tempProducts[index]
    removedProduct.inCart = false
    removedProduct.count = 0
    removedProduct.total = 0
    this.setState(
      () => {
        return { products: [...tempProducts], cart: [...tempCart] }
      },
      () => this.addTotals()
    )
  }
  clearCart = () => {
    this.setState(
      () => {
        return { cart: [] }
      },
      () => {
        this.setProducts()
        this.addTotals()
      }
    )
  }
  addTotals = () => {
    let subTotal = 0
    this.state.cart.map(x => {
      return (subTotal += x.total)
    })
    const tempTax = subTotal * 0.1
    const tax = parseFloat(tempTax.toFixed(2))
    const total = subTotal + tax
    this.setState(() => {
      return { cartSubTotal: subTotal, cartTax: tax, cartTotal: total }
    })
    //setProd({ ...prod, cartSubTotal: subTotal, cartTax: tax, cartTotal: total })
  }
  render () {
    console.log(this.state)
    return (
      //this sets Context api so props can utilize it
      <ProductContext.Provider
        value={{
          ...this.state,
          handleDetail: this.handleDetail,
          addToCart: this.addToCart,
          openModal: this.openModal,
          closeModal: this.closeModal,
          increment: this.increment,
          decrement: this.decrement,
          removeItem: this.removeItem,
          clearCart:this.clearCart
        }}
      >
        {this.props.children}
      </ProductContext.Provider>
    )
  }
}
const ProductConsumer = ProductContext.Consumer
export { ProductProvider, ProductConsumer }
