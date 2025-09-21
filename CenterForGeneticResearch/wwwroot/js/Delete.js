// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
function Delete(url) {
    Swal.fire({
        title: "Вы уверены?",
        text: "Вы не сможете отменить это действие!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Да, удалить!",
        cancelButtonText: "Отмена"
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(url, {
                method: 'POST',
            })
                .then(response => {
                    if (response.ok) {
                        Swal.fire({
                            title: "Удалено!",
                            icon: "success"
                        }).then(() => {
                            location.reload();
                        });
                    } else {
                        Swal.fire({
                            title: "Ошибка!",
                            icon: "error"
                        });
                    }
                });
        }
    });
}
