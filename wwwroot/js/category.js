document.querySelectorAll(".delete-btn")
.forEach(btn => {

    btn.addEventListener("click", async function () {

        if (!confirm("آیا از حذف مطمئن هستید؟"))
            return;

        let id = this.dataset.id;

        const response = await fetch(`/Admin/Category/Delete/${id}`, {
            method: "POST"
        });

        if (response.ok) {

            document
                .getElementById(`row-${id}`)
                .remove();

        }

    });

});