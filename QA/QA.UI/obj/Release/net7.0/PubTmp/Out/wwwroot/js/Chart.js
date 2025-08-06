
function destroyChart(chartId) {
    var chart = Chart.getChart(chartId);

    if (chart) {
        chart.destroy();
    }
}

function destroyChartById(chartId) {
    var canvas = document.getElementById(chartId);
    console.log("Canvas element:", canvas);

    if (canvas) {
        var chart = Chart.getChart(canvas); // przekazujemy element, nie tylko ID
        console.log("Found chart:", chart);

        if (chart) {
            chart.destroy();
            console.log("Chart destroyed:", chartId);
        } else {
            console.warn("No chart found on canvas:", chartId);
        }
    } else {
        console.warn("No canvas element found with ID:", chartId);
    }
}


function renderHistogram(canvasId, name, labels, data, lsl, usl) {
    var ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: name,
                data: data,
                type: "bar",
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1,
                borderDash: [0],
            },
            {
                label: "LSL",
                data: lsl,
                type: "line",
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(255, 0, 0, 1)',
                borderWidth: 2,
                borderDash: [5],
            },
            {
                label: "USL",
                data: usl,
                type: "line",
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(255, 0, 0, 1)',
                borderWidth: 2,
                borderDash: [5],
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                },
                x: {
                    beginAtZero: true,
                    ticks: {
                        autoSkip: false, // Ensure all labels are shown
                        maxRotation: 0,  // Prevent rotation of labels
                        minRotation: 0,  // Prevent rotation of labels
                    },
                    grid: {
                        display: false, // Optional: to hide grid lines
                    }
                }
            },
        },
    });
}
