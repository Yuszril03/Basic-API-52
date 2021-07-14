$(document).ready(function () {
    var no = 1;
    var table = $('#table').DataTable({
        "language": {
            search: '',
            searchPlaceholder: "Search..."
        },
        'ajax': {
            url: "/role/GetAll/",
            dataType: "json",
            dataSrc: ""
        },
        "columns": [

            {
                data: null,
                render: function (data, type, row, meta) {
                    return no++;
                },
                searchable: false,
                orderable: false
            },
            {
                data: "roleName"
            },
            {
                data: null,
                "render": function (data, type, row, meta) {
                    return '<button class="btn btn-outline-info btn-sm m-2 open"  data-id="' + row['roleId'] + '" data-toggle="modal" data-target="#detail"><i class="fa fa-info-circle"></i></button>' +
                        '<button class="btn btn-outline-warning btn-sm m-2 edit"  data-id="' + row['roleId'] + '" data-toggle="modal" data-target="#edit"><i class="fa fa-pencil-alt"></i></button>' +
                        '<button class="btn btn-outline-danger btn-sm m-2 hapus"  data-id="' + row['roleId'] + '" data-toggle="modal" data-target="#hapus"><i class="fa fa-trash-alt"></i></button>';
                },
                searchable: false,
                orderable: false
            }
        ]

    });
})