// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    var cultureItems = document.querySelectorAll('.dropdown-item[data-culture]');
    cultureItems.forEach(function (item) {
        item.addEventListener('click', function (e) {
            e.preventDefault();
            var culture = this.getAttribute('data-culture');
            var url = new URL(window.location.href);
            url.searchParams.set('culture', culture);
            window.location.href = url.href;
        });
    });
});