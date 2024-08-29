$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/autovehicule/getall'
        },
        "columns": [
            { data: 'type', "width": "15%" },
            { data: 'licensePlate', "width": "15%" },
            { data: 'vinNumber', "width": "15%" },
            { data: 'insurenceDate', "width": "15%" },
            { data: 'hasITP', "width": "15%" },
            { data: 'itpExpirationDate', "width": "15%" },
            { data: 'insuranceExpirationDate', "width": "15%" },
            { data: 'hasVinieta', "width": "15%" },
            { data: 'vinietaExpirationDate', "width": "15%" },
            { data: 'imageUrl', "width": "15%" },
            { data: 'employees.fullName', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                      <a href = "/admin/autovehicule/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i>Edit> </a>
                      <a href = "/admin/autovehicule/delete?id=${data}" class="btn btn-danger mx-2">  <i class="bi bi-trash3"></i>Delete </a>
                           </div>`
                },
                "width": "25%"
            }
        ]
    })

}

