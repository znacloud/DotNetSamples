/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Pages/**/*.cshtml", "./Views/**/*.cshtml"],
  theme: {
    extend: {
      backgroundImage: {
        "gradient-radial": "radial-gradient(var(--tw-gradient-stops))",
        "gradient-conic":
          "conic-gradient(from 180deg at 50% 50%, var(--tw-gradient-stops))",
      },
      colors: {
        current: "#FF7F00",
        primary: "#FF7F00",
        primaryLight: "#33FF7F00",
        primaryHover: "#e8f8e8",
        secondary: "#FDDF19",
        secondaryLight: "#09FDDF19",
        info: "rgb(30 99 235)",
        process: "#09116B",
        success: "#33A12C",
        warn: "#FDDF19",
        warnLight: "#fca80025",
        error: "#d10202",
      },
    },
  },
  plugins: [],
};
