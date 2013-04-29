jQuery.fn.filterTable = function (filterSettings) {


    /* DEFAULTS 
    ***************************************************************************************************************/

    var filterDefaults = {
        filterInput: null,
        filterClose: 'filter-close',
        ignoreColumns: null,
        onStart: null,
        onComplete: null,
        rows: []
    };

    var filterSettings = $.extend(filterDefaults, filterSettings);


    /* GLOBAL VARIABLES 
    ***************************************************************************************************************/

    var table = $(this);


    /* INIT
    ***************************************************************************************************************/

    startCallback();
    $(filterSettings.filterInput).css('position', 'relative');
    completeCallback();

    /* EVENTS
    ***************************************************************************************************************/

    $(filterSettings.filterInput).keyup(function () {
        var o = $(this);

        if (o.val().length > 2) {
            if ($('#' + filterSettings.filterClose).length <= 0) {
                o.append('<div id="' + filterSettings.filterClose + '" />'); // Add our closing tag
            }

            applyFilter(o.val());
        } else if (o.val().length == 0) {
            removeFilter();
        }
    });


    /* FUNCTIONS
    ***************************************************************************************************************/

    function startCallback() {
        if (typeof filterSettings.onStart == 'function') {
            filterSettings.onStart.call(this);
        }
    };

    function completeCallback() {
        if (typeof filterSettings.onComplete == 'function') {
            filterSettings.onComplete.call(this);
        }
    };

    function applyFilter(text) {
        $.each($('tbody > tr', table), function (i, row) {
            var bFound = false;
            
            $.each($('td', row), function (n, cell) {
                if ($(this).text().toLowerCase().indexOf(text.toLowerCase()) > -1) {
                    bFound = true;
                }
            });

            if (!bFound) {
                filterSettings.rows.push($(this));
                $(this).remove();
            }

            //console.log(filterSettings.rows);
        });

        completeCallback();
    };

    function removeFilter() {
        $.each(filterSettings.rows, function (i, item) {
            table.append(item); // Append our rows back to our table
        });

        $(filterSettings.filterInput).val(''); // Reset our input
        filterSettings.rows = []; // Recreate our blank array

        completeCallback(); // Call our complete callback function
    };
};