

const menuBtn = document.querySelector(".menu-icon span");
const searchBtn = document.querySelector(".search-icon");
const cancelBtn = document.querySelector(".cancel-icon");
const items = document.querySelector(".nav-items");
const form = document.querySelector("form");
menuBtn.onclick = () => {
    items.classList.add("active");
    menuBtn.classList.add("hide");
    searchBtn.classList.add("hide");
    cancelBtn.classList.add("show");
}
cancelBtn.onclick = () => {
    items.classList.remove("active");
    menuBtn.classList.remove("hide");
    searchBtn.classList.remove("hide");
    cancelBtn.classList.remove("show");
    form.classList.remove("active");
    cancelBtn.style.color = "#ff3d00";
}
searchBtn.onclick = () => {
    form.classList.add("active");
    searchBtn.classList.add("hide");
    cancelBtn.classList.add("show");
}

const links = document.querySelectorAll('.bookLink');
console.log(links);
links.forEach((a) => {
    a.addEventListener('click', () => {
        var folderId = a.dataset.folderid;
        var folderName = a.dataset.foldername;


        $('main').css('display', 'flex');
        $('main').css('justify-content', 'center');
        $('main').css('margin-top', '200px');
        $('main').html('<div class="spinner-border text-dark d-flex justify-content-center align-items-center" role="status"><span class= "sr-only"></span></div> ');

        $.ajax({
            url: "/Home/AudioFiles",
            type: "POST",
            data: { folderId: folderId, folderName: folderName },
            success: function (result) {
                $('main').css('display', '');
                $('main').css('justify-content', '');
                $('main').css('margin-top', '');
                $('main').html(result)
            },
            error: function (result) {
                alert(result)
            }
        })
    });
});