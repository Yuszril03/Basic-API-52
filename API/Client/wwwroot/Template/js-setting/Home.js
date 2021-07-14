GenderSet();
UniversitySet();
GetValueEmploye();
GetValueUni();
GetValueRole();
function GetValueEmploye() {
    $.ajax({
        url: "/employee/GetRegistrasiView/"
    }).done((result) => {
        var jml = 0;
        $.each(result, function (key, val) {
            jml++;
        })
        $("#jmlEmploy").html(jml);
    })
}
function GetValueUni() {
    $.ajax({
        url: "/University/GetUniveristyView"
    }).done((result) => {
        var jml = 0;
        $.each(result, function (key, val) {
            jml++;
        })
        $("#jmlUni").html(jml);
    })
}
function GetValueRole() {
    $.ajax({
        url: "/Role/GetRoleView"
    }).done((result) => {
        var jml = 0;
        $.each(result, function (key, val) {
            jml++;
        })
        $("#jmlRole").html(jml);
    })
}
function UniversitySet() {
    $.ajax({
        url: "/University/Getall/"
    }).done((result) => {
        var namaUni = [];
        var isiUni = [];
        var listt = [];
        var isi = 0;
        $.each(result, function (key, val) {
           
            $.ajax({
                url: "/Education/Getall/"
            }).done((result2) => {
                $.each(result2, function (keyi, vali) {
                    if (val.id == vali.universityId) {
                        isi++;
                    }
                })
               
                namaUni.push(val.universityName);
                isiUni.push(isi);
                isi = 0;
            })
            
        })
       
             
        var optionsUniv = {
            series: [{
                name: 'Inflation',
                data: isiUni
            }],
            chart: {
                height: 350,
                type: 'bar',
            },
            plotOptions: {
                bar: {
                    borderRadius: 10,
                    dataLabels: {
                        position: 'top', // top, center, bottom
                    },
                }
            },
            dataLabels: {
                enabled: true,
                formatter: function (val) {
                    return val + "%";
                },
                offsetY: -20,
                style: {
                    fontSize: '12px',
                    colors: ["#304758"]
                }
            },

            xaxis: {
                categories: namaUni,
                position: 'top',
                axisBorder: {
                    show: false
                },
                axisTicks: {
                    show: false
                },
                crosshairs: {
                    fill: {
                        type: 'gradient',
                        gradient: {
                            colorFrom: '#D8E3F0',
                            colorTo: '#BED1E6',
                            stops: [0, 100],
                            opacityFrom: 0.4,
                            opacityTo: 0.5,
                        }
                    }
                },
                tooltip: {
                    enabled: true,
                }
            },
            yaxis: {
                axisBorder: {
                    show: false
                },
                axisTicks: {
                    show: false,
                },
                labels: {
                    show: false,
                    formatter: function (val) {
                        return val + "%";
                    }
                }

            },
            title: {
                text: 'Statistics University',
                floating: true,
                offsetY: 330,
                align: 'center',
                style: {
                    color: '#444'
                }
            }
        };

        var chartUniv = new ApexCharts(document.querySelector("#university"), optionsUniv);
        chartUniv.render();

    })
}
function GenderSet() {
    $.ajax({
        url: "/employee/GetRegistrasiView/"
    }).done((result) => {
        genderLaki = 0;
        genderPerempuan = 0;
        $.each(result, function (key, val) {
            if (val.gender == 0) {
                genderLaki++;
            } else {
                genderPerempuan++;
            }
        })
        var options = {
            series: [genderLaki, genderPerempuan],
            chart: {
                width: 380,
                type: 'donut',
            },
            labels: ['Male', 'Female'],
            colors: [' #0733d5 ', '#E91E63'],
            responsive: [{
                breakpoint: 480,
                options: {
                    chart: {
                        width: 200
                    },
                    legend: {
                        position: 'bottom'
                    }
                }
            }]
        };

        var chart = new ApexCharts(document.querySelector("#gender"), options);
        chart.render();
    })
}
