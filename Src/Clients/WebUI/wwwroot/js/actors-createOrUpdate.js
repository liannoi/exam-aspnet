'use strict';

function runtime() {
    const autoBlur = new Blur(document.querySelector('#IsNopeFilms'), document.querySelector('#SelectedFilms'));
    autoBlur.subscribeToBlur();
}

runtime();