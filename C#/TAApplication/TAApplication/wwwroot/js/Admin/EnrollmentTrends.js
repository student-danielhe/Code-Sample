$(function () {
    $("#datepicker").datepicker();
    $('#datepicker').datepicker().datepicker("setDate", new Date("November 1, 2022 00:00:00"));
});

$(function () {
    $("#datepicker2").datepicker();
    $('#datepicker2').datepicker().datepicker("setDate", new Date("November 30, 2022 00:00:00"));
});

//Currently not using
function get_courses() {

    $.ajax({
        type: "GET",
        url: "Admin/GetCourseList",
        success: function (data) {
            return data;
        }
    });
}

function add_line() {

    var jsDate = $('#datepicker').datepicker('getDate');
    if (jsDate !== null) { // if any date selected in datepicker
        jsDate instanceof Date; // -> true
        jsDate.getDate();
        jsDate.getMonth();
        jsDate.getFullYear();
    }

    var jsDate2 = $('#datepicker2').datepicker('getDate');
    if (jsDate2 !== null) { // if any date selected in datepicker
        jsDate2 instanceof Date; // -> true
        jsDate2.getDate();
        jsDate2.getMonth();
        jsDate2.getFullYear();
    }

    var dropdown = document.getElementById("dropdown");
    var courseName = dropdown.options[dropdown.selectedIndex].text;
    var list;

    $.ajax({
        type: "POST",
        url: "GetEnrollmentData",
        data: {
            startDate: jsDate.toISOString(),
            endDate: jsDate2.toISOString(),
            courseName: courseName
        },
        success: function (data) {
            list = data;
            console.log(list);
            let series = [];
            let i = 0;
           
     
            list.forEach(function (element) {
                console.log(i);
                series[i] = [Date.UTC(jsDate.getFullYear(), jsDate.getMonth(), jsDate.getDate()+i), element];
                i = i + 1;

            });
            console.log(series);
            $("#EnrollmentChart").highcharts().addSeries(
                {
                    name: courseName,

                    data: series
                });
            $("#EnrollmentChart2").highcharts().addSeries(
                {
                    name: courseName,

                    data: series
                });
        }
    });
}


Highcharts.chart('EnrollmentChart', {

    title: {
        text: 'Number of Enrollments in CS Courses at the University of Utah (Basic Line Graph)',
        align: 'left'
    },

    subtitle: {
        text: 'Select start/end dates and a course in order to view the graph trends.',
        align: 'left'
    },

    yAxis: {
        title: {
            text: 'Number of Enrollments'
        }
    },

xAxis: {
        title:{
        text:"Dates"
        },
        type:"datetime",
    dateTimeLabelFormats: { // don't display the year
        month: '%e. %b',
        year: '%b'
    },
    },

    legend: {
        layout: 'vertical',
        align: 'right',
        verticalAlign: 'middle'
    },

    plotOptions: {
        series: {
            label: {
                connectorAllowed: false
            },
            pointStart: 2010
        }
    },

    responsive: {
        rules: [{
            condition: {
                maxWidth: 500
            },
            chartOptions: {
                legend: {
                    layout: 'horizontal',
                    align: 'center',
                    verticalAlign: 'bottom'
                }
            }
        }]
    }

});

Highcharts.chart('EnrollmentChart2', {

    title: {
        text: 'Number of Enrollments in CS Courses at the University of Utah (Area Line Graph)',
        align: 'left'
    },

    chart: {
        type: 'area'
    },

    subtitle: {
        text: 'Select start/end dates and a course in order to view the graph trends.',
        align: 'left'
    },

    yAxis: {
        title: {
            text: 'Number of Enrollments'
        }
    },

    xAxis: {
        title: {
            text: "Dates"
        },
        type: "datetime",
        dateTimeLabelFormats: { // don't display the year
            month: '%e. %b',
            year: '%b'
        },
    },

    legend: {
        layout: 'vertical',
        align: 'right',
        verticalAlign: 'middle'
    },

    plotOptions: {
        series: {
            label: {
                connectorAllowed: false
            },
            pointStart: 2010
        }
    },

    responsive: {
        rules: [{
            condition: {
                maxWidth: 500
            },
            chartOptions: {
                legend: {
                    layout: 'horizontal',
                    align: 'center',
                    verticalAlign: 'bottom'
                }
            }
        }]
    }

});
