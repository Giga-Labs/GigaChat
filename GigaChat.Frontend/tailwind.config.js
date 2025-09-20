/** @type {import('tailwindcss').Config} */

export default {
  content: ["./index.html","./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    container:{
      center:true,
    },
    extend: {
      colors:{
        mainColor1:"#C5B8F9",
        mainColor2:"#A8F0C0",
        primary: '#a8f0c0',
        secondary: '#c4b5fd',
        backgroundcolor: '#1a1a1a',



      },
      fontFamily:{
        manrope:'Manrope Variable',
        Inter:"Inter"
      }
      
    },
  },
  plugins: [],
}

