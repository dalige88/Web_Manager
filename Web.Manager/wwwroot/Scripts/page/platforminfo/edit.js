
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
        postData.ID = $('#ID').val();
        postData.PlatformName = $('#PlatformName').val();
        postData.AddressURL = $('#AddressUrl').val();
        postData.Remark = $('#Remark').val();

        var url = "/Platforminfo/Ajax_EditPlatforminfo";

        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                history.go(-1);
            });
        });

    }
};


