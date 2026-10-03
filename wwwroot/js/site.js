// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    // Confirmación con SweetAlert antes de eliminar (cualquier formulario con la clase swal-delete-form)
    $(document).on('submit', 'form.swal-delete-form', function (e) {
        e.preventDefault();
        var form = this;
        var nombre = $(form).data('nombre');
        Swal.fire({
            title: 'Está seguro que quiere eliminar este registro?',
            text: nombre ? 'Se eliminará "' + nombre + '". No podrá revertir esta acción!' : "No podrá revertir esta acción!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            confirmButtonText: 'Sí, eliminar!',
            cancelButtonText: 'Cancelar',
        })
            .then(function (result) {
                if (result.isConfirmed) {
                    form.submit();
                }
            });
    });

    // Mensaje de éxito después de eliminar (lee el aviso oculto que genera la vista)
    var mensajeEliminado = $('#mensaje-eliminado').data('mensaje');
    if (mensajeEliminado) {
        Swal.fire({
            icon: 'success',
            title: 'Eliminado',
            text: mensajeEliminado,
            confirmButtonColor: '#1B4079'
        });
    }

    // Resultado de la compra (lee el aviso oculto que genera la vista, puede ser éxito o error)
    var avisoCompra = $('#mensaje-compra');
    if (avisoCompra.length) {
        var tipo = avisoCompra.data('tipo');
        Swal.fire({
            icon: tipo,
            title: tipo === 'success' ? 'Compra realizada' : 'No se pudo comprar',
            text: avisoCompra.data('mensaje'),
            confirmButtonColor: '#1B4079'
        });
    }
});