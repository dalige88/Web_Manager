
//实例化编辑器
var um = UM.getEditor('myEditor');
$(function () {
    
    PostContent.createEditor();
    PostContent.loadPlaData();
    PostContent.pageBind();
    //PostContent.insertHtml($('#msgcontent').val());
});


var PostContent = {
    pageBind: function () {
        
        $('#btn_save').click(function () {
            PostContent.saveInfo();
        });
        $('#btn_post_jrtt').click(function () {
            PostContent.postJRTT();
        });

    }, createEditor: function () {
        um = UM.getEditor('myEditor');
    },

    saveInfo: function () {
        
        var postData = {};
        postData.ID = $('#id').val();
        postData.MsgTitle = $('#MsgTitle').val();
        postData.MsgAuthor = $('#MsgAuthor').val();
        postData.MsgContent = um.getContent();
        postData.MsgType = 0;
        postData.PlatformID = $('#MenuPid').val();
        postData.SubChannelID = $('#MenuSub').val();
        if ($('.RoleStatus:checked').length > 0) {
            postData.MsgType = $('.RoleStatus:checked').eq(0).val();
        }

        postData.OpenStatus = 0;
        if ($('.OpenStatus:checked').length > 0) {
            postData.OpenStatus = $('.OpenStatus:checked').eq(0).val();
        }


        var url = "/PostContent/Ajax_EditPostcontent";

        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                window.location.href = "/PostContent/Index";
            });
        });

    },



    loadPlaData: function () {
        var postData = {};
        var url = "/PostContent/Ajax_GetAllList";
        ajaxHelper.post(url, postData, function (d) {
            if (d.length > 0) {
                PostContent.loadPlaHtml(d);
            } else {
                $('#MenuPid').html('<option value="">--请选择渠道--</option>');
            }
        });
    },
    loadPlaHtml: function (result) {
        var Pid = $('#pid').val();
        PostContent.loadSubData(Pid);
        var html = '';
        $.each(result, function (i) {
            var itemData = result[i];
            if (Pid == itemData.id) {
                html += '<option value=' + itemData.id + ' selected=\'selected\'> ' + itemData.platformName + ' </option>';
            } else {
                html += '<option value=' + itemData.id + '> ' + itemData.platformName + ' </option>';
            }
            
        });
        $('#MenuPid').html(html);
    },





    loadSubData: function (Pid) {
        var postData = {};
        postData.pid = Pid;
        var url = "/Subchannel/Ajax_GetAllList";
        ajaxHelper.post(url, postData, function (d) {
            if (d.length > 0) {
                PostContent.loadSubHtml(d);
            } else {
                $('#MenuSub').html('<option value="">--请选择渠道--</option>');
            }
        });
    },

    loadSubHtml: function (result) {
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
    },

    postJRTT: function () {
        var postData = {}; 
        postData.id = $('#id').val();
        postData.PYScript = $('#PYScript').val();
        var url = "/PostContent/Ajax_PostJRTTWenZhang";
        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                window.location.href = "/PostContent/Index";
            });
        });





    },
};


