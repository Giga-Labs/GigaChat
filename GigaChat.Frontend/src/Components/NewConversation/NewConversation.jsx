import axios from 'axios';
import { useFormik } from 'formik';
import { ChevronLeft } from 'lucide-react';
import { useContext, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import { object, string } from 'yup';
import { userContext } from '../../Context/User.context';

const NewConversation = (children) => {
    const [conversationGroup, setConversationGroup] = useState();
    const [tags, setTags] = useState([]);
    const [inputValue, setInputValue] = useState("");
    const { token } = useContext(userContext);
    const [nameData, setnameData] = useState(null);
    
    const validationSchema = object({
        name:string(),
        membersList: string().required("Members are required")
    });

    async function sendNewConversation(values) {
        const loadingId = toast.loading("Waiting...");
        const payload = {
            ...values,
            membersList: tags // يتم تمرير الأسماء من tags
        };
        console.log(payload)
        try {
            const options = {
                url: "https://gigachat.tryasp.net/Conversations",
                method: "POST",
                data: payload,
                headers: {
                    Authorization: `Bearer ${token}`,
                }
            };
            const { data } = await axios(options);
            console.log(data);
        } catch (error) {
            toast.error("Error sending data");
            console.error(error);
        } finally {
            toast.dismiss(loadingId);
        }
    }

    const formik = useFormik({
        initialValues: {
            ...nameData,
            membersList: "",
        },
        validationSchema,
        onSubmit: sendNewConversation,
    });

    // تحديث قيمة membersList تلقائيًا من tags
    useEffect(() => {
        if(tags.length>0){setnameData({name:""})}
        formik.setFieldValue("membersList", tags.join(" "));
    }, [tags]);

    const handleKeyDown = (e) => {
        if ((e.key === " " || e.key === "Enter") && inputValue.trim()) {
            e.preventDefault();
            const newTag = inputValue.trim();
            if (!tags.includes(newTag)) {
                setTags([...tags, newTag]);
                setInputValue("");
            }
        } else if (e.key === "Backspace" && inputValue === "" && tags.length) {
            setTags(tags.slice(0, -1));
        }
    };

    return (
        <div className="relative inset-0 bg-[rgba(0,0,0,0.5)] min-h-screen flex justify-center items-center" onClick={(e)=>{if(e.target==e.currentTarget){children.setNewConversations(null)}}}>
            <div className="flex flex-col items-center font-Inter z-10" >
                <div className="bg-[#171717] w-[450px] min-h-[300px] rounded-[14px]" >
                    <div className="px-[20px] mt-[25px] space-x-[14px] flex items-center text-[#FFFFFF]">
                        <ChevronLeft size={35} className="cursor-pointer text-[#A1A1AA]" onClick={()=>{setConversationGroup(false)}}/>
                        <p className="text-[20px] font-bold">NewConversation</p>
                    </div>
                    <form className="px-[32px] mt-[32px]" onSubmit={formik.handleSubmit}>
                        {conversationGroup?<>
                        <p className="px-[32px] mb-[12px] text-[14px] font-semibold text-[#A1A1AA]">Name</p>
                        <input type="text" name='name' value={formik.values.name} className=" bg-[#27272A] flex flex-wrap gap-2 text-[#9CA3AF] p-2 rounded-[10px] mb-4 w-[386px] h-[48px] " onChange={formik.handleChange} onBlur={formik.handleBlur}/>
                            </>:<>
                        <p className="px-[32px] mb-[12px] text-[14px] font-semibold text-[#A1A1AA]">Add Members</p>
                        <div className="bg-[#27272A] flex flex-wrap gap-2 p-2 rounded-[10px] mb-4 w-[386px] min-h-[48px]">
                            {tags.map((tag, index) => (
                                <div key={index} className="flex items-center bg-[#39493E] text-[#F7F7F7] text-[16px] px-[17px] py-[3px] rounded-[13px]">
                                    <span>{tag}</span>
                                </div>
                            ))}
                            <input
                                type="text"
                                value={inputValue}
                                onChange={(e) => setInputValue(e.target.value)}
                                onKeyDown={handleKeyDown}
                                className="bg-transparent text-[#9CA3AF] outline-none flex-1 "
                            />
                        </div>
                        </>}
                            {/* {

                            } */}
                        {/* Hidden field for Formik to validate */}
                        {/* <input type="hidden" name="membersList"  /> */}
                        {console.log("tags",tags.length)}
                        
                        {tags.length>1?conversationGroup==true?<button type="submit" className="w-full text-[#27272A] text-[16px] font-semibold bg-mainColor2 mb-[16px] rounded-[10px] py-[12px]">
                            Start Conversation
                        </button>:
                        <button type='button' className="w-full text-[#27272A] text-[16px] font-semibold bg-mainColor2 mb-[16px] rounded-[10px] py-[12px]" onClick={(e)=>{setConversationGroup(true)
                            e.preventDefault()
                        }}>
                            Start Conversation
                        </button>:
                        <button type="submit" className="w-full text-[#27272A] text-[16px] font-semibold bg-mainColor2 mb-[16px] rounded-[10px] py-[12px]">
                            Start Conversation
                        </button>}

                    </form>
                </div>
            </div>
        </div>
    );
};

export default NewConversation;
