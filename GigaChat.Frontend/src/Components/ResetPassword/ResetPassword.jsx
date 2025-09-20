import axios from 'axios';
import { useFormik } from 'formik';
import { ChevronLeft } from 'lucide-react';
import toast from 'react-hot-toast';
import { object, string } from 'yup';

const ResetPassword = (children) => {
    var validationSchema=object({
        email:string().required("Email is required").email("Email is invalid")
    })
    async function sendEmail(values){
        console.log("sendEmail : ")
        const loadingId =toast.loading("Waiting...")
        
        try {
            const options={
                url:"https://gigachat.tryasp.net/Auth/request-password-reset",
                method:"POST",
                data:values,
            }
            let {data}=await axios(options);
            children.setEmailOfForgotPassword(formik.values.email);
            children.setForgetPassword("step2");
        }catch(error){
            toast.error("Incorrect email or password ("+error+")");
            console.log("sendEmail : ",error)
        }finally{
            toast.dismiss(loadingId);
        }
    }
    const formik=useFormik({
        initialValues:{
                email:"",
        },
        validationSchema,
        onSubmit:sendEmail,
    })
    return (
        <div className=" relative inset-0 bg-[rgba(0,0,0,0.5)] min-h-screen flex justify-center items-center" onClick={(e)=>{if(e.target==e.currentTarget){children.setForgetPassword(null)}}}>
                                            <div className="  flex flex-col items-center font-Inter z-10 ">
                                                
                                                <div className="bg-[#171717]  w-[450px] h-[342px]  rounded-[14px]  ">
                                                <div className="px-[20px] mt-[25px] mb-[25px] space-x-[14px] flex items-center   text-[#FFFFFF]">
                                                <ChevronLeft size={35} className='cursor-pointer text-[#A1A1AA]  ' onClick={()=>{children.setForgetPassword(null)}}/>
                                                    <p className="text-[20px] font-bold ">
                                                    Reset Password
                                                    </p>
                                                </div>
                                                <p className="px-[32px]  text-[16px] font-medium text-[#A1A1AA] ">Enter your email address and we’ll send you a verification code to reset your password</p>
                                                <form action="" className=" px-[32px]" onSubmit={formik.handleSubmit}>
                                                    <input type="email" className="bg-[#27272A] py-[16px] mt-[27px] mb-[20px] px-[23px] w-[386px] h-[48px] rounded-[10px] text-[16px]     focus:outline-mainColor1 focus:text-[#9CA3AF] cursor-pointer"  placeholder='Email' name="email" value={formik.values.email} onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                                                    
                                                    <button type='submit' className="bg-mainColor1 hover:bg-[#b5a9e5] transition-all duration-150 text-black font-medium  py-[13px] px-4 rounded-[10px] text-[17px]   w-[386px] h-[48px]  ">Send verificiation code</button>
                                                </form>
                                                <p className="px-[32px]  text-[14px] font-medium text-[#A1A1AA] mt-[20px] text-center">Can’t find the email? Check your spam folder.</p>
                                                </div>
                                            </div>
        </div>
    );
}

export default ResetPassword;
