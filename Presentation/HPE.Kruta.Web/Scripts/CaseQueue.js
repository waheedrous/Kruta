$(document).ready(function () {
    AttachSelectAllToControl();
    AttachToggleSelectAll();
    AttachCaseQueueCommands();
    $('[data-toggle="tooltip"]').tooltip();
});

function AttachCaseQueueCommands() {
    $("#AssignCaseQueueCommand").attr('onclick', 'doAssign()');
}

function AttachToggleSelectAll() {
    $('body').on('click', 'input[id*=chkSelect]', function () {

        toggleSelectAll($(this).closest('table'));

        //Select the grid row when checking one of the check boxes
        //var cb = $(this);
        //if (cb.prop('checked')) {
        //    cb.closest('tr').addClass('k-state-selected');
        //}
        //else {
        //    cb.closest('tr').removeClass('k-state-selected');
        //}
    })
}

function AttachSelectAllToControl() {
    $(document).on('change', '#selectAll', function () {
        selectAll(this);
    });
}

function selectAll(cb) {
    var table = $('table[role = "grid"]');
    table.find('input[type="checkbox"]').each(function (index, element) {
        if (index > 0) {
            element.checked = cb.checked;
        }
    });

    toggleItemDetails(table);
}

function toggleSelectAll(table) {
    // Set the "select all" checkbox to a checked state if all
    // child check boxes are checked.

    if (!table) {
        return;
    }

    var selectallCheckbox = $('input[id=selectAll]');


    if (selectallCheckbox) {
        var checkedCount = table.find('input[id*=chkSelect]:checked').length;

        if (checkedCount > 0) {
            var checkboxCount = table.find('input[id*=chkSelect]').length;

            if (checkedCount == checkboxCount) {
                selectallCheckbox.prop('checked', true);
            }
            else {
                selectallCheckbox.prop('checked', false);
            }
        }
    }

    toggleItemDetails(table);
}

function doAssign() {
    // check if any document has been selected
    if ($('table[role = "grid"] input:checkbox:checked').length == 0) {
        ShowInformationModal('Notification', 'Please select at least one case from the queue.');
    }
    else {
        // show the Assign Modal
        var assignModal = $("#assignModal");
        $('#routingControlStaffList').prop('selectedIndex', 0);
        assignModal.modal('show');
    }
}

function SaveRouteStatus() {
    // show the route modal

    if ($('table[role = "grid"] input:checkbox:checked').length == 0) {
        ShowInformationModal('Notification', 'Please select at least one case from the queue.');
    }
    else {
        // show the CaseModel from index
        var routeBtnModal = $("#routeBtnModal");
        $('#routeDepartmentsList').prop('selectedIndex', 0);
        routeBtnModal.modal('show');
    }

}

function showAssignConfirmModal(title, msg) {
    var confirmationModal = $("#confirmationModal");
    confirmationModal.find("#confirmationModalTitle").html(title);
    confirmationModal.find("#confirmationModalMessage").html(msg);
    asyncShowConfirmModal(yesAssignFunction, noFunction);
}

function showRouteConfirmModal(title, msg) {
    // $("#routeModal").modal("hide");
    var confirmationModal = $("#confirmationModal");
    confirmationModal.find("#confirmationModalTitle").html(title);
    confirmationModal.find("#confirmationModalMessage").html(msg);
    asyncShowConfirmModal(yesRouteFunction, noFunction);
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

function yesAssignFunction() {
    // call the assign method
    // show the success information
    var selectedQueueIds = [];
    $('table[role = "grid"]').find('input[type="checkbox"]').each(function (index, element) {
        if (index > 0 && element.checked) {
            selectedQueueIds.push(element.value);
        }
    });

    var empId = $('#routingControlStaffList :selected').val();

    jQuery.ajaxSettings.traditional = true

    $.ajax({
        url: "/CaseQueue/CaseQueues_BatchAssign",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        data: { selectedQueueIds: selectedQueueIds, empId: empId },
        success: function (data) {
            if (data.Success) {
                ShowInformationModal('Notification', 'The selected Document(s) assigned successfully.');
                RefreshCaseQueue();
            } else {
                ShowInformationModal('Notification', 'Oops! Something wrong just happened.');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
            //in case of error the error view will come back
            //the code below will display it instead of the current page
            $("html").html($(xhr.responseText));
        }
    });
}

function noFunction() {
    // nothing for now
}

function yesRouteFunction() {
    // call the route method
    // show the success information
   
    var selectedQueueIds = [];
    $('table[role = "grid"]').find('input[type="checkbox"]').each(function (index, element) {
        if (index > 0 && element.checked) {
            selectedQueueIds.push(element.value);
        }
    });

    var departmentID = $('#routeDepartmentsList :selected').val();

   jQuery.ajaxSettings.traditional = true

    $.ajax({
        url: "/QueueDetails/RouteQueueAndSave",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        data: { selectedQueueIds: selectedQueueIds, departmentID: departmentID },
        success: function (data) {
            if (data.Success) {
                //DisplayQueueDetails(modelIDVal, documentIDVal);
                ShowInformationModal('Notification', 'The selected document has been routed successfully.');
                QueueDetailsCommandClick();
                RefreshCaseQueue();
            } else {
                ShowInformationModal('Notification', 'Oops! Something wrong just happened.');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
            //in case of error the error view will come back
            //the code below will display it instead of the current page
            $("html").html($(xhr.responseText));
        }
    });

}

function ShowInformationModal(title, msg) {
    var inform = $("#informationModal");
    inform.modal('show');
    inform.find("#informationModalTitle").html(title);
    inform.find("#informationModalMessage").html(msg);
    inform.find("#informationModalOk").off('click').click(function () {
        inform.modal("hide");

        //var selectedQ2 = $('table[role = "grid"]').find('input[type="checkbox"][value=12]')
        //selectedQ2.prop('checked', true);
    });
}

function RefreshCaseQueue() {
    $("#CaseQueue").data("kendoGrid").dataSource.read();
}

function clearFilter() {
    $("#CaseQueue").data("kendoGrid").dataSource.filter([]);
}

function onCaseQueueDataBound(e) {
    // Handle the row number of the grid
    resetCaseQueueRowNumber(e);

    // maintain the selection of the checkbox after routing
    var tempSelectedQueueIds = $('#selectedQueueIds').val();
    if (tempSelectedQueueIds && tempSelectedQueueIds.length > 0) {
        var selectedQueueIds = tempSelectedQueueIds.split(",").map(Number);

        if (selectedQueueIds && selectedQueueIds.length > 0) {
            for (var i in selectedQueueIds) {
                var selectedQ = $('table[role = "grid"]').find('input[type="checkbox"][value=' + selectedQueueIds[i] + ']');
                if (selectedQ) {
                    selectedQ.prop('checked', true);
                }
            }
        }
    }

    toggleSelectAll($('table[role = "grid"]'));
}

var caseQueueRowNumber = 0;

function resetCaseQueueRowNumber(e) {
    caseQueueRowNumber = 0;
}

function renderCaseQueueRowNumber(data) {
    return ++caseQueueRowNumber;
}

function toggleItemDetails(table) {

    if (!table) {
        return;
    }

    var selectedQueueIds = [];
    var checkedQueueIds = table.find('input[id*=chkSelect]:checked');

    checkedQueueIds.each(function (index, element) {
        selectedQueueIds.push(element.value);
    });

    $('#selectedQueueIds').val(selectedQueueIds)
}