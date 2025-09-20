import axios from 'axios';
import { Mail, X } from 'lucide-react';
import { useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import Timer from "../Timer/Timer"
const VerficationEmail = (children) => {
    let [OnTimer,setOnTimer]=useState(false);
    async function ResendVerificationEmail(){
        const loadingId =toast.loading("Waiting...",{position:"top-left"});
        try{
            console.log(children.email)
            const option={
                url:" https://gigachat.tryasp.net/Auth/resend-confirmation-email ",
                method:"POST",
                data:{
                    "email":children.email
                },
            }
            let {data}= await axios(option);
            console.log("resend-confirmation-email")
            setOnTimer(true)
        }catch(error){
            console.log("resend-confirmation-email error",error)
            toast.error(error,{position:"top-left"});
        }
        finally{
            toast.dismiss(loadingId);
        }
    }
    useEffect(()=>{
        ResendVerificationEmail();
    },[])
    return (
        <section className='bg-[rgba(0,0,0,0.5)] z-10  absolute inset-0 min-h-screen flex justify-center items-center '  onClick={(e)=>{if(e.target==e.currentTarget){children.SetVerficiation(false)}}}>
            <div className="w-[450px] h-[410px] bg-[#27272A]  flex flex-col rounded-[14px]" >
                <X className=' ms-auto mt-[18px] mx-[18px]  text-[#A1A1AA]' onClick={(e)=>{children.SetVerficiation(false)}}/>
                
                <div className="px-[32px] flex flex-col items-center">
                    <Mail size={52} className='p-[13px] bg-[#252D28] text-mainColor2 rounded-[12px] mb-[18px]' />
                    <div className="space-y-[13px] text-center">
                        <h1 className="text-mainColor2 text-[30px] font-bold">Check your email</h1>
                        <p className="text-[#A1A1AA] text-[15px] font-medium">We’ve sent a verification link to:</p>
                        <p className="text-[#FFFFFF] text-[17px] font-bold">{children.email}</p>
                    </div>
                    <p className="mt-[15px] mb-[25px] text-[#A1A1AA]">
                    Click the link in the email to verify your address.
                    </p>
                    {OnTimer?
                    <Timer setOnTimer={setOnTimer}/>:<button type="button" className='w-full text-[#27272A] text-[15px] font-semibold bg-mainColor2  rounded-[10px] py-[14px]' onClick={()=>{ResendVerificationEmail()}} >Resend verficiation email</button>
                    }
                    <p className="mt-[18px] mb-[25px] text-[#A1A1AA] text-[14px] font-medium ">Can’t find the email? Check your spam folder.</p>
                </div>
            </div>
        </section>
    );
}

export default VerficationEmail;
