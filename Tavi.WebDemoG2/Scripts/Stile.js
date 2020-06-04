$(document).ready(function () {
    /*=============== check box ========*/
    $('.grid #cbxList').click(function () {
        checkAll_OnClick('cbxList', 'cbxItem');
    });

        function alert() {
            $('#side-menu').metisMenu();
            $('#AlertBox').removeClass('hide');
            $('#AlertBox').delay(1000).slideUp(500);
        };
 
    function checkAll_OnClick(headerCheckboxId, remainingCheckboxesName) {
        var checked = $("#" + headerCheckboxId).prop('checked');
        if (checked == true) {
            $("input[type='checkbox']").prop("checked", true);

        } else {
            $("input[type='checkbox']").prop("checked", false);

        }
    }

    $('.grid #cbxItem').click(function () {
        checkItem_OnClick('cbxList', 'cbxItem');
    });
    function checkItem_OnClick(headerCheckboxId, remainingCheckboxesName) {

    }
    function GetListId(name) {
        var checked = $("input:checked[name=" + name + "]");
        var id = '';
        jQuery.each($("input:checked[name=" + name + "]"), function () {

            if (id == '')
                id = $(this).val();
            else
                id += ',' + $(this).val();
        });
        return id;
    };
    /*=============== end check box ========*/
    if ($.validator) {
        jQuery.extend(jQuery.validator.messages, {
            required: "Trường thông tin bắt buộc.",
            remote: "Dữ liệu nhập vào đã tồn tại.",
            email: "Địa chỉ email không hợp lệ.",
            url: "Không đúng định dạng url.",
            date: "Ngày nhập không hợp lệ.",
            number: "Không đúng định dạng số.",
            digits: "Dữ liệu phải là kiểu số",
            equalTo: "Dữ liệu xác nhận không chính xác.",
            accept: "Dữ liệu nhập vào không đúng định dạng.",
            maxlength: $.validator.format("Phải nhập không quá {0} ký tự."),
            minlength: $.validator.format("Phải nhập ít nhất {0} ký tự."),
            rangelength: $.validator.format("Phải nhập từ {0} đến {1} ký tự."),
            range: $.validator.format("Phải nhập giá trị từ {0} đến {1}."),
            max: $.validator.format("Phải nhập giá trị lớn nhất là {0}."),
            min: $.validator.format("Phải nhập giá trị nhỏ nhất là {0}.")
        });
    }
    alert();
});