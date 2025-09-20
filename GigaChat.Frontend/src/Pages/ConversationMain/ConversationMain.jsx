import { useContext, useState } from 'react';
import Logo from "../../assets/logo/GigaChatLogo.png"
import { CirclePlus, LogOut} from 'lucide-react';
import CustomAvatar from '../../Components/CustomAvatar/CustomAvatar';
import ConversationActive from '../../Components/ConversationActive/ConversationActive';
import { userContext } from '../../Context/User.context';
import Sidebar from '../../Components/Sidebar/Sidebar';
import AcceptOrIgnoreConversation from '../../Components/AcceptOrIgnoreConversation/AcceptOrIgnoreConversation';
import Profile from '../../Components/Profile/Profile';
import EditProfile from '../../Components/EditProfile/EditProfile';
import ChangePassword from '../../Components/ChangePassword/ChangePassword';


const ConversationMain = () => {
    const [conversationActiveId,setConversationActiveId]=useState(false)
    const [profiles,setprofiles]=useState(false)
    

    return (
        <section className=' bg-[#1E1E1E] min-h-screen w-full inset-0 flex'>
            {/* <div className="w-[298px] inset-0 min-h-screen px-[21px] flex flex-col border-r-2 border-[#24242C]">
                <div className="pt-[18px] pb-[16px] mb-[16px] flex justify-between border-b-2 border-[#24242C]">
                    <img src={Logo} alt="" className='w-[125px] ' />
                    <CirclePlus size={20}  className=' text-mainColor2 transition-all duration-200 outline-none border-0  inset-0  bg-transparent hover:bg-transparent rounded-full hover:shadow-[0px_0px_4px_1px_rgba(168,240,192,1)]'/>
                </div>
                <div className="flex-grow text-[#FFFFFF]">
                    <div className="h-[50px]  px-[15px] hover:bg-[#27272A] transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  items-center space-x-3 cursor-pointer" onClick={(e)=>{
                        e.currentTarget.classList.toggle("text-mainColor1")
                        e.currentTarget.classList.toggle("bg-[#3A3744]")
                        if(!conversationActive){ setConversationActive(true)}
                        else{setConversationActive(false)}
                    }}>
                        <CustomAvatar size={34} />
                        
                        <p className="text-[16px] font-semibold">John Doe</p>
                    </div>
                    <div className="h-[50px] px-[15px] hover:bg-[#27272A] transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  items-center space-x-3 cursor-pointer">
                        <div className="w-[34px] h-[34px] rounded-full p-[2px]  grid grid-cols-2 ">
                        <CustomAvatar size={15}/>
                        <CustomAvatar size={15}/>
                        <CustomAvatar size={15}/>
                        <CustomAvatar size={15}/>
                        </div>
                        <p className="text-[16px] font-semibold">Team Chat</p>
                    </div>
                </div>
                <div className="h-[125px] border-t-2 border-[#24242C] py-[8px] space-y-[8px]">
                    <div className="h-[50px]  px-[15px] hover:bg-[#27272A] transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  items-center space-x-3 cursor-pointer" onClick={(e)=>{
                            e.currentTarget.classList.add("text-mainColor1")
                            e.currentTarget.classList.add("bg-[#3A3744]")
                        }}>
                            <CustomAvatar size={34} />
                            
                            <p className="text-[16px] font-semibold">Profile</p>
                    </div>
                    <div className="h-[50px]  px-[15px] hover:bg-[#27272A] transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  items-center space-x-3 cursor-pointer" onClick={()=>{logOut()}}>
                            <LogOut size={25} />
                            
                            <p className="text-[16px] font-semibold ">Logout</p>
                    </div>
                </div>
            </div> */}
            <Sidebar conversationActiveId={conversationActiveId} setprofiles={setprofiles} setConversationActiveId={setConversationActiveId}/>
            
                {conversationActiveId?<ConversationActive conversationActiveId={conversationActiveId}/>:
                <div className="flex-grow ">
                    <div className="flex justify-center items-center h-full ">
                    <p className="text-[18px] font-medium text-[#A1A1AA]">Select a conversation or start a new one</p>
                    </div>
                </div> }
{profiles=="profile"?<div className=" absolute inset-0 z-20 " ><Profile setprofiles={setprofiles}/></div>:
profiles=="editProfile"?<div className=" absolute inset-0 z-20 " ><EditProfile setprofiles={setprofiles}/></div>:
profiles=="changePassword"?<div className=" absolute inset-0 z-20 " ><ChangePassword setprofiles={setprofiles}/></div>:
null}
        </section>
    );
}

export default ConversationMain;
