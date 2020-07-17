$(document).ready(function () {
    var table = $('#dtUsers').DataTable({
        ajax: {
            url: '/administration/users',
            dataSrc: ''
        },
        columns: [
            {
                "data": "id",
                render: function (data, type, row, meta) {
                    return   meta.row + meta.settings._iDisplayStart + 1 ;
                }
            },
            {
               
               data: "id",
                render: function (data) {
                    //return "<a asp-controller='Administration' asp-action='EditRole'  asp-route-id='"+ data +"' class='btn btn-primary'> Edit</a>";
                    return "<a class='btn btn-primary' href='/Administration/Edituser/" + data + "'><i class='material-icons' style='font-size: 24px;'>edit</i></a>";
                }
            },
            { "data": "id" },            
            { "data": "email" },
            {
                data: "id",
                render: function (data) {
                    return "<a href='#' class='btn btn-danger' onclick='ConfirmDelete(\`" + data + "\`)' > <i class='material-icons' style='font-size: 24px;'>delete</i></a >";
                }
            },  
            {
                data: "id",
                render: function (data) {
                    return "<a class='btn btn-danger' href='/Account/ResetPassword/" + data + "'><i class='material-icons' style='font-size: 24px;'>security</i></a>";
                }
            }
        ],
        "paging": true,
        "searching": true,
        "columnDefs": [{            
            "targets": 0,
            "className": "columnCenterbold"
        },
        {        
            "targets": 1,
            "className": "text-center",
            "searchable": false,
            "orderable": false            
        },
        {
            "targets": 2,
            "visible": false       
        },            
        {
            "targets": 4,
            "className": "text-center",
           "searchable": false,
          "orderable": false
        },
        {
            "targets": 5,
            "className": "text-center",
            "searchable": false,
            "orderable": false
        }
        ],
        "order": [[1, 'asc']]
    });

    //table.on('order.dt search.dt', function () {
    //    table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
    //        cell.innerhtml = i + 1;
    //        console.log(cell + "-" + i +"-" + cell.innerhtml);
    //    });
    //}).draw();

});           

function ResetPasswrod(recordId) {
    alert(recordId);
}

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