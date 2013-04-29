jQuery.fn.sortTable = function (sortSettings) {


    /* DEFAULTS 
    ***************************************************************************************************************/

    var sortDefaults = {
        selectedColumn: null, // The initially selected column
        ignoreColumns: null,
        direction: 'desc',
        onStart: null,
        onComplete: null
    };

    var sortSettings = $.extend(sortDefaults, sortSettings);


    /* GLOBAL VARIABLES 
    ***************************************************************************************************************/

    var table = $(this);


    /* INITIATE
    ***************************************************************************************************************/

    sortTable(sortSettings.direction);


    /* CALLBACK FUNCTIONS
    ***************************************************************************************************************/

    if (typeof sortSettings.onStart == "function") {
        sortSettings.onStart.call(this);
    }

    if (typeof sortSettings.onComplete == "function") {
        sortSettings.onComplete.call(this);
    }


    /* EVENTS
    ***************************************************************************************************************/

    $('thead th', table).click(function () {
        var colNumber = $(this).index();
        
        if (isSortable(colNumber)) {
            if (sortSettings.onStart) {
                sortSettings.onStart();
            }

            var isDesc = $(this).hasClass('desc');
            var isAsc = $(this).hasClass('asc');
            var direction = (isDesc) ? 'asc' : 'desc';

            sortSettings.selectedColumn = colNumber;
            
            if (isDesc === false && isAsc === false) {
                sortTable(sortSettings.direction);
            } else {
                sortTable(direction);
            }
        }
    });


    /* FUNCTIONS
    ***************************************************************************************************************/

    function isSortable(index) {
        if (sortSettings.ignoreColumns != null)
        {
            for (var i = 0; i < sortSettings.ignoreColumns.length; i++) {
                if (sortSettings.ignoreColumns[i] == index) return false;
            }
        }

        return true;
    };

    function sortTable(direction) {
        $('thead th', table).css('cursor', 'pointer');

        if (sortSettings.selectedColumn != null) {
            var selector = ':nth-child(' + (sortSettings.selectedColumn + 1) + ')';

            var listitems = $('tbody', table).children('tr').get();

            listitems.sort(function (a, b) {
                var x = formatValue($(a).find('td' + selector, table).text().toUpperCase());
                var y = formatValue($(b).find('td' + selector, table).text().toUpperCase());

                if (direction == 'asc') {
                    return sortColumn(x, y);
                } else {
                    return sortColumn(x, y) * -1;
                }
            })

            $('tbody tr', table).remove();
            $.each(listitems, function (i, item) {
                $('tbody', table).append(item);
            });

            formatColumn(direction, selector);

            if (sortSettings.onComplete) {
                sortSettings.onComplete();
            }
        }
    };

    function sortColumn(a, b) {
        i = 0;
        if (isNaN(a)) {
            i = a.localeCompare(b);
        } else {
            i = parseInt(a) > parseInt(b) ? 1 : -1;
        }
        return i;
    };

    function formatValue(value) {
        var a;
        if (isNaN(value)) {
            if (isNaN(Date.parse(value))) {
                a = value;
            } else {
                a = Date.parse(value);
            }
        } else {
            a = value;
        }
        return a;
    };

    function parseDate(input) {
        var parts = input.match(/(\d+)/g);
        // new Date(year, month [, date [, hours[, minutes[, seconds[, ms]]]]])
        return new Date(parts[2], parts[1], parts[0]); // months are 0-based
    }

    function formatColumn(direction, selector) {
        // Remove classes and arrows from the entire table
        $('.arrow', table).remove();
        $('th', table)
            .removeClass('desc')
            .removeClass('asc')
            .removeClass('selected-column');

        $('td', table).removeClass('selected-column');

        // Add classes and an arrow to the current column
        $('th' + selector, table)
            .addClass('selected-column')
            .addClass(direction)
            .append('<span class="arrow" />');

        $('td' + selector, table).addClass('selected-column');
    };
};