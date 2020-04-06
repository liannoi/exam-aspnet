'use strict';

class SoftDeleter {
    constructor(parentSelector, action, positiveMessage, positiveAction) {
        this.parentSelector = parentSelector;
        this.action = action;
        this.positiveMessage = positiveMessage;
        this.positiveAction = positiveAction;
    }

    async requestDelete(id) {
        const self = this;
        return await fetch(new Request(`${self.action}/${id}`),
            {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(
            function(response) {
                if (response.status === 200) {
                    successDelete().then(() => {
                        if (typeof self.positiveAction != 'undefined') {
                            self.positiveAction();
                        }

                        self.deleteItem.parentNode.parentNode.remove();

                        return true;
                    });
                }
            });

        function successDelete() {
            return Swal.fire(
                'Deleted.',
                self.positiveMessage,
                'success'
            );
        }
    }

    askToDelete(item) {
        const self = this;
        return Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes'
        }).then(async (result) => {
            if (result.value) {
                if (!await self.requestDelete(item.dataset.id)) {
                    return;
                }

                item.parentNode.parentNode.remove();
            }
        });
    }

    subscribeToDelete() {
        const self = this;
        this.parentSelector.forEach(e => {
            e.addEventListener('click',
                function(event) {
                    event.preventDefault();
                    self.deleteItem = e;
                    self.askToDelete(e);
                });
        });
    }
}