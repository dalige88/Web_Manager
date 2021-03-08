
$(function () {
    JRTTImages.pageBind();


});

var JRTTImages = {
    pageBind: function () {

        $('#btn_save').click(function () {
            JRTTImages.saveInfo();
        });

    },

    saveInfo: function () {

        var postData = {};
        postData.ID = $('#id').val()
        postData.yw_title = $('#yw_title').val();
        postData.zw_title = $('#zw_title').val();
        postData.downloadurls = $('#downloadurls').val();

        if ($('.downloadstate:checked').length > 0) {
            postData.downloadstate = $('.downloadstate:checked').eq(0).val();
        }
        if ($('.Poststate:checked').length > 0) {
            postData.poststate = $('.Poststate:checked').eq(0).val();
        }

        var url = "/VideoYouTuBe/Ajax_EditVideoyoutube";

        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                history.go(-1);
            });
        });

    }

}


