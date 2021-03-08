
$(function () {
    JRTTImages.pageBind();
    JRTTImages.loadSubData();


});

var JRTTImages = {
    pageBind: function () {

        $('#btn_save').click(function () {
            JRTTImages.saveInfo();
        });

    },

    saveInfo: function () {

        var postData = {};
        postData.yw_title = $('#yw_title').val();
        postData.zw_title = $('#zw_title').val();
        postData.downloadurls = $('#downloadurls').val();

        var url = "/VideoYouTuBe/Ajax_AddVideoyoutube";

        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                history.go(-1);
            });
        });

    }

}


