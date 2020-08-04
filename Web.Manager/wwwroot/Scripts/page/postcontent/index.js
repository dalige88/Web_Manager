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
            html += '<tr>\
                    <td align="center">' + itemData.id + '</td>\
                    <td align="center">' + itemData.msgTitle + '</td>\
                    <td align="center">' + itemData.msgAuthor + '</td>\
                    <td align="center">' + tools.nullToEmptyString(itemData.createTime) + '</td >\
                    <td align="center">' + itemData.platformName + '</td >\
                    <td align="center">' + itemData.subChannelName + '</td>\
                    <td align="center">' + itemData.openStatusName + '</td>\
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




