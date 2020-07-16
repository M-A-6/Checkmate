$(document).ready(function () {
    var table = $('#dtRequest').DataTable({
        ajax: {
            url: '/request/get',
            dataSrc: ''
        },
        columns: [
            {
                title: 'Id',
                data: 'id'
            }, {
                title: 'booklet size',
                data: 'bookletsize'
            },
            {
                title: 'booklet name',
                data: 'bookletaccountname'
            },
            {
                title: 'booklet name',
                data: 'bookletaccountname'
            },
            {
                title: 'Request type',
                data: 'requesttype',                          
                render: function (data) {
                    var iconVal = "black";
                    if (data == 1) {
                        iconVal = "<i class='material-icons danger' style='font-size: 24px;color:green;'>thumb_up</i>";                        
                    }
                    else if (data == 2) {
                        iconVal = "<i class='material-icons danger' style='font-size: 24px;color:red;  '>thumb_down</i>";
                    }
                    else if (data == 3) {
                        iconVal = "<i class='material-icons danger' style='font-size: 24px;color:red;  '>cancel</i>";
                    }


                    return "<span>" + iconVal + "</span>";
                }
            },
            
        ],
        select: {
            style: 'multi'
        },
        "paging": true,
        "searching": true
    });

    $('#dtRequest tbody').on('click', 'tr', function () {
        $(this).toggleClass('selected');
    });

    function modelRequest() {
        var selectedarrays = [];
        var selectedrecords = "";
        $.each(table.rows('.selected').data(), function (key, value) {
            selectedarrays.push(value.requesttype);
            selectedrecords = selectedrecords + value.id + " ";
        });
        var unique = selectedarrays.filter(function (itm, i, a) {
            return i === a.indexOf(itm);
        });
        var result = "selected records have same types";
        $("#requestIds").val("");
        $("#updateRequest").attr("hidden", true);

        if (unique.length > 1) {
            result = "selected records have different types";
        }
        else if (selectedarrays.length === 0) {
            result = "No record selected";
        }
        else if (selectedarrays.length > 0) {
            $("#requestIds").val(selectedrecords);
            $("#updateRequest").removeAttr('hidden');
        }

        $("#bodytext").html("<b>" + result + " - " + unique + " selected" + selectedrecords + "</b>");
        $("#messageBox").modal();

    };

    $('#btnApprove').click(function () {
        $("#updateRequest").attr("hidden", true);
        $("#requestTypeId").val(1);
        modelRequest();

    });

    $('#btnReject').click(function () {
        $("#updateRequest").attr("hidden", true);
        $("#requestTypeId").val(2);
        modelRequest();

    });

    $('#btnCancel').click(function () {
        $("#updateRequest").attr("hidden", true);
        $("#requestTypeId").val(3);
        modelRequest();
    });




    $("#updateRequest").click(function () {

        $("#messageBox").modal("hide");
        var requestedId = $("#requestIds").val();
        var typeId = $("#requestTypeId").val();

        $.ajax({
            url: '/request/update/' + typeId + "/" + requestedId,
            method: 'POST',
            contentType: 'application/json',
            success: function (response) {

                $('#dtRequest').DataTable().ajax.reload();
            },
            // Display errors if any in the Bootstrap alert <div>
            error: function (jqXHR) {
                // window.location.href = "Employee/Login";
            }
        });
    });


});









