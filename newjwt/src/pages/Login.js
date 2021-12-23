import React, { createRef } from "react";
import { useState } from "react";
import {useForm} from "react-hook-form";
import axios from "axios";
import { useStore } from 'react-context-hook'
import {store} from 'react-context-hook';
import {useNavigate} from 'react-router-dom'
axios.defaults.withCredentials=true;






function Login(props)
{

  let sends=useNavigate();
  const [state,setState]=useState({email:"",password:""});
  const[redirct,setRedirect]=useState(false)




  function Posts(url,objs)
  {
    try{
    axios.post(url,objs,{
      withCredentials: true,
      headers: {
        "Access-Control-Allow-Origin": "https://jwtauthfrontend.azurewebsites.net",
        "Access-Control-Allow-Credentials": true
      }})
    .then((res)=>{
      props.setName(res.data.name)
    setRedirect(true)
    
    })
    .catch((e)=>{});
  
    }
    catch(err){

    }
  
  
   
    
    
  }







 
  //const [loginstatus,setloginstatus,deleteloginstatus] = useStore('loginstatus', false)
  //store.set('user', {name: 'piero', email: 'nappiero@spyna.it'})

 // console.log(loginstatus+" mini");
    const fullName=createRef(null);
    const { register, handleSubmit, watch, formState: { errors } } = useForm();


  const onSubmit = (e) => 
  {
    
    e.preventDefault();
    //setState({...state,name:"sss",password:"sssss"})
    Posts("https://jwtauthfrontend.azurewebsites.net/api/auth/login",state);
   
  };

if(redirct)
{
  sends("/")
}

    return(
    
    <>
   <main className="form-signin">
    <form onSubmit={onSubmit}> 
      
      <h1 className="h3 mb-3 fw-normal">Please sign in</h1>

      <div className="form-floating">
      <input  type="email" {...register("email",{pattern:{value:"^([a-z]){1,}@([a-z]){3,8}(\.)([a-z]){3,5}$",message:"email"}})}className="form-control" id="floatingInput" placeholder="name@example.com" onChange={(e)=>{setState({...state,email:e.target.value}); console.log(state);}}/>
        <label for="floatingInput">Email address</label>
      </div>
      <div className="form-floating">
      <input type="password"  {...register("password",{required:true,pattern:"^[a-zA-z]+$/i"})} className="form-control" id="floatingPassword" placeholder="Password"   onChange={(e)=>{setState({...state,password:e.target.value}); console.log(state);}}/>
        <label for="floatingPassword">Password</label>
      </div>

      <div className="checkbox mb-3">
        <label>
          <input type="checkbox" value="remember-me"/> Remember me please
        </label>
      </div>
      <button className="w-100 btn btn-lg btn-primary" type="submit" onClick={onSubmit}>Sign in</button>
      <p className="mt-5 mb-3 text-muted">© 2017–2021</p>
    </form>
  </main>
    
    
    </>);
}

export default Login;