$(document).ready(function () {
    $('#tblData').DataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/computer/getall'
        },
        "colums": [
            { data: 'type', "width": "15%" },
            { data: 'model', "width": "15%" },
            { data: 'description', "width": "15%" },
            { data: 'serialNumber', "width": "15%" },
            { data: 'imageUrl', "width": "15%" },
            { data: 'employees.fullName', "width": "15%" }
        ]
    })

}
