import { ChevronsLeftRight, Github, Globe, MessageSquare, MoveRight, Shield, Users, Zap,Lock, Smartphone, Eye, Server, FileCheck, RefreshCcw, Check, Twitter, Linkedin, Heart } from 'lucide-react';
import StarryBackground from '../../Components/StarryBackground/StarryBackground';
import Logo from "../../assets/logo/GigaChatLogo.png"
import backgroundPage3 from "../../assets/images/page3.png"
import SecurityModel from '../../Components/SecurityModel/SecurityModel';
import { useState } from 'react';
import { Navigate, useNavigate } from 'react-router-dom';

const LandingPage = () => {
    let navigate=useNavigate()
    const str={0:`from matplotlib import plt`
,1:`// Install the GigaChat SDK
npm install @gigachat/sdk

// Initialize in your app
import {GigaChat} from '@gigachat/sdk';

const chat = new GigaChat(
    container: '#chat-container',
    theme: 'dark',
    branding: true
});

// That’s it! GigaChat is now embedded in your app`,
2:`// Install the GigaChat SDK
npm install @gigachat/react

//Import in your component
import { GigaChatProvider, ChatWindow } from '@gigachat/react';

function App() {
    return (
        <GigaChatProvider theme="dark">
            <div className="my-app">
                <ChatWindow branding={true} />
            </div>
        </GigaChatProvider>
    );
}`,
3:`<!-- Add GigaChat to your website with a simple script tag -→
<div id="gigachat-container"></div>

<script src="https://cdn.gigachat.io/sdk.min.js"></script>
<script>
    GigaChat.init({
        container: '#gigachat-container',
        theme: 'dark',
        branding: true
    });
</script>`
    }
    const [code,setCode]=useState(str[0]);
    function getPart(e){
        let parts=document.getElementById("parts").children;
        
        let count=0;
        for(let part of parts ){
            count++;
            if(part == e){
                setCode(str[count]);
                part.classList.add("text-mainColor2");
                part.classList.add("bg-[#2A382F]");
            }else{
                part.classList.remove("text-mainColor2");
                part.classList.remove("bg-[#2A382F]");
            }
        }
    }
    return (
        <section className='relative'>
            <StarryBackground/>
            <div className=" relative inset-0 min-h-screen ">
                <header className="h-[72px] flex  justify-between items-center ps-[72px] pe-[24px]">
                    <img src={Logo} alt="" className="h-[32px]" />
                    <ul className='text-[14px] text-[#FFFFFF] font-semibold flex space-x-[30px]'>
                        <li><a href="">Features</a></li>
                        <li><a href="">Stecurity</a></li>
                        <li><a href="">Integration</a></li>
                        <li><a href="">Documentation</a></li>
                        <li><a href="">GitHub</a></li>
                    </ul>
                    <div className="text-[14px] font-semibold flex items-center space-x-[18px]">
                        <button type="button" className="px-[14px] py-[11px] rounded-[6px]  text-[#FFFFFF] hover:bg-[#1C2321] hover:text-mainColor2 transition-all duration-200" onClick={()=>{navigate("/Login")}}>Login</button>
                        <button type="button" className="px-[14px] py-[11px] rounded-[6px]  text-[#0B320B] bg-mainColor2 hover:bg-[#99D9AE]  transition-all duration-200" onClick={()=>{navigate("/SignUp")}}>Get Started</button>
                        
                    </div>
                </header>
                <main className="">
                    <section className="page1 px-[43px] mt-[100px]"> 
                        <div className="h-[24px] w-[220px] bg-[#39493E] text-mainColor2 rounded-full text-[12px] font-bold flex justify-center items-center">
                            <p className=" capitalize">SECURE. FAST. OPEN SOURCE.</p>
                        </div>
                        <div className="  mt-[15px] space-y-[22px]">
                            <p className="text-[60px] w-[544px]  h-[192px] font-bold text-[#FFFFFF]  leading-[60px] ">The Next
                            Generation of 
                            <span className="bg-gradient-to-r  from-mainColor2 from-10%  to-mainColor1 to-90%  bg-clip-text text-transparent"> Secure Messaging</span>
                            </p>
                            <p className="text-[18px] text-[#9F9FA8] font-medium w-[523px] h-[53px]  ">
                                GigaChat provides end-to-end encrypted messaging with a clean interface. Free, open source, and built for privacy 
                            </p>
                        </div>
                        <div className="flex items-center space-x-[14px] text-[15px] text-[#0B320B] font-semibold mt-[29px] mb-[56px] ">
                            <div className="flex justify-center items-center bg-mainColor2 h-[44px] w-[162px] rounded-[6px] space-x-[10px] hover:bg-[#99D9AE] transition-all duration-300">
                                <p className="">Get Started</p>
                                <MoveRight />
                            </div>
                            <div className="flex justify-center items-center space-x-[6px]  h-[42px] w-[134px] rounded-[6px] border-2 border-mainColor2 text-mainColor2 hover:bg-[#303A35] hover:text-[#FAFAFA] transition-all duration-300 ">
                                <Github />
                                <p className="">Get Started</p>
                            </div>
                        </div>
                        <div className="h-[110px] bg-[#0C0C0E] rounded-[14px] flex justify-around items-center mb-[140px]">
                            <div className="h-[57px] flex flex-col justify-center items-center">
                                <p className="text-[30px] text-mainColor2 font-bold">10M+</p>
                                <p className="text-[16px] text-[#A1A1AA] font-medium">Active Users</p>
                            </div>
                            <div className="h-[57px] flex flex-col justify-center items-center">
                                <p className="text-[30px] text-mainColor2 font-bold">256-bit</p>
                                <p className="text-[16px] text-[#A1A1AA] font-medium">Encryption</p>
                            </div>
                            <div className="h-[57px] flex flex-col justify-center items-center">
                                <p className="text-[30px] text-mainColor2 font-bold">99.9%</p>
                                <p className="text-[16px] text-[#A1A1AA] font-medium">Uptime</p>
                            </div>
                            <div className="h-[57px] flex flex-col justify-center items-center">
                                <p className="text-[30px] text-mainColor2 font-bold">100%</p>
                                <p className="text-[16px] text-[#A1A1AA] font-medium">Open Source</p>
                            </div>
                        </div>
                    </section>
                    <section className="page2 bg-[#171717] py-[154px] text-[#FFFFFF]"> 
                        <div className="flex flex-col justify-center items-center">
                            <p className="text-[32px] font-bold mb-[20px]">
                                Packed with <span className="bg-gradient-to-r  from-mainColor2 from-10%  to-mainColor1 to-90%  bg-clip-text text-transparent">Powerful Features</span>
                            </p>
                            <p className="w-[647px] text-[15px] text-[#9D9DA6] font-medium text-center">GigaChat combines cutting-edge technology with intuitive design to deliver the ultimate messaging experience</p>
                        </div>
                        <div className="grid grid-cols-4 px-[31px] mt-[65px] gap-[43px]">
                            <div className="w-[311px] h-[244px] bg-[#0C0C0E] rounded-[14px] py-[34px] px-[35px]
                            hover:bg-[#3b504416] border-2 border-transparent group hover:border-mainColor2 transition-all duration-150 ">
                                <MessageSquare size={30} className='text-mainColor2  bg-[#242925] rounded-full shadow-[0px_0px_11.5px_10px_rgb(36,41,37)] hover:bg-[#3b504416] border-2 border-transparent group hover:border-mainColor2 transition-all duration-150' />
                                <p className=" text-[20px] text-[#FFFFFF] font-semibold mt-[35px] mb-[10px] group-hover:text-mainColor2 ">Real-Time Messaging </p>
                                <p className="text-[16px] text-[#9D9DA6] font-medium group-hover:text-[#FFFFFF]">Instant message deliver with typing indicators and read receipts.</p>
                            </div>
                            <div className="w-[311px] h-[244px] bg-[#0C0C0E] rounded-[14px] py-[34px] px-[35px]
                            hover:bg-[#3b504416] border-2 border-transparent group hover:border-mainColor2 transition-all duration-150 ">
                                <Shield size={30} className='text-mainColor2  bg-[#242925] rounded-full shadow-[0px_0px_11.5px_10px_rgb(36,41,37)] hover:bg-[#3b504416] border-2 border-transparent ' />
                                <p className=" text-[20px] text-[#FFFFFF] font-semibold mt-[35px] mb-[10px] group-hover:text-mainColor2 ">End-to End Encryption </p>
                                <p className="text-[16px] text-[#9D9DA6] font-medium group-hover:text-[#FFFFFF]">Your messages are encrypted and can only be read by the intended recipients.</p>
                            </div>
                            <div className="w-[311px] h-[244px] bg-[#0C0C0E] rounded-[14px] py-[34px] px-[35px]
                            hover:bg-[#3b504416] border-2 border-transparent group hover:border-mainColor2 transition-all duration-150 ">
                                <Users size={30} className='text-mainColor2  bg-[#242925] rounded-full shadow-[0px_0px_11.5px_10px_rgb(36,41,37)] hover:bg-[#3b504416] border-2 border-transparent ' />
                                <p className=" text-[20px] text-[#FFFFFF] font-semibold mt-[35px] mb-[10px] group-hover:text-mainColor2 ">Group Chats </p>
                                <p className="text-[16px] text-[#9D9DA6] font-medium group-hover:text-[#FFFFFF]">Create group conversations with advanced admin controls and member management.</p>
                            </div>
                            <div className="w-[311px] h-[244px] bg-[#0C0C0E] rounded-[14px] py-[34px] px-[35px]
                            hover:bg-[#3b504416] border-2 border-transparent group hover:border-mainColor2 transition-all duration-150 ">
                                <Zap size={30} className='text-mainColor2  bg-[#242925] rounded-full shadow-[0px_0px_11.5px_10px_rgb(36,41,37)] hover:bg-[#3b504416] border-2 border-transparent ' />
                                <p className=" text-[20px] text-[#FFFFFF] font-semibold mt-[35px] mb-[10px] group-hover:text-mainColor2 ">Lighting Fast </p>
                                <p className="text-[16px] text-[#9D9DA6] font-medium group-hover:text-[#FFFFFF]">Optimized for speed with minimal latency for a seamless chat experience.</p>
                            </div>
                            <div className="w-[311px] h-[244px] bg-[#0C0C0E] rounded-[14px] py-[34px] px-[35px]
                            hover:bg-[#3b504416] border-2 border-transparent group hover:border-mainColor2 transition-all duration-150 ">
                                <Globe size={30} className='text-mainColor2  bg-[#242925] rounded-full shadow-[0px_0px_11.5px_10px_rgb(36,41,37)] hover:bg-[#3b504416] border-2 border-transparent ' />
                                <p className=" text-[20px] text-[#FFFFFF] font-semibold mt-[35px] mb-[10px] group-hover:text-mainColor2 ">Cross-Platform </p>
                                <p className="text-[16px] text-[#9D9DA6] font-medium group-hover:text-[#FFFFFF]">Available on web, iOS, Android, and desktop for a consistent experience everywhere.</p>
                            </div>
                            <div className="w-[311px] h-[244px] bg-[#0C0C0E] rounded-[14px] py-[34px] px-[35px]
                            hover:bg-[#3b504416] border-2 border-transparent group hover:border-mainColor2 transition-all duration-150 ">
                                <ChevronsLeftRight size={30} className='text-mainColor2  bg-[#242925] rounded-full shadow-[0px_0px_11.5px_10px_rgb(36,41,37)] hover:bg-[#3b504416] border-2 border-transparent ' />
                                <p className=" text-[20px] text-[#FFFFFF] font-semibold mt-[35px] mb-[10px] group-hover:text-mainColor2 ">Developer API </p>
                                <p className="text-[16px] text-[#9D9DA6] font-medium group-hover:text-[#FFFFFF]">Comprehensive API for integrating GigaChat into your own applications.</p>
                            </div>
                            <div className="w-[311px] h-[244px] bg-[#0C0C0E] rounded-[14px] py-[34px] px-[35px]
                            hover:bg-[#3b504416] border-2 border-transparent group hover:border-mainColor2 transition-all duration-150 ">
                                <Lock size={30} className='text-mainColor2  bg-[#242925] rounded-full shadow-[0px_0px_11.5px_10px_rgb(36,41,37)] hover:bg-[#3b504416] border-2 border-transparent  ' />
                                <p className=" text-[20px] text-[#FFFFFF] font-semibold mt-[35px] mb-[10px] group-hover:text-mainColor2 ">Data Privacy </p>
                                <p className="text-[16px] text-[#9D9DA6] font-medium group-hover:text-[#FFFFFF]">Your data is never sold or shared with third parties. We respect your privacy.</p>
                            </div>
                            <div className="w-[311px] h-[244px] bg-[#0C0C0E] rounded-[14px] py-[34px] px-[35px]
                            hover:bg-[#3b504416] border-2 border-transparent group hover:border-mainColor2 transition-all duration-150 ">
                                <Smartphone size={30} className='text-mainColor2  bg-[#242925] rounded-full shadow-[0px_0px_11.5px_10px_rgb(36,41,37)] hover:bg-[#3b504416] border-2 border-transparent ' />
                                <p className=" text-[20px] text-[#FFFFFF] font-semibold mt-[35px] mb-[10px] group-hover:text-mainColor2 ">Voice & Video</p>
                                <p className="text-[16px] text-[#9D9DA6] font-medium group-hover:text-[#FFFFFF]">High-quality voice and video calls with screen sharing capabilities.</p>
                            </div>
                        </div>
                    </section>
                    <section className={`page3 grid grid-cols-2 bg-contain  bg-center  h-[682px] z-30 ps-[37px]`} style={{
                    backgroundImage: `url(${backgroundPage3})`,
                    }}> 
                        <div className="text-[#FFFFFF] flex flex-col justify-center ">
                            <p className="text-[32px] text-[#FFFFFF] font-bold">
                            Bank-Grade <span className="bg-gradient-to-r  from-mainColor2 from-10%  to-mainColor1 to-90%  bg-clip-text text-transparent">Security</span>
                            </p>
                            <p className="text-[18px] text-[#9D9DA6] font-medium mt-[25px] mb-[29px]">
                            GigaChat is built from the ground up with security as the top priority. We use the latest encryption technologies to ensure your conversation remain private and secure.
                            </p>
                            <div className="grid grid-cols-2 gap-[42px]">
                                <div className="flex items-start space-x-[14px]">
                                    <div className="p-[8px]  bg-[#282F2B] flex justify-center items-center rounded-full">
                                        <Shield size={24} className='text-mainColor2'/>
                                    </div>
                                    <div className="space-y-[4px]">
                                        <p className="text-[16px] font-bold">End-to-End Encryption</p>
                                        <p className="text-[14px] text-[#9D9DA6] font-medium w-[247px]">All messages are encrypted on your device and can only be decrypted by the recipient</p>
                                    </div>
                                </div>
                                <div className="flex items-start space-x-[14px]">
                                    <div className="p-[8px]  bg-[#282F2B] flex justify-center items-center rounded-full">
                                        <Eye size={24} className='text-mainColor2'/>
                                    </div>
                                    <div className="space-y-[4px]">
                                        <p className="text-[16px] font-bold">Zero Knowledge Architecture</p>
                                        <p className="text-[14px] text-[#9D9DA6] font-medium w-[247px]">We can’t read your messages, even if we wanted to. Your privacy is guaranteed by design.</p>
                                    </div>
                                </div>
                                <div className="flex items-start space-x-[14px]">
                                    <div className="p-[8px]  bg-[#282F2B] flex justify-center items-center rounded-full">
                                        <Lock size={24} className='text-mainColor2'/>
                                    </div>
                                    <div className="space-y-[4px]">
                                        <p className="text-[16px] font-bold">Multi-Factor Authentication</p>
                                        <p className="text-[14px] text-[#9D9DA6] font-medium w-[247px]">Add an extra layer of security with optional MFA, ensuring only authorized access to your account.</p>
                                    </div>
                                </div>
                                <div className="flex items-start space-x-[14px]">
                                    <div className="p-[8px]  bg-[#282F2B] flex justify-center items-center rounded-full">
                                        <Server size={24} className='text-mainColor2'/>
                                    </div>
                                    <div className="space-y-[4px]">
                                        <p className="text-[16px] font-bold">Secure Data Storage</p>
                                        <p className="text-[14px] text-[#9D9DA6] font-medium w-[247px]">All stored data is encrypted with military-grade encryption and regularly audited.</p>
                                    </div>
                                </div>
                                <div className="flex items-start space-x-[14px]">
                                    <div className="p-[8px]  bg-[#282F2B] flex justify-center items-center rounded-full">
                                        <FileCheck size={24} className='text-mainColor2'/>
                                    </div>
                                    <div className="space-y-[4px]">
                                        <p className="text-[16px] font-bold">Regular Security Audits</p>
                                        <p className="text-[14px] text-[#9D9DA6] font-medium w-[247px]">Our systems undergo regular third-party security audits to ensure maximum protection.</p>
                                    </div>
                                </div>
                                <div className="flex items-start space-x-[14px]">
                                    <div className="p-[8px]  bg-[#282F2B] flex justify-center items-center rounded-full">
                                        <RefreshCcw size={24} className='text-mainColor2'/>
                                    </div>
                                    <div className="space-y-[4px]">
                                        <p className="text-[16px] font-bold">Automatic Updates</p>
                                        <p className="text-[14px] text-[#9D9DA6] font-medium w-[247px]">Security patches are automatically deployed to keep your data safe from emerging threats.</p>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div className="">
                            <SecurityModel/>
                        </div>
                    </section>
                    <section className="page4 font-Inter bg-[#171717] text-[#FFFFFF]"> 
                        <div className="flex flex-col items-center pt-[90px]">
                            <p className="text-[32px] font-bold mb-[25px]">Integrate <span className="bg-gradient-to-r  from-mainColor2 from-0%  to-mainColor1 to-100%  bg-clip-text text-transparent"> GigaChat</span> Anywhere</p>
                            <p className="w-[627px]  text-[16px] text-[#9D9DA6] font-medium text-center">Add secure, real-time chat to your website or app in minutes with our easy-to-use SDK. Completely free and open source.</p>
                        </div>
                        <div className="flex h-[711px] px-[36px]  items-center  space-x-[54px]">
                            <div className=" w-[660px]  rounded-[16px] overflow-hidden ">
                                <div className="h-[50px] bg-[rgba(10,10,12,0.7)] flex items-center px-[20px]">
                                    <div className="flex items-center space-x-[8px]">
                                        <div className="w-[12px] h-[12px] rounded-full bg-[#FF605C]"></div>
                                        <div className="w-[12px] h-[12px] rounded-full bg-[#FFBD44]"></div>
                                        <div className="w-[12px] h-[12px] rounded-full bg-[#00CA4E]"></div>
                                    </div>
                                    <div className=" flex-grow flex justify-center">
                                        <div className="w-[227px] h-[27px]  text-[#A1A1AA] bg-[#222224] rounded-[10px] flex justify-between items-center " id='parts'>
                                            <p className="px-[13px] py-[5px] text-[12px]  font-medium cursor-pointer  rounded-[10px]" onClick={(e)=>{
                                                getPart(e.target)
                                            }}>JavaScript</p>
                                            <p className="px-[13px] py-[5px] text-[12px]  font-medium cursor-pointer rounded-[10px]" onClick={(e)=>{
                                                getPart(e.target)
                                            }}>React</p>
                                            <p className="px-[13px] py-[5px] text-[12px]  font-medium cursor-pointer rounded-[10px]" onClick={(e)=>{
                                                getPart(e.target)
                                            }}>HTML</p>
                                        </div>
                                    </div>
                                </div>
                                <div className="min-h-[246px] bg-[#0B0B0C] text-[14px] text-[#A1A1AA] font-medium">
                                    <div className="px-[32px] py-[22px]">
                                        <p className="white-space: pre-wrap">
                                        <pre>{code}</pre>
                                        </p>

                                    </div>
                                </div>
                            </div>
                            <div className="">
                                <div className="">
                                    <p className="text-[24px] font-bold">Powerful SDK for<span className="bg-gradient-to-r  from-mainColor2 from-0%  to-mainColor1 to-100%  bg-clip-text text-transparent"> Developers</span> </p>
                                    <p className="text-[16px] text-[#9D9DA6] font-medium w-[671px] my-[25px]">
                                    Our SDK makes it simple to add secure messaging to any platform. With just a few lines of code you can integrate GigaChat into your website, mobile app, or desktop application. 
                                    </p>
                                    <div className="flex flex-col space-y-[25px]">
                                        <div className="flex items-start space-x-[15px]">
                                            <div className="p-[5px]  bg-[#282F2B] flex justify-center items-center rounded-full">
                                            <Check size={15} className='text-mainColor2'/>
                                            </div>
                                            <div className="text-[#FFFFFF] ">
                                                <p className="text-[16px] font-semibold ">Simple Integration</p>
                                                <p className="text-[14px] text-[#9D9DA6] font-medium">Add GigaChat to your app with just a few lines of code. No complex setup required.</p>
                                            </div>
                                        </div>
                                        <div className="flex items-start space-x-[15px]">
                                            <div className="p-[5px]  bg-[#282F2B] flex justify-center items-center rounded-full">
                                            <Check size={15} className='text-mainColor2'/>
                                            </div>
                                            <div className="text-[#FFFFFF] ">
                                                <p className="text-[16px] font-semibold ">Customizable UI</p>
                                                <p className="text-[14px] text-[#9D9DA6] font-medium">Match GigaChat’s appearance to your brand with our theme options.</p>
                                            </div>
                                        </div>
                                        <div className="flex items-start space-x-[15px]">
                                            <div className="p-[5px]  bg-[#282F2B] flex justify-center items-center rounded-full">
                                            <Check size={15} className='text-mainColor2'/>
                                            </div>
                                            <div className="text-[#FFFFFF] ">
                                                <p className="text-[16px] font-semibold ">Comprehensive API</p>
                                                <p className="text-[14px] text-[#9D9DA6] font-medium">Access all GigaChat features through our well-documented API.</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div className="h-[178px] bg-[#0C0C0E] rounded-[16px] mt-[30px] py-[29px] px-[24px]">
                                    <h3 className="text-[16px] font-bold">Open Source Community</h3>
                                    <p className="text-[14px] text-[#9D9DA6] font-medium w-[610px] my-[11px]">
                                        Join our growing community of developers. Contribute to the project, report issues, or suggest new features.
                                    </p>
                                    <button className='w-full h-[40px] bg-mainColor2 rounded-[6px] text-[#0C0C0E] flex justify-center items-center space-x-[6px]'>
                                        <Github size={20}/>
                                        <p className="text-[14px] font-semibold">View on GitHub</p>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </section>
                    <section className="page5 font-Inter bg-[#171717] text-[#FFFFFF]"> 
                        <div className="bg-[#0C0C0E] h-[281px] mx-[36px] py-[34px] rounded-[16px] flex flex-col items-center mb-[169px]">
                            <div className="h-[24px] w-[180px] bg-[#39493E] text-mainColor2 rounded-full text-[12px] font-bold flex justify-center items-center">
                                <p className=" capitalize">POWERED BY GIGACHAT</p>
                            </div>
                            <h2 className="text-[24px] font-bold mt-[15px] mb-[19px]">
                                Add the "Powered by GigaChat" badge to your integration 
                            </h2>
                            <p className="text-[16px] text-[#9D9DA6] text-center font-medium w-[627px]">Show your users that you’re using the most secure and reliable open source chat platform available.
                            </p>
                            <div className="flex space-x-[23px] text-[14px] font-semibold mt-[29px]">
                                <div className="h-[54px] border-[1px] border-[#1C1C1C] rounded-[12px] flex justify-center items-center space-x-[6px] px-[17px] ">
                                    <div className="w-[8px] h-[8px] rounded-full bg-mainColor2"></div>
                                    <p className="">
                                    Powered by GigaChat
                                    </p>
                                </div>
                                <div className="h-[54px] border-[1px] border-[#1C1C1C] rounded-[12px] flex justify-center items-center space-x-[6px] px-[17px] ">
                                    <div className="w-[8px] h-[8px] rounded-full bg-mainColor1"></div>
                                    <p className="">
                                    GigaChat Inside
                                    </p>
                                </div>
                                <div className="h-[54px] border-[1px] border-[#1C1C1C] rounded-[12px] flex justify-center items-center space-x-[6px] px-[17px] ">
                                    <div className="w-[8px] h-[8px] rounded-full bg-mainColor2"></div>
                                    <p className="">
                                    Secured by GigaChat
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div className="w-full flex justify-center ">
                            <div className="w-[1024px] h-[278px] bg-[#0C0C0E] mb-[98px] rounded-[16px] pt-[28px] pb-[50px] flex flex-col items-center">
                                <h1 className="text-[36px] font-bold mb-[10px]">Ready to Experience the <span className='text-mainColor2'> Future of Messaging</span>?</h1>
                                <p className="w-[619px]  text-[16px] text-[#9D9DA6] font-medium text-center">
                                Join thousands of users who trust GigaChat for secure, reliable, and feature-rich messaging. GigaChat is completely <span className='text-mainColor2'> free and open source</span>.
                                </p>
                                <div className="flex items-center space-x-[15px] text-[14px] text-[#0B320B] font-semibold mt-[43px] ">
                                    <div className="flex justify-center items-center px-[29px] bg-mainColor2 h-[44px]  rounded-[6px] space-x-[10px] hover:bg-[#99D9AE] transition-all duration-300">
                                        <p className="">Get Started</p>
                                        <MoveRight />
                                    </div>
                                    <div className="flex justify-center items-center space-x-[6px]  h-[42px]  rounded-[6px] border-2 border-mainColor2 text-mainColor2 hover:bg-[#303A35] px-[29px] hover:text-[#FAFAFA] transition-all duration-300 ">
                                        <Github size={20}/>
                                        <p className="">Star on GitHub</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                    
                </main>
                <footer className='h-[549px] border-t-[1px] bg-[#171717] border-[#1C1C1C] flex flex-col items-center pt-[49px] pb-[39px]'>
                    <div className="flex flex-col items-center">
                        <img src={Logo} alt="" className='w-[145px]'/>
                        <p className="text-[16px] text-[#9D9DA6] text-center font-medium w-[448px] mt-[26px] mb-[32px]">The next generation of secure messaging. Free and open source. </p>
                        <div className="grid grid-cols-4 gap-x-[104px] text-center mb-[58px]">
                            <div className="space-y-[10px]">
                                <h4 className="text-[16px] font-semibold text-mainColor2 mb-[4px] ">Product</h4>
                                <p className="text-[14px] font-medium text-[#9D9DA6]">Features</p>
                                <p className="text-[14px] font-medium text-[#9D9DA6]">Security</p>
                                <p className="text-[14px] font-medium text-[#9D9DA6]">Roadmap</p>
                            </div>
                            <div className="space-y-[10px]">
                                <h4 className="text-[16px] font-semibold text-mainColor2 mb-[4px] ">Developers</h4>
                                <p className="text-[14px] font-medium text-[#9D9DA6]">Integration</p>
                                <p className="text-[14px] font-medium text-[#9D9DA6]">Documentation</p>
                                <p className="text-[14px] font-medium text-[#9D9DA6]">API</p>
                            </div>
                            <div className="space-y-[10px]">
                                <h4 className="text-[16px] font-semibold text-mainColor2 mb-[4px] ">Company</h4>
                                <p className="text-[14px] font-medium text-[#9D9DA6]">About</p>
                                <p className="text-[14px] font-medium text-[#9D9DA6]">Blog</p>
                                <p className="text-[14px] font-medium text-[#9D9DA6]">Contact</p>
                            </div>
                            <div className="space-y-[10px]">
                                <h4 className="text-[16px] font-semibold text-mainColor2 mb-[4px] ">Legal</h4>
                                <p className="text-[14px] font-medium text-[#9D9DA6]">Privacy</p>
                                <p className="text-[14px] font-medium text-[#9D9DA6]">Terms</p>
                                <p className="text-[14px] font-medium text-[#9D9DA6]">Cookies</p>
                            </div>
                        </div>
                        <div className="flex  items-center space-x-[40px] text-[#9D9DA6] mb-[40px]">
                        <Github strokeWidth={2} size={30} className='cursor-pointer'/>
                        <Twitter strokeWidth={2} size={30} className='cursor-pointer'/>
                        <Linkedin strokeWidth={2} size={30} className='cursor-pointer'/>
                        </div>
                    </div>
                    <div className="h-[120px] w-full border-t-[1px] border-[#1C1C1C] pt-[37px] flex flex-col items-center space-y-[10px]">
                        <p className="text-[14px] text-[#9D9DA6] font-semibold">© 2025 GigaChat. All rights reserved.</p>
                        <p className="text-[12px] text-[#78787E] font-medium">Made with <Heart size={20} className='text-mainColor2 inline' />  by the GigaChat Team</p>
                    </div>
                </footer>
            </div>
                
        </section>
    );
}

export default LandingPage;
