function showToast(message, type = "success") {

    const toast = document.createElement("div");

    toast.className = `toast align-items-center text-bg-${type} border-0 show`;
    toast.style.position = "fixed";
    toast.style.bottom = "20px";
    toast.style.right = "20px";
    toast.style.zIndex = 9999;
    toast.style.minWidth = "200px";

    toast.innerHTML = `
        <div class="d-flex">
            <div class="toast-body">
                ${message}
            </div>
        </div>
    `;

    document.body.appendChild(toast);

    setTimeout(() => {
        toast.remove();
    }, 3000);
}

document.getElementById("uploadForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const formData = new FormData(this);

    const res = await fetch("/Admin/Product/UploadImage", {
        method: "POST",
        body: formData
    });

    if (res.ok) {
        showToast("Image uploaded");
        location.reload();
    } else {
        showToast("Upload failed", "danger");
    }
});

async function deleteImage(imageId, productId) {

    if (!confirm("Delete image?"))
        return;

    const res = await fetch(`/Admin/Product/DeleteImage?imageId=${imageId}&productId=${productId}`, {
        method: "POST"
    });

    if (res.ok) {
        showToast("Deleted");
        location.reload();
    } else {
        showToast("Error", "danger");
    }
}

async function setMain(imageId, productId) {

    const res = await fetch(`/Admin/Product/SetMainImage?imageId=${imageId}&productId=${productId}`, {
        method: "POST"
    });

    if (res.ok) {
        showToast("Main image updated");
        location.reload();
    } else {
        showToast("Error", "danger");
    }
}