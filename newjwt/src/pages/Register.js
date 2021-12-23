import React, { createRef } from "react";
import { useState } from "react";
import {useForm} from "react-hook-form";
import axios from "axios";
import { useStore,useGetAndSet,store} from 'react-context-hook'
import {useNavigate} from 'react-router-dom'; 
const headers = {
  'Content-Type': 'application/json',
  "Access-Control-Allow-Origin": "*"
}

function Register()
{


  function Posts(url,objs)
{
  
  axios.post(url,objs,headers)
  .then((res)=>{console.log(res.data);setRegistered(true)})
  .catch((e)=>{console.log(e)});
  
}
  //const [loginstatus,setloginstatus,deleteloginstatus] = useStore('loginstatus');
  
  const [state,setState]=useState({name:"",email:"bob@gmail.com",password:""});

  const [registered,setRegistered]=useState(false);



//console.log(store.getState() +" ddd ");
    const fullName=createRef(null);
    const { register, handleSubmit, watch, formState: { errors } } = useForm();


  const onSubmit = (e) => 
  {
    
    e.preventDefault();
    //setState({...state,name:"sss",password:"sssss"})
    Posts("https://jwtauthfrontend.azurewebsites.net/api/auth/register",    state )
   
  };

let sends=useNavigate()
  if(registered==true)
        {
          sends('/login')
        }

    return (<>
    

    <main className="form-signin">
    <form onSubmit={onSubmit}>
     
      <h1 className="h3 mb-3 fw-normal">Sign Up This Is Practice Jwt Sign Up Form</h1>

      <div className="form-floating">
        <input ref={fullName} {...register('fullname',{maxLength:15,minLength:3})} className="form-control" id="floatingPassword" placeholder="Name" onChange={(e)=>{setState({...state,name:e.target.value}); console.log(state);}}/>
        <label for="floatingPassword">Name</label>
      </div>
      <div className="form-floating">
        <input  type="email" {...register("email",{pattern:{value:"^([a-z]){1,}@([a-z]){3,8}(\.)([a-z]){3,5}$",message:"email"}})}className="form-control" id="floatingInput" placeholder="name@example.com" onChange={(e)=>{setState({...state,email:e.target.value}); console.log(state);}}/>
        <label for="floatingInput">Email address</label>
      </div>
      <div className="form-floating">
        <input type="password"  {...register("password",{required:true,pattern:"^[a-zA-z]+$/i"})} className="form-control" id="floatingPassword" placeholder="Password"   onChange={(e)=>{setState({...state,password:e.target.value}); console.log(state);}}/>
        <label for="floatingPassword">Password</label>
      </div>

     

     
      <button onClick={onSubmit} className="w-100 btn btn-lg btn-primary" type="submit">Sign in</button>
      <p className="mt-5 mb-3 text-muted">© 2017–2021</p>
    </form>
  </main>
    
    </>);
} 

export default Register;