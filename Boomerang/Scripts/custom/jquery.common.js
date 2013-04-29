

/* INIT 
***************************************************************************************************************/

createButtons();


/* BUTTONS 
***************************************************************************************************************/

$('button, .fileinput-button').bind('mouseenter', function () {
    $(this).addClass('ajax-hover');
}).bind('mouseleave', function () {
    $(this).removeClass('ajax-hover');
});

function createButtons() {
    $.each($('a.button'), function (i, item) {
        $(this).html('<span class=\"icon\"></span><span class=\"text\">' + $(this).text() + '</span>');
    });
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


/* FUNCTIONS 
***************************************************************************************************************/