import {  Phone } from 'lucide-react';
import CustomAvatar from '../CustomAvatar/CustomAvatar';
CustomAvatar
const IncomingAudioCall = () => {
    return (
        <div className=" relative inset-0 bg-[rgba(0,0,0,0.5)] min-h-screen flex justify-center items-center">
                        <div className="  flex flex-col items-center font-Inter z-10 ">
                        <div className="bg-[#171717]  w-[400px] h-[330px]  rounded-[14px]  ">
                        <div className="flex justify-center items-center relative mt-[44px] mb-[27px]">
                            <div className="w-[72px]  relative">
                                <CustomAvatar size={72}/>
                                <div className=" absolute  -top-1 -right-3  ">
                                    <Phone size={30} className=' text-mainColor2 p-[5px] rounded-full bg-[#344238] cursor-pointer'/>
                                </div>
                            </div>
                        </div>
                        <div className="text-center space-y-[4px]">
                            <p className="text-[22px] text-[#FFFFFF] font-bold ">John Doe</p>
                            <p className="text-[14px] text-[#A1A1AA] font-medium">Incoming audio call...</p>
                        </div>
                        <div className="flex justify-center items-center space-x-[40px] my-[38px]">
                            <Phone size={56} className=' text-[#FFFFFF] p-[15px] rounded-full bg-[#7F1D1D] cursor-pointer rotate-[135deg] '/>
                            <Phone size={56} className=' text-mainColor2 p-[15px] rounded-full bg-[#344238] cursor-pointer'/>
                        </div>
                        </div>
                    </div>
        </div>
    );
}

export default IncomingAudioCall;
