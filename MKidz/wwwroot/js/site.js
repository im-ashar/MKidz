function pauseOthers(ele) {
    $("audio").not(ele).each(function (index, audio) {
        audio.pause();
    });
}

let currentlyPlaying;

const buttons = document.querySelectorAll('.show-audio');

buttons.forEach((button) => {
    button.addEventListener('click', () => {
        const audioId = button.getAttribute('data-audio-id');
        const container = document.querySelector(`.audio-container[data-audio-id="${audioId}"]`);
        if (container.innerHTML === '') {
            container.innerHTML = `
                                    <audio controls controlsList="nodownload noplaybackrate" onplay="pauseOthers(this);">
                                    <source src="https://drive.google.com/uc?export=view&id=${audioId}">
                                    Your browser does not support the audio tag.
                                </audio>
                <button class="btn btn-outline-dark" onclick="repeatAudio(this)"><i class="repeat-button bi bi-repeat-1"></i></button>
                            `;
        }
        const audio = container.querySelector('audio');
        container.classList.toggle('show');
        if (audio.play) {
            audio.pause();
        }
        const icon = button.querySelector('i');
        if (icon.classList.contains('bi-chevron-down')) {
            icon.classList.remove('bi-chevron-down');
            icon.classList.add('bi-chevron-up');
        } else {
            icon.classList.remove('bi-chevron-up');
            icon.classList.add('bi-chevron-down');
        }
    });

});
function repeatAudio(button) {
    const container = button.closest('.audio-container');
    const audio = container.querySelector('audio');
    const repeatButton = button.querySelector('.repeat-button');

    if (audio.loop) {
        audio.removeAttribute('loop');
        repeatButton.classList.remove('bi-repeat');
        repeatButton.classList.add('bi-repeat-1');
    } else {
        audio.setAttribute('loop', true);
        repeatButton.classList.remove('bi-repeat-1');
        repeatButton.classList.add('bi-repeat');
    }
}


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