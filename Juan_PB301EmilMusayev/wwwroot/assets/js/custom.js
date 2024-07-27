$(document).ready(function () {

    //search function
    $(document).on("keyup", "#searchInput", function () {
        $("#searchList").html("")
        let searchValue = $(this).val();
        if (searchValue.trim().length > 3) {
            axios.get("/product/searchProduct?searchInput=" + searchValue)
                .then(function (datas) {
                    $("#searchList").html(datas.data)
                })
        }
    });

    //partial modal function
    let links = $(".productModal").click(function (ev) {
        ev.preventDefault();
        let url = ($(this).attr("href"));
        axios.get(url)
            .then(function (response) {
                $(".modal-body").html(response.data);
                $('.img-zoom').zoom();
                $('.product-large-slider').slick({
                    fade: true,
                    arrows: false,
                    asNavFor: '.pro-nav'
                });
                $('.pro-nav').slick({
                    slidesToShow: 4,
                    asNavFor: '.product-large-slider',
                    arrows: false,
                    focusOnSelect: true
                });
                $('.img-zoom').zoom();
            });
    })

    //addToCart
    $(".addToCart").click(function (ev) {
        ev.preventDefault();
        let id = $(this).data("id");
        axios.get("/cart/addtocart?id=" + id)
            .then(function (datas) {
                $(".minicart-content-box").html(datas.data);
            })
    });

});
