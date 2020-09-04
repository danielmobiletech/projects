import { ProductConsumer } from './Context'
import React, {  Component } from 'react'
import { Link } from 'react-router-dom'
import { ButtonContainer } from './Button'
import styled from 'styled-components'



  
const ModalContainer = styled.div`
position: fixed;
top: 0;
left: 0;
right: 0;
bottom: 0;
background: rgba(0, 0, 0, 0.3);
display: flex;
align-items: center;
justify-content: center;
`;


export default class Modal extends Component {


  render(){ return (
    
    <ProductConsumer>
      {(value) => {
        const { modalOpen, closeModal } = value
        const { img, title, price } = value.modalProduct
        
        if (!modalOpen) {
          return null
        } else {
          return(
          <ModalContainer>
            <div className='container'>
              <div className='row'>
                <div className='col-8 mx-auto col-md-6 col-lg-4 text-center text-capitalize'>
                  <h5>it added to cart</h5>
                  <img className='img-fluid' src={img} alt="this product"/>
                  <h5>{title}</h5>
                  <h4 className='text-muted'>price: $ {price}</h4>
                  <Link to="/">
                      <ButtonContainer onClick={()=>{closeModal()}}>
                          store

                      </ButtonContainer>
                  </Link>

                  <Link to="/cart">
                      <ButtonContainer cart onClick={()=>{closeModal()}}>
                          go to cart

                      </ButtonContainer>
                  </Link>
                </div>
              </div>
            </div>
          </ModalContainer>
          )
        }
      }}
     
    </ProductConsumer>
  )
}
}
