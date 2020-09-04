import React, { Component } from 'react'

import Title from '../Title'
import CartColumns from './CartColumns'
import { ProductConsumer } from '../Context'
import EmptyCart from './EmptyCart'
import CartList from './CartList'
import CartTotals from './CartTotals'
export default class Cart extends Component {
  constructor (props) {
    super()
  }
  render () {
    return (
      <section>
        <ProductConsumer>
          {value => {
            console.log(value)
            const { cart } = value
            if (cart.length > 0) {
              return (
                <React.Fragment>
                  <Title name='your ' title=' cart' />
                  <CartColumns />
                  <CartList value={value} />
                  <CartTotals value={value} history={this.props.history} />
                </React.Fragment>
              )
            } else {
              return <EmptyCart />
            }
          }}
        </ProductConsumer>
      </section>
    )
  }
}
