$(document).ready(function () {
    $(".flipUser, .flipName,.flipPhone,.flipEmail,.flipBio,.flipLoction,.flipBirthDay,.flipCateguri,.flipSexuality").click(function () {
        if ($(this).hasClass("flipUser")) {
            $(".panelUser").slideToggle("slow");
        } else if ($(this).hasClass("flipName")) {
            $(".Namepanel").slideToggle("slow");
        }
        else if ($(this).hasClass("flipPhone")) {
            $(".panelPhone").slideToggle("slow");
        }
        else if ($(this).hasClass("flipEmail")) {
            $(".panelEmail").slideToggle("slow");
        }
        else if ($(this).hasClass("flipBio")) {
            $(".panelBio").slideToggle("slow");
        }
        else if ($(this).hasClass("flipLoction")) {
            $(".panelLoction").slideToggle("slow");
        }
        else if ($(this).hasClass("flipBirthDay")) {
            $(".panelBirthDay").slideToggle("slow");
        }
        else if ($(this).hasClass("flipCateguri")) {
            $(".panelCateguri").slideToggle("slow");
        }
        else if ($(this).hasClass("flipSexuality")) {
            $(".panelSexuality").slideToggle("slow");
        }

    });

});