
$(document).ready(function () {
    var no = 1;
    var table = $('#table').DataTable({
        "language": {
            search: '',
            searchPlaceholder: "Search..."
        },
        "lengthChange": true,
        "dom": 'Bfrtip',
        "buttons": [
            {
                extend: 'copy',
                className: 'copy',
                text: '<i class="far fa-copy"> <b></b></i>',
                titleAttr: 'Copy'
            },
            {
                extend: 'csv',
                className: 'csv',
                text: '<i class="fas fa-file-csv"> <b></b></i>',
                titleAttr: 'CSV'
            },
            {
                extend: 'excel',
                className: 'excel',
                text: '<i class="far fa-file-excel" > <b></b> </i>',
                titleAttr: 'Excel'
            },
            {
                extend: 'pdf',
                className: 'pdf',
                text: '<i class="far fa-file-pdf"> <b></b></i>',
                titleAttr: 'PDF'
            },
            {
                extend: 'print',
                className: 'print',
                text: '<i class="fa fa-print"> <b></b></i>',
                titleAttr: 'Print'
            }
            //'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        'ajax': {
            url: "/employee/GetRegistrasiView/",
            dataType: "json",
            dataSrc: ""
        },
        "columns": [

            {
                data: "nik"
            },
            {
                data: null,
                render: function (data, type, row, meta) {
                    return row['firstName'] + " " + row['lastName']
                }
            },
            {
                data: null,
                render: function (data, type, row, meta) {
                    return (row['gender'] == 0) ? "Male" : "Famale";
                }
            }, {
                data: null,
                render: function (data, type, row, meta) {
                    return formatTelfon(row['phoneNumber'], "");
                }
            }, {
                data: null,
                render: function (data, type, row, meta) {
                    return formatRupiah('' + row['salary'], '');
                }
            }, {
                data: "roleName"
            }, {
                data: "universityName"
            },
            {
                data: null,
                "render": function (data, type, row, meta) {
                    return '<button class="btn btn-outline-info btn-sm m-2 open"  data-id="' + row['nik'] + '" data-toggle="modal" data-target="#detail"><i class="fa fa-info-circle"></i></button>' +
                        '<button class="btn btn-outline-warning btn-sm m-2 edit"  data-id="' + row['nik'] + '" data-toggle="modal" data-target="#edit"><i class="fa fa-pencil-alt"></i></button>' +
                        '<button class="btn btn-outline-danger btn-sm m-2 hapus"  data-id="' + row['nik'] + '" data-toggle="modal" data-target="#hapus"><i class="fa fa-trash-alt"></i></button>';
                },
                searchable: false,
                orderable: false
            }
        ]

    });
    
    $(document).on("click", ".hapus", function () {
        var nik = $(this).data('id');
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.isConfirmed) {
                
                $.ajax({
                    url: "/AccountRole/GetAll",
                }).done((result) => {
                    $.each(result, function (key, val) {
                        if (val.accountId == nik) {
                            $.ajax({
                                url: "/AccountRole/Delete/" + val.accountRoleId,
                                type: "DELETE"
                            }).done((result2) => {

                            })
                        }
                    })
                    $.ajax({
                        url: "/Employee/Delete/" + nik,
                        type: "DELETE"
                    }).done((result3) => {
                        Swal.fire({
                            position: 'Center',
                            icon: 'success',
                            title: 'Deleted Successfully',
                            showConfirmButton: false,
                            timer: 1500
                        })
                        table.ajax.reload(null, false)

                    }).fail((error3) => {

                    })
                }).fail((error) => {
                    console.log(error)
                })
                
            }
        })
    })
    $(document).on("click", ".open", function () {
        var nik = $(this).data('id');
        $.ajax({
            url: "/Employee/GetRegistrasiView/" + nik
        }).done((result) => {
            //console.log(result);
            text = "";
            no = 1;
            $.each(result, function (key, val) {
                $("#judulD").html("Detail " + val.firstName + " " + val.lastName);
                $("#nikD").html(val.nik);
                $("#nameD").html(val.firstName + " " + val.lastName);
                $("#emailD").html(val.email);
                $("#salaryD").html(formatRupiah('' + val.salary, ''));
                $("#phoneD").html(formatTelfon(val.phoneNumber, ""));
                $("#dateD").html(val.birthDate);
                $("#genderD").html((val.gender == 0) ? "Laki-Laki" : "Perempuan");
                $("#roleD").html(val.roleName);
                $("#degreeD").html(val.degree);
                $("#gpaD").html(val.gpa);
                $("#uniD").html(val.universityName);
            })
        }).fail((error) => {
            console.log(error);
        });
    })
    $("#submit").click(function () {
        count = 0;
        var form = $("#formRegis").serializeArray();
        for (var t in form) {
            if (form[t].name == "email") {
                if (document.getElementById("stEmail").innerHTML == "") {
                    $('#' + form[t].name).addClass("is-invalid");
                    count++;
                }
            }
            else if (form[t].name == "nik") {
                if (document.getElementById("stNik").innerHTML == "") {
                    $('#' + form[t].name).addClass("is-invalid");
                    count++;
                }
            }
            else if (form[t].name == "pass") {
                if (($("#pass").val()).length < 8) {
                    $('#' + form[t].name).addClass("is-invalid");
                    count++;
                }
            }
            else if (form[t].value == "") {
                $('#' + form[t].name).addClass("is-invalid");
                count++;
            }


        }
        if (count == 0) {
            var obj2 = new Object();
            obj2.Nik = $("#nik").val();
            obj2.FirstName = $("#fname").val()
            obj2.LastName = $("#lname").val()
            obj2.Email = $("#email").val()
            obj2.Salary = parseInt($("#salary").val())
            obj2.PhoneNumber = $("#phone").val()
            obj2.Gender = $("#gender").val();
            obj2.BirthDate = $("#date").val()
            obj2.Password = $("#pass").val()
            obj2.degree = $("#degree").val()
            obj2.gpa = $("#gpa").val()
            obj2.UniversityId = parseInt($("#uni").val())
            const myJSON = JSON.stringify(obj2);
            //console.log(obj2)
            $.ajax({
                url: "https://localhost:44321/API/Employees/Register",
                type: "POST",
                contentType: "application/json",
                data: myJSON
            }).done((result) => {
                console.log(result)
                Swal.fire({
                    position: 'Center',
                    icon: 'success',
                    title: 'Saved Successfully',
                    showConfirmButton: false,
                    timer: 1500
                })
                table.ajax.reload(null, false)
                $('#regis').modal('hide')
            }).fail((error) => {
                console.log(error)
            })
        }


    })

    $("#register").click(function () {
        table.draw();
        var form2 = $("#formRegis").serializeArray();
        document.getElementById("stEmail").innerHTML == ""
        for (var t in form2) {
            document.getElementById(form2[t].name).value = "";
            $('#' + form2[t].name).removeClass("is-invalid");
        }
        $('#degree').attr("disabled", true);
        $('#gpa').attr("disabled", true);
    })
    $("#nik").blur(function () {
        nikk = $(this).val()
        if ($(this).val() == "") {
            $("#validNIK").html("Please fill in a valid NIK.")
            $(this).addClass("is-invalid")
        } else {
            $.ajax({
                url: "https://localhost:44321/API/Employees/GetProfil/" + nikk
            }).done((result) => {
                document.getElementById("stNik").innerHTML = "";
                $("#validNIK").html("The NIK That Is Entered Already Exists")
                $("#nik").addClass("is-invalid")
            }).fail((error) => {
                document.getElementById("stNik").innerHTML = "1";
                $("#nik").removeClass("is-invalid")
            });
        }
    })
    $("#fname").keyup(function () {
        ($(this).val() == "") ? $(this).addClass("is-invalid") : $(this).removeClass("is-invalid");
    })
    $("#lname").keyup(function () {
        ($(this).val() == "") ? $(this).addClass("is-invalid") : $(this).removeClass("is-invalid");
    })
    $("#email").blur(function () {
        emaill = $(this).val()
        if ($(this).val() == "") {
            $("#validEmail").html(" Please fill in a valid Email.")
            $(this).addClass("is-invalid")
        } else if (ValidateEmail($(this).val()) == false) {
            $("#validEmail").html(" Please fill in a valid Email.")
            $(this).addClass("is-invalid");
            document.getElementById("stEmail").innerHTML = "";
        } else {
            $.ajax({
                url: "https://localhost:44321/API/Employees/GetProfil/" + emaill
            }).done((result) => {
                document.getElementById("stEmail").innerHTML = "";
                $("#validEmail").html("The Email That Is Entered Already Exists")
                $("#email").addClass("is-invalid")
            }).fail((error) => {
                document.getElementById("stEmail").innerHTML = "1";
                $("#email").removeClass("is-invalid")
            });

        }
        //($(this).val() == "") ? $(this).addClass("is-invalid") : $(this).removeClass("is-invalid");
    })
    $("#salary").keyup(function () {
        document.getElementById("salary").value = formatAngka(document.getElementById("salary").value, "");
        ($(this).val() == "") ? $(this).addClass("is-invalid") : $(this).removeClass("is-invalid");
    })
    $("#phone").keyup(function () {
        document.getElementById("phone").value = formatAngka(document.getElementById("phone").value, "");
        ($(this).val() == "") ? $(this).addClass("is-invalid") : $(this).removeClass("is-invalid");
    })
    $("#date").change(function () {
        ($(this).val() == "") ? $(this).addClass("is-invalid") : $(this).removeClass("is-invalid");
    })
    $("#gender").change(function () {
        ($(this).val() == "") ? $(this).addClass("is-invalid") : $(this).removeClass("is-invalid");
    })

    $("#degree").change(function () {
        if ($(this).val() == "") {
            $(this).addClass("is-invalid")
            $('#gpa').attr("disabled", true);
            document.getElementById('gpa').value = "";
        } else {
            $('#gpa').attr("disabled", false);
            $(this).removeClass("is-invalid");
        }
    })
    $("#gpa").keyup(function () {
        ($(this).val() == "") ? $(this).addClass("is-invalid") : $(this).removeClass("is-invalid");
    })
    $("#pass").keyup(function () {
        ($(this).val() == "" || ($(this).val()).length < 8) ? $(this).addClass("is-invalid") : $(this).removeClass("is-invalid");
    })
    $("#uni").change(function () {
        var z = $(this).val();
        if ($(this).val() == "") {
            $(this).addClass("is-invalid")
            $('#degree').attr("disabled", true)
            $('#degree').html('<option value="" selected>Choose...</option>');
            $('#gpa').attr("disabled", true);
            document.getElementById('gpa').value = "";
        } else {
            $(this).removeClass("is-invalid");
            $('#degree').attr("disabled", false)
            var temp1 = '<option value="" selected>Choose...</option>';
            $.ajax({
                url: "https://localhost:44321/API/Education"
            }).done((result) => {
                text = "";
                no = 1;
                $.each(result, function (key, valu) {
                    if (z == valu.universityId && text != valu.degree) {
                        text = valu.degree;
                        temp1 += `<option value="${valu.degree}">${valu.degree}</option>`
                    }

                })
                $("#degree").html(temp1);
            }).fail((error) => {
                console.log(error);
            });
        }
    })
    //$(document).on("click", ".open", function () {
    //    var myBookId = $(this).data('id');
    //    $.ajax({
    //        url: myBookId
    //    }).done((result) => {
    //        //console.log(result);

    //        $("#imgFoto").html('<img class=" w-50" src="' + result.sprites.other.dream_world.front_default+'" alt="" />');
    //        $("#exampleModalLabel").html("Detail " + result.name);
    //        $("#namePoke").html(result.name);
    //        $("#heightPoke").html(result.height+" cm");
    //        $("#weightPoke").html(result.weight + " Kg");
    //        hslABS = "";
    //        $.each(result.abilities, function (key, val) {
    //            hslABS += `<span class="badge badge-primary">${val.ability.name}</span> `;
    //        })
    //        $("#abilitiesPoke").html(hslABS);
    //        hslstance = "";
    //        $.each(result.moves, function (key, val) {
    //            hslstance += `<span class="badge badge-secondary">${val.move.name}</span> `;
    //        })
    //        $("#stancePoke").html(hslstance);
    //        hsltype = "";
    //        $.each(result.types, function (key, val) {
    //            hsltype += `<span class="badge badge-success">${val.type.name}</span> `;
    //        })
    //        $("#typePoke").html(hsltype);
    //        hslstatic = "";
    //        $.each(result.stats, function (key, val) {
    //            hslstatic += `<p class="font-weight-normal text-capitalize m-1" style="margin-bottom:-1px;">${val.stat.name}</p>
    //                <div class="progress">
    //                    <div class="progress-bar bg-info" role="progressbar" style="width: ${val.base_stat}%" aria-valuenow="${val.base_stat}" aria-valuemin="0" aria-valuemax="100">${val.base_stat}%</div>
    //                </div> `;
    //        })
    //        $("#static").html(hslstatic);


    //    }).fail((error) => {
    //        console.log(error);
    //    });

    //});

    function ValidateEmail(mail) {
        if (/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(mail)) {
            return (true)
        }
        return (false)
    }

    function formatRupiah(angka, prefix) {
        var number_string = angka.replace(/[^,\d]/g, '').toString(),
            split = number_string.split(','),
            sisa = split[0].length % 3,
            rupiah = split[0].substr(0, sisa),
            ribuan = split[0].substr(sisa).match(/\d{3}/gi);

        // tambahkan titik jika yang di input sudah menjadi angka ribuan
        if (ribuan) {
            separator = sisa ? '.' : '';
            rupiah += separator + ribuan.join('.');
        }

        rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
        return prefix == undefined ? rupiah : (rupiah ? 'Rp. ' + rupiah : '');
    }
    function formatAngka(angka, prefix) {
        var number_string = angka.replace(/[^,\d]/g, '').toString(),
            split = number_string.split(','),
            sisa = split[0].length % 3,
            rupiah = split[0].substr(0, sisa),
            ribuan = split[0].substr(sisa).match(/\d{3}/gi);

        // tambahkan titik jika yang di input sudah menjadi angka ribuan
        if (ribuan) {
            separator = sisa ? '' : '';
            rupiah += separator + ribuan.join('');
        }

        rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
        return prefix == undefined ? rupiah : (rupiah ? '' + rupiah : '');
    }
    function formatTelfon(angka, prefix) {
        var parse = parseInt(angka);
        var stringTO = '' + parse;
        var number_string = stringTO.replace(/^[\+]?[(]?[1-9]{3}[)]?[-\s\.]?[1-9]{3}[-\s\.]?[1-9]{4,6}$/im, '').toString(),
            split = number_string.split(','),
            sisa = split[0].length % 3,
            rupiah = split[0].substr(0, sisa),
            ribuan = split[0].substr(sisa).match(/\d{3}/gi);

        // tambahkan titik jika yang di input sudah menjadi angka ribuan
        if (ribuan) {
            separator = sisa ? '' : '';
            rupiah += separator + ribuan.join('');
        }

        rupiah = split[1] != undefined ? rupiah + ',' + split[1] : rupiah;
        return prefix == undefined ? rupiah : (rupiah ? '+62' + rupiah : '');
    }

});
setSelectUni();
//function registrasi() {
//    var obj2 = new Object();
//    obj2.UniversityName = "UNDIP";
//    console.log(obj2)
//    const myJSON = JSON.stringify(obj2);
//    $.ajax({
//        url: "https://localhost:44321/API/University",
//        type: "POST",
//        contentType: "application/json",
//        data: myJSON
//    }).done((result) => {
//        console.log(result)
//    }).fail((error) => {
//        console.log("sss")
//    })
//}

function setSelectUni() {
    var temp1 = '<option value="" selected>Choose...</option>';
    $.ajax({
        url: "https://localhost:44321/API/University"
    }).done((result) => {
        console.log(result);
        text = "";
        no = 1;
        $.each(result, function (key, val) {
            temp1 += `<option value="${val.id}">${val.universityName}</option>`
        })
        $("#uni").html(temp1);
    }).fail((error) => {
        console.log(error);
    });
}