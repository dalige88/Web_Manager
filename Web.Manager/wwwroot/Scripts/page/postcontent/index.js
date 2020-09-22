$(function () {
    postcontentlist.pageBind();
    postcontentlist.loadData();
});

var postcontentlist = {
    pageBind: function () {
        $('#searchBtn').click(function () {
            postcontentlist.loadData();
        });
    },
    loadData: function () {
        var Param = {};
        Param.SubChannelName = $('#KeyWord').val();
        $("#infoPage").page({
            url: '/PostContent/Ajax_GetList',
            pageSize: 10,
            searchparam: Param,
            viewCallback: postcontentlist.outList
        });
    },
    outList: function (result, j) {
        var html = '';
        $.each(result, function (i) {
            j++;
            var itemData = result[i];
            var StatusName = "";
            if (itemData.openStatusName =="未发布") {
                StatusName = "|&nbsp;&nbsp;<a href=\"#\" style=\"color:red;\">立即发布</a>&nbsp;&nbsp;|";
            } else {
                StatusName = "|&nbsp;&nbsp;<span style=\"color:green;\">" + itemData.openStatusName + "</span>&nbsp;&nbsp;|";
            }
            html += '<tr>\
                    <td align="center">' + itemData.id + '</td>\
                    <td align="center">' + itemData.msgTitle + '</td>\
                    <td align="center">' + itemData.msgAuthor + '</td>\
                    <td align="center">' + tools.nullToEmptyString(itemData.createTime) + '</td >\
                    <td align="center">' + StatusName + '</td>\
                    <td align="center">' + itemData.createTypeName + '</td>\
                    <td align="center">|&nbsp;&nbsp;'+ authHelper.createLink('/PostContent/EditPostContent', 'id=' + itemData.id) + '&nbsp;&nbsp;|<br />|&nbsp;&nbsp;<a href=\'javascript:postcontentlist.delPostContent("' + itemData.id + '")\'>删除</a>&nbsp;&nbsp;|</td>';

            html += '</td></tr>';
        });
        $('#goodsbody').html(html);
    },
    delPostContent: function (id) {
        var url = "/PostContent/Ajax_DelPostcontent";
        var postData = {};
        postData.id = id;
        ajaxHelper.post(url, postData, function (d) {
            msg.success("删除成功！", function () {
                window.location.reload(true);
            });
        });
    }
}




