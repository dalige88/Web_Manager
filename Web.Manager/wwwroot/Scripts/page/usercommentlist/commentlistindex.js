$(function () {
    CommentList.pageBind();
    CommentList.loadData();
});

var CommentList = {
    pageBind: function () {
        $('#searchBtn').click(function () {
            CommentList.loadData();
        });
    },
    loadData: function () {
        var Param = {};
        Param.UserCommentTargetInfoID = $('#tid').val();
        url = '/UserCommentList/Ajax_GetUserCommentList';


        $("#infoPage").page({
            url: url,
            pageSize: 10,
            searchparam: Param,
            viewCallback: CommentList.outList
        });


    },
    outList: function (result, j) {
        var html = '';
        $.each(result, function (i) {
            j++;
            var itemData = result[i];
            html += '<tr>\
                    <td align="center">' + itemData.id + '</td>\
                    <td align="center">' + itemData.commentTargetTitle + '</td>\
                    <td align="center">' + itemData.userAccount + '</td>\
                    <td align="center">' + itemData.userNice + '</td>\
                    <td align="center">' + itemData.commentContent + '</td>\
                    <td align="center">' + itemData.commentTime + '</td>\
                    <td align="center">|&nbsp;&nbsp;回复>>&nbsp;&nbsp;|</td >';

            html += '</td></tr>';
        });
        $('#goodsbody').html(html);
    },

}




