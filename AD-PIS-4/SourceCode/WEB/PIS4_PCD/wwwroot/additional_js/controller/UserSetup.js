var regUser = userlogin;
console.log(regUser);
$(document).ready(function () {

    LoadData();

    var responsiveHelper_dt_basic = undefined;
    var breakpointDefinition = {
        tablet: 1024,
        phone: 480
    };

    $("#tableID").dataTable({
        bDestroy: true,
        data: [],
        columns: [
            {
                "data": "Action", className: "txtAlignCenter", "render": function (data, type, row) {
                    return '<a href="#mymodal" style="color:var(--bs-app-theme);" data-bs-toggle="modal" class="fa fa-pencil" onclick="UpdateData(\'' + row.UserID + '\')"></a> <span style="color:var(--bs-app-theme);">|</span> <a href="#modal-alert" style="color:var(--bs-app-theme);" data-bs-toggle="modal" class="fa fa-trash" onclick="DeleteData(\'' + row.UserID + '\')"></a>'
                }
            },
            { "data": "UserID", "autoWidth": true, className: "txtAlignLeft" },
            { "data": "UserName", "autoWidth": true, className: "txtAlignLeft" },
            { "data": "ShopDesc", "autoWidth": true, className: "txtAlignCenter" },
            { "data": "ShiftDesc", "autoWidth": true, className: "txtAlignCenter" },
            { "data": "UserGroupDesc", "autoWidth": true, className: "txtAlignCenter" },
            {
                "data": "Locked", className: "txtAlignCenter", "render": function (data, type, row) {
                    if (row.Locked == "1") {
                        return '<input disabled type="checkbox" checked="checked" />';
                    } else {
                        return '<input disabled type="checkbox"/>';
                    }
                }
            },
            { "data": "Description", "autoWidth": true, className: "txtAlignLeft" },
            { "data": "UpdateUser", "autoWidth": true, className: "txtAlignCenter" },
            { "data": "UpdateDate", "autoWidth": true, className: "txtAlignCenter" }
        ],
        "autoFill": false,
        "bSearchable": false,
        "bSortable": false,
    });

})

/*======================================================
 For showing Password
========================================================*/
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

/*======================================================
 Insert Data User Setup
========================================================*/
$("#InsertData").click(function () {
    ClearValidasi() 

    var formData = new FormData();
    formData.append("ActionType", "0");
    formData.append("UserID", $("#userid").val());
    formData.append("UserName", $("#username").val());
    formData.append("Password", $("#password").val());
    formData.append("ShopType", $("#shoptype").val());
    formData.append("Shift", $("#shift").val());
    formData.append("UserGroupID", $("#groupid").val());
    formData.append("Description", $("#description").val());
    formData.append("RegisterUser", regUser);

    var locked = "";
    if ($("#locked").prop('checked') == true) {
        locked = "1";
    } else if ($("#locked").prop('checked') == false) {
        locked = "0";
    }
    formData.append("Locked", locked);

    if ($("#userid").val() == null || $("#userid").val() == "") {
        $("#userid").addClass("is-invalid");
        return
    }

    else if ($("#username").val() == null || $("#username").val() == "") {
        $("#username").addClass("is-invalid");
        return
    }

    else if ($("#password").val() == null || $("#password").val() == "") {
        $("#password").addClass("is-invalid");
        return
    }

    else if ($("#shoptype").val() == null || $("#shoptype").val() == "") {
        $("#shoptype").addClass("is-invalid");
        return
    }

    else if ($("#shift").val() == null || $("#shift").val() == "") {
        $("#shift").addClass("is-invalid");
        return
    }

    else if ($("#groupid").val() == null || $("#groupid").val() == "") {
        $("#groupid").addClass("is-invalid");
        return
    }

    else {
        $.ajax({
            type: "Post",
            url: "/UserSetup/Update/",
            data: formData,
            dataType: "json",
            processData: false,
            contentType: false,
            success: function (result) {
                /*=======Result OK========*/
                if (result.ID == "0") {
                    toastr.success(result.Message, 'Success', { timeOut: 3000, "closeButton": true });
                    $('#mymodal').modal('hide');
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                    LoadData();
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
                toastr.error('Failed to retrieve  Shift Group' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
            }
        })
    }
})

/*======================================================
 Update Data User Setup
========================================================*/
$("#UpdateData").click(function () {
    ClearValidasi() 

    var formData = new FormData();
    formData.append("ActionType", "1");
    formData.append("UserID", $("#userid").val());
    formData.append("UserName", $("#username").val());
    formData.append("Password", $("#password").val());
    formData.append("ShopType", $("#shoptype").val());
    formData.append("Shift", $("#shift").val());
    formData.append("UserGroupID", $("#groupid").val());
    formData.append("Description", $("#description").val());
    formData.append("AdminStatus", $("#adminstatus").val());
    formData.append("RegisterUser", regUser);

    var locked = "";
    if ($("#locked").prop('checked') == true) {
        locked = "1";
    } else if ($("#locked").prop('checked') == false) {
        locked = "0";
    }
    formData.append("Locked", locked);

    if ($("#userid").val() == null || $("#userid").val() == "") {
        $("#userid").addClass("is-invalid");
        return
    }

    else if ($("#username").val() == null || $("#username").val() == "") {
        $("#username").addClass("is-invalid");
        return
    }

    else if ($("#password").val() == null || $("#password").val() == "") {
        $("#password").addClass("is-invalid");
        return
    }

    else if ($("#shoptype").val() == null || $("#shoptype").val() == "") {
        $("#shoptype").addClass("is-invalid");
        return
    }

    else if ($("#shift").val() == null || $("#shift").val() == "") {
        $("#shift").addClass("is-invalid");
        return
    }

    else if ($("#groupid").val() == null || $("#groupid").val() == "") {
        $("#groupid").addClass("is-invalid");
        return
    }

    else {
        $.ajax({
            type: "Post",
            url: "/UserSetup/Update/",
            data: formData,
            dataType: "json",
            processData: false,
            contentType: false,
            success: function (result) {
                /*=======Result OK========*/
                if (result.ID == "0") {
                    toastr.success(result.Message, 'Success', { timeOut: 3000, "closeButton": true });
                    $('#mymodal').modal('hide');
                    $('body').removeClass('modal-open');
                    $('.modal-backdrop').remove();
                    LoadData();
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
                toastr.error('Failed to retrieve  Shift Group' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
            }
        })
    }
   
})

/*======================================================
 Delete Data User Setup
========================================================*/
$("#DeletedData").click(function () {
    var formData = new FormData();
    formData.append("ActionType", "2");
    formData.append("UserID", $("#userid").val());

    $.ajax({
        type: "Post",
        url: "/UserSetup/Update/",
        data: formData,
        dataType: "json",
        processData: false,
        contentType: false,
        success: function (result) {
            /*=======Result OK========*/
            if (result.ID == "0") {
                toastr.success(result.Message, 'Success', { timeOut: 3000, "closeButton": true });
                $('#modal-alert').modal('hide');
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                LoadData();
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
            toastr.error('Failed to retrieve  Shift Group' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
        }
    })
})


/*======================================================
  Load data (get sel)
========================================================*/
function LoadData() {
    var userid = "AdminTos";
    var formData = new FormData();
    formData.append("ActionType", "0");
    formData.append("UserID", userid);

    $.ajax({
        type: "Post",
        url: "/UserSetup/GetList/",
        data: formData,
        dataType: "json",
        processData: false,
        contentType: false,
        success: function (result) {
            /*=======Result OK========*/
            if (result.ID == "0") {
                $("#tableID").DataTable().clear();
                $("#tableID").DataTable().rows.add(result.Contents).draw(false);
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
            toastr.error('Failed to retrieve  Shift Group' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
            return
        }
    })
}


/*======================================================
  Popup Add new data
========================================================*/
function AddNewData() {
    ClearModal();

    $("#ModalTitle").html("Create New User");
    $('#InsertData').show();
    $('#UpdateData').hide();

    ShopType();
    Shift();
    UserGroup();
}


/*======================================================
  Popup Add Update Data
========================================================*/
function UpdateData(userid) {
    $("#ModalTitle").html("Update Data User");
    $('#InsertData').hide();
    $('#UpdateData').show();
    $("#userid").prop('disabled', true);

    var formData = new FormData(); 1
    formData.append("ActionType", "1");
    formData.append("UserID", userid)
    $.ajax({
        type: "Post",
        url: "/UserSetup/GetList/",
        dataType: "json",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            /*=======Result OK========*/
            if (result.ID == "0") {
                $("#userid").val(result.Contents[0].UserID);
                $("#username").val(result.Contents[0].UserName);
                $("#password").val(result.Contents[0].Password);
                $("#description").val(result.Contents[0].Description);

                if (result.Contents[0].Locked == "1") {
                    $("#locked").prop('checked', true);
                } else {
                    $("#locked").prop('checked', false);
                }

                ShopType(result.Contents[0].ShopType);
                Shift(result.Contents[0].Shift);
                UserGroup(result.Contents[0].UserGroupID);
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
            toastr.error('Failed to retrieve Shop Code' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
        }
    });
}


/*======================================================
  Popup Add Delete Data
========================================================*/
function DeleteData(prm) {
    $("#userid").val(prm)
}


/*======================================================
  Function for get shoptype 
========================================================*/
function ShopType(prm) {
    var formData = new FormData();
    formData.append("ActionType", "1");

    $.ajax({
        type: "Post",
        url: "/GlobalFilter/Filter/",
        dataType: "json",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            /*=======Result OK========*/
            if (result.ID == "0") {
                $('#shoptype').empty();
                $("#shoptype").append('<option value="" disabled selected> Fill Shop Type </option>');
                $.each(result.Contents, function (i, bind) {
                    if (bind.Code == prm) {
                        $("#shoptype").append('<option value="' + bind.Code + '" selected>' + bind.CodeDesc + '</option>');
                    } else {
                        $("#shoptype").append('<option value="' + bind.Code + '">' + bind.CodeDesc + '</option>');
                    }
                });
                $('#shoptype').focus();
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
            toastr.error('Failed to retrieve Shop Type' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
        }
    });
}


/*======================================================
 Function for get shift 
========================================================*/
function Shift(prm) {
    var formData = new FormData();
    formData.append("ActionType", "2");

    $.ajax({
        type: "Post",
        url: "/GlobalFilter/Filter/",
        dataType: "json",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            /*=======Result OK========*/
            if (result.ID == "0") {
                $('#shift').empty();
                $("#shift").append('<option value="" disabled selected> Fill Shift </option>');
                $.each(result.Contents, function (i, bind) {
                    if (bind.Code == prm) {
                        $("#shift").append('<option value="' + bind.Code + '" selected>' + bind.CodeDesc + '</option>');
                    }
                    else {
                        $("#shift").append('<option value="' + bind.Code + '">' + bind.CodeDesc + '</option>');
                    }
                });
                $('#shift').focus();
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
            toastr.error('Failed to retrieve Shift' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
        }
    });
}


/*======================================================
 Function for get user group
========================================================*/
function UserGroup(prm) {
    var formData = new FormData();
    formData.append("ActionType", "3");

    $.ajax({
        type: "Post",
        url: "/GlobalFilter/Filter/",
        dataType: "json",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {

            /*=======Result OK========*/
            if (result.ID == "0") {
                $('#groupid').empty();
                $("#groupid").append('<option value="" disabled selected> Fill Group ID </option>');
                $.each(result.Contents, function (i, bind) {
                    if (bind.Code == prm) {
                        $("#groupid").append('<option value="' + bind.Code + '" selected>' + bind.CodeDesc + '</option>');
                    }
                    else {
                        $("#groupid").append('<option value="' + bind.Code + '">' + bind.CodeDesc + '</option>');
                    }
                });
                $('#groupid').focus();
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
            toastr.error('Failed to retrieve Group ID' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
        }
    });
}


/*======================================================
 Function Clear Data Modal
========================================================*/
function ClearModal() {
    ClearValidasi();
    $("#userid").val("");
    $("#userid").prop('disabled', false);   
    $("#username").val("");
    $("#password").val("");
    $("#password").attr("type", "password");
    $("#description").val("");
    $("#locked").prop('checked', false);
    ShopType("");
    Shift("");
    UserGroup("");
}


/*======================================================
  Function Clear Validasi
========================================================*/
function ClearValidasi() {
    $("#userid").removeClass("is-invalid");
    $("#username").removeClass("is-invalid");
    $("#password").removeClass("is-invalid");
    $("#shoptype").removeClass("is-invalid");
    $("#shift").removeClass("is-invalid");
    $("#groupid").removeClass("is-invalid");
}
