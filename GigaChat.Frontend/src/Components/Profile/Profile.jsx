import { ChevronLeft } from 'lucide-react';
import CustomAvatar from '../CustomAvatar/CustomAvatar';
import { useContext, useEffect } from 'react';
import { userContext } from '../../Context/User.context';
import toast from 'react-hot-toast';
import axios from 'axios';
import { useState } from 'react';

const Profile = (children) => {
    let {token}=useContext(userContext)
    let [profileData,setProfileData]=useState()
    function check(e){
        if(e.classList.contains('bg-[#27272A]'))
            e.classList.replace('bg-[#27272A]','bg-[#A8F0C0]')
        else{e.classList.replace('bg-[#A8F0C0]','bg-[#27272A]')}
        e.firstElementChild.classList.toggle("right-[3px]")
    }
    async function getDataProfile() {
        const loadingId = toast.loading("Waiting...");
        try {
            const options = {
                url: `https://gigachat.tryasp.net/Profiles/me`,
                method: "Get",
                headers: {
                    Authorization: `Bearer ${token}`,
                }
            };
            const { data } = await axios(options);
            setProfileData(data)
            console.log(data);
        } catch (error) {
            toast.error("Error sending data");
            console.error(error);
        } finally {
            toast.dismiss(loadingId);
        }
    }
    useEffect(()=>{getDataProfile()},[])
    useEffect(()=>{
        if(profileData){let allowGroupInvites=document.querySelector(".allowGroupInvites");
        let twoFactorEnabled=document.querySelector(".twoFactorEnabled")
        if(!profileData.allowGroupInvites){
            allowGroupInvites.classList.add('bg-[#27272A]')
            allowGroupInvites.classList.remove('bg-[#A8F0C0]')
        }else{
            allowGroupInvites.classList.remove('bg-[#27272A]')
            allowGroupInvites.classList.add('bg-[#C5B8F9]')
            allowGroupInvites.firstElementChild.classList.add("right-[3px]")
        }
        if(!profileData.twoFactorEnabled){
            twoFactorEnabled.classList.add('bg-[#27272A]')
            twoFactorEnabled.classList.remove('bg-[#A8F0C0]')
        }else{
            twoFactorEnabled.classList.remove('bg-[#27272A]')
            twoFactorEnabled.classList.add('bg-[#C5B8F9]')
            twoFactorEnabled.firstElementChild.classList.add("right-[3px]")
        }}
    },[profileData])
    return (
        profileData?<div className="  relative inset-0  bg-[rgba(0,0,0,0.5)] min-h-screen flex justify-center items-center" onClick={(e)=>{if(e.target==e.currentTarget){children.setprofiles(null)}}}>
                                            <div className="bg-[#1E1E1E]  flex flex-col items-center font-Inter z-10 rounded-[14px] " >
                                                
                                                <div className="  w-[450px] h-[623px]  rounded-[14px]  " >
                                                <div className="px-[20px] mt-[25px]  space-x-[14px] flex items-center   text-[#FFFFFF]">
                                                <ChevronLeft size={35} className='cursor-pointer text-[#A1A1AA]  '  onClick={()=>{children.setprofiles(null)}}/>
                                                    <p className="text-[20px] font-bold ">
                                                    Profile
                                                    </p>
                                                </div>
                                                <div className="flex justify-center items-center mt-[12px] mb-[19px]">
                                                    <CustomAvatar size={80} />
                                                </div>
                                                <form action="" className=" px-[32px] ">
                                                    <div className="Input">
                                                        <div className="Username">
                                                            <label htmlFor="Username" className='text-[12px] font-medium text-[#9CA3AF]'>Username</label>
                                                            <input type="text" id='Username' placeholder={profileData.userName} disabled className="bg-[#27272A] py-[16px]   px-[23px] w-full h-[48px] rounded-[10px] text-[16px] font-semibold   focus:outline-mainColor2 focus:text-[#FFFFFF] cursor-pointer" />
                                                        </div>
                                                        <div className="grid grid-cols-2 gap-[16px]">
                                                            <div className="First Name">
                                                                <label htmlFor="FirstName" className='text-[12px] font-medium text-[#9CA3AF]'>First Name</label>
                                                                <input type="text" id='FirstName' placeholder={profileData.firstName} disabled className="bg-[#27272A] py-[16px]   px-[23px] w-full h-[48px] rounded-[10px] text-[16px] font-semibold   focus:outline-mainColor2 focus:text-[#FFFFFF] cursor-pointer" />
                                                            </div>
                                                            <div className="LastName">
                                                                <label htmlFor="LastName" className='text-[12px] font-medium text-[#9CA3AF]'>Last Name</label>
                                                                <input type="text" id='LastName' placeholder={profileData.lastName} disabled className="bg-[#27272A] py-[16px]   px-[23px] w-full h-[48px] rounded-[10px] text-[16px] font-semibold   focus:outline-mainColor2 focus:text-[#FFFFFF] cursor-pointer" />
                                                            </div>
                                                        </div>
                                                        <div className="Email">
                                                            <label htmlFor="Email" className='text-[12px] font-medium text-[#9CA3AF]'>Email</label>
                                                            <input type="email" id='Email' placeholder={profileData.email} disabled className="bg-[#27272A] py-[16px]   px-[23px] w-full h-[48px] rounded-[10px] text-[16px] font-semibold   focus:outline-mainColor2 focus:text-[#FFFFFF] cursor-pointer" />
                                                        </div>
                                                    </div>
                                                    <div className="Check space-y-[20px] my-[20px]">
                                                        <div className="flex justify-between items-center">
                                                            <p className="text-[#FFFFFF] text-[15px] font-medium">Allow others to add me to group chats</p>
                                                            <div className=' w-[35px] h-[20px] rounded-2xl allowGroupInvites  flex items-center relative px-1 transition-all duration-300 bg-[#27272A] cursor-pointer ' >
                                                                <div className="h-[15px] w-[15px] bg-black rounded-full absolute   transition-all duration-700 " ></div>
                                                            </div>
                                                        </div>
                                                        <div className="flex justify-between items-center">
                                                            <p className="text-[#FFFFFF] text-[15px] font-medium">Enable multi-factor authentication</p>
                                                            <div className=' w-[35px] h-[20px] rounded-2xl  flex items-center twoFactorEnabled relative px-1 transition-all duration-300 bg-[#27272A] cursor-pointer ' >
                                                                <div className="h-[15px] w-[15px] bg-black rounded-full absolute   transition-all duration-700 " ></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div className="button2 space-y-[16px]">
                                                        <button className='w-full  text-[18px] font-medium  text-[#FFFFFF] bg-[#27272A]  rounded-[10px] py-[12px]' onClick={()=>{children.setprofiles("editProfile")}}>Edit Profile</button>
                                                        <button className='w-full text-[#27272A] text-[18px] font-semibold  bg-mainColor1  rounded-[10px] py-[12px]' onClick={()=>{children.setprofiles("changePassword")}}>Change Password</button>
                                                    </div>
                                                </form>
                                                </div>
                                            </div>
        </div>
    :null
    );
}

export default Profile;
