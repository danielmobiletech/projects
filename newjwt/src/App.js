import logo from './logo.svg';
import './App.css';
import Login from "./pages/Login";
import { Route ,Routes} from "react-router-dom";
import Register from "./pages/Register";
import Home from "./pages/Home";
import Cookies from 'js-cookie';
import { withStore} from 'react-context-hook'
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Nav from './components/Nav'
function App() {
  const [loginstatus,setloginstatus]=useState('');




  useEffect(()=>{

axios.get('https://jwtauthfrontend.azurewebsites.net/api/auth/user',{headers: {
  "Access-Control-Allow-Origin": "https://jwtauthfrontend.azurewebsites.net",
  "Access-Control-Allow-Credentials": true
}}).then(e=>setloginstatus(e.data.name))






  })
//   useEffect(()=>{axios.get('http://localhost:8089/api/auth/userstate',{
//     withCredentials: true,
//     headers: {
//       "Access-Control-Allow-Origin": "http://localhost:8089",
//       "Access-Control-Allow-Credentials": true
//     }})
//   .catch(e=>console.log(e))
//   .then((res)=>{setloginstatus(res.data.loginstate);
//     console.log(res.data)})

// })
return (
  <>
    <div className="App">
        
        <Nav name={loginstatus}  setName={setloginstatus}/>

        <main className="form-signin">
<Routes>

<Route path="/" element={<Home name={loginstatus}/>}/>
<Route  path="/Login" element={<Login setName={setloginstatus}/>}/>
<Route path="/Register" element={<Register/>}/>
</Routes>
</main>
       
  </div>


            

          
  

</>

    
  );
}

export default withStore(App);
