document.querySelectorAll(".delete-product")
    .forEach(btn => {

        btn.addEventListener("click", async function () {

            if (!confirm("حذف محصول؟")) return;

            const id = this.dataset.id;

            const res = await fetch(`/Admin/Product/Delete/${id}`, {
                method: "POST"
            });

            if (res.ok) {
                document.getElementById(`row-${id}`).remove();
            }

        });

    });

const imageInput = document.getElementById("imageInput");
const previewContainer = document.getElementById("previewContainer");

if (imageInput) {

    imageInput.addEventListener("change", function () {

        previewContainer.innerHTML = "";

        Array.from(this.files).forEach(file => {

            const reader = new FileReader();

            reader.onload = function (e) {

                const img = document.createElement("img");
                img.src = e.target.result;

                previewContainer.appendChild(img);
            };

            reader.readAsDataURL(file);
        });

    });

}

document.querySelectorAll(".delete-image")
    .forEach(btn => {

        btn.addEventListener("click", async function () {

            const imageId = this.dataset.id;
            const productId = this.dataset.product;

            const res = await fetch(`/Admin/Product/DeleteImage?imageId=${imageId}&productId=${productId}`, {
                method: "POST"
            });

            if (res.ok) {
                document.getElementById(`img-${imageId}`).remove();
            }

        });

    });

document.querySelectorAll(".set-main")
    .forEach(btn => {

        btn.addEventListener("click", async function () {

            const imageId = this.dataset.id;
            const productId = this.dataset.product;

            const res = await fetch(`/Admin/Product/SetMainImage?imageId=${imageId}&productId=${productId}`, {
                method: "POST"
            });

            if (res.ok) {
                location.reload();
            }

        });

    });document.querySelectorAll(".set-main")
    .forEach(btn => {

        btn.addEventListener("click", async function () {

            const imageId = this.dataset.id;
            const productId = this.dataset.product;

            const res = await fetch(`/Admin/Product/SetMainImage?imageId=${imageId}&productId=${productId}`, {
                method: "POST"
            });

            if (res.ok) {
                location.reload();
            }

        });

    });

