// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function () {
    $('[data-toggle="tooltip"]').tooltip()
});

const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
});

function errorMessage(msg) {
    Toast.fire({
        icon: 'error',
        title: msg
    });
}

function successMessage(msg) {
    Toast.fire({
        icon: 'success',
        title: msg
    });
}
