/*################################################################
## AJAX Load Notification
## Add AJAX loading img and removal
## One object and its two methods are exposed 
##
## Jose A Magana
################################################################*/

(function ($) {
    "use strict";

    $.customLoader = {
        loadImage: function () {
            appendAJAXImage();
        },

        removeImage: function () {
            removeAJAXImage();
        }
    };

    var outerBoxClass = "ajaxImgBox";

    var innerBoxClass = "ajaxInnerBox";

    var imagePath = "/plugins/custom/show-loader/images/progress.gif";

    try {
        imagePath = $.appendRootToPath(imagePath);
    } catch (e) {
    }

    var outerBox = document.createElement("div");

    var innerBox = document.createElement("div");

    var addOuterStyle = function (elem) {
        elem.className = outerBoxClass;
        elem.style.position = "fixed";
        elem.style.zIndex = "1500";
        elem.style.top = (window.innerHeight / 2).toString() + "px";
        elem.style.width = "100%";
    },

        addInnerStyle = function (elem) {
            elem.className = innerBoxClass;
            elem.style.margin = "-3.5rem auto 0"; // the (-) is half the elem height
            elem.style.width = "8rem";
            elem.style.height = "8rem";
            elem.style.background = "url(" + imagePath + ") rgba(255, 255, 255, 0.9) center center no-repeat";
            elem.style.backgroundSize = "auto 90%";
            elem.style.borderRadius = "50%";
        },

        appendAJAXImage = function () {
            addOuterStyle(outerBox);
            addInnerStyle(innerBox);
            outerBox.appendChild(innerBox);
            document.body.appendChild(outerBox);
        },

        removeAJAXImage = function () {
            if (document.body.contains(outerBox))
                document.body.removeChild(outerBox);
        };

})(jQuery);