/* Dutch (UTF-8) initialization for the jQuery Numeric UI Widget. */
/* Written by Paul Bossuyt */
(function($) {
    $.extend(Number.globalization.defaultFormat, {
        decimalChar: ',',
        thousandsChar: '.'
    });
})(jQuery);
