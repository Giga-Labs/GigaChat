import React, { useEffect, useState } from "react";
import { motion } from "framer-motion";

function StarryBackground() {
    const [mounted, setMounted] = useState(false);

    useEffect(() => {
        setMounted(true);
    }, []);

    if (!mounted) return null;

    return (
        <div className="fixed inset-0 overflow-hidden pointer-events-none z-0">
        {/* Background color */}
        <div className="absolute inset-0 bg-[#171717] z-0" />

        {/* Static background gradient */}
        <div className="absolute inset-0 z-0">
            <div
            className="absolute inset-0 opacity-30"
            style={{
                backgroundImage: `radial-gradient(circle at 50% 50%, rgba(168, 240, 192, 0.15), transparent 40%),
                                radial-gradient(circle at 70% 30%, rgba(196, 181, 253, 0.15), transparent 40%)`,
            }}
            />
        </div>

        {/* Moving green light */}
        <motion.div
            className="absolute w-[500px] h-[500px] rounded-full bg-primary/5 blur-[100px] z-0"
            animate={{
            x: ["-20%", "60%", "30%", "-20%"],
            y: ["10%", "30%", "60%", "10%"],
            opacity: [0.3, 0.5, 0.3, 0.5, 0.3],
            }}
            transition={{
            duration: 20,
            repeat: Infinity,
            ease: "easeInOut",
            }}
        />

        {/* Secondary moving light */}
        <motion.div
            className="absolute w-[300px] h-[300px] rounded-full bg-secondary/5 blur-[80px] z-0"
            animate={{
            x: ["60%", "20%", "70%", "60%"],
            y: ["60%", "20%", "40%", "60%"],
            opacity: [0.2, 0.4, 0.2, 0.4, 0.2],
            }}
            transition={{
            duration: 15,
            repeat: Infinity,
            ease: "easeInOut",
            }}
        />

        {/* Stars/particles with slow movement and twinkling */}
        <div className="absolute inset-0 z-0 overflow-hidden">
            {/* Small stars */}
            {Array.from({ length: 50 }).map((_, i) => (
            <motion.div
                key={`small-${i}`}
                className="absolute w-1 h-1 rounded-full bg-white/20"
                initial={{
                x: Math.random() * window.innerWidth,
                y: Math.random() * window.innerHeight,
                }}
                animate={{
                x: [
                    Math.random() * window.innerWidth,
                    Math.random() * window.innerWidth,
                    Math.random() * window.innerWidth,
                ],
                y: [
                    Math.random() * window.innerHeight,
                    Math.random() * window.innerHeight,
                    Math.random() * window.innerHeight,
                ],
                opacity: [0.1, 0.5, 0.1],
                }}
                transition={{
                x: {
                    duration: 180 + Math.random() * 60,
                    repeat: Infinity,
                    ease: "linear",
                },
                y: {
                    duration: 180 + Math.random() * 60,
                    repeat: Infinity,
                    ease: "linear",
                },
                opacity: {
                    duration: 2 + Math.random() * 3,
                    repeat: Infinity,
                    ease: "easeInOut",
                    delay: Math.random() * 2,
                },
                }}
            />
            ))}

            {/* Medium stars */}
            {Array.from({ length: 30 }).map((_, i) => (
            <motion.div
                key={`medium-${i}`}
                className="absolute w-1.5 h-1.5 rounded-full bg-white/30"
                initial={{
                x: Math.random() * window.innerWidth,
                y: Math.random() * window.innerHeight,
                }}
                animate={{
                x: [
                    Math.random() * window.innerWidth,
                    Math.random() * window.innerWidth,
                    Math.random() * window.innerWidth,
                ],
                y: [
                    Math.random() * window.innerHeight,
                    Math.random() * window.innerHeight,
                    Math.random() * window.innerHeight,
                ],
                opacity: [0.2, 0.6, 0.2],
                }}
                transition={{
                x: {
                    duration: 210 + Math.random() * 60,
                    repeat: Infinity,
                    ease: "linear",
                },
                y: {
                    duration: 210 + Math.random() * 60,
                    repeat: Infinity,
                    ease: "linear",
                },
                opacity: {
                    duration: 3 + Math.random() * 4,
                    repeat: Infinity,
                    ease: "easeInOut",
                    delay: Math.random() * 2,
                },
                }}
            />
            ))}

            {/* Large stars */}
            {Array.from({ length: 15 }).map((_, i) => (
            <motion.div
                key={`large-${i}`}
                className="absolute w-2 h-2 rounded-full bg-white/40"
                initial={{
                x: Math.random() * window.innerWidth,
                y: Math.random() * window.innerHeight,
                }}
                animate={{
                x: [
                    Math.random() * window.innerWidth,
                    Math.random() * window.innerWidth,
                    Math.random() * window.innerWidth,
                ],
                y: [
                    Math.random() * window.innerHeight,
                    Math.random() * window.innerHeight,
                    Math.random() * window.innerHeight,
                ],
                opacity: [0.3, 0.7, 0.3],
                scale: [1, 1.2, 1],
                }}
                transition={{
                x: {
                    duration: 240 + Math.random() * 60,
                    repeat: Infinity,
                    ease: "linear",
                },
                y: {
                    duration: 240 + Math.random() * 60,
                    repeat: Infinity,
                    ease: "linear",
                },
                opacity: {
                    duration: 4 + Math.random() * 4,
                    repeat: Infinity,
                    ease: "easeInOut",
                    delay: Math.random() * 2,
                },
                scale: {
                    duration: 4 + Math.random() * 4,
                    repeat: Infinity,
                    ease: "easeInOut",
                    delay: Math.random() * 2,
                },
                }}
            />
            ))}

            {/* Green accent stars */}
            {Array.from({ length: 10 }).map((_, i) => (
            <motion.div
                key={`accent-${i}`}
                className="absolute w-2 h-2 rounded-full bg-primary/60"
                initial={{
                x: Math.random() * window.innerWidth,
                y: Math.random() * window.innerHeight,
                }}
                animate={{
                x: [
                    Math.random() * window.innerWidth,
                    Math.random() * window.innerWidth,
                    Math.random() * window.innerWidth,
                ],
                y: [
                    Math.random() * window.innerHeight,
                    Math.random() * window.innerHeight,
                    Math.random() * window.innerHeight,
                ],
                opacity: [0.3, 0.8, 0.3],
                scale: [1, 1.3, 1],
                }}
                transition={{
                x: {
                    duration: 270 + Math.random() * 60,
                    repeat: Infinity,
                    ease: "linear",
                },
                y: {
                    duration: 270 + Math.random() * 60,
                    repeat: Infinity,
                    ease: "linear",
                },
                opacity: {
                    duration: 5 + Math.random() * 5,
                    repeat: Infinity,
                    ease: "easeInOut",
                    delay: Math.random() * 2,
                },
                scale: {
                    duration: 5 + Math.random() * 5,
                    repeat: Infinity,
                    ease: "easeInOut",
                    delay: Math.random() * 2,
                },
                }}
            />
            ))}

            {/* Purple accent stars */}
            {Array.from({ length: 8 }).map((_, i) => (
            <motion.div
                key={`accent2-${i}`}
                className="absolute w-2 h-2 rounded-full bg-secondary/60"
                initial={{
                x: Math.random() * window.innerWidth,
                y: Math.random() * window.innerHeight,
                }}
                animate={{
                x: [
                    Math.random() * window.innerWidth,
                    Math.random() * window.innerWidth,
                    Math.random() * window.innerWidth,
                ],
                y: [
                    Math.random() * window.innerHeight,
                    Math.random() * window.innerHeight,
                    Math.random() * window.innerHeight,
                ],
                opacity: [0.3, 0.8, 0.3],
                scale: [1, 1.3, 1],
                }}
                transition={{
                x: {
                    duration: 300 + Math.random() * 60,
                    repeat: Infinity,
                    ease: "linear",
                },
                y: {
                    duration: 300 + Math.random() * 60,
                    repeat: Infinity,
                    ease: "linear",
                },
                opacity: {
                    duration: 5 + Math.random() * 5,
                    repeat: Infinity,
                    ease: "easeInOut",
                    delay: Math.random() * 2,
                },
                scale: {
                    duration: 5 + Math.random() * 5,
                    repeat: Infinity,
                    ease: "easeInOut",
                    delay: Math.random() * 2,
                },
                }}
            />
            ))}

            {/* Shooting stars */}
            {Array.from({ length: 5 }).map((_, i) => (
            <motion.div
                key={`shooting-${i}`}
                className="absolute w-0.5 h-[1px] bg-white"
                initial={{
                x: -100,
                y: Math.random() * window.innerHeight,
                rotate: 20 + Math.random() * 20,
                scaleX: 0,
                opacity: 0,
                }}
                animate={{
                x: window.innerWidth + 100,
                scaleX: [0, 15, 0],
                opacity: [0, 0.8, 0],
                }}
                transition={{
                duration: 2 + Math.random() * 4,
                repeat: Infinity,
                repeatDelay: 10 + Math.random() * 20,
                ease: "easeInOut",
                delay: Math.random() * 10,
                }}
            />
            ))}
        </div>
        </div>
    );
}

export default React.memo(StarryBackground);