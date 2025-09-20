
import CustomAvatar from '../../Components/CustomAvatar/CustomAvatar';
import { Mic, Phone, ScreenShare, Volume2 } from 'lucide-react';

const VoiceCall = () => {
    return (
        <section className='relative inset-0 bg-[rgba(0,0,0,0.5)] min-h-screen flex justify-center items-center'>
            <div className=" absolute inset-[15px] bg-[#111111] border-[1px] border-[#27272A] rounded-[12px] flex flex-col">
                <div className="h-[75px] p-[17px] flex space-x-[11px] border-b-[1px] border-[#27272A] ">
                        <CustomAvatar size={40}/>
                    <div className="">
                        <p className="text-[16px] font-bold text-[#FFFFFF]">John Doe</p>
                        <div className="text-mainColor2 flex items-center space-x-[3px]">
                            <Phone size={16}/>
                            <p className="text-[14px] font-medium ">Voice call • 03:16</p>
                        </div>
                    </div>
                </div>
                <div className=" flex-grow flex flex-col justify-center items-center">
                    <div className="flex  justify-center items-center border-[15px] border-[#202723] rounded-full mb-[16px]">
                        <CustomAvatar size={140}/>
                    </div>
                    <p className="text-[16px] font-semibold text-[#FFFFFF]">johndoe</p>
                </div>
                <div className="h-[85px] border-t-[1px] border-[#27272A] flex justify-center items-center">
                    <div className="flex space-x-[16px]">
                    <ScreenShare size={56} className=' text-[#FFFFFF] bg-[#27272A] p-[15px] rounded-full  cursor-pointer  '/>
                    <Mic size={56} className=' text-[#FFFFFF] bg-[#27272A] p-[15px]  rounded-full  cursor-pointer  '/>
                    <Volume2 size={56} className=' text-[#FFFFFF] bg-[#27272A] p-[15px] rounded-full  cursor-pointer  '/>
                    <Phone size={56} className=' text-[#FFFFFF] p-[15px] rounded-full bg-[#7F1D1D] cursor-pointer rotate-[135deg] '/>

                    </div>
                </div>
            </div>
        </section>
    );
}

export default VoiceCall;
