import React, { useEffect, useState } from 'react';
import * as signalR from "@microsoft/signalr";
import { useContext } from 'react';
import { userContext } from '../../Context/User.context';
const ConversationMessage = (children) => {
    //  const [messages, setMessages] = useState([]);
    let {token}=useContext(userContext)
        useEffect(() => {
            const connection = new signalR.HubConnectionBuilder()
            .withUrl('https://gigachat.tryasp.net/hubs/messages', {
                accessTokenFactory: () => token,
            })
            .withAutomaticReconnect()
            .build();
        
            connection.on('ReceiveMessage', (message) => {
            console.log('New message:', message);
            children.setMessages(prev => [...prev, message]);
            });
        
            connection
            .start()
            .then(() => {
                console.log('Connected to SignalR hub');
                connection.invoke('SubscribeToConversations', children.conversationId);
            })
            .catch(err => console.error('Connection failed:', err));
        
            return () => {
            connection.stop();
            };
        }, [children.conversationId, token]);
        
        // return (
        //     <div>
        //     <h2>Messages for Conversation {children.conversationId}</h2>
        //     <ul>
        //         {children.messages.map((msg, index) => (
        //         <li key={index}>{JSON.stringify(msg)}</li>
        //         ))}
        //     </ul>
        //     </div>
        // );
}

export default ConversationMessage;
