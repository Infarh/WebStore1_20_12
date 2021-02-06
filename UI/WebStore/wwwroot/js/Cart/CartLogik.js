Cart = {
    _properties: {
        getCartViewLink: "",
        addToCartLink: ""
    },

    init: function(properties) {
        $.extend(Cart._properties, properties);

        Cart.initEvents();
    },

    initEvents: function() {
        $(".add-to-cart").click(Cart.addToCart);

        $(".cart_quantity_up").click(Cart.incrementItem);
        $(".cart_quantity_down").click(Cart.decrementItem);
        $(".cart_quantity_delete").click(Cart.removeItem);
    },

    addToCart: function(event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        $.get(Cart._properties.addToCartLink + "/" + id)
            .done(function() {
                Cart.showToolTip(button);
                Cart.refreshCartView();
            })
            .fail(function () { console.log("addToCart fail") });
    },

    showToolTip: function(button) {
        button.tooltip({ title: "Добавлено в корзину!" }).tooltip("show");
        setTimeout(function() {
            button.tooltip("destroy");
        }, 500);
    },

    refreshCartView: function() {
        var container = $("#cart-container");
        $.get(Cart._properties.getCartViewLink)
            .done(function(cartHtml) {
                container.html(cartHtml);
            })
            .fail(function() { console.log("refreshCartView fail"); });
    },

    incrementItem: function (event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        //$.get(Cart._properties.addToCartLink + "/" + id)
        //    .done(function () {
        //        Cart.showToolTip(button);
        //        Cart.refreshCartView();
        //    })
        //    .fail(function () { console.log("addToCart fail") });
    },

    decrementItem: function (event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        //$.get(Cart._properties.addToCartLink + "/" + id)
        //    .done(function () {
        //        Cart.showToolTip(button);
        //        Cart.refreshCartView();
        //    })
        //    .fail(function () { console.log("addToCart fail") });
    },

    removeItem: function (event) {
        event.preventDefault();

        var button = $(this);
        const id = button.data("id");

        //$.get(Cart._properties.addToCartLink + "/" + id)
        //    .done(function () {
        //        Cart.showToolTip(button);
        //        Cart.refreshCartView();
        //    })
        //    .fail(function () { console.log("addToCart fail") });
    }
}