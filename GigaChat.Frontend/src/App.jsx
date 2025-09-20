
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import Login from './Pages/Login/Login';
import SignUp from './Pages/SignUp/SignUp';
import ConversationMain from './Pages/ConversationMain/ConversationMain';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import UserProvider from './Context/User.context';
import LandingPage from './Pages/LandingPage/LandingPage';
import { Toaster } from 'react-hot-toast';
import ProtectedRoute from './Components/ProtectedRoute/ProtectedRoute';
import Layout  from './Components/Layout/Layout';
// import ForgetPassword from './Components/ForgetPassword/ForgetPassword';
// import AllProvider from './Context/All.context';
// import Dashboard from './Pages/Dashboard/Dashboard';
// import Home from "./Pages/Home/Home"
// import Fields from './Pages/Fields/Fields';
// import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
// import VerficationEmail from './Components/VerficationEmail/VerficationEmail';
// import OptVerfication from './Components/OptVerfication/OptVerfication';
// import Check from './Components/Check/Check';
// import ResetPassword from './Components/ResetPassword/ResetPassword';
// import NewConversation from './Components/NewConversation/NewConversation';
// import ChangePassword from './Components/ChangePassword/ChangePassword';
// import GroupChatSettings from './Components/GroupChatSettings/GroupChatSettings';
// import IncomingAudioCall from './Components/IncomingAudioCall/IncomingAudioCall';
// import IncomingVideoCall from './Components/IncomingVideoCall/IncomingVideoCall';
// import IncomingGroupAudioCall from './Components/IncomingGroupAudioCall/IncomingGroupAudioCall';
// import VoiceCall from './Pages/VoiceCall/VoiceCall';
// import GroupGideoCall from './Pages/GroupGideoCall/GroupGideoCall';
// import LandingPage from './Pages/LandingPage/LandingPage';
// import SecurityModel from './Components/SecurityModel/SecurityModel';

//import ProtectedRoute from './Components/ProtectedRoute/ProtectedRoute';
// import Layout from './Components/Layout/Layout';
// import Login from './Pages/Login/Login'
// import SignUp from './Pages/SignUp/SignUp'
// import UserProvider from './Context/User.context';
// import { Toaster } from 'react-hot-toast';
// import Landing from './Pages/Landing/Landing';
// import EmailVerified from './Pages/EmailVerified/EmailVerified';
// import VerificationFailed from './Pages/VerificationFailed/VerificationFailed';
// import EmailConfirmation from './Pages/EmailConfirmation/EmailConfirmation';
ConversationMain
function App() {
  const router=createBrowserRouter([
    {index:true,element:<LandingPage/>},
    {path:"/",element:<ProtectedRoute><Layout/></ProtectedRoute> ,children:[
      {path:"ConversationMain",element:<ConversationMain/>},
]},
    {path:"/",element:<Layout/>},
    {path:"/LandingPage",element:<LandingPage/>},
    {path:"/Login",element:<Login/>},
    {path:"/SignUp",element:<SignUp/>}
  ]);
  const myClient=new QueryClient();
  
  return (
    <>
    <QueryClientProvider client={myClient}>
      <UserProvider>
            <RouterProvider router={router}/>
            <Toaster position="top-right" reverseOrder={false}/>
      </UserProvider>
    </QueryClientProvider>
    </>
    
  )
}

export default App
