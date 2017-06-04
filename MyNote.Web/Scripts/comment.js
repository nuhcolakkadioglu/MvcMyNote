var notid = -1;
var modalCommentBodyId = "#modalYorum_body";
$(function () {
    $('#modalYorum').on('show.bs.modal', function (e) {
        var btn = $(e.relatedTarget);
        notid = btn.data("note-id");
        $(modalCommentBodyId).load("/Comment/ShowNoteComments/" + notid);

    })
});

function doComment(btn, e, commentid, spanid) {
    var button = $(btn);
    var mod = button.data("edit-mode");
    if (e === "edi_clicked") {
        if (!mod) {
            button.data("edit-mode", true);
            button.removeClass("btn-warning");
            button.addClass("btn-success");
            button.find("span").removeClass("glyphicon-edit");
            button.find("span").addClass("glyphicon-ok");
            $(spanid).addClass("edittable")
            $(spanid).attr("contenteditable", true);
            $(spanid).focus();
        } else {
            button.data("edit-mode", false);
            button.addClass("btn-warning");
            button.removeClass("btn-success");
            button.find("span").addClass("glyphicon-edit");
            button.find("span").removeClass("glyphicon-ok");
            $(spanid).removeClass("edittable")
            $(spanid).attr("contenteditable", false);
            var txt = $(spanid).text();
            $.ajax({
                method: "POST",
                url: "/Comment/Edit/" + commentid,
                data: { text: txt }
            }).done(function (data) {
                if (data.result) {
                    $(modalCommentBodyId).load("/Comment/ShowNoteComments/" + notid);

                }
                else {
                    alert("Yorum güncellenemedi");
                }
            }).fail(function (data) {
                alert("sunucu bağlantı hatası")
            });

        }

    }

    else if (e === "delete_clicked") {
        var dialogResult = confirm("yorum silinsinmi");
        if (!dialogResult) return false;

        $.ajax({
            method: "GET",
            url: "/Comment/Delete/" + commentid,

        }).done(function (data) {
            if (data.result) {
                $(modalCommentBodyId).load("/Comment/ShowNoteComments/" + notid);

            }
            else {
                alert("yorum silinemedi");
            }
        }).fail(function (data) {
            alert("sunucuya erişelemiyor.");
        });

    }

    else if (e === "new_clicked") {
        {
            var txt = $("#yeni_yorum").val();
            $.ajax({
                method: "POST",
                url: "/Comment/Create/",
                data: { Text: txt, noteid: notid }

            }).done(function (data) {
                if (data.result) {
                    $(modalCommentBodyId).load("/Comment/ShowNoteComments/" + notid);

                }
                else {
                    alert("yorum eklenemedi");
                }
            }).fail(function (data) {
                alert("sunucuya erişelemiyor.");
            });

        }
    }


}