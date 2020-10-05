import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import logo from './logo.svg'
import styled from 'styled-components'
import {ButtonContainer} from './Button'
/*
   Organizes Navigation Links For Home, Products, and Cart
*/
export default class Navbar extends Component {
  
  render () {
    console.log(logo)
    return (
      <NavWrapper className='navbar navbar-expand-sm navbar-dark px-sm-5'>
        <Link to='/'>
          <img src={logo} alt='store' className="navbar-brand"/>
        </Link>
        <ul className='navbar-nav align-items-center'>
          <li className='nav-item ml-5'>
            <Link to='/' className='nav-link'>
              products
            </Link>
          </li>
        </ul>

        <Link to='/cart' className='ml-auto'>
          <ButtonContainer>
            <span className="mr-2">

            <i className='fas fa-cart-plus' />
            </span>
            my cart
          </ButtonContainer>
        </Link>
      </NavWrapper>
    )
  }
}

//this Designs the css of the NavWrapper background and fonts deisgn that inside the wrapper
const NavWrapper = styled.nav`
  background: var(--lightBlue);
  .nav-link {
    color: var(--mainWhite) !important;
    font-size: 1.3rem;
    text-transform: capitalize;
  }
`