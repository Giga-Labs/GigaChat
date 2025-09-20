import  { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { useContext } from "react";
import { userContext } from "../../Context/User.context";

const ConversationHub = (children) => {
    let {token,userId}=useContext(userContext)
    let conversations=children.conversations
    let setConversations = children.setConversations
    let connection=children.connection
    let setConnection=children.setConnection

    const jwtToken = token;
    const hubUrl = "https://gigachat.tryasp.net/hubs/conversations";

    useEffect(() => {
        const newConnection = new signalR.HubConnectionBuilder()
        .withUrl(hubUrl, {
            accessTokenFactory: () => jwtToken,
        })
        .withAutomaticReconnect()
        .build();

        setConnection(newConnection);
        console.log()
    }, []);

    useEffect(() => {
        if (!connection) return;

        connection.start()
        .then(() => {
            console.log("✅ Connected to ConversationHub");

            connection.invoke("SubscribeToConversationUpdates")
            .then(() => console.log(`🧩 Subscribed to private group: user-${userId}`))
            .catch(console.error);

            connection.on("NewConversation", (data) => {
            console.log("📥 NewConversation:", data);
            setConversations(prev => [...prev, data]);
            });

            connection.on("ConversationRemoved", (conversationId) => {
            console.log("❌ ConversationRemoved:", conversationId);
            setConversations(prev => prev.filter(c => c.id !== conversationId));
            });

            connection.on("ConversationUpdated", (data) => {
            console.log("🔄 ConversationUpdated:", data);
            setConversations(prev =>
                prev.map(c => (c.id === data.id ? data : c))
            );
            });
        })
        .catch(err => {
            console.error("❌ Connection failed:", err);
        });

        return () => {
        connection.stop();
        };
    }, [connection]);
{
    console.log(conversations)
}
//   return (
//     <div style={{ padding: "20px", fontFamily: "sans-serif" }}>
//       <h2>💬 Live Conversations</h2>
//       {conversations.length === 0 && <p>😴 Nothing to see yet...</p>}
//       <ul>
//         {conversations.map(conv => (
//           <li key={conv.id} style={{ marginBottom: "10px" }}>
//             <pre>{JSON.stringify(conv, null, 2)}</pre>
//           </li>
//         ))}
//       </ul>
//     </div>
//   );
};

export default ConversationHub;