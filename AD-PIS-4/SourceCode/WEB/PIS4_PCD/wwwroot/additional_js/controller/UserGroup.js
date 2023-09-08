var regUser = userlogin;
console.log(regUser);
$(document).ready(function () {
    /*====== START LOAD FORM ============ */
    LoadData();
    /*====== END START LOAD FORM ======== */


    /*====== TABLE USER GROUP ============ */
    $("#tableID").dataTable({
        bDestroy: true,
        data: [],
        columns: [
            {
                "data": "Action", className: "txtAlignCenter", "render": function (data, type, row) {
                    return '<a href="#modal-privilege" data-bs-toggle="modal" class="fa fa-user" style="color:var(--bs-app-theme);" onclick="Privilege(\'' + row.UserGroupID + '\')" ></a> <span style="color:var(--bs-app-theme);">|</span> <a href="#mymodal" style="color:var(--bs-app-theme);" data-bs-toggle="modal" class="fa fa-pencil" onclick="UpdateData(\'' + row.UserGroupID + '\')"></a> <span style="color:var(--bs-app-theme);">|</span> <a href="#modal-alert" style="color:var(--bs-app-theme);" data-bs-toggle="modal" class="fa fa-trash" onclick="DeleteData(\'' + row.UserGroupID + '\')"></a>'
                }
            },
            { "data": "UserGroupID", "autoWidth": true, className: "txtAlignLeft" },    
            { "data": "UserGroupName", "autoWidth": true, className: "txtAlignLeft" },         
            { "data": "UpdateUser", "autoWidth": true, className: "txtAlignCenter" },
            { "data": "UpdateDate", "autoWidth": true, className: "txtAlignCenter" }
        ],
        "autoFill": false,
        "bSearchable": false,
        "bSortable": false,
    });
    /*====== END TABLE USER GROUP ============ */
})

$("#InsertData").click(function () {
    ClearValidasi()

    var formData = new FormData();
    formData.append("ActionType", "0");
    formData.append("UserGroupID", $("#groupid").val());
    formData.append("UserGroupName", $("#groupname").val());
    formData.append("RegisterUser", regUser);

    if ($("#groupid").val() == null || $("#groupid").val() == "") {
        $("#groupid").addClass("is-invalid");
        return
    }

    else if ($("#groupname").val() == null || $("#groupname").val() == "") {
        $("#groupname").addClass("is-invalid");
        return
    }

    else {
        $.ajax({
            type: "Post",
            url: "/UserGroup/Update/",
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
                toastr.error('Failed to retrieve insert user group' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
            }
        })
    }
})

$("#UpdateData").click(function () {
    ClearValidasi()

    var formData = new FormData();
    formData.append("ActionType", "1");
    formData.append("UserGroupID", $("#groupid").val());
    formData.append("UserGroupName", $("#groupname").val());
    formData.append("RegisterUser", regUser);

    if ($("#groupid").val() == null || $("#groupid").val() == "") {
        $("#groupid").addClass("is-invalid");
        return
    }

    else if ($("#groupname").val() == null || $("#groupname").val() == "") {
        $("#groupname").addClass("is-invalid");
        return
    }

    else {
        $.ajax({
            type: "Post",
            url: "/UserGroup/Update/",
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
                toastr.error('Failed to retrieve update user group' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
            }
        })
    }  
})

$("#DeletedData").click(function () {
    var formData = new FormData();
    formData.append("ActionType", "2");
    formData.append("UserGroupID", $("#groupid").val());

    $.ajax({
        type: "Post",
        url: "/UserGroup/Update/",
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
            toastr.error('Failed to retrieve delete user group' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
        }
    })
})
function LoadData() {
    var userid = "AdminTos";
    var formData = new FormData();
    formData.append("ActionType", "0");
    formData.append("UserID", userid);

    $.ajax({
        type: "Post",
        url: "/UserGroup/GetList/",
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
            toastr.error('Failed to retrieve get list user Group' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
        }
    })
}
function AddNewData() {
    ClearModal();

    $("#ModalTitle").html("Create New User Group");
    $('#InsertData').show();
    $('#UpdateData').hide();
}
function UpdateData(groupid) {
    console.log(groupid);
    $("#ModalTitle").html("Update Data User Group");
    $('#InsertData').hide();
    $('#UpdateData').show();
    $("#groupid").prop('disabled', true);

    var formData = new FormData(); 1
    formData.append("ActionType", "1");
    formData.append("UserGroupID", groupid)

    $.ajax({
        type: "Post",
        url: "/UserGroup/GetList/",
        dataType: "json",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            /*=======Result OK========*/
            if (result.ID == "0") {
                $("#groupid").val(result.Contents[0].UserGroupID);
                $("#groupname").val(result.Contents[0].UserGroupName);
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
function DeleteData(prm) {
    $("#groupid").val(prm)
}
function ClearModal() {
    ClearValidasi();
    $("#groupid").val("");
    $("#groupid").prop('disabled', false);
    $("#groupname").val("");
}
function ClearValidasi() {
    $("#groupid").removeClass("is-invalid");
    $("#groupname").removeClass("is-invalid");
}

