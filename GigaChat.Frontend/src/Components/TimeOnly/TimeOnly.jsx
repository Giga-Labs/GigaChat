import React from 'react';

const TimeOnly = ({ dateStr }) => {
    const date = new Date(dateStr);
    const time = date.toLocaleTimeString('en-US', {
        hour: '2-digit',
        minute: '2-digit',
        hour12: true
    });

    return <span>{time}</span>;
};

export default TimeOnly;