var timer, a, p, s;

$(function () {
    positionActionPanel();
});

$(window).resize(function () {
    clearTimeout(timer);
    timer = setTimeout(positionActionPanel, 500);
});

$(window).scroll(function () {
    clearTimeout(timer);
    timer = setTimeout(positionActionPanel, 500);
});

function positionActionPanel() {
    a = $('#action-panel');

    if (a.length > 0) {
        h = $(window).height();
        p = (h / 2) - (a.height() / 2);
        s = $(window).scrollTop();
        //console.log('height: ' + h + 'scroll: ' + s + 'position: ' + p);

        $(a).animate({
            top: s + p + 'px'
        }, 1000);
    }
};

function displayButtons(target) {
    var n = 0;

    $.each($('.selected', target), function (i, index) {
        n++;
    });

    switch (n) {
        case 0:
            $.each($('.button', '.actions'), function () {
                $(this).hide();
            });
            $('#create').show();
            $('#export').show();
            break;
        case 1:
            $('#edit').show();
            $('#view').show();
            $('#cv').show();
            $('#delete').show();
            $('#up').show();
            $('#down').show();

            $status = $('.selected .status', target);

            if ($status.find('span').length > 0) {
                _approved = $('.selected .status .approved', target);
                _locked = $('.selected .status .locked', target);

                console.log(_approved.length);

                if (_approved.length > 0) {
                    $('#deactivate').show();
                } else {
                    $('#activate').show();
                }

                if (_locked.length > 0) {
                    $('#unlock').show();
                }
            } else {
                var $checkBox = $status.find('input[type=checkbox]');

                if ($checkBox.length > 0) {
                    if ($checkBox.is(':checked')) {
                        $('#deactivate').show();
                    } else {
                        $('#activate').show();
                    }
                }
            }
            break;
        default:
            $('.button').hide();
            $('#create').show();
            $('#delete').show();
            break;
    }

    return n;
};