
import axios from "axios";
import { createContext, useEffect, useState } from "react";
import toast from "react-hot-toast";
// import toast from "react-hot-toast";

export const userContext =createContext("");
export default function UserProvider(items){
    let children=items.children
    const [token,setToken]=useState(localStorage.getItem("token"));
    // const [userId,setUserId]=useState();
    const [refreshToken,setRefreshToken]=useState(localStorage.getItem("refreshToken"));
    const [userId,setUserId]=useState(localStorage.getItem("userId"));
    const [expiresIn,setExpiresIn]=useState(null)
    // const [verification,setVerification]=useState(false);
    // const [refreshToken,setRefreshToken]=useState(null);
    // const [expiresIn,setExpiresIn]=useState(null)
    function logOut(){
        setToken(null);
        setRefreshToken(null)
        setUserId(null)
        localStorage.removeItem("token");
        localStorage.removeItem("refreshToken");
    }
    async function RefreshToken(){
        console.log("RefreshToken : ")
            const loadingId =toast.loading("Waiting...")
            try {
                const options={
                    url:"https://gigachat.tryasp.net/Auth/refresh",
                    method:"POST",
                    data:{
                        token:token,
                        refreshToken:refreshToken,
                    },
                }
                let {data}=await axios(options);
                    setToken(data.token);
                    localStorage.setItem("token",data.token);
                    localStorage.setItem("refreshToken",data.refreshToken);
                    setRefreshToken(data.refreshToken);
                    console.log("RefreshToken : end :"+token)
                    
                }
                catch(error){
                toast.error("Incorrect email or password ("+error+")");
                console.log(error)
                
            }finally{
                toast.dismiss(loadingId);
            }
        }
        useEffect(()=>{
            let intervalId;
            if(token&&refreshToken){
                intervalId=setInterval(() => {RefreshToken()}
                ,(expiresIn*1000*6-3))
            }
            clearInterval(intervalId)
        }
        ,[token,refreshToken])
    return <userContext.Provider value={{token,setToken,logOut,refreshToken,setRefreshToken,expiresIn,setExpiresIn,userId,setUserId}}>
        {children}
    </userContext.Provider>
}