var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#Table_1').DataTable(

        {
            "ajax": {
                url: '/employee/home/getallautovehicule',
            },

            "columns": [
                { data: 'type', "width": "15%" },
                { data: 'licensePlate', "width": "15%" },
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
                        columns: ':visible:not(:last-child)'
                    }
                },
                {
                    extend: 'pdf',
                    messageTop:
                        'The information in this table is copyright to Komora Engineering.',
                    text: '<i class="bi bi-filetype-pdf"></i> PDF',
                    className: 'btn btn-default',
                    exportOptions: {
                        columns: ':visible:not(:last-child)'
                    }
                },
                {
                    extend: 'csv',
                    messageTop:
                        'The information in this table is copyright to Komora Engineering.',
                    text: '<i class="bi bi-filetype-csv"></i> CSV',
                    className: 'btn btn-default',
                    exportOptions: {
                        columns: ':visible:not(:last-child)'
                    }
                },
                {
                    extend: 'print',
                    text: '<i class="bi bi-printer"></i> PRINT',
                    className: 'btn btn-default',
                    exportOptions: {
                        columns: ':visible:not(:last-child)'
                    }
                }
            ],
            language: {
                search: "",
                searchPlaceholder: "Search records"
            },
        }
    
    )

}


