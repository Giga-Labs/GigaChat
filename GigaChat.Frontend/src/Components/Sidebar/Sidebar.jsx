import { CirclePlus, LogOut } from 'lucide-react';
import React, { useContext, useState } from 'react';
import CustomAvatar from '../CustomAvatar/CustomAvatar';
import { userContext } from '../../Context/User.context';
import Logo from "../../assets/logo/GigaChatLogo.png"
import ConversationHub from '../ConversationHub/ConversationHub';
import NewConversation from '../NewConversation/NewConversation';
import toast from 'react-hot-toast';
import axios from 'axios';
import { useEffect } from 'react';
const Sidebar = (children) => {
    // const [conversationActive,setConversationActive]=useState(false)
    const [conversations, setConversations] = useState([]);
    const [allconversations, setAllConversations] = useState([]);
    const [connection, setConnection] = useState(null);
    let {logOut,token,userId} =useContext(userContext);
    const [Newconversations, setNewConversations] = useState();
    const [tags, setTags] = useState([]);
    
    async function getConversations() {
        const loadingId = toast.loading("Waiting...");
        try {
            const options = {
                url: "https://gigachat.tryasp.net/Conversations",
                method: "Get",
                headers: {
                    Authorization: `Bearer ${token}`,
                }
            };
            const { data } = await axios(options);
            setAllConversations(data)
            console.log(data);
        } catch (error) {
            toast.error("Error sending data");
            console.error(error);
        } finally {
            toast.dismiss(loadingId);
        }
    }
    useEffect(()=>{
        getConversations()
    },[])
        
    function selectElement(e){
        let list = document.querySelectorAll("div.listItems div");
        for(let element of list){
            if(e==element ){element.classList.add("bg-[#3A3744]","text-mainColor1")}
            else{element.classList.remove("bg-[#3A3744]","text-mainColor1")}}
        }
    

    return (
        <>
        <ConversationHub conversations={conversations}  setConversations={setConversations} connection={connection} setConnection={setConnection} />
        {console.log("conversations",conversations)}
            <div className="w-[298px] inset-0 min-h-screen px-[21px] flex flex-col border-r-2 border-[#24242C]">
                <div className="pt-[18px] pb-[16px] mb-[16px] flex justify-between border-b-2 border-[#24242C]">
                    <img src={Logo} alt="" className='w-[125px] ' />
                    <CirclePlus size={20}  className=' text-mainColor2 transition-all duration-200 outline-none border-0  inset-0  bg-transparent hover:bg-transparent rounded-full hover:shadow-[0px_0px_4px_1px_rgba(168,240,192,1)]' onClick={()=>{setNewConversations("Newconversations")}}/>
                </div>
                
                <div className="flex-grow text-[#FFFFFF] listItems" onClick={(element)=>{
                        element.children.map((e)=>{
                            selectElement(e.currentTarget)
                            console.log(e.currentTarget)
                            
                        })
                }}  >
                    
                    {allconversations?allconversations.map((conversation ,index)=>{
                        return <div key={index} className="h-[50px]  px-[15px] hover:bg-[#27272A] transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  items-center space-x-3 cursor-pointer" onClick={(e)=>{
                            // e.currentTarget.classList.toggle("text-mainColor1")
                            // e.currentTarget.classList.toggle("bg-[#3A3744]")
                            selectElement(e.currentTarget)
                            console.log(e.currentTarget)
                            children.setConversationActiveId(conversation.id)
                        }}>
                            <CustomAvatar size={34} />
                            
                            <p className="text-[16px] font-semibold">{
                            conversation.isGroup?conversation.name
                            :userId==conversation.membersList[0].id?conversation.membersList[1].userName:conversation.membersList[0].userName}</p>
                        </div> 
                    }):null
                    }
                    {conversations?conversations.map((conversation ,index)=>{
                        return <div key={index} className="h-[50px]  px-[15px] hover:bg-[#27272A] transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  items-center space-x-3 cursor-pointer" onClick={(e)=>{
                            // e.currentTarget.classList.toggle("text-mainColor1")
                            // e.currentTarget.classList.toggle("bg-[#3A3744]")
                            children.setConversationActiveId(conversation.id)
                            console.log('conversation.id',conversation.id)
                        }}>
                            <CustomAvatar size={34} />
                            
                            <p className="text-[16px] font-semibold">{
                            userId==conversation.membersList[0].id?conversation.membersList[1].userName:conversation.membersList[0].userName}</p>
                        </div> 
                    }):null
                    }
                    {/* <div className="h-[50px]  px-[15px] hover:bg-[#27272A] transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  items-center space-x-3 cursor-pointer" onClick={(e)=>{
                        e.currentTarget.classList.toggle("text-mainColor1")
                        e.currentTarget.classList.toggle("bg-[#3A3744]")
                        if(!conversationActive){ setConversationActive(true)}
                        else{setConversationActive(false)}
                    }}>
                        <CustomAvatar size={34} />
                        
                        <p className="text-[16px] font-semibold">John Doe</p>
                    </div> */}
                    {/* <div className="h-[50px] px-[15px] hover:bg-[#27272A] transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  items-center space-x-3 cursor-pointer">
                        <div className="w-[34px] h-[34px] rounded-full p-[2px]  grid grid-cols-2 ">
                        <CustomAvatar size={15}/>
                        <CustomAvatar size={15}/>
                        <CustomAvatar size={15}/>
                        <CustomAvatar size={15}/>
                        </div>
                        <p className="text-[16px] font-semibold">Team Chat</p>
                    </div> */}
                </div>
                <div className="h-[125px] border-t-2 border-[#24242C] py-[8px] space-y-[8px]">
                    <div className="h-[50px]  px-[15px] hover:bg-[#27272A] transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  items-center space-x-3 cursor-pointer" onClick={(e)=>{
                            e.currentTarget.classList.add("text-mainColor1")
                            e.currentTarget.classList.add("bg-[#3A3744]")
                            children.setprofiles("profile")
                        }}>
                            <CustomAvatar size={34} />
                            
                            <p className="text-[16px] font-semibold">Profile</p>
                    </div>
                    <div className="h-[50px]  px-[15px] hover:bg-[#27272A] transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  items-center space-x-3 cursor-pointer" onClick={()=>{logOut()}}>
                            <LogOut size={25} />
                            
                            <p className="text-[16px] font-semibold ">Logout</p>
                    </div>
                </div>
            </div>
            {Newconversations=="Newconversations"?<div className=" absolute inset-0 z-20 " ><NewConversation setNewConversations={setNewConversations} tags={tags} setTags={setTags}/></div> :null}
        </>
    );
}

export default Sidebar;
