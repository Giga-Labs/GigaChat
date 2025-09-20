// import  {  useState } from 'react';
PinInput
//import { AllContext } from '../../Context/All.context';

import { PinInput } from 'react-input-pin-code';

const Check = (children) => {
        
        //const [values, setValues] = useState(["", "", "", "", "", ""]);
        return (
            <PinInput
            values={children.otpValue}
            onChange={(value, index, values) => children.setOtpValue(values)}
            onFocus={"text-red-600"}
            autoFocus={"true"}
            validBorderColor={"rgb(197,184,249)"}
            focusBorderColor={"rgb(197,184,249)"}
            errorBorderColor={"rgb(255,51,58)"}
            placeholder={"*"}
            inputClassName={'mx-2 !border-0 text-center  text-white !bg-[#27272A]  rounded-[10px] '}
            containerClassName='flex justify-between items-center '
            />
        );
    
}

export default Check;
