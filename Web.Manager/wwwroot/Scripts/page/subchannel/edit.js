
$(function () {
    Subchannel.pageBind();
});

var Subchannel = {
    pageBind: function () {

        $('#btn_save').click(function () {
            Subchannel.saveInfo();
        });
       
    },

    saveInfo: function () {
        var postData = {};
        
        postData.ID = $('#ID').val();
        postData.SubChannelName = $('#SubChannelName').val();
        postData.AddressURL = $('#AddressURL').val();
        postData.UserName = $('#UserName').val();
        postData.UserPwd = $('#UserPwd').val();
        postData.Remark = $('#Remark').val();
        postData.AnalogPacket = $('#AnalogPacket').val();

        var url = "/Subchannel/Ajax_editSubchannel";

        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                window.location.href = "/Subchannel/Index";
            });
        });

    }
};


