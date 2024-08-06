function FillYBSTypeListByYBSCompany(lstYBSCompanyCtrl, ybsTypes) {

    var lstYBSTypes = $("#" + ybsTypes);
    lstYBSTypes.empty();

    var selectedYBSCompany = lstYBSCompanyCtrl.options[lstYBSCompanyCtrl.selectedIndex].value;

    if (selectedYBSCompany != null && selectedYBSCompany != '') {
        $.getJSON("/TransportationManagement/YBSType/GetYBSTypeByYBSCompanyId", { ybsCompanyId: selectedYBSCompany }, function (ybsTypes) {
            if (ybsTypes != null && !jQuery.isEmptyObject(ybsTypes)) {
                let i = 0;
                $.each(ybsTypes, function (index, ybsType) {
                    if (ybsType.value !== null && ybsType.value !== '')
                   {
                        var tr = $('<tr/>');

                        var pkIdInput = $('<input/>', {
                            'type': 'hidden',
                            'class': 'Pkid',
                            'value': ybsType.value
                        });

                        var pkIdCell = $('<td/>', {
                            'class': 'pkId',
                            'style': 'display:none;'
                        });

                        pkIdCell.append(pkIdInput);
                        tr.append(pkIdCell);

                        var indexCell = $('<td/>').text(index++);
                        tr.append(indexCell);

                        var nameCell = $('<td/>', {
                            'class': 'name'
                        });

                        var nameLink = $('<a/>', {
                            'class': 'text-primary',
                            'href': '/VehicleData/List?SearchString=' + encodeURIComponent(ybsType.text.split(";")[0]) + '&searchOption=ybsType'
                        }).text(ybsType.text.split(";")[0]);

                        nameCell.append(nameLink);
                        tr.append(nameCell);


                        var totalYBSCountCell = $('<td/>', {
                            'class': 'name'
                        }).text(ybsType.text.split(";")[1]);
                        tr.append(totalYBSCountCell);

                        var actionBtnCell = $('<td/>', {
                            'class': 'd-flex justify-content-center align-item-center actionBtn',
                        });

                        var editLink = $('<a/>', {
                            'class': 'editBtn btn btn-outline me-3',
                            'data-toggle': 'tooltip',
                            'data-placement': 'top',
                            'title': 'Edit'
                        }).html('<i class="fa fa-duotone fa-pencil fa-lg" style="color:black;"></i>');

                        var deleteLink = $('<a/>', {
                            'class': 'deleteBtn btn btn-outline',
                            'data-toggle': 'tooltip',
                            'data-placement': 'top',
                            'title': 'Delete'
                        }).html('<i class="fas fa-regular fa-trash fa-lg text-danger"></i>');

                        actionBtnCell.append(editLink);
                        actionBtnCell.append(deleteLink);

                        tr.append(actionBtnCell);

                        lstYBSTypes.append(tr);

                   }
                });
            };
        });
    }

    return;
}


