
$(function () {
    JRTTImages.pageBind();
    JRTTImages.loadSubData();

    var images_html = '';
    var images = $('#images').val().split(',');
    for (var i = 0; i < images.length; i++) {

        var im = "/" + images[i].substring(images[i].lastIndexOf("upload"), images[i].length);
        images_html += "<div class=\"row rowmb15\"><div class=\"col-md-12\"><label class=\"col-sm-2 edit-group-label\"><span class=\"spanrequired\">*</span>图片" + (i + 1) + "：</label><div class=\"col-md-7\" ><img class=\"showimg\" src=\"" + im + "\" style=\"width:60%\"></div></div></div>";

    }
    $('#showui_li').html(images_html);
});

var JRTTImages = {
    pageBind: function () {

        $('#btn_save').click(function () {
            JRTTImages.saveInfo();
        });
       
    },

    saveInfo: function () {
        var pics = "";
        var pic_url = document.getElementsByName("pic_url");
        for (var i = 0; i < pic_url.length; i++) {
            
            if (i < pic_url.length-1 ) {
                pics += pic_url[i].value + ",";
            } else {
                pics += pic_url[i].value;
            }
        }
        //alert(pics);
        //选中的渠道信息
        var qd = "";
        $('input[class="qudao"]:checked').each(function () {
            qd += $(this).val() + ',';
        });
        //将字符串中最后一个元素","逗号去掉，
        qd = qd.substring(0, qd.lastIndexOf(','));
        

        var postData = {};
        postData.Id = $('#id').val();
        postData.Pid = qd;
        postData.Content = $('#content').val(); 
        postData.Images = pics;

        var url = "/JRTTImages/Ajax_EditWTT";

        ajaxHelper.post(url, postData, function (d) {
            msg.success('操作成功！', function () {
                history.go(-1);
            });
        });

    },
    delepic: function (picid) {
        var rowmb15_img_ = "rowmb15_img_" + picid;
        var rowmb15_fwqdz_ = "rowmb15_fwqdz_" + picid;
        var box = document.getElementById(rowmb15_img_);
        box.remove();
        var box1 = document.getElementById(rowmb15_fwqdz_);
        box1.remove();
    },
    calculation: function ()
    {
        var nMax = 2000;
        var textDom = document.getElementById("content");
        var len = textDom.value.length;
        if (len > nMax) {
            textDom.value = textDom.value.substring(0, nMax);
            return;
        }
        document.getElementById("messages").innerText = "你还可以输入" + (nMax - len) + "个字";
    },


    loadSubData: function () {
        var postData = {};
        postData.types = 1;
        var url = "/Subchannel/Ajax_GetAllList";
        ajaxHelper.post(url, postData, function (d) {
            console.log(d);
            if (d.length > 0) {
                JRTTImages.loadSubHtml(d);
            } else {
                //$('#MenuSub').html('<option value="">--请选择渠道--</option>');
            }
        });
    },

    loadSubHtml: function (result) {
        var htmls = '';
        var Pid = $('#pid').val();

       var pids = Pid.split(',');
        $.each(result, function (i) {
            var itemData = result[i];
            var html = "";
            for (var j = 0; j < pids.length; j++) {
                if (pids[j] == itemData.id) {
                    html += '<label style=\"font-weight:bold;\"><input type=\"checkbox\" value=\"' + itemData.id + '\" class=\"qudao\" checked />' + itemData.subChannelName + '</label>&nbsp;&nbsp;&nbsp;&nbsp;';
                } 
            }

            if (html=="") {
                html = '<label style=\"font-weight:bold;\"><input type=\"checkbox\" value=\"' + itemData.id + '\" class=\"qudao\" />' + itemData.subChannelName + '</label>&nbsp;&nbsp;&nbsp;&nbsp;';
            }
           
            htmls += html;


        });
        $('#qudao').html(htmls);
    },
};




