var mgpModule = angular.module("mgp", [
   "ngAnimate",
    "mgp.controllers",
    "mgp.services",
    "ui.directives",
    "ui.filters",
    "ui.bootstrap",
    "angularFileUpload",
    "ngGrid",
    "ngWig",
    "ngFileUpload",
    "ngImgCrop"
    ]);
/** Filters **/
mgpModule.filter('startFrom', function () {
    return function (input, start) {
        start = +start;
        return input.slice(start);
    }
});
mgpModule.filter('toDate', function () {
   return function (input) {
      return new Date(input);
   }
});
mgpModule.directive('ckEditor', function () {
    return {
        require: '?ngModel',
        link: function (scope, elm, attr, ngModel) {
            var ck = CKEDITOR.replace(elm[0], {
                extraPlugins: 'autogrow,wordcount',
                toolbar: [
                    ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript'],
                    ['NumberedList', 'BulletedList', 'Outdent', 'Indent', 'Blockquote', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
                    ['Link', 'Unlink', 'Anchor'],
                    ['Styles', 'Format', 'Font', 'FontSize', 'TextColor', 'BGColor']
                ],
                wordcount: {
                    showCharCount: true,
                    showWordCount: true
                }
            });

            if (!ngModel) return;

            ck.on('pasteState', function () {
                scope.$apply(function () {
                    ngModel.$setViewValue(ck.getData());
                });
            });

            ngModel.$render = function (value) {
                ck.setData(ngModel.$viewValue);
            };
        }
    };
});
mgpModule.directive('triggerChange', function ($sniffer) {
    return {
        link: function (scope, elem, attrs) {
            elem.bind('click', function () {
                // Get all auto-populated elements that are explicitly marked "dirty" and then trigger a change event
                if ($(attrs.triggerChange).length > 0)
                    $(attrs.triggerChange).trigger($sniffer.hasEvent('input') ? 'input' : 'change');
            });
        },
        priority: 1
    }
});