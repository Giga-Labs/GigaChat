import { ChevronLeft } from 'lucide-react';
import React, { useContext } from 'react';
import CustomAvatar from '../CustomAvatar/CustomAvatar';
import { bool, object, ref, string } from 'yup';
import toast from 'react-hot-toast';
import axios from 'axios';
import { useFormik } from 'formik';
import { userContext } from '../../Context/User.context';

const EditProfile = (children) => {
    const { token } = useContext(userContext);
    const validationSchema=object({
        userName:string().required("Name is required").min(3,"Name must be at least 3 characters").max(32,"Name can not be than 32 characters"),
        email:string().required("Email is required").email("Email is not a valid email address."),
        firstName:string().required("firstName is required").min(3,"Name must be at least 3 characters").max(100,"Name can not be than 100 characters"),
        lastName:string().required("lastName is required").min(3,"Name must be at least 3 characters").max(100,"Name can not be than 100 characters"),
    }) ;
    async function updateProfile(values){
        const loadingId =toast.loading("Waiting...");
        try{
            const option={
                url:"https://gigachat.tryasp.net/Profiles",
                method:"PUT",
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
                userName:"",
                email:"",
                firstName: "",
                lastName:"",
                allowGroupInvites:false,
                twoFactorEnabled:false,
        },
        validationSchema,
        onSubmit:updateProfile,
    });
        function check(e){
            if(e.classList.contains('bg-[#27272A]'))
                e.classList.replace('bg-[#27272A]','bg-[#A8F0C0]')
            else{e.classList.replace('bg-[#A8F0C0]','bg-[#27272A]')}
            e.firstElementChild.classList.toggle("right-[3px]")
        }
    return (
        <div className=" relative inset-0  bg-[rgba(0,0,0,0.5)] min-h-screen flex justify-center items-center" onClick={(e)=>{if(e.target==e.currentTarget){children.setprofiles(null)}}}>
                                            <div className="bg-[#1E1E1E]  flex flex-col items-center font-Inter z-10 rounded-[14px] ">
                                                
                                                <div className="  w-[450px] h-[623px]  rounded-[14px]  ">
                                                <div className="px-[20px] mt-[25px]  space-x-[14px] flex items-center   text-[#FFFFFF]">
                                                <ChevronLeft size={35} className='cursor-pointer text-[#A1A1AA]  ' onClick={()=>{
                                                            children.setprofiles("profile")
                                                        }}/>
                                                    <p className="text-[20px] font-bold ">
                                                    Profile
                                                    </p>
                                                </div>
                                                <div className="flex justify-center items-center mt-[12px] mb-[19px]">
                                                    <CustomAvatar size={80} />
                                                </div>
                                                <form action="" className=" px-[32px] " onSubmit={formik.handleSubmit}>
                                                    <div className="Input">
                                                        <div className="UsernameDiv">
                                                            <label  className='text-[12px] font-medium text-[#9CA3AF]'>Username</label>
                                                            <input type="text"  className="bg-[#27272A] py-[16px]   px-[23px] w-full h-[48px] rounded-[10px] text-[16px] font-semibold   focus:outline-mainColor2 text-[#FFFFFF] cursor-pointer"  name="userName"  value={formik.values.userName} onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                                                        </div>
                                                        <div className="grid grid-cols-2 gap-[16px]">
                                                            <div className="FirstNameDiv">
                                                                <label  className='text-[12px] font-medium text-[#9CA3AF]'>First Name</label>
                                                                <input type="text"  className="bg-[#27272A] py-[16px]   px-[23px] w-full h-[48px] rounded-[10px] text-[16px] font-semibold   focus:outline-mainColor2 text-[#FFFFFF] cursor-pointer"  name="firstName" value={formik.values.firstName} onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                                                            </div>
                                                            <div className="LastName">
                                                                <label className='text-[12px] font-medium text-[#9CA3AF]'>Last Name</label>
                                                                <input type="text"  className="bg-[#27272A] py-[16px]   px-[23px] w-full h-[48px] rounded-[10px] text-[16px] font-semibold   focus:outline-mainColor2 text-[#FFFFFF] cursor-pointer"  name="lastName" value={formik.values.lastName} onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                                                            </div>
                                                        </div>
                                                        <div className="EmailDiv">
                                                            <label  className='text-[12px] font-medium text-[#9CA3AF]'>Email</label>
                                                            <input type="email"  className="bg-[#27272A] py-[16px]   px-[23px] w-full h-[48px] rounded-[10px] text-[16px] font-semibold   focus:outline-mainColor2 text-[#FFFFFF] cursor-pointer"  name="email" value={formik.values.email} onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                                                        </div>
                                                    </div>
                                                    <div className="Check space-y-[20px] my-[20px]">
                                                        <div className="flex justify-between items-center">
                                                            <p className="text-[#FFFFFF] text-[15px] font-medium">Allow others to add me to group chats</p>
                                                            <div className=' w-[35px] h-[20px] rounded-2xl  flex items-center relative px-1 transition-all duration-300 bg-[#27272A] cursor-pointer 'onClick={(e)=>{
                                                                if(formik.values.allowGroupInvites==false){
                                                                    formik.setFieldValue("allowGroupInvites", true)
                                                                }else{
                                                                    formik.setFieldValue("allowGroupInvites",false)
                                                                }
                                                                check(e.currentTarget);}} >
                                                                <div className="h-[15px] w-[15px] bg-black rounded-full absolute   transition-all duration-700 " ></div>
                                                            </div>
                                                        </div>
                                                        <div className="flex justify-between items-center">
                                                            <p className="text-[#FFFFFF] text-[15px] font-medium">Enable multi-factor authentication</p>
                                                            <div className=' w-[35px] h-[20px] rounded-2xl  flex items-center relative px-1 transition-all duration-300 bg-[#27272A] cursor-pointer 'onClick={(e)=>{
                                                                if(formik.values.twoFactorEnabled==false){
                                                                    formik.setFieldValue("twoFactorEnabled",true)
                                                                }else{
                                                                    formik.setFieldValue("twoFactorEnabled",false)
                                                                }
                                                                check(e.currentTarget);}} >
                                                                <div className="h-[15px] w-[15px] bg-black rounded-full absolute   transition-all duration-700 " ></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    {console.log(formik.errors)}
                                                    <div className="button2 space-y-[16px]">
                                                        <button type='button' className='w-full  text-[18px] font-medium  text-[#FFFFFF] bg-[#27272A]  rounded-[10px] py-[12px]' onClick={(e)=>{
                                                            e.preventDefault()
                                                            children.setprofiles(null)
                                                        }}>Cancel</button>
                                                        <button type='submit' className='w-full text-[#27272A] text-[18px] font-semibold  bg-mainColor2  rounded-[10px] py-[12px]'>Save Changes</button>
                                                    </div>
                                                </form>
                                                </div>
                                            </div>
        </div>
    );
}

export default EditProfile;
