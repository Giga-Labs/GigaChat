import { ChevronLeft, CirclePlus, EllipsisVertical } from "lucide-react";
import CustomAvatar from "../CustomAvatar/CustomAvatar";
import { useContext, useEffect, useState } from "react";
import { userContext } from "../../Context/User.context";
import toast from "react-hot-toast";
import axios from "axios";
import AddMembersToGroup from "../AddMembersToGroup/AddMembersToGroup";



const GroupChatSettings = (children) => {
    let [conversation,setConversation]=useState([])
    let [OpenMenuDots,setOpenMenuDots]=useState()
    let [Member,setMember]=useState()
    let {token,userId}=useContext(userContext)
    let [roleMy,setRoleMy]=useState([])
    console.log("children.conversation",children.conversation)
    async function toggleAdmin(Id) {
        console.log(children.conversation.id,userId)
        const loadingId = toast.loading("Waiting...");
        try {
            const options = {
                url: `https://gigachat.tryasp.net/Conversations/${children.conversation.id}/members/${Id}/admin`,
                method: "PUT",
                headers: {
                    Authorization: `Bearer ${token}`,
                }
            };
            const { data } = await axios(options);
            console.log(data);
            children.getConversation()
        } catch (error) {
            toast.error("Error sending data");
            console.error(error);
        } finally {
            toast.dismiss(loadingId);
        }
    }
    async function DeleteMember(Id) {
        console.log(children.conversation.id,userId)
        const loadingId = toast.loading("Waiting...");
        try {
            const options = {
                url: `https://gigachat.tryasp.net/Conversations/${children.conversation.id}/members/${Id}`,
                method: "DELETE",
                headers: {
                    Authorization: `Bearer ${token}`,
                }
            };
            const { data } = await axios(options);
            console.log(data);
            children.getConversation()
        } catch (error) {
            toast.error("Error sending data");
            console.error(error);
        } finally {
            toast.dismiss(loadingId);
        }
    }
    useEffect(()=>{setConversation(children.conversation.membersList)
                    
        setRoleMy(conversation.find(member => member.id === userId));
        console.log(roleMy)
    },[conversation,children.conversation])
    return (
        <div className=" relative inset-0 bg-[rgba(0,0,0,0.5)] min-h-screen flex justify-center items-center">
            <div className="  flex flex-col items-center font-Inter z-10 ">
                
                <div className="bg-[#171717]  w-[450px] min-h-[200px]  rounded-[14px]  ">
                <div className="px-[32px] mt-[25px] mb-[28px] space-x-[18px] flex items-center  text-[#FFFFFF]">
                <ChevronLeft size={35} onClick={()=>{children.setOpenSetting(null)}}/>
                    <p className="text-[20px] font-bold">
                    Group Chat Settings
                    </p>
                </div>
                <div className="flex justify-between items-center px-[32px] mb-[15px]">
                    <p className="  text-[20px] font-semibold text-[#FFFFFF] ">Members</p>
                    <CirclePlus size={24}  className=' text-mainColor1 transition-all duration-200 outline-none border-0  inset-0  bg-transparent hover:bg-transparent rounded-full hover:shadow-[0px_0px_4px_1px_rgba(197,184,249,1)]' onClick={()=>{
                        setMember(true)
                    }}/>
                </div>
                <p className="px-[32px] text-[#8B919B] ">
                    Owner: <span className="text-[#FFFFFF]">user123</span>
                </p>
                <form action="" className=" px-[32px]    ">
                    <div className="space-y-[10px] mt-[15px] mb-[18px]">
                        {/* <div className="flex justify-between items-center">
                            <div className="flex items-center space-x-[9px]">
                                <CustomAvatar size={32}/>
                                <p className="text-[16px] font-semibold text-[#FFFFFF]">user123</p>
                            </div>
                            <p className="text-[12px] font-medium text-[#8B919B]">Admin</p>
                        </div> */}
                        { conversation?conversation.map((member,index)=>{
                            return <div key={index} className="flex justify-between items-center relative">
                            <div className="flex items-center space-x-[9px]">
                                <CustomAvatar size={32}/>
                                <p className="text-[16px] font-semibold text-[#FFFFFF]">{member.userName}</p>
                            </div>
                            <div className="flex space-x-[8px]">
                                {member.isAdmin?<p className="text-[16px] font-semibold text-[#989898]">Admin</p>:null}
                                <EllipsisVertical
                                            size={20}
                                            className="text-[#FFFFFF] cursor-pointer"
                                            onClick={() => setOpenMenuDots(prev => (prev === index ? null : index))}
                                        />
                                        {OpenMenuDots === index && (
                                            <div className="absolute -bottom-[60px] -right-[130px] min-h-[20px] min-w-[119px] py-[5px] z-50 text-white cursor-pointer  bg-[#27272A] rounded-[10px]">
                                                <p className="hover:bg-slate-500 py-[4px] px-[13px] rounded-[10px] capitalize" onClick={()=>{toggleAdmin(member.id)}}>{member.isAdmin?"Remove Admin":"Make Admin"}</p>
                                                <p className="hover:bg-slate-500 py-[4px] px-[13px] rounded-[10px] capitalize" onClick={()=>{DeleteMember(member.id)}}>Remove</p>
                                            </div>
                                        )}
                            </div>

                        </div>}):null}
                        {/* <div className="flex justify-between items-center">
                            <div className="flex items-center space-x-[9px]">
                                <CustomAvatar size={32}/>
                                <p className="text-[16px] font-semibold text-[#FFFFFF]">janedoe</p>
                            </div>
                            <EllipsisVertical size={22} className="text-[#FFFFFF]" />

                        </div>
                        <div className="flex justify-between items-center">
                            <div className="flex items-center space-x-[9px]">
                                <CustomAvatar size={32}/>
                                <p className="text-[16px] font-semibold text-[#FFFFFF]">bobsmith</p>
                            </div>
                            <EllipsisVertical size={22} className="text-[#FFFFFF]" />

                        </div> */}
                    </div>
                    <button type='button' className="bg-mainColor1 hover:bg-[#b5a9e5] transition-all duration-150 text-black font-medium  py-[13px] px-4 rounded-[10px]  text-[17px]   w-full h-[48px]  " onClick={()=>{
                        // SetVerficiation(true);
                    }}>Delete Group Chat</button>
                </form>
                            
                </div>
            </div>
            {Member?<div className=" absolute inset-0 z-20 " ><AddMembersToGroup conversation={conversation} getConversation={children.getConversation()} setMember={setMember}/></div>:null}
        </div>
    
    );
}

export default GroupChatSettings;
