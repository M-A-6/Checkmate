$(document).ready(function () {
    var table = $('#dtRoles').DataTable({
        ajax: {
            url: '/administration/roles',
            dataSrc: ''
        },
        columns: [
            {
                "data": "id",
                render: function (data, type, row, meta) {
                    return  meta.row + meta.settings._iDisplayStart + 1 ;
                }
            },
            {
                data: "id",
                render: function (data) {
                    return "<a class='btn btn-primary' href='/Administration/EditRole/" + data + "'><i class='material-icons' style='font-size: 24px;'>edit</i></a>";
                }
            },
            { "data": "id" },
            { "data": "name" },
            {
                data: "id",
                render: function (data) {
                    return "<a href='#' class='btn btn-danger' onclick='ConfirmDelete(\`" + data + "\`)' > <i class='material-icons' style='font-size: 24px;'>delete</i></a >";
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
        }
        ],
        "order": [[1, 'asc']]

    });

});           


function ConfirmDelete(recordId) {
    $("#deleteId").val(recordId);
    $("#DeleteConfirm").modal();
}


$('#yesDelete').click(function () {  
    $("#DeleteConfirm").modal("hide");
    deleteRole($("#deleteId").val());
});
function deleteRole(id) {    
    $.ajax({
        url: '/administration/deleterole/' + id,
        method: 'DELETE',
        contentType: 'application/json',
        success: function (response) {     
            
            $('#dtRoles').DataTable().ajax.reload();
        },
        // Display errors if any in the Bootstrap alert <div>
        error: function (jqXHR) {
            // window.location.href = "Employee/Login";
        }
    });
}