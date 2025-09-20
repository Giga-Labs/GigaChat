import toast from 'react-hot-toast';
import CustomAvatar from '../CustomAvatar/CustomAvatar';
import { EllipsisVertical, Mic, Phone, Send, Settings, Video } from 'lucide-react';
import { useContext } from 'react';
import { userContext } from '../../Context/User.context';
import axios from 'axios';
import { useState } from 'react';
import { useEffect } from 'react';
import GroupChatSettings from '../GroupChatSettings/GroupChatSettings';
import ConversationMessage from '../ConversationMessage/ConversationMessage';
import { object, string } from 'yup';
import { useFormik } from 'formik';
import TimeOnly from '../TimeOnly/TimeOnly';
import MessagesFromSignalR from '../MessagesFromSignalR/MessagesFromSignalR';

const ConversationActive = (children) => {
    let [conversation,setConversation]=useState()
    let [OpenMenuDots,setOpenMenuDots]=useState()
    let [OpenSetting,setOpenSetting]=useState()
    let {token,userId}=useContext(userContext)
    // const [messages, setMessages] = useState([]);
    const [allMessages, setAllMessages] = useState([]);
    async function getConversation() {
        const loadingId = toast.loading("Waiting...");
        try {
            const options = {
                url: `https://gigachat.tryasp.net/Conversations/${children.conversationActiveId}`,
                method: "Get",
                headers: {
                    Authorization: `Bearer ${token}`,
                }
            };
            const { data } = await axios(options);
            setConversation(data)
            if(data){getMessages(data.id)}
            console.log("data getConversation",data);
        } catch (error) {
            toast.error("Error sending data");
            console.error(error);
        } finally {
            toast.dismiss(loadingId);
        }
    }
    async function sendBlock(userId) {
        const loadingId = toast.loading("Waiting...");
        try {
            const options = {
                url: `https://gigachat.tryasp.net/Block/${userId}/toggle`,
                method: "POST",
                headers: {
                    Authorization: `Bearer ${token}`,
                }
            };
            const { data } = await axios(options);
            console.log("sendBlock",data);
        } catch (error) {
            toast.error("Error sending data");
            console.error(error);
        } finally {
            toast.dismiss(loadingId);
        }
    }
    const validationSchema = object({
        content:string(),
    });
    async function getMessages(conversationId) {
        const loadingId = toast.loading("Waiting...");
        try {
            const options = {
                url: `https://gigachat.tryasp.net/Conversations/${conversationId}/Messages`,
                method: "GET",
                headers: {
                    Authorization: `Bearer ${token}`,
                }
            };
            const { data } = await axios(options);
            console.log("data getMessages",data);
            setAllMessages(data)
        } catch (error) {
            toast.error("Error sending data");
            console.error(error);
        } finally {
            toast.dismiss(loadingId);
        }
    }
    async function sendMessages(values) {
        const loadingId = toast.loading("Waiting...");
        try {
            const options = {
                url: `https://gigachat.tryasp.net/Conversations/${children.conversationActiveId}/Messages`,
                method: "POST",
                data: values,
                headers: {
                    Authorization: `Bearer ${token}`,
                }
            };
            const { data } = await axios(options);
            console.log(data);
            // setAllMessages(data)
        
        } catch (error) {
            toast.error("Error sending data");
            console.error(error);
        } finally {
            toast.dismiss(loadingId);
        }
    }

    const formik = useFormik({
        initialValues: {
            content:"",
        },
        validationSchema,
        onSubmit: (values, { resetForm }) => {
            sendMessages(values); // تأكد أن هذه ترسل البيانات
            resetForm();  // يقوم بتفريغ الحقول
           
          },

    });
    useEffect(()=>{getConversation()},[children.conversationActiveId])
    return (<>
        {conversation?<div className="flex-grow  relative  " 
        >
                
                <div className="px-[24px] flex flex-col">
                    <div className="flex justify-between  items-center pt-[24px] pb-[28px]">
                        <p className="text-[#FFFFFF] text-[24px] font-bold">{
                            
                        conversation.isGroup?conversation.name
                        :userId==conversation.membersList[0].id?conversation.membersList[1].userName:conversation.membersList[0].userName}</p>
                        <div className="flex items-center space-x-2 relative">
                            <Video size={34} className=' text-mainColor2 p-[7px] rounded-full bg-[#344238] cursor-pointer'/>
                            <Phone size={34} className=' text-mainColor2 p-[7px] rounded-full bg-[#344238] cursor-pointer'/>
                            {conversation.isGroup?<Settings size={34} className=' text-[#FFFFFF] p-[7px] rounded-full bg-[#27272A] cursor-pointer' onClick={()=>{setOpenSetting(true)}}/>:null}
                            <EllipsisVertical size={34} className=' text-[#FFFFFF] p-[7px] rounded-full bg-[#27272A] cursor-pointer' onClick={()=>{
                                if(OpenMenuDots){
                                    setOpenMenuDots(false)
                                }else{
                                    setOpenMenuDots(true)
                                }
                            }}/>
                                {OpenMenuDots?<div className=" absolute -bottom-[120px] min-h-[90px] w-[119px] py-[5px]  text-white cursor-pointer right-0 bg-[#27272A] rounded-[10px] ">
                                    <p className="hover:bg-slate-500 py-[4px] px-[13px] rounded-[10px] capitalize">clear</p>
                                    <p className="hover:bg-slate-500 py-[4px] px-[13px] rounded-[10px] capitalize">Delete</p>
                                    <p className="hover:bg-slate-500 py-[4px] px-[13px] rounded-[10px] text-[#9D2929] capitalize" onClick={()=>{
                                        
                                        sendBlock(conversation.membersList[0].id)}}>Block</p>
                                </div>:null}
                        </div>
                    </div>

                    {/* <ConversationMessage messages={messages} setMessages={setMessages} conversationId={children.conversationActiveId}/>
                    {console.log("messages",messages)} */}
                    <div className=" h-[80lvh]  overflow-y-scroll" style={{ scrollbarWidth: "none" }}>
                    {
                        allMessages?allMessages.slice().reverse().map((message,index)=>{
                            console.log("allMessages" ,message,userId)
                            if(message.senderId==userId){
                                
                                return <div key={index} className="h-[50px] my-2  px-[15px]  transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  justify-end   items-center space-x-3 ">
                                <div className="px-[10px] pt-[5px] pb-[3px] bg-mainColor2 text-black rounded-[12px]">
                                    <p className="text-[16px] font-medium">{message.content}</p>
                                    <p className="flex justify-end text-[10px] font-semibold "><TimeOnly dateStr={message.sentAt}/></p>
                                </div>
                                <CustomAvatar size={34} />
                        </div>
                            }else{
                                return <div key={index} className="h-[50px] my-2  px-[15px]   transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex   items-center space-x-3 ">
                                <CustomAvatar size={34} />
                                <div className="px-[10px] pt-[5px] pb-[3px] bg-mainColor1 text-black rounded-[12px]">
                                    <p className="text-[16px] font-medium">{message.content}</p>
                                    <p className="flex justify-end text-[10px] "><TimeOnly dateStr={message.sentAt}/></p>
                                </div>
                        </div>
                            }
                        }):null
                    }
                    <MessagesFromSignalR userId={userId} conversationId={children.conversationActiveId}/>
                    </div>
                    {/* {
                        messages?messages.map((massage,index)=>{
                            if(messages.senderId==userId&&messages.conversationId==children.conversationActiveId){
                                return <div key={index} className="h-[50px] my-2  px-[15px]  transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  justify-end   items-center space-x-3 ">
                                <div className="px-[10px] pt-[5px] pb-[3px] bg-mainColor2 text-black rounded-[12px]">
                                    <p className="text-[16px] font-medium">{massage.content}</p>
                                    <p className="flex justify-end text-[10px] "><TimeOnly dateStr={massage.sentAt}/></p>
                                </div>
                                <CustomAvatar size={34} />
                        </div>
                            }else{
                                return <div key={index} className="h-[50px] my-2  px-[15px]   transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex   items-center space-x-3 ">
                                <CustomAvatar size={34} />
                                <div className="px-[10px] pt-[5px] pb-[3px] bg-mainColor1 text-black rounded-[12px]">
                                    <p className="text-[16px] font-medium">{massage.content}</p>
                                    <p className="flex justify-end text-[10px] "><TimeOnly dateStr={massage.sentAt}/></p>
                                </div>
                        </div>
                            }
                        }):null
                    } */}
                    {/* <div className="flex flex-col flex-grow h-[600px]   space-y-[5px]">
                        <div className="h-[50px]  px-[15px]   transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex   items-center space-x-3 ">
                                <CustomAvatar size={34} />
                                <div className="px-[10px] pt-[5px] pb-[3px] bg-mainColor1 text-black rounded-[12px]">
                                    <p className="text-[16px] font-medium">John Doe</p>
                                    <p className="flex justify-end text-[10px] ">12:25</p>
                                </div>
                        </div>
                        <div className="h-[50px]  px-[15px]  transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  justify-end   items-center space-x-3 ">
                                <div className="px-[10px] pt-[5px] pb-[3px] bg-mainColor2 text-black rounded-[12px]">
                                    <p className="text-[16px] font-medium">John Doe</p>
                                    <p className="flex justify-end text-[10px] ">12:25</p>
                                </div>
                                <CustomAvatar size={34} />
                        </div>
                        
                    </div> */}
                    <div className=" absolute bottom-5 left-5 right-5 flex flex-grow space-x-3 h-[48px]  ">
                        
                        <form action="" className="flex items-center w-full  space-x-3 h-full" onSubmit={formik.handleSubmit}>
                            <div className="text-[#FFFFFF] text-[20px] w-[36px] h-[36px] flex justify-center items-center  bg-[#27272A] rounded-full cursor-pointer">
                                <i className="fa-regular fa-face-smile "></i>
                            </div>
                            <input type="text" className=' flex-grow  text-[15px] font-medium px-[14px] pt-[15px] pb-[9px] bg-[#27272A] text-white rounded-[12px]' placeholder='Type a message...' name="content" value={formik.values.content} onChange={formik.handleChange} onBlur={formik.handleBlur} />
                            <Mic size={45} className=' text-[#FFFFFF] p-[13px] rounded-[12px] bg-[#27272A] cursor-pointer'/>
                            <Send size={45} className=' text-black p-[13px] rounded-[12px] bg-mainColor2 cursor-pointer' onClick={()=>formik.submitForm()}/>
                        </form>
                    </div>
                    
                </div>
            </div>:null}
            {OpenSetting==true&&conversation?<div className=" absolute inset-0 z-20 " ><GroupChatSettings conversation={conversation} getConversation={getConversation} setOpenSetting={setOpenSetting}/></div>:null}
            </>
    );
}

export default ConversationActive;
