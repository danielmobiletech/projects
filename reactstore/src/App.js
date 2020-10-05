import React, { Component } from 'react'
//import logo from './logo.svg'
import './App.css'
import 'bootstrap/dist/css/bootstrap.min.css'

import ProductList from './ProductList'
import Navbar from './Navbar'
import Details from './Details'
import Cart from './Cart/Cart'
import Default from './Default'
import Modal from './Modal'
import { Switch, Route } from 'react-router-dom'
/**
 * The main allow sub comonents are loaded into app comonents
 * the router controls the routing between cart, details, productlist while 
 * the navbar sits at the top
 */
export default class App extends Component {
  
  render () {
    return (
      <React.Fragment>
        <Navbar />

        <Switch>
          <Route exact path='/' component={ProductList} />
          <Route path='/details' component={Details} />
          <Route path='/cart' component={Cart} />
          <Route component={Default} />
        </Switch>

        <Modal />
      </React.Fragment>
    )
  }
}

//export default App
