function PieChart(data) {
    Highcharts.chart('pie-container', {
        chart: {
            backgroundColor: "#F2F4F9",
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        exporting: {
            buttons: {
                contextButton: {
                    menuItems: [
                        'printChart',
                        'separator',
                        'downloadPNG',
                        'downloadJPEG',
                        'downloadPDF',
                        'downloadSVG',
                        'separator',
                        'downloadCSV',
                        'viewData'
                    ]
                }
            }
        },
        title: {
            text: ""
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>',
                    fontSize: '15px',
                    fontFamily: 'tahoma',
                }
            }
        },
        series: [{
            name: 'percentage',
            colorByPoint: true,
            data: data
        }],
    });
    HideFromCharts()

}
function BarChart(data) {

    Highcharts.chart('bar-container', {
        chart: {
            backgroundColor: "#F2F4F9",
            type: 'column'
        },
        title: {
            text: ""
        },
        exporting: {
            buttons: {
                contextButton: {
                    menuItems: [
                        'printChart',
                        'separator',
                        'downloadPNG',
                        'downloadJPEG',
                        'downloadPDF',
                        'downloadSVG',
                        'separator',
                        'downloadCSV',
                        'viewData'
                    ]
                }
            }
        },
        xAxis: {
            categories: data.years,
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: ""
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: data.data
    });
    HideFromCharts()
}
function LineChart(data) {

    Highcharts.chart('line-container', {
        chart: {
            backgroundColor: "#F2F4F9",
            type: 'line'
        },
        title: {
            text: ""
        }, exporting: {
            buttons: {
                contextButton: {
                    menuItems: [
                        'printChart',
                        'separator',
                        'downloadPNG',
                        'downloadJPEG',
                        'downloadPDF',
                        'downloadSVG',
                        'separator',
                        'downloadCSV',
                        'viewData'
                    ]
                }
            }
        },
        xAxis: {
            categories: data.years
        },
        yAxis: {
            title: {
                text: ""
            }
        },
        plotOptions: {
            line: {
                dataLabels: {
                    enabled: true
                },
                enableMouseTracking: false
            }
        },
        series: data.data
    });
    HideFromCharts()
}

function HideFromCharts() {

        $(".highcharts-credits").hide()
    

}

function renameComboboxToArabic(comboboxId, length) {

    var selector = ".comboTreeInputBox#" + comboboxId
    $(selector).val("(المختار (" + length)


}
function moveBtnToRight() {

    setTimeout(function () {

        $(".comboTreeInputBox").each(function () {
            var offWidth = $(this)[0].offsetWidth - 37.5
            $(this).next().css("margin-right", +offWidth + "px")
        })
        $(".comboTreeInputBox").val("(المختار (0")
    }, 200)


}
function runTimeRTL() {

    runTimeRTLWithoutClearNames()
   
    $(".comboTreeDropDownContainer").children("input").attr("placeholder", "اكتب للاختيار")
    $(".comboTreeInputBox").val("(المختار (0")
}
function runTimeRTLWithoutClearNames() {
    $(".comboTreeDropDownContainer li").css("text-align", "right").css("padding", 0).css("padding-right", "15px")
    $(".comboTreeDropDownContainer li").children("span").children("input").css("margin-left", "5px")

    $(".comboTreeInputBox").css("text-align", "right")

    $(".comboTreeInputBox").each(function () {
        var offWidth = $(this)[0].offsetWidth - 37.5
        $(this).next().css("margin-right", +offWidth + "px")
    })

    $(".comboTreeParentPlus").css("float", "right")
    $(".comboTreeParentPlus").css("right", "-12px")
}