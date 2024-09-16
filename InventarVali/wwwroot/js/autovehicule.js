﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/autovehicule/getall'
        },
        "columns": [
            { data: 'type', "width": "10%" },
            { data: 'licensePlate', "width": "10%" },
            { data: 'vinNumber', "width": "10%" },
            { data: 'insurenceDate', "width": "10%" },
            { data: 'hasITP', "width": "10%" },
            { data: 'itpExpirationDate', "width": "10%" },
            { data: 'insuranceExpirationDate', "width": "10%" },
            { data: 'hasVinieta', "width": "10%" },
            { data: 'vinietaExpirationDate', "width": "10%" },
            { data: 'employees.fullName', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                      <a href = "/admin/autovehicule/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit </a>
                      <a onClick = Delete('/admin/autovehicule/delete/${data}') class="btn btn-danger mx-2">  <i class="bi bi-trash3"></i> Delete </a>
                           </div>`
                },
                "width": "10%",
                "className": 'noExport'
            }
        ],
        
        dom: 'Bfltip',
        "buttons": [
            {

                caption: "Inventar KMRE",
                extend: 'excel',
                messageTop:
                    'The information in this table is copyright to Komora Engineering.',
                text: 'Export in Excel',
                className: 'btn btn-default',
                exportOptions: {
                    columns: ':visible:not(:last-child)'
                }
            },
            {
                extend: 'pdf',
                messageTop:
                    'The information in this table is copyright to Komora Engineering.',
                text: 'Export in PDF',
                className: 'btn btn-default',
                exportOptions: {
                    columns: ':visible:not(:last-child)'
                }
            },
            {
                extend: 'csv',
                messageTop:
                    'The information in this table is copyright to Komora Engineering.',
                text: 'Export in CSV',
                className: 'btn btn-default',
                exportOptions: {
                    columns: ':visible:not(:last-child)'
                }
            },
            {
                extend: 'print',
                messageTop:
                    'The information in this table is copyright to Komora Engineering.',
                text: 'Print Records',
                className: 'btn btn-default',
                exportOptions: {
                    columns: ':visible:not(:last-child)'
                }
            }
        ],
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