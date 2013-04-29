var settings = {
    selectedColumn: 2,
    ignoreColumns: [0],
    direction: 'asc',
    onComplete: function () {
        $('.page_navigation').paginate({ collection: 'tbody > tr', recordsperpage: 20, container: 'tbody > tr' });
    }
};

var filterSettings = {
    filterInput: '#Filter',
    ignoreColumns: [0],
    onComplete: function () {
        $('.candidates').sortTable(settings);
    }
};

var pMessage = $('.message');

$(function () {
    $('.candidates').filterTable(filterSettings);

    displayButtons('.candidates');

    $('.candidates').live('click', function () {
        displayButtons('.candidates');
    });

    $('#edit').click(function (e) {
        e.preventDefault();
        window.location = '/Candidates/Edit/' + $('tbody tr.selected', '.candidates').attr('id');
    });

    $('#delete').click(function (e) {
        e.preventDefault();
        $("#dialog-confirm").dialog('open');
    });

    $('#activate').click(function (e) {
        e.preventDefault();
        activate($('tbody tr.selected', '.candidates').attr('id'));
    });

    $('#deactivate').click(function (e) {
        e.preventDefault();
        deactivate($('tbody tr.selected', '.candidates').attr('id'));
    });

    $('#show-active-only').change(function () {
        filterResults($(this).is(":checked"));
    });
    
    $('#export').click(function () {
        var table = '#' + $(this).attr('data-id');
        var fileName = $(this).attr('data-id');

        $('#FileContent').val($(table).TableCSVExport({ 'delivery': 'value' }));
        $('#FileName').val(fileName);

        $('#export-form').submit();
    });

    $("#dialog-confirm").dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        buttons: {
            "Delete all items": function () {
                deleteCandidates();
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
});

function filterResults(showOnlyActive) {
    if (showOnlyActive) {
        rows = [];

        $.each($("tbody > tr", ".candidates"), function (i, item) {
            var checkbox = $(this).find('.status > input');
            if (!checkbox.is(":checked")) {
                rows.push(item);
                $(this).remove(); // remove from our table
            }
        });
    } else {
        $.each(rows, function (i, item) {
            $('.candidates').append(item);
        });
    }

    $('.candidates').sortTable(settings);
}

function deleteCandidates() {
    $.each($('tbody tr.selected', '.candidates'), function () {
        deleteCandidate($(this).attr('id'));
    });
};
