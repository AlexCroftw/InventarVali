$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/employee/getall'
        },
        "columns": [
            { data: 'firstName', "width": "15%" },
            { data: 'lastName', "width": "15%" },
            { data: 'email', "width": "15%" },
            { data: 'fullName', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                      <a href = "/admin/employee/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i>Edit> </a>
                      <a href = "/admin/employee/delete?id=${data}" class="btn btn-danger mx-2">  <i class="bi bi-trash3"></i>Delete </a>
                           </div>`
                },
                "width": "25%"
            }
        ]
    })

}

