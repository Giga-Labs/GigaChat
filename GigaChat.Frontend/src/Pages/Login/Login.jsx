import React, { useContext, useState } from 'react';
import StarryBackground from '../../Components/StarryBackground/StarryBackground';
import logo from "../../assets/logo/GigaChatLogo.png"
import googleLogo from "../../assets/logo/google-icon.png"
import { useNavigate } from 'react-router-dom';
import VerficationEmail from '../../Components/VerficationEmail/VerficationEmail';
import { object, string } from 'yup';
import toast from 'react-hot-toast';
import { userContext } from '../../Context/User.context';
import axios from 'axios';
import { useFormik } from 'formik';
import ResetPassword from '../../Components/ResetPassword/ResetPassword';
import OptVerfication from '../../Components/OptVerfication/OptVerfication';
import CreateNewPassword from '../../Components/CreateNewPassword/CreateNewPassword';
CreateNewPassword
const Login = () => {
    let [tokenRsetpassword,setTokenRsetpassword]=useState(null)
    const navigate=useNavigate();
    const {setToken,setRefreshToken,setUserId}=useContext(userContext);
    let [forgetPassword,setForgetPassword]=useState(null)
    let [emailOfForgotPassword,setEmailOfForgotPassword ]=useState(null);
    // let [otpOfForgotPassword,setOtpOfForgotPassword ]=useState(null);
    let [otpValue,setOtpValue]=useState(["","","","","",""])
    const validationSchema=object({
        email:string().required("Email is required").email("Email is invalid"),
        password:string().required("Password is required"),
    });
    async function sendDataToLogin(values){
        console.log("sendDataToLogin : ")
        const loadingId =toast.loading("Waiting...")
        
        try {
            const options={
                url:"https://gigachat.tryasp.net/Auth",
                method:"POST",
                data:values,
            }
            let {data}=await axios(options);
            if(data.token && data.refreshToken){
                setToken(data.token);
                setRefreshToken(data.refreshToken);
                setUserId(data.id)
                toast.success("User Logged in successfully");
                localStorage.setItem("token",data.token);
                localStorage.setItem("refreshToken",data.refreshToken);
                localStorage.setItem("userId",data.id)
                console.log(data)
                setTimeout(()=>{navigate("/ConversationMain")},500);
            }
            
        }catch(error){
            toast.error("Incorrect email or password ",error);
            console.log(error)
        }finally{
            toast.dismiss(loadingId);
        }
    }
    
    // async function RefreshToken(){
    //     console.log("RefreshToken : ")
    //         const loadingId =toast.loading("Waiting...")
    //         try {
    //             const options={
    //                 url:"https://agrivision.tryasp.net/Auth/refresh",
    //                 method:"POST",
    //                 data:{
    //                     token:token,
    //                     refreshToken:refreshToken,
    //                 },
    //             }
    //             let {data}=await axios(options);
    //                 setToken(data.token);
    //                 localStorage.setItem("token",data.token);
    //                 setRefreshToken(data.refreshToken);
    //                 console.log("RefreshToken : end :"+token)
                    
    //             }
    //             catch(error){
    //             toast.error("Incorrect email or password ("+error+")");
    //             console.log(error)
                
    //         }finally{
    //             toast.dismiss(loadingId);
    //         }
    //     }
    //     useEffect(()=>{
    //         if(token&&refreshToken){
    //             const counter=0;
    //             counter=setTimeout(() => {RefreshToken()}
    //             ,((6-5)*1000*6))
    //             clearTimeout(counter)
    //         }
    //     }
    //     ,[token,refreshToken])
    const formik=useFormik({
        initialValues:{
                email:"",
                password:"",
        },
        validationSchema,
        onSubmit:sendDataToLogin,
    });
    return (
        <>
            <div className="relative ">
                <StarryBackground/>
                <div className=" relative inset-0 min-h-screen flex justify-center items-center">
                    <div className="  flex flex-col items-center font-Inter z-10 ">
                        <img src={logo} alt="" className=" w-[230.42px] mb-[50px]" />
                        <div className="bg-[#171717] w-[450px] h-[494px]  rounded-[14px] ">
                        <p className="text-mainColor2 text-center text-[34px]  font-bold mt-4  mb-5">Login</p>
                        <form action="" className=" px-[32px] text-[#A1A1AA] space-y-[15px]" onSubmit={formik.handleSubmit}>
                            <input type="email" className="bg-[#27272A] pt-[16px] pb-[17px] px-[23px] w-[386px] h-[50px] rounded-[10px] text-[17px]     focus:outline-mainColor2 focus:text-[#A1A1AA] cursor-pointer"  placeholder='Email' name="email" value={formik.values.email} onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                            <input type="password" className="bg-[#27272A] pt-[16px] pb-[17px] px-[23px] w-[386px] h-[50px] rounded-[10px] text-[17px]   focus:outline-mainColor2 focus:text-[#A1A1AA] cursor-pointer"  placeholder='Password' name="password" value={formik.values.password} onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                            {<p className="text-red-500 mt-1 mx-6 text-ms">{formik.errors.email||formik.errors.password}</p>}
                            <button type='submit' className="bg-mainColor2 hover:bg-[#86c59b] transition-all duration-150 text-black font-medium  py-3 px-4 rounded-[10px] text-[17px]  w-full  ">Login</button>
                        </form>
                        <div className="px-[32px] mt-[23px] mb-[22px]">
                            <div className="flex justify-center items-center space-x-2 my-2">
                                <div className="w-[140px] h-[1px] bg-[#27272A]  rounded-md "></div>
                                <p className="text-[10px] text-[#A1A1AA] ">Or Login With</p>
                                <div className="w-[140px] h-[1px] bg-[#27272A]  rounded-md "></div>
                            </div>
                            <div className="grid grid-cols-2 gap-[18px] ">
                                <div className="flex justify-center items-center space-x-2 w-[184px] h-[50px] bg-[#27272A] rounded-[10px] cursor-pointer ">
                                    <img src={googleLogo} alt="" className="w-[30px]" />
                                    <p className="text-[#FFFFFF] text-[16px] font-semibold">Google</p>
                                </div>
                                <div className="flex justify-center items-center space-x-2 w-[184px] h-[50px] bg-[#27272A] rounded-[10px] text-[#FFFFFF] cursor-pointer">
                                    <i className="fa-brands fa-github text-[28px]"></i>
                                    <p className="text-[16px] font-semibold">GitHub</p>
                                </div>
                                
                            </div>
                            <div className="flex flex-col justify-center items-center  text-[16px]  text-[#A1A1AA]">
                                <p className=" hover:text-mainColor2 transition-all duration-150 cursor-pointer mt-[28px] mb-[14px]" onClick={()=>{setForgetPassword("step1")}}>Forgot password?</p>
                                <div className="flex  space-x-2">
                                    <p className="">Don’t have an account?</p>
                                    <p  className=' text-mainColor2 cursor-pointer' onClick={()=>{navigate("/SignUp")}}>Register here</p>
                                </div>
                            </div>
                        </div>

                        </div>
                    </div>
                </div>
                
            </div>
            {forgetPassword=="step1"?<div className=" absolute inset-0 z-20 " ><ResetPassword setForgetPassword={setForgetPassword} setEmailOfForgotPassword={setEmailOfForgotPassword}/></div> :
            forgetPassword=="step2"?<div className="absolute inset-0 z-20" ><OptVerfication setTokenRsetpassword={setTokenRsetpassword} setForgetPassword={setForgetPassword} emailOfForgotPassword={emailOfForgotPassword} otpValue ={otpValue} setOtpValue={setOtpValue} /></div>:
            forgetPassword=="step3"?<div className=" absolute inset-0 z-20"><CreateNewPassword tokenRsetpassword={tokenRsetpassword} setForgetPassword={setForgetPassword} emailOfForgotPassword={emailOfForgotPassword}/></div>:""
                    // optLogin?<div className=" absolute inset-0"><OPTLogin/></div>:""
    }

        </>
    );
} 

export default Login;
