
//实例化编辑器
var um = UM.getEditor('myEditor');
$(function () {

    PostContent.createEditor();
    PostContent.pageBind();
});


var PostContent = {
    pageBind: function () {

        $('#btn_save').click(function () {
            PostContent.saveInfo();
        });

    }, createEditor: function () {
        um = UM.getEditor('myEditor');
    },

    saveInfo: function () {
        var postData = {};
        postData.MsgTitle = $('#MsgTitle').val();
        postData.MsgAuthor = $('#MsgAuthor').val();
        postData.MsgContent = um.getContent();
        postData.MsgType = 0;
        if ($('.RoleStatus:checked').length > 0) {
            postData.MsgType = $('.RoleStatus:checked').eq(0).val();
        }

        //alert(postData.MsgContent);
        var url = "/PostContent/Ajax_AddPostcontent";

        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                window.location.href = "/PostContent/Index";
            });
        });

    }
};


