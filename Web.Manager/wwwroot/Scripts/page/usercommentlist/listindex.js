$(function () {
    UserCommentList.pageBind();
    UserCommentList.loadData();
});

var UserCommentList = {
    pageBind: function () {
        $('#searchBtn').click(function () {
            UserCommentList.loadData();
        });
    },
    loadData: function () {
        var Param = {};
        Param.CommentTargetTitle = $('#KeyWord').val();
        //Param.Status = $('#Status').val();
        url = '/UserCommentList/Ajax_GetList';


        $("#infoPage").page({
            url: url,
            pageSize: 10,
            searchparam: Param,
            viewCallback: UserCommentList.outList
        });


    },
    outList: function (result, j) {
        var html = '';
        $.each(result, function (i) {
            j++;
            var itemData = result[i];
            html += '<tr>\
                    <td align="center">' + itemData.id + '</td>\
                    <td align="center">' + itemData.platformName + '</td>\
                    <td align="center">' + itemData.commentTypeName + '</td>\
                    <td align="center"><a href=\"' + itemData.soureUrl + '\" target=\"_blank\">' + itemData.commentTargetTitle + '</a></td >\
                    <td align="center"></td>';

            html += '</td></tr>';
        });
        $('#goodsbody').html(html);
    },

}




