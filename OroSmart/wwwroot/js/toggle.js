$(document).ready(function () {

    // Check if sidebar state cookie exists and set initial state
    var sidebarState = getCookie("sidebarState");
    if (sidebarState === "collapsed") {
        $(".sidebar").addClass("sidebar-collapsed");
    }

    // Sidebar toggle button click event
    $(".js-sidebar-toggle").click(function () {
        $(".sidebar").toggleClass("sidebar-collapsed");
        // Save sidebar state in cookie
        var newState = $(".sidebar").hasClass("sidebar-collapsed") ? "collapsed" : "expanded";
        document.cookie = "sidebarState=" + newState + "; path=/";
    });

    // Function to get cookie value by name
    function getCookie(name) {
        var value = "; " + document.cookie;
        var parts = value.split("; " + name + "=");
        if (parts.length == 2) return parts.pop().split(";").shift();
    }
});