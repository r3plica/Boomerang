var _button;
var _text;

$("body")
    .bind('AjaxButtonClick', function (event, item, text) {
        _button = $(item);
        _text = _button.text();
        
        _button.addClass('ajax-executing');

        if (text == null) {
            _button.text('');
        } else {
            _button.text(text);
        }
    })
    .bind('AjaxButtonError', function (event, location) {
        _button
            .text('Failed')
            .removeClass('ajax-executing')
            .addClass('ajax-error');

        resetAjaxButton(location);
    })
    .bind('AjaxButtonSuccess', function (event, location) {
        _button
            .text('Success!')
            .removeClass('ajax-executing')
            .addClass('ajax-success');

        resetAjaxButton(location);
    });

function resetAjaxButton(location) {
    setTimeout(function () {
        _button
            .text(_text)
            .removeClass('ajax-executing')
            .removeClass('ajax-error')
            .removeClass('ajax-success');
    }, 1000, function () {
        if (location == null) {
            window.location = '#content-wrap';
        } else {
            window.location = location;
        }
    });
};