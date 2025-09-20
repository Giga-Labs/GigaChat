import { useState, useEffect } from 'react';
import Avatar from 'avataaars';
import { createAvatar } from '@dicebear/core';
import { identicon } from '@dicebear/collection';

const CustomAvatar = ({size}) => {
  const [avatarSvg, setAvatarSvg] = useState('');

  useEffect(() => {
    // إنشاء seed عشوائي جديد لكل استدعاء
    const newSeed = Math.random().toString(36).substring(7);
    
    // إنشاء الصورة باستخدام Dicebear
    const avatar = createAvatar(identicon, {
      seed: newSeed,
      scale: 80,
      radius: 50,
      backgroundColor: ['F9C440', '0099A8'],
      size:size,
    }).toString();

    setAvatarSvg(avatar);
  }, []); // يتم استدعاء useEffect مرة واحدة عند تحميل المكون

  return <div dangerouslySetInnerHTML={{ __html: avatarSvg }} />;
};

export default CustomAvatar;
