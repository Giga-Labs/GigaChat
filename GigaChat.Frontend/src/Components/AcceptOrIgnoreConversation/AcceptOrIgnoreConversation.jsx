import React from 'react';
import CustomAvatar from '../CustomAvatar/CustomAvatar';

const AcceptOrIgnoreConversation = () => {
    return (
        <div className='flex flex-col justify-center items-center w-full'>
            <div className="flex flex-col items-center">
                <CustomAvatar size={110}/>
                <p className="text-[33px] text-[#FFFFFF] capitalize my-[32px] font-[700]">John Doe</p>
            </div>
            <div className="flex flex-col items-center ">
                <p className="text-[19px] text-[#CCCCD1] font-[500]">John Doe wants to start a conversation with you</p>
                <p className="text-[17px] text-[#6E6E76] font-[500] mt-[12px]">You can accept, ignore, or report this user for trying to interact with another human being in 2025.</p>
            </div>
            <div className="mt-[50px] space-x-[80px]">
            <button className='text-[14px] text-[#CCCCD1] bg-[#27272A] py-[12px] px-[44px] rounded-[10px] font-[600]'>Ignore</button>
                <button className='text-[14px] bg-mainColor2 py-[12px] px-[44px] rounded-[10px] font-[600]'>Accept</button>
            </div>
        </div>
    );
}

export default AcceptOrIgnoreConversation;
