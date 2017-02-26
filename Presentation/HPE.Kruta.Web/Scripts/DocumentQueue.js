$(document).ready(function () {
    AttachSelectAllToControl();
    AttachToggleSelectAll();
    AttachDocumentQueueCommands();
});

function AttachDocumentQueueCommands() {
    $("#AssignDocumentQueueCommand").attr('onclick', 'doAssign()');
    $("#RefreshDocumentQueueCommand").attr('onclick', 'RefreshDocumentQueue()');
}

function AttachToggleSelectAll() {
    $('body').on('click', 'input[id*=chkSelect]', function () {
        toggleSelectAll($(this).closest('table'));
    })
}

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

function doAssign() {
    // check if any document has been selected
    if ($('table[role = "grid"] input:checkbox:checked').length == 0) {
        ShowInformationModal('Notification', 'Please select at least one document from the queue.');
    }
    else {
        // show the Assign Modal
        var confirm = $("#assignModal");
        $('#routingControlStaffList').prop('selectedIndex', 0);
        confirm.modal('show');
    }
}

function showConfirmModal() {
    asyncShowConfirmModal(yesFunction, noFunction);
}

function asyncShowConfirmModal(yesFunction, noFunction) {
    var confirm = $("#confirmationModal");
    confirm.modal('show');
    confirm.find("#btnYesConfirmYesNo").off('click').click(function () {
        yesFunction();
        confirm.modal("hide");
    });
    confirm.find("#btnNoConfirmYesNo").off('click').click(function () {
        noFunction();
        confirm.modal("hide");
    });
}

function yesFunction() {
    // call the assign method
    // show the succss informaion
    var selectedQueueIdsTemp = [];
    $('table[role = "grid"]').find('input[type="checkbox"]').each(function (index, element) {
        if (index > 0 && element.checked) {
            selectedQueueIdsTemp.push(element.value);
        }
    });

    var selectedQueueIds = selectedQueueIdsTemp.join(', ');
    var empId = $('#routingControlStaffList :selected').val();

    $.ajax({
        url: "/DocumentQueue/Queues_BatchAssign",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        data: { selectedQueueIds: selectedQueueIds, empId: empId },
        success: function (data) {
            if (data.Success) {
                ShowInformationModal('Notification', 'The selected queue(s) assigned successfully.');
                RefreshDocumentQueue();
            } else {
                ShowInformationModal('Notification', 'Opps! Somthing wrong just happend.');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
        }
    });
}
function noFunction() {
    // nothing for now
}

function ShowInformationModal(title, msg) {
    var inform = $("#informationModal");
    inform.modal('show');
    inform.find("#informationModalTitle").html(title);
    inform.find("#informationModalMessage").html(msg);
    inform.find("#informationModalOk").off('click').click(function () {
        inform.modal("hide");
    });
}

function RefreshDocumentQueue() {
    $("#DocumentQueue").data("kendoGrid").dataSource.read();
}

function DisplayQueueDetails(queueID, documentID) {
    $.ajax({
        url: "/QueueDetails/DisplayQueueDetails",
        type: "GET",
        datatype: "json",
        data: { queueID: queueID },
        success: function (data) {
            doDisplayQueueDetails(data, documentID);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
        }
    });
}

function doDisplayQueueDetails(dataToDisplay, documentID) {
    $('#QueueDetailsSection').html(dataToDisplay);
    $('#queueDetailsCommand').click();

    $.ajax({
        url: "/QueueDetails/GetDocumentPath",
        type: "GET",
        datatype: "json",
        data: { documentID: documentID },
        success: function (data) {
            if (data.DocumentPath) {
                window.open(data.DocumentPath, "_blank", "", true);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
        }
    });
}