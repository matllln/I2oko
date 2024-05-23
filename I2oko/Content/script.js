$(document).ready(function () {
    $(".flip, .flipName").click(function () {
        if ($(this).hasClass("flip")) {
            $(".panel").slideToggle("slow");
        } else if ($(this).hasClass("flipName")) {
            $(".Namepanel").slideToggle("slow");
        }
    });
});
