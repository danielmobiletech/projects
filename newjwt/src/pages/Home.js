import React,{useEffect,useState} from "react";
import axios from "axios"
import { useNavigate } from "react-router";
export default function Home()
{
    let sends=useNavigate()
    let denied=false;
    axios.interceptors.response.use(response => {
        if(response.status === 401) {sends('/login');}
        return response;
    });
    const [loginstate,setLoginState]= useState({name:'',email:''});
    
    useEffect(()=>{



        
        
            
            // async()=>
            // {
    //await 
    try{
    axios.get('https://jwtauthfrontend.azurewebsites.net/api/auth/user',{
    withCredentials: true,
    headers: {
      "Access-Control-Allow-Origin": "https://jwtauthfrontend.azurewebsites.net",
      "Access-Control-Allow-Credentials": true
    }})
  .catch((e)=>{console.log("no worky")
denied=true;
sends('/login')
})
.then((res)=>{if(denied==false)
    
    {setLoginState({...res.data})}})

            
        
 
        
        
        
        
   
    //})()


    }
    catch(err){

        sends('/login')
    }

})         
    

    return( 
    
    <div>
        Welcome Back {loginstate.name}
    </div> 
    
    );
}
