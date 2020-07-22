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
                    <td align="center">' + itemData.platformID + '</td>\
                    <td align="center">' + itemData.subChannelName + '</td>\
                    <td align="center">' + itemData.addressURL + '</td >\
                    <td align="center">' + itemData.states + '</td >\
                    <td align="center">' + itemData.userNameData + '</td >\
                    <td align="center">' + tools.nullToEmptyString(itemData.createTime) + '</td>\
                    <td align="center">' + itemData.remark + '</td>\
                    <td align="center"></td>';

            html += '</td></tr>';
        });
        $('#goodsbody').html(html);
    },
    delPlatform: function (Id) {
        var url = "/Platforminfo/Ajax_DelPlatforminfo";
        var postData = {};
        postData.Id = Id;
        ajaxHelper.post(url, postData, function (d) {
            msg.success("删除成功！", function () {
                window.location.reload(true);
            });
        });
    }
}




