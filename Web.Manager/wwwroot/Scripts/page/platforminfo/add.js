
$(function () {
    goods.pageBind();
});

var goods = {
    pageBind: function () {

        $('#btn_save').click(function () {
            goods.saveInfo();
        });
       
    },

    saveInfo: function () {
        var postData = {};
        postData.PlatformName = $('#PlatformName').val();
        postData.AddressURL = $('#AddressURL').val();
        postData.Remark = $('#Remark').val();

        var url = "/Platforminfo/Ajax_AddPlatforminfo";

        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                history.go(-1);
            });
        });

    }
};


