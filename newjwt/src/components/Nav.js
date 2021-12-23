import React, { useState, useEffect } from 'react';
import {Link} from 'react-router-dom';
import axios from 'axios';
export default function Nav(prop)
{

function logout()
{
    axios.post("https://jwtauthfrontend.azurewebsites.net/api/auth/logout",{},{
        withCredentials: true,
        headers: {
          "Access-Control-Allow-Origin": "https://jwtauthfrontend.azurewebsites.net",
          "Access-Control-Allow-Credentials": true
        }})
      .then((res)=>{console.log(res.data)})
      .catch((e)=>{});
    
}


let menu;
if(prop.name==='')
{
    menu=(
        <>
             <ul className="navbar-nav me-auto mb-2 mb-md-0">
              <li className="nav-item">
              <Link className="nav-link active"  to="/Login">Login</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link active"  to="/Register">Register</Link>
              </li>
              
            </ul>

        </>
    )
}
else{
    menu=(
        <>
             <ul className="navbar-nav me-auto mb-2 mb-md-0">
              <li className="nav-item">
              <Link className="nav-link active"  to="/" onClick={logout}>Logout</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link active"  to="/Register">Register</Link>
              </li>
              
            </ul>

        </>
    )
}







    return (

<>
        <nav className="navbar navbar-expand-md navbar-dark bg-dark mb-4">
        <div className="container-fluid">
         <Link to="/" className="navbar-brand">Home</Link>
      
          <div>
            {menu}
            
          </div>
        </div>
      </nav>
 </>
    );
}