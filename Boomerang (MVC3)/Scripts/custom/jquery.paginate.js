jQuery.fn.paginate = function (pageSettings) {


    /* DEFAULTS 
    ***************************************************************************************************************/

    var pageDefaults = {
        collection: 'article', // The target elements to paginate
        container: '.assets > .row', // The overall container which they sit in
        dataelement: null, // optional parameter that is a sub container (default null)
        pagestart: 0, // Page number to start at
        interval: 0, // number of items in the sub container
        recordsperpage: null // Records per page
    };

    var pageSettings = $.extend(pageDefaults, pageSettings);
    
    var records = $(pageSettings.collection).length; // Get our collection length
    var elements = $(pageSettings.dataelement).length; // Get our dataelement length
    var elementsperpage = 0;
    var pageCount = Math.ceil(records / pageSettings.recordsperpage);
    var currentPage = pageSettings.pagestart;


    /* GLOBAL VARIABLES 
    ***************************************************************************************************************/

    var element = this;


    /* INITIATE
    ***************************************************************************************************************/

    paginate();

    $('.button', element).live('click', function () {
        navigate(Number($(this).attr('id')) - 1);
    });

    $('.button', element).live('mouseenter', function () {
        $(this).addClass('hover');
    }).live('mouseleave', function () {
        $(this).removeClass('hover');
    });


    /* FUNCTIONS
    ***************************************************************************************************************/

    function paginate() {
        if (pageCount > 1) {
            if (elements > 0) {
                elementsperpage = Math.ceil(pageSettings.recordsperpage / pageSettings.interval);
            }

            navigate(currentPage);
        }
    };

    function buildControls() {
        element.html(''); // Clear our controls

        var previousPage = (currentPage < 1) ? 1 : currentPage;
        var nextPage = (currentPage > (pageCount - 2)) ? pageCount : currentPage + 2;
        
        var button = '<div class="button"></div>';
        var first = '<div class="button" id="' + 1 + '"><<</div>';
        var prev = '<div class="button" id="' + previousPage + '"><</div>';
        var next = '<div class="button" id="' + nextPage + '">></div>';
        var last = '<div class="button" id="' + pageCount + '">>></div>';

        var j = 1;
        var n = 5;

        if (currentPage + 2 >= pageCount) {
            // At the end
            if ((pageCount - 4) > 0) {
                j = pageCount - 4;
            } else {
                j = 1
            }
            n = pageCount;
        } else if (currentPage - 2 <= 0) {
            // At the beginning
            if (pageCount < 5)
                n = pageCount;
        } else {
            // In the middle
            j = currentPage - 1;
            n = j + 4;
        }

        for (i = j; i <= n; i++) {
            $(button)
                .attr('id', i)
                .removeClass('active')
                .text(i)
                .appendTo(element);

            if (Number(currentPage + 1) == i)
                $('#' + i, element).addClass('active');
        }

        //if (currentPage > 0)
            $(prev).prependTo(element);

        //if (currentPage > 1)
            $(first).prependTo(element);

        //if (Number(currentPage + 1) < pageCount)
            $(next).appendTo(element);

        //if (Number(currentPage + 1) < Number(pageCount - 1))
            $(last).appendTo(element);
    };

    function navigate(page) {
        currentPage = page;
        buildControls();

        var start = page;

        if (elementsperpage > 0) {
            $(pageSettings.dataelement).hide();

            if (start > 0)
                start = page * elementsperpage;
            var pageEnd = start + elementsperpage;

            $(pageSettings.dataelement).slice(start, pageEnd).show();
        } else {
            $(pageSettings.container).hide();

            if (start > 0)
                start = page * pageSettings.recordsperpage;

            var pageEnd = start + pageSettings.recordsperpage;

            $(pageSettings.container).slice(start, start + pageSettings.recordsperpage).show();
        }
    };
};