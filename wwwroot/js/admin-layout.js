const menuBtn = document.getElementById('menuBtn');
const sidebar = document.getElementById('sidebar');

menuBtn?.addEventListener('click', () => {
    sidebar.classList.toggle('active');
});

document.querySelectorAll('.submenu-btn')
    .forEach(btn => {

        btn.addEventListener('click', () => {

            const submenu = btn.nextElementSibling;

            submenu.classList.toggle('active');

        });

    });