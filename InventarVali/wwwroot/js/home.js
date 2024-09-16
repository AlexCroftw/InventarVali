var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable(

        {
        "ajax": {
            url: '/employee/home/getall'
            },

        "columns": [
            { data: 'type', "width": "15%" },
            { data: 'licensePlate', "width": "15%" },
            { data: 'model', "width": "15%" },
            { data: 'fullName', "width": "15%" } 
            ],

            dom: 'Bfltip',
            "buttons": [
                {
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
                    text: 'Print Records',
                    className: 'btn btn-default',
                    exportOptions: {
                        columns: ':visible:not(:last-child)'
                    }
                }
            ],
            language: {
                search: "",
                searchPlaceholder: "Search records"
            }
            
        
    })

}
