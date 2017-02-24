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

function doAssign() {
    // check if any document has been selected
    if ($('table[role = "grid"] input:checkbox:checked').length == 0) {
        ShowInformationModal('Notification', 'Please select at least one document from the queue.');
    }
    else {
        AsyncConfirmYesNo(yesFunction, noFunction);
    }
}

function AsyncConfirmYesNo(yesFunction, noFunction) {
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
    var selectedQueueIds = "";
    $('table[role = "grid"]').find('input[type="checkbox"]').each(function (index, element) {
        if (index > 0 && element.checked) {
            selectedQueueIds = selectedQueueIds + element.value + ',';
        }
    });

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
            } else {
                ShowInformationModal('Notification', 'Opps! Somthing wrong just happend.');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.responseText);
        }
    });

    //$.ajax({
    //    type: 'GET',
    //    url: base_url + '/AdvisorDashboard/Summary/FilterSections/?assetDescriptionDistinctRowNumbers=' + selectedValues + "&teamMemberKey=" + teamMemberKey,
    //    data: {},
    //    success: function (result) {
    //        if (result) {
    //            var viewModel = JSON && JSON.parse(result) || $.parseJSON(result);
    //            // Sales/Assets
    //            $("#SalesAssetsYearToDateSalesLabel").text(viewModel.SalesAssetsYearToDateSalesLabel);
    //            $("#SalesAssetsLast12MonthSalesLabel").text(viewModel.SalesAssetsLast12MonthSalesLabel);
    //            $("#SalesAssetsLastYearSalesLabel").text(viewModel.SalesAssetsLastYearSalesLabel);
    //            $("#SalesAssetsCurrentAssetsLabel").text(viewModel.SalesAssetsCurrentAssetsLabel);
    //            // Pipeline Summary
    //            $("#PipelineSummaryThisMonthLabel").text(viewModel.PipelineSummaryThisMonthLabel);
    //            $("#PipelineSummaryNextMonthLabel").text(viewModel.PipelineSummaryNextMonthLabel);
    //            $("#PipelineSummaryNext3MonthsLabel").text(viewModel.PipelineSummaryNext3MonthsLabel);
    //            $("#PipelineSummaryNext12MonthsLabel").text(viewModel.PipelineSummaryNext12MonthsLabel);

    //            var thisMonthLink = $(".PipelineSummary .title.this-month a");
    //            var thisMonthLabel = $(".PipelineSummary .title.this-month span");
    //            var nextMonthLink = $(".PipelineSummary .title.next-month a");
    //            var nextMonthLabel = $(".PipelineSummary .title.next-month span");
    //            var next3MonthsLink = $(".PipelineSummary .title.next-three-months a");
    //            var next3MonthsLabel = $(".PipelineSummary .title.next-three-months span");
    //            var next12MonthsLink = $(".PipelineSummary .title.next-twelve-months a");
    //            var next12MonthsLabel = $(".PipelineSummary .title.next-twelve-months span");

    //            if (viewModel.PipelineSummaryThisMonthShowLink) {
    //                thisMonthLink.show();
    //                thisMonthLabel.hide();
    //            }
    //            else {
    //                thisMonthLink.hide();
    //                thisMonthLabel.show();
    //            }
    //            if (viewModel.PipelineSummaryNextMonthShowLink) {
    //                nextMonthLink.show();
    //                nextMonthLabel.hide();
    //            }
    //            else {
    //                nextMonthLink.hide();
    //                nextMonthLabel.show();
    //            }
    //            if (viewModel.PipelineSummaryNext3MonthsShowLink) {
    //                next3MonthsLink.show();
    //                next3MonthsLabel.hide();
    //            }
    //            else {
    //                next3MonthsLink.hide();
    //                next3MonthsLabel.show();
    //            }
    //            if (viewModel.PipelineSummaryNext12MonthsShowLink) {
    //                next12MonthsLink.show();
    //                next12MonthsLabel.hide();
    //            }
    //            else {
    //                next12MonthsLink.hide();
    //                next12MonthsLabel.show();
    //            }

    //            // Asset Breakdown chart
    //            LoadAssetBreakdown(selectedValues);
    //        }
    //    },
    //    error: function (xhr, ajaxOptions, thrownError) {
    //        console.log(xhr.responseText);
    //    }
    //});


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