import React, { Component } from 'react'
export default class Default extends Component {
  constructor(props)
  {
    
  }
  /**
   * This Page if a route 
   */
  render () {
    return (
      <div className='container'>
        <div className='row'>
          <div className='col-10 mx-auto text-center text-title text-uppercase pt-5'></div>
          <h1 className='display-3'>404</h1>
          <h1>error</h1>
          <h2>Page Not Found</h2>
          <h3>
            tech help<span>{this.props.location.pathname}</span>
          </h3>
        </div>
      </div>
    )
  }
}
