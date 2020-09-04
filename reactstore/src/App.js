import React, { Component } from 'react'
//import logo from './logo.svg'
import './App.css'
import 'bootstrap/dist/css/bootstrap.min.css'
//import Cart from '../files/'
//import Product from './ProductList'
import ProductList from './ProductList'
import Navbar from './Navbar'
import Details from './Details'
import Cart from './Cart/Cart'
import Default from './Default'
import Modal from './Modal'
import { Switch, Route } from 'react-router-dom'
//import { ProductProvider } from './Context'
//import Modal from './Modal'
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
