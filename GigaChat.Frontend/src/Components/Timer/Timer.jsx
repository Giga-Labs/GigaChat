import React, { useEffect, useState } from 'react';

const Timer = ({ setOnTimer }) => {
    const [seconds, setSeconds] = useState(30);

    useEffect(() => {
        const intervalID = setInterval(() => {
            setSeconds(prevSeconds => {
                if (prevSeconds > 1) {
                    return prevSeconds - 1;
                } else {
                    clearInterval(intervalID);
                    setOnTimer(false);
                    return 0;
                }
            });
        }, 1000);

        return () => clearInterval(intervalID); // Cleanup on unmount
    }, [setOnTimer]);

    return (
        <p className="bg-[#a8f0c058] px-9 py-[12px] rounded-full text-[#cacccd] font-medium">
            Resend in {seconds} S
        </p>
    );
};

export default Timer;
