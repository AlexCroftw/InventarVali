var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/employee/home/getall'
        },
        "columns": [
            { data: 'type', "width": "15%" },
            { data: 'licensePlate', "width": "15%" },
            { data: 'model', "width": "15%" },
            { data: 'fullName', "width": "15%" } 
        ]
    })

}