
function PieChart(data, unit) {
    google.charts.load("current", { packages: ["corechart"] });
    data = data.map(x => [x.name, x.y])
    data.unshift(['Name', 'Value'])
    window.pieData = data
    window.pieUnit = unit
    google.charts.setOnLoadCallback(drawPieChart);

}

function drawPieChart() {
    console.log(pieData)
    var data = google.visualization.arrayToDataTable(pieData);

    var formatter = new google.visualization.NumberFormat({
        suffix: pieUnit
    });
    var options = {
        title: '',
        pieHole: 0.4,
        height: 500,
        backgroundColor: 'transparent',
        suffix: formatter.format(data, 1),
        fontName: 'Tajawal',
        sliceVisibilityThreshold: 0
    };

    var chart = new google.visualization.PieChart(document.getElementById('pie-container'));
    chart.draw(data, options);

}

function BarChart(data, unit) {
    var mainArr = GetDataReadyForChart(data);
    window.barData = mainArr
    window.barUnit = unit
    google.charts.load('current', { packages: ['corechart', 'bar'] });
    google.charts.setOnLoadCallback(drawBarChart);


}
function drawBarChart() {
    var data = google.visualization.arrayToDataTable(barData);

    var options = {
        chart: {
            title: '',
            subtitle: '',
        },

        height: 850,
        backgroundColor: 'transparent',
        chartArea: { backgroundColor: 'transparent' },
        vAxis: {
            title: barUnit,
        },
        hAxis: {
            slantedText: true,
            slantedTextAngle: 45,
            textStyle: {
                fontSize: 14
            }
        },
        fontName: 'Tajawal',
  
    };


    var chart = new google.visualization.ColumnChart(document.getElementById('bar-container'));
    chart.draw(data, google.charts.Bar.convertOptions(options));
}
function LineChart(data, unit) {
    var mainArr = GetDataReadyForChart(data);
    window.lineData = mainArr
    window.lineUnit = unit
    google.charts.load('current', { packages: ['corechart', 'line'] });  
    google.charts.setOnLoadCallback(drawLineChart);
}
function GetDataReadyForChart(data) {
    var mainArr = [];
    var titlesArr = [''];
    for (var i = 0; i < data.years.length; i++) {
        var arr = [data.years[i]];
        for (var j = 0; j < data.data.length; j++) {
            if (i == 0) {
                titlesArr.push(data.data[j].name);
            }
            arr.push(data.data[j].data[i]);
        }
        mainArr.push(arr);
    }
    mainArr.unshift(titlesArr);
    return mainArr;
}

function drawLineChart() {
    var data = google.visualization.arrayToDataTable(lineData);

    var options = {
        chart: {
            title: '',
            subtitle: ''
        },
        height: 850,
        backgroundColor: 'transparent',
        chartArea: { backgroundColor: 'transparent' },
        axes: {
            x: {
                0: { side: 'top' }
            }
        },
        vAxis: {
            title: lineUnit,
        },
        hAxis: {
            slantedText: true,
            slantedTextAngle: 45,
        },
        fontName: 'Tajawal',
        pointSize: 10,
        interpolateNulls: true
    };


    var chart = new google.visualization.LineChart(document.getElementById('line-container'));

    chart.draw(data, google.charts.Line.convertOptions(options));
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