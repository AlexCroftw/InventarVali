var dataTable;
var dataTable2;

$(document).ready(function () {
    dataTable = $('#Table_1').DataTable(

        {
            "ajax": {
                url: '/employee/home/getallautovehicule',
            },


            "destroy": true,
            "deferRender": true,
            "columnDefs": [{
                "targets": 0, //<-- index of column that should be rendered as link
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return $('<a>')
                            .attr('href', `/employee/home/detailsautovehicule?id=${row.id}`)
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
                    className: 'btn btn-default'
                   
                },
                {
                    extend: 'pdf',
                    messageTop:
                        'The information in this table is copyright to Komora Engineering.',
                    text: '<i class="bi bi-filetype-pdf"></i> PDF',
                    className: 'btn btn-default'
                    
                },
                {
                    extend: 'csv',
                    messageTop:
                        'The information in this table is copyright to Komora Engineering.',
                    text: '<i class="bi bi-filetype-csv"></i> CSV',
                    className: 'btn btn-default'
                   
                },
                {
                    extend: 'print',
                    text: '<i class="bi bi-printer"></i> PRINT',
                    className: 'btn btn-default'
                  
                }
            ],
            language: {
                search: "",
                searchPlaceholder: "Search records"
            },
        }

    )

    dataTable2 = $('#Table_2').DataTable(
        {
            "ajax": {
                url: '/employee/home/getallcomputers',
            },

            "columns": [
                { data: 'type', "width": "15%" },
                { data: 'description', "width": "15%" },
            ],

            dom: 'Bfltip',
            "buttons": [
                {
                    extend: 'excel',
                    messageTop:
                        'The information in this table is copyright to Komora Engineering.',
                    text: '<i class="bi bi-file-earmark-excel"></i> EXCEL',
                    className: 'btn btn-default'
                   
                },
                {
                    extend: 'pdf',
                    messageTop:
                        'The information in this table is copyright to Komora Engineering.',
                    text: '<i class="bi bi-filetype-pdf"></i> PDF',
                    className: 'btn btn-default'
                    
                },
                {
                    extend: 'csv',
                    messageTop:
                        'The information in this table is copyright to Komora Engineering.',
                    text: '<i class="bi bi-filetype-csv"></i> CSV',
                    className: 'btn btn-default'
                    
                },
                {
                    extend: 'print',
                    text: '<i class="bi bi-printer"></i> PRINT',
                    className: 'btn btn-default'
                    
                }
            ],
            language: {
                search: "",
                searchPlaceholder: "Search records"
            },

        })
});




