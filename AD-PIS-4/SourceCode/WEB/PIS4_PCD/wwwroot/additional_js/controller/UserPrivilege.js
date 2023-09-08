var regUser = userlogin;
console.log(regUser);
$(document).ready(function () {
    /*====== TABLE USER PRIVILEGE ============ */
    $("#tablePrivilege").dataTable({
        bDestroy: true,
        data: [],
        columns: [
            {
                "data": "Group", className: "txtAlignLeft", "mRender": function (data, type, row) {
                    return '<span>' + row.GroupName + '</span>';  //'<span class="' + row.GroupIcon + '">'+ row.GroupName +'</span>'
                }
            },
            { "data": "MenuID", "autoWidth": true, className: "txtAlignLeft" },
            { "data": "MenuDesc", "autoWidth": true, className: "txtAlignLeft" },
            { "data": "AllowAccess", "autoWidth": true, className: "txtAlignCenter" },
            { "data": "AllowUpdate", "autoWidth": true, className: "txtAlignCenter" },
            { "data": "AllowDelete", "autoWidth": true, className: "txtAlignCenter" }
        ],
        columnDefs: [{
            'targets': [3],
            'searchable': false,
            'orderable': false,
            'className': 'dt-body-center',
            "mRender": function (data, type, full, meta) {
                console.log(data);
                return '<input id="AllowAccess" type="checkbox" ' + (data == '1' ? ' checked' : '') + '/>';

            }
        },
        {
            'targets': [4],
            'searchable': false,
            'orderable': false,
            'className': 'dt-body-center',
            "mRender": function (data, type, full, meta) {
                return '<input id="AllowUpdate" type="checkbox" ' + (data == '1' ? ' checked' : '') + '/>';

            }
        },
        {
            'targets': [5],
            'searchable': false,
            'orderable': false,
            'className': 'dt-body-center',
            "mRender": function (data, type, full, meta) {
                return '<input id="AllowDelete" type="checkbox" ' + (data == '1' ? ' checked' : '') + '/>';

            }
        }],
        "autoFill": false,
        "bSearchable": false,
        "bSortable": false,
    });
    /*====== END TABLE USER PRIVILEGE ============ */
})

$("#SavePrivilege").click(function () {
    var prm = [];
    prm.length = 0;

    //var prm = new Object();
    //prm.UserGroupID = $("#txtgroupid").val();

    $.each($("#tablePrivilege tbody tr"), function () {
        var allowaccess
        if ($(this).find('input:eq(0):checked').is(":checked")) {
            allowaccess = "1";
        } else {
            allowaccess = "0";
        }

        var allowupdate
        if ($(this).find('input:eq(1):checked').is(":checked")) {
            allowupdate = "1";
        } else {
            allowupdate = "0";
        }

        var allowdelete
        if ($(this).find('input:eq(2):checked').is(":checked")) {
            allowdelete = "1";
        } else {
            allowdelete = "0";
        }

        prm.push({
            UserGroupID: $("#txtgroupid").val(),
            MenuID: $(this).find('td:eq(1)').html(),
            AllowAccess: allowaccess,
            AllowUpdate: allowupdate,
            AllowDelete: allowdelete,
            RegisterUser: regUser
        });
    });

    console.log(prm);

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '/UserGroup/Update_Privilege/',
        data: JSON.stringify(prm),
        success: function (result) {
            /*=======Result OK========*/
            if (result.ID == "0") {
                toastr.success(result.Message, 'Message', { timeOut: 3000, "closeButton": true });
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
            toastr.error('Failed to retrieve Update User Privilege' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
        }
    });
})
function Privilege(prm) {
    $("#txtgroupid").val(prm);

    var formData = new FormData();
    formData.append("ActionType", "0");
    formData.append("UserGroupID", prm)

    $.ajax({
        type: "Post",
        url: "/UserGroup/GetList_Privilege/",
        dataType: "json",
        data: formData,
        processData: false,
        contentType: false,
        success: function (result) {
            /*=======Result OK========*/
            if (result.ID == "0") {
                $("#tablePrivilege").DataTable().clear();
                $("#tablePrivilege").DataTable().rows.add(result.Contents).draw(false);
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
            toastr.error('Failed to retrieve Get User Privilege' + ex, 'Warning', { timeOut: 3000, "closeButton": true });
        }
    });
}
function ClearModalPrivilege() {
    $("#tablePrivilege").DataTable().clear();
}

$('#AllowAccess-All').on('click', function () {
    $('input[id="AllowAccess"]').prop('checked', this.checked);
});

$('#AllowUpdate-All').on('click', function () {
    $('input[id="AllowUpdate"]').prop('checked', this.checked);
});

$('#AllowDelete-All').on('click', function () {
    $('input[id="AllowDelete"]').prop('checked', this.checked);
});

