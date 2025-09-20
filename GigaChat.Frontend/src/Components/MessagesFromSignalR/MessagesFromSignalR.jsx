import React, { useContext, useState } from 'react';
import ConversationMessage from '../ConversationMessage/ConversationMessage';
import CustomAvatar from '../CustomAvatar/CustomAvatar';
import TimeOnly from '../TimeOnly/TimeOnly';
import { userContext } from '../../Context/User.context';

const MessagesFromSignalR = (children) => {
    const [messages, setMessages] = useState([]);
    let {userId}=useContext(userContext)
    console.log("userId",userId)
    return (
        <div>
                <ConversationMessage messages={messages} setMessages={setMessages} conversationId={children.conversationId}/>

                    {}
                    {console.log("messages",messages)}
                    {
                        messages?messages.filter((message) => message.conversationId === children.conversationId).map((message,index)=>{
                            {console.log(messages.senderId)}
                            
                            if(message.senderId===userId){return <div key={index} className="h-[50px] my-2  px-[15px]  transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex  justify-end   items-center space-x-3 ">
                                <div className="px-[10px] pt-[5px] pb-[3px] bg-mainColor2 text-black rounded-[12px]">
                                    <p className="text-[16px] font-medium">{message.content}</p>
                                    <p className="flex justify-end text-[10px] "><TimeOnly dateStr={message.sentAt}/></p>
                                </div>
                                <CustomAvatar size={34} />
                        </div>}
                        else{return <div key={index} className="h-[50px] my-2  px-[15px]   transition-all duration-200 rounded-[12px] text-[#FFFFFF] flex   items-center space-x-3 ">
                                <CustomAvatar size={34} />
                                <div className="px-[10px] pt-[5px] pb-[3px] bg-mainColor1 text-black rounded-[12px]">
                                    <p className="text-[16px] font-medium">{message.content}</p>
                                    <p className="flex justify-end text-[10px] "><TimeOnly dateStr={message.sentAt}/></p>
                                </div>
                        </div>}

                        }):null
                    }
        </div>
    );
}

export default MessagesFromSignalR;
