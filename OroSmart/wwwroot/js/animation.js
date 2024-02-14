var btnUpload = $("#upload_file"),
    btnOuter = $(".button_outer");
btnUpload.on("change", function (e) {
    var ext = btnUpload.val().split('.').pop().toLowerCase();
    if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
        $(".error_msg").text("Not an Image...");
    } else {
        $(".error_msg").text("");
        btnOuter.addClass("file_uploading");
        setTimeout(function () {
            btnOuter.addClass("file_uploaded");
        }, 3000);
        var uploadedFile = URL.createObjectURL(e.target.files[0]);
        setTimeout(function () {
            $("#uploaded_view").append('<img src="' + uploadedFile + '" />').addClass("show");
        }, 3500);
    }
});
$(".file_remove").on("click", function (e) {
    $("#uploaded_view").removeClass("show");
    $("#uploaded_view").find("img").remove();
    btnOuter.removeClass("file_uploading");
    btnOuter.removeClass("file_uploaded");
});

$(document).ready(function () {

    $('.processing_bar').on('transitionend webkitTransitionEnd oTransitionEnd otransitionend', function () {
        // Use a timeout to delay showing the alert
        setTimeout(function () {
            $('.error_msg').html('<div class="alert alert-info custom_alert" role="alert">If everything is OK, press the Green Button!</div>');
        }, 500);
    });

    $('.success_button').on('click', function () {
        $('.custom_alert').fadeOut();
    });

    $('.file_remove').on('click', function () {
        $('.custom_alert').fadeOut();
    });
});