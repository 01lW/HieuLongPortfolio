document.addEventListener(
    "DOMContentLoaded",
    function () {

        const themeToggle =
            document.getElementById("themeToggle");

        const themeIcon =
            document.getElementById("themeIcon");


        if (!themeToggle || !themeIcon) {
            return;
        }


        function getTheme() {

            return (
                document.documentElement
                    .getAttribute("data-theme")
                || "dark"
            );

        }


        function updateThemeIcon() {

            const theme =
                getTheme();


            if (theme === "dark") {

                themeIcon.textContent =
                    "☀";

                themeToggle.setAttribute(
                    "aria-label",
                    "Switch to light mode"
                );

            }
            else {

                themeIcon.textContent =
                    "☾";

                themeToggle.setAttribute(
                    "aria-label",
                    "Switch to dark mode"
                );

            }

        }


        themeToggle.addEventListener(
            "click",
            function () {

                const currentTheme =
                    getTheme();


                const newTheme =
                    currentTheme === "dark"
                        ? "light"
                        : "dark";


                document.documentElement
                    .setAttribute(
                        "data-theme",
                        newTheme
                    );


                localStorage.setItem(
                    "portfolio-theme",
                    newTheme
                );


                updateThemeIcon();

            }
        );


        updateThemeIcon();

    }
);