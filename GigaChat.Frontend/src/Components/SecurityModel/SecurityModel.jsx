import { motion, AnimatePresence } from "framer-motion";
import { useState } from "react";
import { Shield, Lock, Eye } from 'lucide-react';

function SecurityModel() {
    const [hovered, setHovered] = useState(false);

    return (
        <div className="w-full h-full flex items-center justify-center">
        <motion.div
            onMouseEnter={() => setHovered(true)}
            onMouseLeave={() => setHovered(false)}
            className="relative w-[400px] h-[400px]"
        >
            {/* Outer rings */}
            <motion.div
            className="absolute inset-0 rounded-full border-4 border-primary"
            animate={{
                rotate: 360,
                scale: hovered ? [1, 1.05, 1] : 1,
            }}
            transition={{
                rotate: { duration: 20, repeat: Infinity, ease: "linear" },
                scale: { duration: 1.5, repeat: hovered ? Infinity : 0, ease: "easeInOut" },
            }}
            />

            <motion.div
            className="absolute inset-0 rounded-full border-4 border-secondary/30"
            animate={{
                rotate: -360,
                scale: hovered ? [1.1, 1.15, 1.1] : 1.1,
            }}
            transition={{
                rotate: { duration: 25, repeat: Infinity, ease: "linear" },
                scale: { duration: 2, repeat: hovered ? Infinity : 0, ease: "easeInOut" },
            }}
            />

            {/* Orbiting elements */}
            {[0, 1, 2].map((i) => (
            <motion.div
                key={i}
                className="absolute w-12 h-12 rounded-full"
                style={{
                left: "calc(50% - 24px)",
                top: "calc(50% - 24px)",
                }}
                animate={{
                x: Math.cos(i * ((Math.PI * 2) / 3)) * 150,
                y: Math.sin(i * ((Math.PI * 2) / 3)) * 150,
                rotate: 360,
                }}
                transition={{
                x: { duration: 10 + i * 2, repeat: Infinity, ease: "linear" },
                y: { duration: 10 + i * 2, repeat: Infinity, ease: "linear" },
                rotate: { duration: 10, repeat: Infinity, ease: "linear" },
                }}
            >
                <motion.div
                className="w-full h-full flex items-center justify-center bg-black/80 rounded-full p-2"
                whileHover={{ scale: 1.2 }}
                >
                {i === 0 && <Lock className="w-6 h-6 text-primary" />}
                {i === 1 && <Shield className="w-6 h-6 text-secondary" />}
                {i === 2 && <Eye className="w-6 h-6 text-primary" />}
                </motion.div>
            </motion.div>
            ))}

            {/* Center element */}
            <motion.div
            className="absolute left-1/2 top-1/2 -translate-x-1/2 -translate-y-1/2 w-32 h-32 bg-black/80 rounded-full flex items-center justify-center"
            animate={{
                scale: hovered ? [1, 1.05, 1] : 1,
                boxShadow: hovered
                ? [
                    "0 0 0 0 rgba(168, 240, 192, 0)",
                    "0 0 0 20px rgba(168, 240, 192, 0.2)",
                    "0 0 0 0 rgba(168, 240, 192, 0)",
                    ]
                : "0 0 0 0 rgba(168, 240, 192, 0)",
            }}
            transition={{
                scale: { duration: 1.5, repeat: hovered ? Infinity : 0, ease: "easeInOut" },
                boxShadow: { duration: 1.5, repeat: hovered ? Infinity : 0, ease: "easeInOut" },
            }}
            >
            <Shield className="w-16 h-16 text-primary" />
            </motion.div>

            {/* Particle effects */}
            <AnimatePresence>
            {hovered && (
                <>
                {[...Array(10)].map((_, i) => (
                    <motion.div
                    key={`particle-${i}`}
                    className="absolute left-1/2 top-1/2 w-2 h-2 rounded-full bg-primary/60"
                    initial={{
                        x: 0,
                        y: 0,
                        scale: 0,
                        opacity: 1,
                    }}
                    animate={{
                        x: (Math.random() - 0.5) * 300,
                        y: (Math.random() - 0.5) * 300,
                        scale: Math.random() * 2,
                        opacity: 0,
                    }}
                    exit={{ opacity: 0, scale: 0 }}
                    transition={{ duration: 1 + Math.random() * 1 }}
                    />
                ))}
                </>
            )}
            </AnimatePresence>
        </motion.div>
        </div>
    );
}

export default SecurityModel;