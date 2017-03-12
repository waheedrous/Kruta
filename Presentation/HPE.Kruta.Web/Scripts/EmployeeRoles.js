function onChangeEmployeeList(selectedEmpId) {
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
            console.log(xhr.responseText);
        }
    });
}