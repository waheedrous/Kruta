function LoadEmployeeRoles(selectedEmpId) {
    // Refresh the selected employee roles
    $.ajax({
        url: "/EmployeeRoles/LoadEmployeeRoles",
        type: "GET",
        datatype: "json",
        data: { id: selectedEmpId },
        success: function (data) {
            $('#employeeRoles').html(data);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("html").html($(xhr.responseText));
        }
    });
}

function onSelectEmployeeRole(chkSelectRole) {
    var roleId = chkSelectRole.value;
    var employeeId = $('#EmployeeList').val();
    var isSelected = chkSelectRole.checked;
    var employeeRoleId = chkSelectRole.getAttribute('data-EmployeeRoleID');

    $.ajax({
        url: "/EmployeeRoles/HandleEmployeeRoleSelection",
        type: "GET",
        datatype: "json",
        data: {
            roleId: roleId, employeeId: employeeId, isSelected: isSelected, employeeRoleId: employeeRoleId
        },
        success: function (data) {
            
        },
        error: function (xhr, ajaxOptions, thrownError) {
            $("html").html($(xhr.responseText));
        }
    });
}