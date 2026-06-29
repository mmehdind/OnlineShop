document.getElementById("changeStatusBtn")?.addEventListener("click", async function () {

    const status = document.getElementById("statusSelect").value;

    const orderId = window.location.pathname.split("/").pop();

    const res = await fetch(`/Admin/Order/ChangeStatus`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            orderId: orderId,
            status: status
        })
    });

    if (res.ok) {
        location.reload();
    }

});