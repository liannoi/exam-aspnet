'use strict';

function runtime() {
    const softDeleter = new SoftDeleter(document.querySelectorAll('.btn-cmd-delete'),
        '/Films/Delete',
        'Film successfully removed.');
    softDeleter.subscribeToDelete();
}

runtime();