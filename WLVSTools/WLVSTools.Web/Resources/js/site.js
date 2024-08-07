﻿/**
 * Send API Request.
 * @param {any} url
 * @param {any} method
 * @returns {jqXHR}
 */
function sendRequest(url, method = 'GET', data = undefined, requestingBtnElement = undefined) {
    var _requestingBtnElement = requestingBtnElement;
    var btnClickSpinner = new ButtonClickSpinner(_requestingBtnElement)

    if (_requestingBtnElement == undefined)
        $.customLoader.loadImage();
    else
        btnClickSpinner.showSpinner();

    return $.ajax({
        url: $.appendRootToPath(url),
        method: method,
        data: data,
        dataType: "json"
    })
    .always(function () {
        if (_requestingBtnElement == undefined)
            $.customLoader.removeImage();
        else
            btnClickSpinner.hideSpinner();
    });
}

/**
 * Show Spinner on the button upon click.
 * @param {any} element
 */
function ButtonClickSpinner(element) {
    var _element = element;
    var defaultButtonText = $(_element).html();

    this.showSpinner = function () {
        $(_element).html(`
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
            Please wait...
        `);
    };

    this.hideSpinner = function () {
        $(_element).html(defaultButtonText);
    };
}

/**
 * Show json in a pretty print fashion.
 * @param {any} jsonObject
 * @param {boolean} withCopyButton  
 */
function PrettyPrint(jsonObject, withCopyButton = false) {
    var _jsonObject = jsonObject;
    var _withCopyButton = withCopyButton;

    var replacer = function (match, pIndent, pKey, pVal, pEnd) {
        var key = '<span class=json-key>';
        var val = '<span class=json-value>';
        var str = '<span class=json-string>';
        var r = pIndent || '';

        if (pKey)
            r = r + key + pKey.replace(/[": ]/g, '') + '</span>: ';
        if (pVal) {
            if (_withCopyButton) {
                r = r + (pVal[0] == '"' ? str : val) + pVal + '<button type="button" class="btn-copy btn btn-xs btn-success ml-1"><i class="fas fa-copy"></i></button></span>';
            } else {
                r = r + (pVal[0] == '"' ? str : val) + pVal;
            }
        }

        return r + (pEnd || '');
    };

    /**
     * Pretty Print JSON string.
     * @returns JSON String
     */
    this.print = function () {
        var jsonLine = /^( *)("[\w]+": )?("[^"]*"|[\w.+-]*)?([,[{])?$/mg;
        return JSON.stringify(_jsonObject, null, 3)
            .replace(/&/g, '&amp;').replace(/\\"/g, '&quot;')
            .replace(/</g, '&lt;').replace(/>/g, '&gt;')
            .replace(jsonLine, replacer);
    };
}

$(function () {
    $("#btnSubmit, .btnSubmit").on("click", function () {
        var btnClickSpinner = new ButtonClickSpinner(this);
        btnClickSpinner.showSpinner();
    });
});