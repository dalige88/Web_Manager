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
            url: '/VideoYouTuBe/Ajax_GetList',
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

            var downloadstate = itemData.downloadstate;
            //<td align="center">' + tools.nullToEmptyString(itemData.createTime) + '</td >\
            html += '<tr>\
                    <td align="center">' + itemData.id + '</td>\
                    <td align="center">' + itemData.ywTitle + '</td>\
                    <td align="center">' + itemData.zwTitle + '</td>\
                    <td align="center">' + itemData.downloadStateName + '</td>\
                    <td align="center">' + itemData.postStateName + '</td>\
                    <td align="center" style="min-width:200px;">|&nbsp;&nbsp;'+ authHelper.createLink('/VideoYouTuBe/EditYouTuBe', 'id=' + itemData.id) + '&nbsp;&nbsp;|&nbsp;&nbsp;<a href=\'javascript:postcontentlist.delVideoyoutube("' + itemData.id + '")\'>删除</a>&nbsp;&nbsp;|</td>';

            html += '</td></tr>';
        });
        $('#goodsbody').html(html);
    },
    delVideoyoutube: function (id) {
        var url = "/VideoYouTuBe/Ajax_DelVideoyoutube";
        var postData = {};
        postData.id = id;
        ajaxHelper.post(url, postData, function (d) {
            msg.success("删除成功！", function () {
                window.location.reload(true);
            });
        });
    }
}




