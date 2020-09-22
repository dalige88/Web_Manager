
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
        postData.PYScript_Video = $('#PYScript_Video').val();
        postData.PYScript_ShortEssay = $('#PYScript_ShortEssay').val();
        postData.PYScript_LongEssay = $('#PYScript_LongEssay').val();
        postData.PYScript_Comment = $('#PYScript_Comment').val();
        postData.PYScript_PIC = $('#PYScript_PIC').val();
        postData.PlatformID = $('#PlatformID').val();
        postData.SubChannelName = $('#SubChannelName').val();
        postData.AddressURL = $('#AddressURL').val();
        postData.AnalogPacket = $('#AnalogPacket').val();
        postData.UserName = $('#UserName').val();
        postData.UserPwd = $('#UserPwd').val();
        postData.Remark = $('#Remark').val();

        var url = "/Subchannel/Ajax_AddSubchannel";

        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                window.location.href = "/Platforminfo/Index";
            });
        });

    }
};


