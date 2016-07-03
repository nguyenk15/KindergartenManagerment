$(function () {
    //Datemask dd/mm/yyyy
    var date = new Date();
    var d = date.getDate(),
    y = date.getFullYear();
    $(".datemask").inputmask({
        alias: "dd/mm/yyyy",
        yearrange: { minyear: y - 100, maxyear: y }
    });
    $('.inputmask').inputmask({
        mask: '9999-999-999'
    })
});