﻿$(document).ready(function () {
    AttachSelectAllToControl();
    AttachToggleSelectAll();
    AttachDocumentQueueCommands();
    $('[data-toggle="tooltip"]').tooltip();
});

function AttachDocumentQueueCommands() {
    $("#AssignDocumentQueueCommand").attr('onclick', 'doAssign()');
    $("#RefreshDocumentQueueCommand").attr('onclick', 'RefreshDocumentQueue()');
    $("#queueDetailsCommand").attr('onclick', 'QueueDetailsCommandClick()');
    $("#CreateCaseCommand").attr('onclick', 'doCase()');
    //$("#btnRoute_temp").attr('onclick', 'SaveRouteStatus()');
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
        ShowInformationModal('Notification', 'Please select at least one document from the queue.');
    }
    else {
        // show the Assign Modal
        var assignModal = $("#assignModal");
        $('#routingControlStaffList').prop('selectedIndex', 0);
        assignModal.modal('show');
    }
}

function doCase() {
        // check if any document has been selected
    if ($('table[role = "grid"] input:checkbox:checked').length == 0) {
        ShowInformationModal('Notification', 'Please select at least one document from the queue.');
    }
    else {
        // show the CaseModel from index
        var caseModal = $("#caseModal");
        $('#createCaseDepartmentsList').prop('selectedIndex', 0);
        caseModal.modal('show');
    }
}


function SaveRouteStatus() {
    // show the route modal

    if ($('table[role = "grid"] input:checkbox:checked').length == 0) {
        ShowInformationModal('Notification', 'Please select at least one document from the queue.');
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

function showCaseConfirmModal(title, msg) {
    // $("#routeModal").modal("hide");
    var confirmationModal = $("#confirmationModal");
    confirmationModal.find("#confirmationModalTitle").html(title);
    confirmationModal.find("#confirmationModalMessage").html(msg);
    asyncShowConfirmModal(yesCaseFunction, noFunction);
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
        url: "/DocumentQueue/Queues_BatchAssign",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        data: { selectedQueueIds: selectedQueueIds, empId: empId },
        success: function (data) {
            if (data.Success) {
                ShowInformationModal('Notification', 'The selected Document(s) assigned successfully.');
                RefreshDocumentQueue();
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

//Added Case function to load data into database
function yesCaseFunction() {
    // call the assign method
    // show the success information
    var selectedQueueIds = [];
    $('table[role = "grid"]').find('input[type="checkbox"]').each(function (index, element) {
        if (index > 0 && element.checked) {
            selectedQueueIds.push(element.value);
        }
    });

    var departmentID = $('#createCaseDepartmentsList :selected').val();
    var caseTypeID = $('#createCaseTypeList :selected').val();

    jQuery.ajaxSettings.traditional = true

    $.ajax({
        url: "/DocumentQueue/CreatePropertyCase",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        data: { selectedQueueIds: selectedQueueIds, departmentID: departmentID, caseTypeID: caseTypeID },
        success: function (data) {
            if (data.Success) {
                ShowInformationModal('Notification', 'Case created successfully.');
                RefreshDocumentQueue();
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
        url: "/DocumentQueue/RouteQueueAndSave",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        data: { selectedQueueIds: selectedQueueIds, departmentID: departmentID },
        success: function (data) {
            if (data.Success) {
                //DisplayQueueDetails(modelIDVal, documentIDVal);
                ShowInformationModal('Notification', 'The selected document has been routed successfully.');
                QueueDetailsCommandClick();
                RefreshDocumentQueue();
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

function RefreshDocumentQueue() {
    $("#DocumentQueue").data("kendoGrid").dataSource.read();
}

//Added clear filter functionality
function clearFilter() {
    $("#DocumentQueue").data("kendoGrid").dataSource.filter([]);
}
function DisplayQueueDetails(queueID, documentID) {
    $.ajax({
        url: "/QueueDetails/DisplayQueueDetails",
        type: "GET",
        datatype: "json",
        data: { queueID: queueID },
        success: function (data) {
            $('#queueDetailsSection').html(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
            //in case of error the error view will come back
            //the code below will display it instead of the current page
            $("html").html($(xhr.responseText));
        }
    });
}


var childWindow;       // variable to hold the opened window.
var currentDocNumber = "";  // variable to store the previous document number.

function OpenDocument(documentID) {
    $.ajax({
        url: "/QueueDetails/GetDocumentPath",
        type: "GET",
        datatype: "json",
        data: { documentID: documentID },
        success: function (data) {
            if (data.DocumentPath) {
                // check if the section is already extended  to prevent opening the window another time when collapsing the panel
                if (!$('#queueDetailsCommand').hasClass('collapsed')) {
                    //alert(window.closed.toString());

                    if (currentDocNumber == "") {                                           // open new tab/window if it's the 1st time opening the new tab/window.
                        childWindow = window.open(data.DocumentPath, "_blank", "", true);
                    }
                    else if (childWindow.closed) {                                         // in case the opened tab/window was closed open a new one.
                        childWindow = window.open(data.DocumentPath, "_blank", "", true);
                    }
                    else if (currentDocNumber != documentID && !childWindow.closed) {      // refresh the opened tab/window with new URL.
                        childWindow.location.href = data.DocumentPath;
                    }

                    currentDocNumber = documentID;                                         // store the prior document number.
                }
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

function QueueDetailsCommandClick() {
    $('table[role = "grid"]').find('input[type="checkbox"]').each(function (index, element) {
        if (index > 0 && element.checked) {
            DisplayQueueDetails(this.value, this.getAttribute('data-docid'));
            return;
        }
    });
}

function SaveQueueStatus() {

    var noteVal = $('#Notes').val();
    var statusVal = $('#DocumentStatuses').val();
    var modelIDVal = $('#modelIDVal').val();
    var documentIDVal = $('#documentIDVal').val();

    $.ajax({
        url: '/QueueDetails/SaveStatus',
        contentType: "application/json; charset=utf-8",
        type: 'GET',
        dataType: 'json',
        data: { queueID: modelIDVal, documentStatusID: statusVal, notes: noteVal },
        success: function (data) {
            if (data.Success) {
                DisplayQueueDetails(modelIDVal, documentIDVal);
                RefreshDocumentQueue();
                ShowInformationModal('Notification', 'Status saved successfully.');
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

function AddNote() {

    var noteVal = $('#Notes').val();
    var modelIDVal = $('#modelIDVal').val();
    var documentIDVal = $('#documentIDVal').val();

    $.ajax({
        url: '/QueueDetails/AddNote',
        contentType: "application/json; charset=utf-8",
        type: 'GET',
        dataType: 'json',
        data: { queueID: modelIDVal, notes: noteVal },
        success: function (data) {
            if (data.Success) {
                DisplayQueueDetails(modelIDVal, documentIDVal);
                RefreshDocumentQueue();
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


function onDocumentQueueDataBound(e) {
    // Handle the row number of the grid
    resetDocumentQueueRowNumber(e);

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

var documentQueueRowNumber = 0;

function resetDocumentQueueRowNumber(e) {
    documentQueueRowNumber = 0;
}

function renderDocumentQueueRowNumber(data) {
    return ++documentQueueRowNumber;
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

    var checkedCount = checkedQueueIds.length;

    // handle the queue detail button appearance and behavior
    var queueDetailsCommand = $('#queueDetailsCommand');

    if (checkedCount == 1) {
        if (queueDetailsCommand.hasClass('disabled')) {
            queueDetailsCommand.removeClass('disabled');
        }
    }
    else {
        if (!queueDetailsCommand.hasClass('disabled')) {
            queueDetailsCommand.addClass('disabled');
            // hide the queue detail section when disabling the button
            $('#queueDetailsSection').collapse('hide');
            // Clear the stored model ID (Queue ID) to avoid selecting it on onDocumentQueueDataBound after any grid action
            $('#modelIDVal').val("");
        }
    }
}