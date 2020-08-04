
//实例化编辑器
var um = UM.getEditor('myEditor');
$(function () {
    
    PostContent.createEditor();
    //PostContent.loadPlat();
    PostContent.pageBind();
    //PostContent.insertHtml($('#msgcontent').val());
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
        postData.PlatformID = $('#MenuPid').val();
        postData.SubChannelID = $('#MenuSub').val();
        if ($('.RoleStatus:checked').length > 0) {
            postData.MsgType = $('.RoleStatus:checked').eq(0).val();
        }

        var url = "/PostContent/Ajax_AddPostcontent";

        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                window.location.href = "/PostContent/Index";
            });
        });

    },

    loadPlat: function () {

        var Pid = $('#MenuPid').val();

        var postData = {};
        postData.pid = Pid;

        var url = "/Subchannel/Ajax_GetAllList";

        ajaxHelper.post(url, postData, function (d) {
            console.log(d);
            if (d.length > 0) {
                PostContent.loadSub(d);
            } else {
                $('#MenuSub').html('<option value="">--请选择渠道--</option>');
            }
        });



    },

    loadSub: function (result) {
        var html = '';
        $.each(result, function (i) {
            var itemData = result[i];
            html += '<option value=' + itemData.id + '> ' + itemData.subChannelName + ' </option>';
        });
        $('#MenuSub').html(html);

    },

    //按钮的操作
    insertHtml: function (value) {
        um.execCommand('insertHtml', value)
    }
};


