/**
 *  Document   : table_data.js
 *  Author     : redstar
 *  Description: advance table page script
 *
 **/

$(document).ready(function() {
	'use strict';
    $('#example1').DataTable( {
        "scrollX": true
    } );
    
    var table = $('#example2').DataTable( {
        "scrollY": "200px",
        "paging": false
    } );
 
    $('a.toggle-vis').on( 'click', function (e) {
        e.preventDefault();
 
        // Get the column API object
        var column = table.column( $(this).attr('data-column') );
 
        // Toggle the visibility
        column.visible( ! column.visible() );
    } );
    
    var t = $('#example3').DataTable( {
        "scrollX": true
    } );
    var counter = 1;
 
    $('#addRow').on( 'click', function () {
        t.row.add( [
            counter +'.1',
            counter +'.2',
            counter +'.3',
            counter +'.4',
            counter +'.5'
        ] ).draw( false );
 
        counter++;
    } );
 
    // Automatically add a first row of data
    $('#addRow').click();
    
    var table = $('.example4').DataTable( {
        "scrollX": true,
        "dom": 'B<"top"f>rt<"bottom"lp><"clear">',
        language: {
            search: "Zoeken"
        },
        "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            }
        ],
        responsive: true,
        buttons: [
           {
                text: '<i class="fa fa-print"></i><span>Print</span>',
                extend: 'print',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                className: 'buttonAsLink  assets-export-btn export-csv ttip printbtn',
            }, {
                text: '<i class="fa fa-file-excel-o"></i><span>Excel</span>',
                extend: 'excelHtml5',
                className: 'buttonAsLink assets-export-btn export-xls ttip excelbtn',
                title: "Lijst bobijnen - " + (new Date).getDay() + "/" + (new Date).getMonth() + "/" + (new Date).getFullYear(),
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                extension: '.xls'

            }, {
                text: '<i class="fa fa-file-pdf-o"></i><span>Pdf</span>',
                extend: 'pdf',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                className: 'buttonAsLink assets-export-btn export-pdf ttip pdfbtn',
                extension: '.pdfHtml5',
                title: "Lijst bobijnen - " + (new Date).getDay() + "/" + (new Date).getMonth() + "/" + (new Date).getFullYear(),
                customize: function (doc) {
                    doc.styles.title = {
                        color: 'black',
                        fontSize: '14',
                        alignment: 'center'
                    }
                }  
            },

        ]
    });
    
    $('.pdfbtn').appendTo('.pdf-container');
    $('.printbtn').appendTo('.print-container');
    $('.excelbtn').appendTo('.excel-container');

    $('#exampleBobbinDebit').DataTable({
        "scrollX": true,
        "dom": '<"top">rt<"bottom"p><"clear">',
        "bSortClasses": false,
        "columnDefs": [            
            {
                "width": "10%",
                "targets": 1
            },
            {
                "width": "10%",
                "targets": 2
            },
            {
                "width": "10%",
                "targets": 3
            }
        ]
    });
    $('#example4').DataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
    $('#saveStage').DataTable( {
    	 "scrollX": true,
        stateSave: true
    } );
    
    var table = $('#tableGroup').DataTable({
        "columnDefs": [
            { "visible": false, "targets": 2 }
        ],
        "order": [[ 2, 'asc' ]],
        "scrollX": true,
        "displayLength": 25,    
        "drawCallback": function ( settings ) {
            var api = this.api();
            var rows = api.rows( {page:'current'} ).nodes();
            var last=null;
 
            api.column(2, {page:'current'} ).data().each( function ( group, i ) {
                if ( last !== group ) {
                    $(rows).eq( i ).before(
                        '<tr class="group"><td colspan="5">'+group+'</td></tr>'
                    );
 
                    last = group;
                }
            } );
        }
    } );
 
    // Order by the grouping
    $('#tableGroup tbody').on( 'click', 'tr.group', function () {
        var currentOrder = table.order()[0];
        if ( currentOrder[0] === 2 && currentOrder[1] === 'asc' ) {
            table.order( [ 2, 'desc' ] ).draw();
        }
        else {
            table.order( [ 2, 'asc' ] ).draw();
        }
    });    
   
} );