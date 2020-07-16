$(document).ready(function () {
    var table = $('#dtUsers').DataTable({
        ajax: {
            url: '/administration/users',
            dataSrc: ''
        },
        columns: [
            {
               data: "id",
                render: function (data) {
                    //return "<a asp-controller='Administration' asp-action='EditRole'  asp-route-id='"+ data +"' class='btn btn-primary'> Edit</a>";
                    return "<a class='btn btn-primary' href='/Administration/Edituser/" + data + "'><i class='material-icons' style='font-size: 24px;'>edit</i></a>";
                }
            },
            { "data": "id" },
            { "data": "username" },
            { "data": "email" },
            //{
            //    data: "id",
            //    render: function (data) {
            //        return "<a href='#' class='btn btn-danger' onclick='confirmDelete('" + data + "', true)' > Delete</a >";
            //    }
            //},
            {
                data: "id",
                render: function (data) {
                    return "<a href='#' class='btn btn-danger' onclick='ConfirmDelete(\`" + data + "\`)' > <i class='material-icons' style='font-size: 24px;'>delete</i></a >";
                }
            }  

        ],
        "paging": true,
        "searching": true
       
    });

});           

function ConfirmDelete(recordId) {
    $("#deleteId").val(recordId);
    $("#DeleteConfirm").modal();
}

$('#yesDelete').click(function () {
    $("#DeleteConfirm").modal("hide");
    deleteUser($("#deleteId").val());
});
function deleteUser(id) {
    $.ajax({
        url: '/administration/deleteuser/' + id,
        method: 'DELETE',
        contentType: 'application/json',
        success: function (response) {

            $('#dtUsers').DataTable().ajax.reload();
        },
        // Display errors if any in the Bootstrap alert <div>
        error: function (jqXHR) {
            // window.location.href = "Employee/Login";
        }
    });
}