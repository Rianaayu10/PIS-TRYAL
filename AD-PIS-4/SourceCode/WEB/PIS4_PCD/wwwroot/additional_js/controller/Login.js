$("#btnlogin").click(function () {
    ClearValidasi();

    /*Valiadasi*/
    if ($("#txtuserid").val() == null || $("#txtuserid").val() == "") {
        $("#txtuserid").addClass("is-invalid");
        return

    } else if ($("#txtpassword").val() == null || $("#txtpassword").val() == "") {
        $("#txtpassword").addClass("is-invalid");
        return

    } else {
        var formData = new FormData();
        formData.append("ActionType", "0");
        formData.append("UserID", $("#txtuserid").val());
        formData.append("Password", $("#txtpassword").val());

        $.ajax({
            type: "Post",
            url: "/UserLogin/Login/",
            data: formData,
            dataType: "json",
            processData: false,
            contentType: false,
            success: function (result) {

                /*=======Result OK========*/
                if (result.ID == "0") {
                    window.open("/Home/Index", "_self");
                }

                /*=======Result BadRequest========*/
                else {
                    toastr.warning(result.Message, 'Warning', { timeOut: 3000, "closeButton": true });
                }
            },
            error: function (ex) {
                toastr.error('Failed to retrieve User Login' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
            }
        })
    }
})

/*======================================================
For showing Password
========================================================*/
$("#passwordshow").click(function () {
    if ($("#txtpassword").attr("type") == "password") {
        $("#txtpassword").attr("type", "text");
        $("#passwordshow-icon").addClass("bi-unlock-fill");
        $("#passwordshow-icon").removeClass("bi-lock-fill");
    } else {
        $("#txtpassword").attr("type", "password");
        $("#passwordshow-icon").addClass("bi-lock-fill");
        $("#passwordshow-icon").removeClass("bi-unlock-fill");
    }
});

/*======================================================
  Function Clear Validasi
========================================================*/
function ClearValidasi() {
    $("#txtuserid").removeClass("is-invalid");
    $("#txtpassword").removeClass("is-invalid");

}
