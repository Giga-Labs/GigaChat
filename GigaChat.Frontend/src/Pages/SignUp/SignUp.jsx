
import StarryBackground from '../../Components/StarryBackground/StarryBackground';
import logo from "../../assets/logo/GigaChatLogo.png"
import googleLogo from "../../assets/logo/google-icon.png"
import { object, ref, string } from "yup";
import VerficationEmail from '../../Components/VerficationEmail/VerficationEmail';
import { useState } from 'react';
import toast from 'react-hot-toast';
import axios from 'axios';
import { useFormik } from 'formik';
import { useNavigate } from 'react-router-dom';
const SignUp = () => {
    let navigate=useNavigate()
    const [verficiation,SetVerficiation]=useState(false);
    const validationSchema=object({
        userName:string().required("Name is required").min(3,"Name must be at least 3 characters").max(32,"Name can not be than 32 characters"),
        email:string().required("Email is required").email("Email is not a valid email address."),
        password:string().required("Password is required").matches(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$/,"password should be at least eight characters, at least one upper case English letter, one lower case English letter, one number and one special character"),
        rePassword:string().required("Confirm Password is required").oneOf([ref("password")],"Password and Confirm Password should be the same"),
        firstName:string().required("firstName is required").min(3,"Name must be at least 3 characters").max(100,"Name can not be than 100 characters"),
        lastName:string().required("lastName is required").min(3,"Name must be at least 3 characters").max(100,"Name can not be than 100 characters"),
    }) ;
    async function sendDataToRegister(values){
        const loadingId =toast.loading("Waiting...");
        try{
            const option={
                url:"https://gigachat.tryasp.net/Auth/register",
                method:"POST",
                data:values,
            }
            let {data}= await axios(option);
            console.log(data);
            toast.success("The Account has been created successfully.")
            SetVerficiation(true);
        }catch(error){
            console.log(error);
            toast.error("There is an error creating the account.")
        }
        finally{
            toast.dismiss(loadingId);
        }
    }
    const formik=useFormik({
        initialValues:{
                userName:"",
                email:"",
                password:"",
                rePassword:"",
                firstName: "",
                lastName:"",
        },
        validationSchema,
        onSubmit:sendDataToRegister,
    });
    return (
        <>
            <div className=" relative">
                <StarryBackground/>
                <div className=" relative inset-0 min-h-screen flex justify-center items-center">
                                    <div className="  flex flex-col items-center font-Inter z-10 ">
                                        <img src={logo} alt="" className=" w-[230.42px] my-[35px] " />
                                        <div className="bg-[#171717] w-[450px] h-[600px]  rounded-[14px] ">
                                        <p className="text-mainColor1 text-center text-[34px]  font-bold mt-[25px]  mb-[22px] ">Register</p>
                                        <form action="" className="text-[#A1A1AA] px-[32px] space-y-[13px]" onSubmit={formik.handleSubmit}>
                                            <input type="text" className="bg-[#27272A] py-[16px]  px-[23px] w-[386px] h-[48px] rounded-[10px] text-[16px]     focus:outline-mainColor1 focus:text-[#A1A1AA] cursor-pointer"  placeholder='Username' name="userName" value={formik.values.userName} onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                                            <div className="grid grid-cols-2 gap-[16px] ">
                                            <input type="text" className="bg-[#27272A] py-[16px]  px-[23px] w-[185px] h-[48px] rounded-[10px] text-[16px]   focus:outline-mainColor1 focus:text-[#A1A1AA] cursor-pointer"  placeholder='First Name' name="firstName" value={formik.values.firstName} onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                                            <input type="text" className="bg-[#27272A] py-[16px]  px-[23px] w-[185px] h-[48px] rounded-[10px] text-[16px]   focus:outline-mainColor1 focus:text-[#A1A1AA] cursor-pointer"  placeholder='Last Name' name="lastName" value={formik.values.lastName} onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                                            </div>
                                            <input type="email" className="bg-[#27272A] py-[16px]  px-[23px] w-[386px] h-[48px] rounded-[10px] text-[16px]     focus:outline-mainColor1 focus:text-[#A1A1AA] cursor-pointer"  placeholder='Email' name="email" value={formik.values.email} onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                                            <input type="password" className="bg-[#27272A] py-[16px]  px-[23px] w-[386px] h-[48px] rounded-[10px] text-[16px]     focus:outline-mainColor1 focus:text-[#A1A1AA] cursor-pointer"  placeholder='Password' name="password" value={formik.values.password} onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                                            <input type="password" className="bg-[#27272A] py-[16px]  px-[23px] w-[386px] h-[48px] rounded-[10px] text-[16px]     focus:outline-mainColor1 focus:text-[#A1A1AA] cursor-pointer"  placeholder='Confirm Password' name="rePassword" value={formik.values.rePassword} onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                                            {<p className="text-red-500 mt-1 mx-6 text-ms">{formik.errors.userName||formik.errors.firstName||formik.errors.lastName||formik.errors.email||formik.errors.password ||formik.errors.rePassword}</p>}
                                            <button type='submit' className="bg-mainColor1 hover:bg-[#b5a9e5] transition-all duration-150 text-black font-medium  py-[13px] px-4 rounded-[10px] text-[17px]   w-[386px] h-[48px]  ">Register</button>
                                        </form>
                                        <div className="px-[32px] ">
                                            <div className="flex justify-center items-center space-x-2 mt-[14px] mb-[12px]">
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
                                            <div className="flex flex-col justify-center items-center  text-[14px]  text-[#A1A1AA]">
                                                <div className="flex  space-x-2 mt-[22px] text-[16px]">
                                                    <p className="">Already have an account?</p>
                                                    <p className=' text-mainColor1 cursor-pointer' onClick={()=>{navigate("/Login")}}>Login here</p>
                                                </div>
                                            </div>
                                        </div>
                    
                                        </div>
                                    </div>
                </div>
                {
                    verficiation?<VerficationEmail email={formik.values.email} SetVerficiation={SetVerficiation}/>:""
                }
                </div>
            
            
        </>
    );
}

export default SignUp;
