$(function () {
    accountlist.pageBind();
    accountlist.loadData();
});

var accountlist = {
    pageBind: function () {
        $('#searchBtn').click(function () {
            accountlist.loadData();
        });
    },
    loadData: function () {
        var Param = {};
        Param.UserName = $('#KeyWord').val();
        $("#infoPage").page({
            url: '/WebSysLog/GetLogList',
            pageSize: 20,
            searchparam: Param,
            viewCallback: accountlist.outList
        });
    },
    outList: function (result, j) {
        var html = '';
        $.each(result, function (i) {
            j++;
            var itemData = result[i];
            html += '<tr>\
                    <td align="center">' + itemData.id + '</td>\
                    <td align="center"><div  title="' + itemData.logName + '">' + tools.nullToEmptyString(itemData.logName) + '</div></td>\
                    <td align="center"><div  title="' + itemData.mapMethod + '">' + tools.nullToEmptyString(itemData.mapMethod) + '</div></td>\
                    <td align="center"><div onclick="msg.alert($(this).html())" class="autocut" title="' + itemData.logContent + '">' + tools.nullToEmptyString(itemData.logContent) + '</div></td>\
                    <td align="center"><div onclick="msg.alert($(this).html())"  title=\'' + itemData.managerGuid + '\'>' + tools.nullToEmptyString(itemData.managerGuid) + '</div></td>\
                    <td align="center"><div onclick="msg.alert($(this).html())"  title=\'' + itemData.managerAccount + '\'>' + tools.nullToEmptyString(itemData.managerAccount) + '</div></td>\
                    <td align="center">' + tools.formatTime(itemData.logTime) + '</td>\
                    <td align="center">' + tools.nullToEmptyString(itemData.logIp) + '</td>\
                </tr>';
        });
        $('#accountbody').html(html);
    }
}