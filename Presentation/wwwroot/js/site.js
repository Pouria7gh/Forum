
// sets up sites color theme
$(document).ready(function () {
    const darkModeMql = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)');

    if (darkModeMql && darkModeMql.matches) {
        $("#html").attr("data-bs-theme", "dark");
    } else {
        $("#html").attr("data-bs-theme", "light");
    }
});

// admin navbar
$(document).ready(() => {
    $("#sidebar-toggle").on("click", () => {
        const $sidebarWrapper = $("#sidebar-wrapper");
        if (!$sidebarWrapper) {
            return;
        }

        if ($sidebarWrapper.hasClass("hide")) {
            $sidebarWrapper.removeClass("hide");
        } else {
            $sidebarWrapper.addClass("hide");
        }
    });
});