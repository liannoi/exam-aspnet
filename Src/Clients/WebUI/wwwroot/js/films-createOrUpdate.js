'use strict';

function runtime() {
    const autoBlur = new Blur(document.querySelector('#IsNopeActors'), document.querySelector('#SelectedActors'));
    autoBlur.subscribeToBlur();
}

runtime();