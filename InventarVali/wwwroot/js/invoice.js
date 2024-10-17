var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/invoice/getall'
        },
        "destroy": true,
        "deferRender": true,
        "columnDefs": [{
            "targets": 1, //<-- index of column that should be rendered as link
            render: function (data, type, row, meta) {
                if (type === 'display') {
                    return $('<a>')
                        .attr('href', `/admin/invoice/displayinvoice?id=${row.id}`)
                        .text(data)
                        .wrap('<div></div>')
                        .parent()
                        .html();
                } else {
                    return data;
                }
            }
        }],

        "columns": [
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                      <a href = "/admin/invoice/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit </a>
                      <a onClick = Delete('/admin/invoice/delete/${data}') class="btn btn-danger mx-2">  <i class="bi bi-trash3"></i> Delete </a>
                           </div>`
                },
                "width": "10%",
                "className": 'noExport'
            },
            { data: 'invoiceNumber', "width": "10%" },
            { data: 'displayInvoiceDate', "width": "10%" },
            { data: 'totalPrice', "width": "10%" },
            {
                data: 'autovehicule',
                "render": function (d)
                {
                    if (d !== null) {
                        var table = "<table>";
                        $.each(d, function (k, v) {
                            table += " " + v.licensePlate
                        });
                        return table + "</table>";

                    }
                    else
                    {
                        return "";
                    }
                }
            }
        ],

        dom: 'Bfltip',
        "buttons": [
            {
                extend: 'excel',
                messageTop:
                    'The information in this table is copyright to Komora Engineering.',
                text: '<i class="bi bi-file-earmark-excel"></i> EXCEL',
                className: 'btn btn-default',
                exportOptions: {
                    columns: ':visible:not(:first-child)'
                }
            },
            {
                extend: 'pdf',
                messageTop:
                    'The information in this table is copyright to Komora Engineering.',
                text: '<i class="bi bi-filetype-pdf"></i> PDF',
                className: 'btn btn-default',
                exportOptions: {
                    columns: ':visible:not(:first-child)'
                }
            },
            {
                extend: 'csv',
                messageTop:
                    'The information in this table is copyright to Komora Engineering.',
                text: '<i class="bi bi-filetype-csv"></i> CSV',
                className: 'btn btn-default',
                exportOptions: {
                    columns: ':visible:not(:first-child)'
                }
            },
            {
                extend: 'print',
                text: '<i class="bi bi-printer"></i> PRINT',
                className: 'btn btn-default',
                exportOptions: {
                    columns: ':visible:not(:first-child)'
                }
            }
        ],
        processing: true,
        "exportOptions": {
            columns: ":visible:not(.noExport)"
        },
        "language": {
            search: "",
            searchPlaceholder: "Search records"
        }

    })


}

function Delete(url) {
    Swal.fire({
        title: "Are you sure you want to delete this?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}

//$(document).ready(function () {
//    var ITPCheck = $('input#chkbxITP[type=checkbox]');
//    var shITPDate = $('#ITPDate');
//    var ITPLabel = $('#ITPLabel')
//    // check for default status 
//    if (ITPCheck.attr('checked') !== undefined) {
//        shITPDate.show();
//        ITPLabel.show();
//    }
//    else {
//        shITPDate.hide();
//        ITPLabel.hide();
//    }

//    ITPCheck.change(function () {
//        shITPDate.toggle();
//        ITPLabel.toggle();
//    });
//});

//$(document).ready(function () {
//    var VinietaCheck = $('input#chkbxVinieta[type=checkbox]');
//    var shVinietaDate = $('#VinietaDate');
//    var VinietaLabel = $('#VinietaLabel')
//    // check for default status 
//    if (VinietaCheck.attr('checked') !== undefined) {
//        shVinietaDate.show();
//        VinietaLabel.show();
//    }
//    else {
//        shVinietaDate.hide();
//        VinietaLabel.hide();
//    }
//    VinietaCheck.change(function () {
//        shVinietaDate.toggle();
//        VinietaLabel.toggle();
//    });
//}); 