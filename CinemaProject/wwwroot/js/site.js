$(document).on("click", ".menu-shedule-item", function () {

    var data = $(this).attr("data-sheduleclicker");

    $(".menu-shedule-item").removeClass("active");
    $(this).addClass("active")

    $(".cinema-itemouter").each(function (e) {
        $(this).hide();

        var dataMenu = $(this).attr("data-sheduleopener");

        if (dataMenu == data) {
            $(this).show();
        }
    });
})