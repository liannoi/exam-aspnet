'use strict';

function runtime() {
    const softDeleter = new SoftDeleter(document.querySelectorAll('.btn-cmd-delete'),
        '/Actors/Delete',
        'Actor successfully removed.');
    softDeleter.subscribeToDelete();
}

runtime();