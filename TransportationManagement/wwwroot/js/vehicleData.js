document.addEventListener('DOMContentLoaded', function () {
    var fileInput = document.getElementById('fileInput');
    var uploadBtn = document.getElementById('uploadBtn');

    if (fileInput && uploadBtn) {
        fileInput.addEventListener('change', function () {
            if (this.files && this.files.length > 0) {
                uploadBtn.style.display = 'inline-block';
            } else {
                uploadBtn.style.display = 'none';
            }
        });
    } else {
        console.error('fileInput or uploadBtn element not found.');
    }
});

function FillYBSTypesByYBSCompany(lstYBSCompanyCtrl, lstYBSTypeId) {

    var lstYBSTypes = $("#" + lstYBSTypeId);
    lstYBSTypes.empty();

    var selectedYBSCompany = lstYBSCompanyCtrl.options[lstYBSCompanyCtrl.selectedIndex].value;

    if (selectedYBSCompany != null && selectedYBSCompany != '') {
        $.getJSON("/TransportationManagement/VehicleData/GetYBSTypeByYBSCompanyId", { ybsCompanyId: selectedYBSCompany }, function (ybsTypes) {
            if (ybsTypes != null && !jQuery.isEmptyObject(ybsTypes)) {
                $.each(ybsTypes, function (index, ybsType) {
                    lstYBSTypes.append($('<option/>',
                        {
                            value: ybsType.value,
                            text: ybsType.text
                        }));
                });
            };
        });
    }

    return;
}


$(document).ready(function () {

    var alertBox = $(".alert");

    if (alertBox.length > 0) {
        setTimeout(function () {
            alertBox.fadeOut(500);
        }, 3000);
    }
});

$(document).ready(function () {
    console.log("dorument .ready")
    $('#createBtn').click(function () {
        console.log("here create btn click")
        $('#edit').hide();
        $('#delete').hide();
        $('#create').show();
        var selectedValue = $('#lstYBSTypeId').val();
        if (selectedValue != null && selectedValue !== '') {
            $('.companyPkId').val(selectedValue);
        } 
        $('#nameCreate').val('');
        $('#nameCreate').focus();
    });
});

$(document).ready(function () {
    $(document).on('click', '.editBtn', function () {
        console.log("here edit btn click");

        $('#create').hide();
        $('#delete').hide();
        $('#edit').show();
        var selectedValue = $('#lstYBSTypeId').val();
        if (selectedValue != null && selectedValue !== '') {
            $('.companyPkId').val(selectedValue);
        } 
        var name = $(this).closest('tr').find('.name').text().trim();
        var pkId = $(this).closest('tr').find('.pkId').children(".Pkid").val();
        $('#editPkId').val(pkId);
        $('#nameEdit').val(name);
    });
});

$(document).ready(function () {
    $(document).on('click', '.deleteBtn', function () {
        console.log("here delete btn click");

        $('#create').hide();
        $('#edit').hide();
        $('#delete').show();
        var selectedValue = $('#lstYBSTypeId').val();
        if (selectedValue != null && selectedValue !== '') {
            $('.companyPkId').val(selectedValue);
        } 
        var name = $(this).closest('tr').find('.name').text();
        var pkId = $(this).closest('tr').find('.pkId').children(".Pkid").val();
        $('#nameDelete').text(name);
        $('#deletePkId').val(pkId);
    });
});

function hideForm(elementId) {
    $('#' + elementId).hide();
}

$(document).ready(function () {
    $('#lstYBSTypeId').change(function () {

        $('#create').hide();
        $('#edit').hide();
        $('#delete').hide();
        $('#pagin').hide();
        var selectedIndex = $(this).prop('selectedIndex');

        if (selectedIndex !== 0) {
            $('.actionBtn').removeClass('hideImportant');
            $('.actionBtn').show();
            $('#createBtn').show();
        } else {
            $('.actionBtn').hide();
            $('.actionBtn').addClass('hideImportant');
            $('#createBtn').hide();
        }
    });
});
