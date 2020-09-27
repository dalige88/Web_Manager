$(function () {
    subchannellist.pageBind();
    subchannellist.loadData();
});

var subchannellist = {
    pageBind: function () {
        $('#searchBtn').click(function () {
            subchannellist.loadData();
        });
    },
    loadData: function () {
        var Param = {};
        Param.SubChannelName = $('#KeyWord').val();
        $("#infoPage").page({
            url: '/Subchannel/Ajax_GetList',
            pageSize: 10,
            searchparam: Param,
            viewCallback: subchannellist.outList
        });
    },
    outList: function (result, j) {
        var html = '';
        $.each(result, function (i) {
            j++;
            var itemData = result[i];
            html += '<tr>\
                    <td align="center">' + itemData.id + '</td>\
                    <td align="center">' + itemData.subChannelName + '</td>\
                    <td align="center">' + itemData.statesName + '</td >\
                    <td align="center">' + itemData.addressURL + '</td >\
                    <td align="center"><a href=\'/JRTTImages/ImagesList?pid=' + itemData.id + '\'>图库管理</a></td>\
                    <td align="center">' + tools.nullToEmptyString(itemData.createTime) + '</td>\
                    <td align="center">|&nbsp;&nbsp;'+ authHelper.createLink('/Subchannel/EditSubchannel', 'id=' + itemData.id) + '&nbsp;&nbsp;|<br />|&nbsp;&nbsp;<a href=\'javascript:subchannellist.delSubchannel("' + itemData.id + '")\'>删除</a>&nbsp;&nbsp;|</td>';

            html += '</td></tr>';
        });
        $('#goodsbody').html(html);
    },
    delSubchannel: function (id) {
        var url = "/Subchannel/Ajax_DelSubchannel";
        var postData = {};
        postData.id = id;
        ajaxHelper.post(url, postData, function (d) {
            msg.success("删除成功！", function () {
                window.location.reload(true);
            });
        });
    }
}




