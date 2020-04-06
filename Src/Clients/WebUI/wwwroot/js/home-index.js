'use strict';

let checked = [];

function runtime() {
    $(document).on('click',
        '.votingCheckBox',
        function() {
            const checkBox = this.children[0].children[0].children[0].children[0].children[0];
            if (!checkBox.checked) {
                if (checked.length === 2) {
                    Swal.fire(
                        'Error',
                        'You cannot vote for more than two films at once!',
                        'error'
                    );
                    return;
                }

                checkBox.checked = true;
                checked.push(checkBox.dataset.id);
            } else {
                checked.pop(checkBox.dataset.id);
                checkBox.checked = false;
            }
        });

    document.querySelector('.btn-cmd-vote').addEventListener('click',
        function() {
            this.blur();

            fetch(new Request('https://localhost:5001/api/voting/getanswersbyvoting/1'))
                .then((response) => {
                    return response.json();
                })
                .then((data) => {
                    var tmp = [];
                    data.votingAnswers.forEach(fetched => {
                        checked.forEach(x => {
                            if (fetched.votingAnswerId == x) tmp.push(fetched);
                        });
                    });

                    fetch(new Request('https://localhost:5001/api/voting/createvotingpolle',
                        {
                            method: 'POST',
                            body: JSON.stringify({ 'Answers': tmp }),
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        })).then((response) => {
                        if (response.status === 200) {
                            Swal.fire(
                                'You have successfully voted.',
                                'All further voting attempts will not be considered.',
                                'success');
                        }
                    });
                });
        });
}

runtime();