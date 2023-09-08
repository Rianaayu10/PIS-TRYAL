var regUser = userlogin;
$(document).ready(function () {
    var formData = new FormData();
    formData.append("UserID", regUser);

    $.ajax({
        type: "Post",
        url: "/ChangePassword/GetList/",
        dataType: "json",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            /*=======Result OK========*/
            if (result.ID == "0") {
                $("#userid").val(result.Contents.UserID);
                $("#username").val(result.Contents.UserName);
                $("#password").val(result.Contents.Password);
            }

            /*=======Result Token Expired OK========*/
            else if (result.ID == "400") {
                window.open("/UserLogin/UserLogin", "_self");
            }

            /*=======Result BadRequest========*/
            else {
                toastr.warning(result.Message, 'Warning', { timeOut: 3000, "closeButton": true });
            }
        },
        error: function (ex) {
            toastr.error('Failed to retrieve get change password list' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
        }
    });
})

$("#passwordshow").click(function () {
    if ($("#password").attr("type") == "password") {
        $("#password").attr("type", "text");
        $("#passwordshow-icon").addClass("bi-unlock-fill");
        $("#passwordshow-icon").removeClass("bi-lock-fill");
    } else {
        $("#password").attr("type", "password");
        $("#passwordshow-icon").addClass("bi-lock-fill");
        $("#passwordshow-icon").removeClass("bi-unlock-fill");
    }
});

$("#newpasswordshow").click(function () {
    if ($("#newpassword").attr("type") == "password") {
        $("#newpassword").attr("type", "text");
        $("#newpasswordshow-icon").addClass("bi-unlock-fill");
        $("#newpasswordshow-icon").removeClass("bi-lock-fill");
    } else {
        $("#newpassword").attr("type", "password");
        $("#newpasswordshow-icon").addClass("bi-lock-fill");
        $("#newpasswordshow-icon").removeClass("bi-unlock-fill");
    }
});

$("#confpasswordshow").click(function () {
    if ($("#confpassword").attr("type") == "password") {
        $("#confpassword").attr("type", "text");
        $("#confpasswordshow-icon").addClass("bi-unlock-fill");
        $("#confpasswordshow-icon").removeClass("bi-lock-fill");
    } else {
        $("#confpassword").attr("type", "password");
        $("#confpasswordshow-icon").addClass("bi-lock-fill");
        $("#confpasswordshow-icon").removeClass("bi-unlock-fill");
    }
});

$("#Clear").click(function () {
    ClearValidasi();
    $("#newpassword").val("");
    $("#confpassword").val("");
})

$("#Save").click(function () {
    ClearValidasi();

    if ($("#newpassword").val() == null || $("#newpassword").val() == "") {
        $("#newpassword").addClass("is-invalid");
    }
    else if ($("#confpassword").val() == null || $("#confpassword").val() == "") {
        $("#confpassword").addClass("is-invalid");
        $("#msg_validasi_confpassword").html("Please fill a Confirmation Password!")
    }
    else if ($("#newpassword").val() != $("#confpassword").val()) {
        $("#confpassword").addClass("is-invalid");
        $("#msg_validasi_confpassword").html("Confirm password must be the same as New Password!")
    }
    else {

        var formData = new FormData();
        formData.append("UserID", userlogin);
        formData.append("Password", $("#newpassword").val());

        $.ajax({
            type: "Post",
            url: "/ChangePassword/Update/",
            dataType: "json",
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                /*=======Result OK========*/
                if (result.ID == "0") {
                    toastr.success(result.Message, 'Success', { timeOut: 3000, "closeButton": true });
                }

                /*=======Result Token Expired OK========*/
                else if (result.ID == "400") {
                    window.open("/UserLogin/UserLogin", "_self");
                }

                /*=======Result BadRequest========*/
                else {
                    toastr.warning(result.Message, 'Warning', { timeOut: 3000, "closeButton": true });
                }
            },
            error: function (ex) {
                toastr.error('Failed to retrieve update change password ' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
            }
        });
    }

})

function ClearValidasi() {
    $("#newpassword").removeClass("is-invalid");
    $("#confpassword").removeClass("is-invalid");;
}