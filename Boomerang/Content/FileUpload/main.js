/*
 * jQuery File Upload Plugin JS Example 6.5.1
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/*jslint nomen: true, unparam: true, regexp: true */
/*global $, window, document */

$(function () {
    'use strict';

    $('#documents').fileupload({
        maxFileSize: 51200, // 50MB
        filesContainer: $('#progress-list'),
        sequentialUploads: true,
        uploadTemplateId: null,
        downloadTemplateId: '#existing-files',
        uploadTemplate: function (o) {
            var rows = $();
            
            $.each(o.files, function (index, file) {                
                if (file.error) {
                    pMessage.show().addClass('ui-state-error').removeClass('ui-state-highlight').text(locale.fileupload.errors[file.error]);
                } else {
                    var row = '<tr class="template-upload"><td id="' + file.Id + '">' + file.name + '</td><td id="file-size">' + o.formatFileSize(file.size) + '</td><td><div class="progress"><div class="bar" style="width:0%;"></div></div></td><td class="start"><button><span>Start</span></button></td><td class="cancel"><button>Cancel</button></td></tr>';
                    rows = rows.add(row);
                }
            });

            return rows;
        }
    }).bind('fileuploadfail', function (e, data) {
        console.log(e);
    }).bind('fileuploadcompleted', function (e, data) {
        var row = '<tr id="' + data.result.Id + '"><td><a href="/Components/UploadHandler.ashx?Id=' + data.result.Id + '" title="' + data.result.FileName + '">' + data.result.FileName + '</a></td><td id="file-size">' + data.files[0].size + '</td><td>' + new Date().toString() + '</td><td class="delete"><button data-id="' + data.result.Id + '"><span>Delete</span></button></td></tr>';
        $('#existing-files tbody').append(row);
    });
});