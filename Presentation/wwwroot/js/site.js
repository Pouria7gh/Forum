
// sets up sites color theme
$(document).ready(function () {
    const darkModeMql = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)');

    if (darkModeMql && darkModeMql.matches) {
        $("#html").attr("data-bs-theme", "dark");
    } else {
        $("#html").attr("data-bs-theme", "light");
    }
});
