$(function () {
    var accountid = myfuns.GetQueryInt('accountid');
    accountedit.loadInfo(accountid);
    accountedit.pageBind();

});

var accountedit = {
    pageBind: function () {
        $('#btn_save').click(function () {
            accountedit.saveInfo();
        });
        $('#btn_cancel').click(function () {
            history.go(-1);
        });
    },
    loadInfo: function (accountid) {
        var postData = {
            accountid: accountid
        };
        ajaxHelper.post('/WebSystem/AccountLoadInfo', postData, function (d) {
            $('#ManagerId').val(d.managerId);
            $('#ManagerEmail').val(d.managerEmail);
            $('#ManagerRealname').val(d.managerRealname);

            $.each($('.ManagerStatus'), function (i) {
                if ($('.ManagerStatus').eq(i).val() == d.managerStatus) {
                    $('.ManagerStatus').eq(i).prop('checked', true);
                }
            });

            $.each($('.IsSupper'), function (i) {
                if ($('.IsSupper').eq(i).val() == d.isSupper) {
                    $('.IsSupper').eq(i).prop('checked', true);
                }
            });

            $('#ManagerTel').val(d.managerTel);
            $('#ManagerName').val(d.managerName);
            if (d.managerId > 0) {
                $('#ManagerName').attr('disabled', 'disabled');
            }
            $('#Password').val(d.password);
            $('#RePassword').val(d.rePassword);
            var roleHtml = '';
            $.each(d.roles, function (i) {
                var curRole = d.roles[i];
                roleHtml += '<label><input type="checkbox" class="roleitem" ' + (curRole.checked == 1 ? ' checked="checked"' : '') + ' value="' + curRole.roleID + '"/> ' + curRole.roleName + '</label>';
            });
            $('#div_roles').html(roleHtml);
        });
    },
    saveInfo: function () {
        var postData = {};
        postData.ManagerId = $('#ManagerId').val();
        postData.ManagerName = $('#ManagerName').val();
        postData.ManagerRealname = $('#ManagerRealname').val();
        postData.ManagerEmail = $('#ManagerEmail').val();
        postData.ManagerTel = $('#ManagerTel').val();
        var url = $("#btn_save").data('url');
        postData.ManagerStatus = 0;
        if ($('.ManagerStatus:checked').length > 0) {
            postData.ManagerStatus = $('.ManagerStatus:checked').eq(0).val();
        }
        postData.IsSupper = 0;
        if ($('.IsSupper:checked').length > 0) {
            postData.IsSupper = $('.IsSupper:checked').eq(0).val();
        }
        $.each($('.roleitem:checked'), function (i) {
            postData['Roles[' + i + '].RoleID'] = $('.roleitem:checked').eq(i).val();
        });
        ajaxHelper.post(url, postData, function (d) {
            msg.success('账号保存成功！', function () {
                history.go(-1);
            });
        });
    }
};