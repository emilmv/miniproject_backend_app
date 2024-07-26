$(document).ready(function () {
    let links = $(".productModal").click(function (ev) {
        ev.preventDefault();
        let url = ($(this).attr("href"));
        console.log(url)
        axios.get(url)
            .then(function (response) {
                $(".modal-body").html(response.data);
				$('.img-zoom').zoom();

				$('.product-large-slider').slick({
					fade: true,
					arrows: false,
					asNavFor: '.pro-nav'
				});


				// product details slider nav active
				$('.pro-nav').slick({
					slidesToShow: 4,
					asNavFor: '.product-large-slider',
					arrows: false,
					focusOnSelect: true
				});

            });
           
    })
});
