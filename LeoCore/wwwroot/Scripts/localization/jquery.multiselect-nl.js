/* Dutch (UTF-8) initialization for the jQuery MultiSelect UI Widget. */
/* Written by Paul Bossuyt */
(function ($) {
    // Default settings for multiselect widget
    $.extend($.ech.multiselect.prototype.options, {
        noneSelectedText: "",
        selectedText: "# van # geselecteerd",
        checkAllText: "Alle",
        uncheckAllText: "Geen"
    });

    // Default settings for multiselect widget filter plugin
    $.extend($.ech.multiselectfilter.prototype.options, {
        placeholder: "zoektermen"
    });
})(jQuery);
