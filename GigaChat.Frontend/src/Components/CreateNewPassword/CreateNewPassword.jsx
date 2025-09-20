import axios from 'axios';
import { useFormik } from 'formik';
import { ChevronLeft } from 'lucide-react';
import { useEffect } from 'react';
import toast from 'react-hot-toast';
import { object, ref, string } from 'yup';
const CreateNewPassword = (children) => {
    var validationSchema=object({
        token:string(),
        newPassword:string().required("New Password is required"),
        ConfirmPassword:string().required("Confirm Password is required").oneOf([ref("newPassword")],"Passwords must match")
    })
    async function sendResetPassword(values){
        console.log("sendResetPassword : ")
        const loadingId =toast.loading("Waiting...")
        
        try {
            const options={
                url:"https://gigachat.tryasp.net/Auth/password-reset",
                method:"POST",
                data:values,
            }
            let {data}=await axios(options);
            console.log(data)
            toast.success("The password has been changed successfully.")
            children.setForgetPassword(null)
        }catch(error){
            toast.error("Incorrect email or password ("+error+")");
            console.log("sendResetPassword error : " ,error)
        }finally{
            toast.dismiss(loadingId);
        }
    }
    const formik=useFormik({
        initialValues:{
                token:"",
                newPassword:"",
                ConfirmPassword:"",
        },
        validationSchema,
        onSubmit:sendResetPassword,
    })
    useEffect(()=>{formik.values.token=children.tokenRsetpassword,[]})
    return (
        <div className=" relative inset-0 bg-[rgba(0,0,0,0.5)] min-h-screen flex justify-center items-center"  onClick={(e)=>{if(e.target==e.currentTarget){children.setForgetPassword(null)}}}>
            <div className="  flex flex-col items-center font-Inter z-10 ">
                
                <div className="bg-[#171717]  w-[450px] h-[342px]  rounded-[14px]  ">
                <div className="px-[32px] mt-[28px] mb-[38px] space-x-[18px] flex items-center  text-[#FFFFFF]">
                <ChevronLeft size={35} onClick={()=>{children.setForgetPassword("step2")}}/>
                    <p className="text-[20px] font-bold">
                    Create New Password
                    </p>
                </div>
                <p className="px-[32px]  text-[16px] font-medium text-[#A1A1AA] mb-[18px]">Please enter your new password below</p>
                <form action="" className=" px-[32px] space-y-[13px]" onSubmit={formik.handleSubmit}>
                    <input type="password" className="bg-[#27272A] py-[16px]  px-[23px] w-[386px] h-[48px] rounded-[10px] text-[16px]     focus:outline-mainColor1 focus:text-[#9CA3AF] cursor-pointer"  placeholder='New Password'  name="newPassword" value={formik.values.newPassword} onBlur={formik.handleBlur} onChange={formik.handleChange}/>
                    <input type="password" className="bg-[#27272A] py-[16px]  px-[23px] w-[386px] h-[48px] rounded-[10px] text-[16px]     focus:outline-mainColor1 focus:text-[#9CA3AF] cursor-pointer"  placeholder='Confirm New Password'  name="ConfirmPassword" value={formik.values.ConfirmPassword} onBlur={formik.handleBlur} onChange={formik.handleChange}/>
                    
                    <button type='submit' className="bg-mainColor1 hover:bg-[#b5a9e5] transition-all duration-150 text-black font-medium  py-[13px] px-4 rounded-[10px] text-[17px]   w-[386px] h-[48px]  ">Reset Password</button>
                </form>
                            
                </div>
            </div>
        </div>
    );
}

export default CreateNewPassword;
