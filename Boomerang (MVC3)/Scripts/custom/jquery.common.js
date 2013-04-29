

/* INIT 
***************************************************************************************************************/

createButtons();


/* BUTTONS 
***************************************************************************************************************/

$('button').live('mouseenter', function () {
    $(this).addClass('ajax-hover');
}).live('mouseleave', function () {
    $(this).removeClass('ajax-hover');
});

function createButtons() {
    $.each($('a.button'), function (i, item) {
        $(this).html('<span class=\"icon\"></span><span class=\"text\">' + $(this).text() + '</span>');
    });
};


function setSavingText(target, text, reset) {
    var oldText = target.text(); // Store the old text

    target.text(text); // Set our text to the new text

    if (reset) { // If we want to reset, then set our old text back to our new text
        setTimeout(function () {
            setSavingText(target, oldText, false);
        }, 2000);
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
            break;
        case 1:
            $('#edit').show();
            $('#view').show();
            $('#cv').show();
            $('#delete').show();
            $('#up').show();
            $('#down').show();

            if ($('.approved', target) != undefined) {
                if ($('.approved', target).text().toLowerCase() == 'true') {
                    $('#unapprove').show();
                } else {
                    $('#approve').show();
                }
            }
            
            if ($('.lock', target) != undefined) {
                if ($('.lock', target).text().toLowerCase() == 'true') {
                    $('#unlock').show();
                } else {
                    $('#lock').show();
                }
            }
            
            if ($('.active', target) != undefined) {
                if ($('.active', target).text().toLowerCase() == 'true') {
                    $('#in-active').show();
                } else {
                    $('#active').show();
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


/* TABLES 
***************************************************************************************************************/

$('table.default-table tr th span.tick').live('click', function () {
    var checked = $('table.default-table thead tr').hasClass('selected');

    $.each($('table.default-table tbody tr'), function (i, item) {
        if (checked) {
            $(this).removeClass('selected');
            $('table.default-table thead tr').removeClass('selected');
        } else {
            $(this).addClass('selected');
            $('table.default-table thead tr').addClass('selected');
        }
    });
});

$('table.default-table tbody tr').live('mouseenter', function () {
    $(this).addClass('hover');
}).live('mouseleave', function () {
    $(this).removeClass('hover');
});

$('table.default-table tbody tr').live('click', function () {
    var table = $(this).parent('tbody').parent('table');

    if (table.hasClass('single-select')) {  // If the table is only single select
        $('table.default-table tbody tr').removeClass('selected'); // de-select all other elements
        $(this).addClass('selected'); // Select the curret row
    } else if (table.hasClass('no-select')) {
        $('table.default-table tbody tr').removeClass('selected'); // de-select all other elements
    } else {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            $(this).addClass('selected');
        }
    }
});


/* LISTS 
***************************************************************************************************************/

$('ul.default-list > li').live('click', function () {
    var $li = $(this);
    var checked = $li.hasClass('selected');

    if (checked) {
        $li.removeClass('selected');
    } else {
        $li.addClass('selected');
    }
});

$('ul.default-list > li').live('mouseenter', function () {
    $(this).addClass('hover');
}).live('mouseleave', function () {
    $(this).removeClass('hover');
});