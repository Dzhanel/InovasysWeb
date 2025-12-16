$(function () {

    /* ==========================================================
       PASSWORD GENERATOR
    ========================================================== */
    (function () {
        const UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const LOWER = "abcdefghijklmnopqrstuvwxyz";
        const DIGIT = "0123456789";
        const SYMBOL = "!@#$%^&*()-_=+[]{};:,.?/";

        function randInt(max) {
            const arr = new Uint32Array(1);
            crypto.getRandomValues(arr);
            return arr[0] % max;
        }

        function shuffle(chars) {
            for (let i = chars.length - 1; i > 0; i--) {
                const j = randInt(i + 1);
                [chars[i], chars[j]] = [chars[j], chars[i]];
            }
            return chars;
        }

        function generatePassword(length = 12) {
            const required = [
                UPPER[randInt(UPPER.length)],
                LOWER[randInt(LOWER.length)],
                DIGIT[randInt(DIGIT.length)],
                SYMBOL[randInt(SYMBOL.length)]
            ];

            const all = UPPER + LOWER + DIGIT + SYMBOL;
            const out = [...required];

            while (out.length < length) {
                out.push(all[randInt(all.length)]);
            }

            return shuffle(out).join("");
        }

        $('#genPassBtn').on('click', async function () {
            const pass = generatePassword(12);
            const $input = $('#modalPassword');

            $input.val(pass).attr('type', 'text');
            setTimeout(() => $input.attr('type', 'password'), 1200);

            try { await navigator.clipboard.writeText(pass); } catch { }
        });
    })();


    /* ==========================================================
       PASSWORD VALIDATION HELPER
    ========================================================== */
    function validatePasswordInRow(userId) {
        const $row = $(`tr[data-user-id="${userId}"]`);
        if (!$row.length) return;

        const $passwordInput = $(`input.password-input[data-user-id="${userId}"]`);
        const password = $passwordInput.val() || '';
        const isValid = password.length >= 8;

        const $badge = $row.find('.badge');

        if (isValid) {
            $row.removeClass('password-invalid');
            $badge.remove();
        } else {
            $row.addClass('password-invalid');
            if (!$badge.length) {
                const $actionsCell = $row.find('td:last');
                const $newBadge = $(`
                    <span class="badge bg-danger me-2" title="Password required (min 8 characters)">
                        <i class="bi bi-exclamation-triangle-fill"></i> Password Required
                    </span>
                `);
                $actionsCell.prepend($newBadge);
            }
        }
    }


    /* ==========================================================
       DATATABLE INIT
    ========================================================== */
    $('#usersTable').DataTable({
        paging: true,
        searching: true,
        ordering: true,
        info: true,
        pageLength: 10,
        order: [[1, 'asc']],
        columnDefs: [
            { orderable: false, targets: [6, 7] }
        ]
    });


    /* ==========================================================
       MODAL HANDLING
    ========================================================== */
    const modal = new bootstrap.Modal($('#editUserModal')[0]);

    $('.edit-user-btn').on('click', function () {
        const userId = $(this).data('userid');
        const userName = $(this).data('name');

        $('#modalIndex').val(userId);
        $('#modalUserName').text(userName);

        $('#modalNote').val(
            $(`input.note-input[data-user-id="${userId}"]`).val() || ''
        );

        $('#modalPassword').val(
            $(`input.password-input[data-user-id="${userId}"]`).val() || ''
        );

        modal.show();
    });


    /* ==========================================================
       SAVE MODAL CHANGES
    ========================================================== */
    $('#saveModalBtn').on('click', function () {
        const userId = $('#modalIndex').val();
        const note = $('#modalNote').val();
        const password = $('#modalPassword').val();

        $(`input.note-input[data-user-id="${userId}"]`).val(note);

        if (!password || password.length < 8) {
            /*alert('Password must be at least 8 characters long');*/
            return;
        }

        const $passwordInput = $(`input.password-input[data-user-id="${userId}"]`);
        $passwordInput.val(password);
        validatePasswordInRow(userId);

        modal.hide();
    });


    /* ==========================================================
       FORM SUBMIT LOADER
    ========================================================== */
    $('#usersForm').on('submit', function (e) {
        if ($('.password-invalid').length) {
            e.preventDefault();
            return false;
        }

        const $btn = $('#saveAllBtn');
        if ($btn.length) {
            $btn.prop('disabled', true)
                .html('<span class="spinner-border spinner-border-sm me-2"></span>Saving...');
        }
    });


    /* ==========================================================
       AUTO-DISMISS ALERTS
    ========================================================== */
    setTimeout(() => {
        $('.alert').each(function () {
            bootstrap.Alert.getOrCreateInstance(this).close();
        });
    }, 5000);

});
