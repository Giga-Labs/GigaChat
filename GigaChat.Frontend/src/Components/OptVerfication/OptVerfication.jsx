import {  ChevronLeft, KeyRound } from 'lucide-react';
import Check from '../Check/Check';
import { object, string } from 'yup';
import toast from 'react-hot-toast';
import axios from 'axios';
import { useFormik } from 'formik';
import { useEffect } from 'react';
const OptVerfication = (children) => {
    console.log(children.emailOfForgotPassword)
    var validationSchema=object({
            email:string().required("Email is required"),
            otpCode:string().required("OTP is required")
        })
        async function sendOTP(values){
            console.log("sendOTP : ")
            const loadingId =toast.loading("Waiting...")
            
            try {
                const options={
                    url:"https://gigachat.tryasp.net/Auth/verify-password-reset-otp",
                    method:"POST",
                    data:values,
                }
                let {data}=await axios(options);
                console.log("sendOTP done")
                children.setTokenRsetpassword(data);
                children.setForgetPassword("step3");
            }catch(error){
                toast.error("Incorrect email or password ("+error+")");
                console.log(error)
            }finally{
                toast.dismiss(loadingId);
            }
        }
        const formik=useFormik({
            initialValues:{
                email:"",
                otpCode:""
            },
            validationSchema,
            onSubmit:sendOTP,
        })
        useEffect(()=>{
                formik.values.email=children.emailOfForgotPassword
                formik.values.otpCode=children.otpValue.join("")
            },[children.otpValue])
        
    return (
        <div className=" relative inset-0 bg-[rgba(0,0,0,0.5)] min-h-screen flex justify-center items-center"  onClick={(e)=>{if(e.target==e.currentTarget){children.setForgetPassword(null)}}}>
            <div className="  flex flex-col items-center font-Inter z-10 ">
                
                <div className="bg-[#171717] w-[450px] h-[410px] text-center  rounded-[14px]  ">
                <ChevronLeft size={35} className='text-[#A1A1AA] mt-[20px] ml-[22px] cursor-pointer' onClick={()=>{children.setForgetPassword("step1")}}/>
                <div className="flex flex-col items-center">
                    <KeyRound size={58} className='p-[13px] bg-[#3A3744] text-mainColor1 rounded-[12px]' />
                    <div className="px-[32px] mt-[18px] mb-[20px] space-x-[18px] flex items-center  text-[#FFFFFF]">
                        <p className="text-[24px] text-mainColor1 font-bold">
                        Enter verification code
                        </p>
                    </div>
                    <p className="px-[32px]  text-[16px] font-medium text-[#A1A1AA] mb-[27px]">We’ve sent a 6-digit code to <span className='text-[#FFFFFF]'>giga@example.com</span></p>
                </div>
                <form action="" className=" px-[32px]" onSubmit={formik.handleSubmit}>
                    <div className="mb-[27px] ">
                        <Check otpValue={children.otpValue} setOtpValue={children.setOtpValue}/>
                    </div>
                    <button type='submit' className="bg-mainColor1 hover:bg-[#b5a9e5] transition-all duration-150 text-black font-medium  py-[13px] px-4 rounded-[10px] text-[17px]   w-full h-[48px]  " >Verify code</button>
                </form>
                <p className="px-[32px]  text-[14px] font-medium text-[#A1A1AA] mt-[18px]">
                    Didn’t receive the code?<span className='text-mainColor1 font-medium cursor-pointer'> Resend code </span>
                </p>
                </div>
            </div>
        </div>
    );
}

export default OptVerfication;
