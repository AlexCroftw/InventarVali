$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/computer/getall'
        },
        "columns": [
            { data: 'type', "width": "15%" },
            { data: 'model', "width": "15%" },
            { data: 'description', "width": "15%" },
            { data: 'serialNumber', "width": "15%" },
            { data: 'imageUrl', "width": "15%" },
            { data: 'employees.fullName', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                      <a href = "/admin/computer/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i>Edit> </a>
                      <a href = "/admin/computer/delete?id=${data}" class="btn btn-danger mx-2">  <i class="bi bi-trash3"></i>Delete </a>
                           </div>`
                },
                "width": "25%"
            }
        ]
    })

}

