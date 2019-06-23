var common = {
    init: function () {
        common.registerEvents();
    },
    registerEvents: function () {
        $("#txtKeyword").autocomplete({
            minLength: 1,
            source: function (request, response) {
                $.ajax({
                    url: "/Product/GetListProductByName",
                    dataType: "json",
                    data: {
                        keyword: request.term
                    },
                    success: function (res) {
                        response(res.data);
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            }
        }).autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
              .append("<a>" + item.label + "</a>")
              .appendTo(ul);
        };
        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            $.ajax({
                url: '/ShoppingCart/Add',
                data: {
                    productId: productId
                },
                type: 'POST',
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        Swal.fire({
                            position: 'top-end',
                            type: 'success',
                            title: 'Đã thêm sản phẩm vào giỏ hàng',
                            showConfirmButton: false,
                            timer: 1000
                        })
                        $.preloader.start();
                        setTimeout(function () {
                            $.preloader.stop();
                        }, 1000);
                    }
                    else
                    {
                        Swal.fire({
                            position: 'top-end',
                            type: 'warning',
                            title: response.message,
                            showConfirmButton: false,
                            timer: 1000
                        })
                        $.preloader.start();
                        setTimeout(function () {
                            $.preloader.stop();
                        }, 1000);
                    }
                }
            });
        });
    }
}
common.init();