$(document).ready(function () {
    AttachSelectAllToControl();

    $('body').on('click', 'input[id*=chkSelect]', function () {
        toggleSelectAll($(this).closest('table'));
    })
});

function AttachSelectAllToControl() {
    $(document).on('change', '#chkSelectAll', function () {
        selectAll(this);
    });
}

function selectAll(cb) {
    $('table[role = "grid"]').find('input[type="checkbox"]').each(function (index, element) {
        if (index > 0) {
            element.checked = cb.checked;
        }
    });
}

function toggleSelectAll(table) {
    // Set the "select all" checkbox to a checked state if all
    // child checkboxes are checked.

    if (!table) {
        return;
    }

    var selectallCheckbox = $('input[id=chkSelectAll]');

    if (selectallCheckbox) {
        var checkboxCount = table.find('input[id*=chkSelect]').length;
        var checkedCount = table.find('input[id*=chkSelect]:checked').length;

        if (checkedCount == checkboxCount) {
            selectallCheckbox.prop('checked', true);
        }
        else {
            selectallCheckbox.prop('checked', false);
        }
    }
}