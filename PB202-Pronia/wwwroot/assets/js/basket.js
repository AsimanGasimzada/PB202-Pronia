const addToBasketButtons = document.querySelectorAll('.addToBasketBtn');
const basketModalArea = document.querySelector("#miniCart");
const basketModalCount = document.querySelector(".basketModalCount")
function RenderRemoveButtonEvent() {
    $('.button-close').on('click', function (e) {
        var dom = $('.main-wrapper').children();
        e.preventDefault();
        var $this = $(this);
        $this.parents('.open').removeClass('open');
        dom.find('.global-overlay').removeClass('overlay-open');
    });
}


addToBasketButtons.forEach(btn => {
    btn.addEventListener('click', async (e) => {
        e.preventDefault();

        const response = await fetch(btn.href);

        const result = await response.text();

        basketModalArea.innerHTML = result;

        RenderRemoveButtonEvent();

        Swal.fire({
            position: "center",
            icon: "success",
            showConfirmButton: false,
            timer: 1500
        });

        let count = basketModalArea.querySelectorAll(".minicart-product").length

        document.querySelector(".basketModalCount").innerHTML = count
        document.querySelector(".basketModalCount2").innerHTML = count
    })
})