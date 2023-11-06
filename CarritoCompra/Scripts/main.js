let contador = 0;

let onFailure = function (xhr) {
    $("body").html(xhr.responseText || xhr);
}

$(".ContenedorLayout").on("click", ".restar", function () {
    let cantidadElement = $(this).siblings(".cantidad");
    let cantidad = parseInt(cantidadElement.text());

    if (cantidad > 1) {
        cantidadElement.text(cantidad - 1);
    }
});

$(".ContenedorLayout").on("click", ".sumar", function () {
    let cantidadMaxima = $(this).data("cantidad-maxima");
    let cantidadElement = $(this).siblings(".cantidad");
    let cantidad = parseInt(cantidadElement.text());

    if (cantidad < cantidadMaxima) {
        cantidadElement.text(cantidad + 1);
    }
});

$(".ContenedorLayout").on("click", ".btnAgregarCarrito", function (e) {
    contador += 1;
    e.stopPropagation();
    e.preventDefault();

    let $this = $(this);
    $this.button('loading');

    let id = $this.data("id");
    let nombre = $this.data("nombre");
    let cantidad = parseInt($this.siblings(".cantidad-container").find(".cantidad").text());
    let idUsuario = $(".idUsuario").val();

    $(".sinResultados").hide();

    let plantillaCarrito = '<div class="panel panel-default productoCarrito{posicion}">' +
        '    <div class="panel-body">' +
        '        <div class="row">' +
        '            <div class="col-lg-8 col-md-8">' +
        '                {producto}' +
        '            </div>' +
        '            <div class="col-lg-2 col-md-2">' +
        '                {cantidad}' +
        '            </div>' +
        '            <div class="col-lg-2 col-md-2">' +
        '                <span class="removerProducto" data-posicion="{posicion}"> <i class="glyphicon glyphicon-remove" style="color:Red;"></i></span>' +
        '            </div>' +
        '        </div>' +
        '    </div>' +
        '<input type="hidden" class="inpuInformacionProducto" data-nombre="{nombre}" data-id-usuario="{usuario}" data-id-producto="{idProducto}" data-cantidad-producto="{cantidad}" />' +
        '</div>';

    plantillaCarrito = plantillaCarrito.replace("{posicion}", contador);
    plantillaCarrito = plantillaCarrito.replace("{producto}", nombre);
    plantillaCarrito = plantillaCarrito.replace("{cantidad}", cantidad);
    plantillaCarrito = plantillaCarrito.replace("{idProducto}", id);
    plantillaCarrito = plantillaCarrito.replace("{usuario}", idUsuario);
    plantillaCarrito = plantillaCarrito.replace("{cantidad}", cantidad);
    plantillaCarrito = plantillaCarrito.replace("{nombre}", nombre);
    plantillaCarrito = plantillaCarrito.replace("{posicion}", contador);

    let contenedor = $(".listaPedidos");
    contenedor.append(plantillaCarrito);
    $this.button('reset');
    $(".guardarTransaccion").show();
});

$(".ContenedorLayout").on("click", ".guardarTransaccion", function (e) {
    e.stopPropagation();
    e.preventDefault();

    let $this = $(this);
    $this.button('loading');

    let jsonDatos = [];
    let _ProductosCarrito = $(".inpuInformacionProducto")

    _ProductosCarrito.each(function () {
        let idProducto = $(this).data("id-producto");
        let cantidadProducto = $(this).data("cantidad-producto");
        let idUsuario = $(this).data("id-usuario");
        let nombre = $(this).data("nombre");

        let dato = {
            id_producto: idProducto,
            Cantidad_disponible: cantidadProducto,
            id_usuario: idUsuario,
            nombre: nombre
        };

        // Agregar el objeto a la lista de datos
        jsonDatos.push(dato);
    });

    validarCantidadDisponileProducto(jsonDatos);
});

let validarCantidadDisponileProducto = function (productos) {
    $.ajax({
        url: "/Cliente/ValidarCantidadProductos",
        type: "POST",
        data: JSON.stringify(productos),
        contentType: "application/json",
        success: function (data) {
            if (data.productosSinStock) {
                let plantilla = "- <b>{nombre}:</b> cantidad disponible {cantidad} <br/>"
                let mensaje = "";

                for (var i = 0; i < data.lista.length; i++) {
                    mensaje += plantilla.replace("{nombre}", data.lista[i].nombre)
                        .replace("{cantidad}", data.lista[i].Cantidad_disponible);
                }

                $(".productosSinStock").html(mensaje);
                $("#confirmacionModal").modal('show');
                $(".guardarTransaccion").button('reset');

                $(".ContenedorLayout").on("click", "#btnAceptarGuardar", function (e) {
                    e.stopPropagation();
                    e.preventDefault();
                    GuardarProductos(productos);
                });
            }
            else {
                GuardarProductos(productos);
            }
        }
    });
}

let GuardarProductos = function (productos) {
    $.ajax({
        url: "/Cliente/GuardarDatos",
        type: "POST",
        data: JSON.stringify(productos),
        contentType: "application/json",
        success: function (data) {
            location.reload();
        }
    });
}

$(".ContenedorLayout").on("click", ".removerProducto", function () {
    let clase = ".productoCarrito" + $(this).data("posicion");
    $(clase).remove();

    if ($(".inpuInformacionProducto").length <= 0) {
        $(".guardarTransaccion").hide();
        $(".sinResultados").show();
    }
});