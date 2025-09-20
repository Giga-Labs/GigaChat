import { ChevronLeft } from 'lucide-react';
import { Children, useContext } from 'react';
import toast from 'react-hot-toast';
import { object, ref, string } from 'yup';
import { userContext } from '../../Context/User.context';
import axios from 'axios';
import { useFormik } from 'formik';
const ChangePassword = (children) => {
    const { token } = useContext(userContext);
     const validationSchema=object({
        currentPassword:string().required("Password is required").matches(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$/,"password should be at least eight characters, at least one upper case English letter, one lower case English letter, one number and one special character"),
        newPassword:string().required("Password is required").matches(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$/,"password should be at least eight characters, at least one upper case English letter, one lower case English letter, one number and one special character"),
        rePassword:string().required("Confirm Password is required").oneOf([ref("newPassword")],"Password and Confirm Password should be the same"),
        }) ;
        async function updateProfile(values){
            const loadingId =toast.loading("Waiting...");
            try{
                const option={
                    url:"https://gigachat.tryasp.net/Profiles/change-password",
                    method:"POST",
                    data:values,
                    headers: {
                        Authorization: `Bearer ${token}`,
                    }
                }
                let {data}= await axios(option);
                console.log(data);
                toast.success("The Account has been created successfully.")
                children.setprofiles(null)
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
                currentPassword:"",
                newPassword:"",
                rePassword:""
            },
            validationSchema,
            onSubmit:updateProfile,
        });
    return (
        <div className=" relative inset-0 bg-[rgba(0,0,0,0.5)] min-h-screen flex justify-center items-center" onClick={(e)=>{if(e.target==e.currentTarget){children.setprofiles(null)}}}>
            <div className="  flex flex-col items-center font-Inter z-10 ">
                
                <div className="bg-[#171717]  w-[450px] h-[379px]  rounded-[14px]  ">
                <div className="px-[32px] mt-[28px] mb-[31px] space-x-[18px] flex items-center  text-[#FFFFFF]">
                <ChevronLeft size={35} onClick={()=>{children.setprofiles("profile")}}/>
                    <p className="text-[20px] font-bold">
                    Change Password
                    </p>
                </div>
                <p className="px-[32px]  text-[16px] font-medium text-[#A1A1AA] mb-[18px]">Please enter your new password below</p>
                <form action="" className=" px-[32px] space-y-[13px]" onSubmit={formik.handleSubmit}>
                    <input type="password" className="bg-[#27272A] py-[16px]  px-[23px] w-[386px] h-[48px] rounded-[10px] text-[16px]     focus:outline-mainColor1 focus:text-[#9CA3AF] cursor-pointer"  placeholder='Old Password' name="currentPassword" value={formik.values.currentPassword}  onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                    <input type="password" className="bg-[#27272A] py-[16px]  px-[23px] w-[386px] h-[48px] rounded-[10px] text-[16px]     focus:outline-mainColor1 focus:text-[#9CA3AF] cursor-pointer"  placeholder='New Password' name="newPassword" value={formik.values.newPassword}  onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                    <input type="password" className="bg-[#27272A] py-[16px]  px-[23px] w-[386px] h-[48px] rounded-[10px] text-[16px]     focus:outline-mainColor1 focus:text-[#9CA3AF] cursor-pointer"  placeholder='Confirm New Password' name="rePassword" value={formik.values.rePassword}  onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                    
                    <button type='submit' className="bg-mainColor1 hover:bg-[#b5a9e5] transition-all duration-150 text-black font-medium  py-[13px] px-4 rounded-[10px] text-[17px]   w-[386px] h-[48px]  ">Change Password</button>
                </form>
                            
                </div>
            </div>
        </div>
    );
}

export default ChangePassword;
