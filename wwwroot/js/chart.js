let ordersChart;
let revenueChart;

function initCharts(data) {

    const ctx1 = document.getElementById("ordersChart");
    const ctx2 = document.getElementById("revenueChart");

    ordersChart = new Chart(ctx1, {
        type: "line",
        data: {
            labels: data.labels,
            datasets: [{
                label: "Orders Count",
                data: data.orderCounts,
                borderWidth: 2
            }]
        }
    });

    revenueChart = new Chart(ctx2, {
        type: "bar",
        data: {
            labels: data.labels,
            datasets: [{
                label: "Revenue",
                data: data.revenues,
                borderWidth: 2
            }]
        }
    });
}

async function loadChart(period) {

    const res = await fetch(`/Admin/Dashboard/SalesChart?period=${period}`);
    const data = await res.json();

    // destroy old charts
    if (ordersChart) ordersChart.destroy();
    if (revenueChart) revenueChart.destroy();

    initCharts(data);
}

document.getElementById("periodSelect").addEventListener("change", function () {
    loadChart(this.value);
});

window.addEventListener("load", async function () {
    await loadChart("Daily");
});

