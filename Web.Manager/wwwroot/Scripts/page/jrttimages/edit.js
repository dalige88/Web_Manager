
$(function () {
    JRTTImages.pageBind();

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

        var postData = {};
        postData.Id = $('#id').val();
        postData.Pid = $('#pid').val();
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
    }
};




