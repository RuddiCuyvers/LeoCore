//----------------------------------------------------------
// Copyright (C) WGK. All rights reserved.
//----------------------------------------------------------
// WGK.JQuery.js

/// <reference path="jquery-1.7.2-vsdoc.js" />
/// <reference path="jquery.validate-vsdoc.js" />
/// <reference path="jquery.json-2.3.js" />
/// <reference path="globalize.js" />
/// <reference path="jquery.jqGrid.js" />
/// <reference path="jquery.multiselect.js" />
/// <reference path="jquery-ui-numeric.js" />

(function ($) {
    // WGK namespace
    $.WGK = {};

    //#region -- Constants
    $.WGK.ButtonKeys = {
        enterKey: 13
    };

    // UserCode constants are copied from C# class WGK.Lib.UserCode.UserCode
    $.WGK.UserCode = {
        blankCode: "_BLNK",
        zeroCode: "0",
        allCode: "_ALL"
    };

    // Literals are copied from C# resource WGK.XXX.Common.CommonLiterals
    $.WGK.Literals = {
        errorCap: "Fout",
        selectButton: "Selecteren",
        editButton: "Bewerken",
        deleteButton: "Verwijderen",
        detailButton: "Detail",
        cancelButton: "Annuleren",
        searchButton: "Zoeken",
        printButton: "Afdrukken",
        blankLine: "----Kiezen----",
        validationSummary: "Er is een fout opgetreden",
        booleanYes: "Ja",
        booleanNo: "Nee"
    };
    //#endregion -- Constants 

    //#region -- Utility functions
    $.WGK.isNullOrEmptyOrBlankCode = function WGK$isNullOrEmptyOrBlankCode(pCode) {
        /// <summary>
        /// Checks if usercode is empty or blank code
        /// </summary>
        /// <returns type="Boolean"></returns>
        return pCode == null || pCode == '' || pCode == $.WGK.UserCode.blankCode;
    };

    $.WGK.isNullOrEmptyOrZeroCode = function WGK$isNullOrEmptyOrZeroCode(pCode) {
        /// <summary>
        /// Checks if usercode is empty or zero code
        /// </summary>
        /// <returns type="Boolean"></returns>
        return pCode == null || pCode == '' || pCode == $.WGK.UserCode.zeroCode;
    };

    $.WGK.formatGuidString = function WGK$formatGuidString(pGuid) {
        /// <summary>
        /// Removes blanks and curly braces from a guid string and converts letters to lower case
        /// </summary>
        /// <returns type="String"></returns>
        if (pGuid == null || pGuid === "" || typeof pGuid !== "string") {
            return "";
        }
        return $.trim(pGuid).toLowerCase().replace(/^{|}$/g, '');
    };

    $.WGK.ajaxErrorHtml = function WGK$ajaxErrorHtml(pResponse, pStatus, pStatusText) {
        /// <summary>
        /// Return a formatted Html message for an ajax error
        /// </summary>
        /// <returns type="String"></returns>
        var vErrorMsg = '<b>' + (pStatusText ? pStatusText : $.WGK.Literals.validationSummary) + '</b>';
        if (pResponse.responseText) {
            vErrorMsg += '<div>' + pResponse.responseText + '</div>';
        }
        return vErrorMsg;
    };
    
    $.WGK.errorHtml = function WGK$errorHtml(pErrorMessage, pErrorTitle) {
        /// <summary>
        /// Return a formatted Html message for an error
        /// </summary>
        /// <returns type="String"></returns>
        var vErrorMsg = '<b>' + (pErrorTitle ? pErrorTitle : $.WGK.Literals.validationSummary) + '</b>';
        if (pErrorMessage) {
            vErrorMsg += '<div>' + pErrorMessage + '</div>';
        }
        return vErrorMsg;
    };
    //#endregion -- Utility functions

    //#region -- Date functions
    $.WGK.dateGetDaysInMonth = function WGK$dateGetDaysInMonth(pDate) {
        return [31, ($.WGK.dateIsLeapYear(pDate) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][pDate.getMonth()];
    };

    $.WGK.dateIsLeapYear = function WGK$dateIsLeapYear(pDate) {
        var vYear = pDate.getFullYear();
        return (((vYear % 4 === 0) && (vYear % 100 !== 0)) || (vYear % 400 === 0));
    };

    $.WGK.dateAddDays = function WGK$dateAddDays(pDate, pDays) {
    	/// <summary>
    	/// Add a number of days to a date
    	/// </summary>
    	/// <param name="pDate">Original date</param>
    	/// <param name="pDays">Number of days to add to the original date</param>
    	/// <returns type="Date">New date instance with number of days added</returns>
        var vResult = new Date(pDate);
        vResult.setDate(pDate.getDate() + pDays);
        return vResult;
    };

    $.WGK.dateAddMonths = function WGK$dateAddMonths(pDate, pMonths) {
        /// <summary>
        /// Add a number of months to a date
        /// </summary>
        /// <param name="pDate">Original date</param>
        /// <param name="pMonths">Number of months to add to the original date</param>
        /// <returns type="Date">New date instance with number of months added</returns>
        var vResult = new Date(pDate);
        // First, set day to first day in month 
        vResult.setDate(1);
        // Update the month
        vResult.setMonth(pDate.getMonth() + pMonths);
        // Now set correct day in month, taking into account the number of days of the current the month for the current year
        vResult.setDate(Math.min(pDate.getDate(), $.WGK.dateGetDaysInMonth(vResult)));
        return vResult;
    };
    //#endregion -- Date utility functions

    //#region -- DynamicInfo functions
    $.WGK.dynamicInfoSummary = function WGK$dynamicInfoSummary(pDynamicInfoSet$) {
    	/// <summary>
        /// Returns a dynamicInfoSummary string for the DynamicInfoSet jQuery wrapped set
    	/// </summary>
        /// <param name="pDynamicInfoSet$">jQuery wrapped set containing DynamicInfoSet labels and fields</param>
    	/// <returns type="String"></returns>
        var vDynamicInfoSummary = '';
        //Loop through all label elements in the DynamicInfoSet
        $('label', pDynamicInfoSet$).each(function (pIndex, pElement) {
            var vLabel$ = $(this);
            var vAttrName = vLabel$.text();
            //Get the Html ID of the DynamicInfo field from the 'for' attribute of the label
            var vHtmlID = vLabel$.attr('for');
            //Get value of the DynamicInfo field
            var vAttrValue = '';
            // Check if there is an associated description field (e.g. WGKSelectorByDescription widget)
            var vField$ = $('#' + vHtmlID + '_Description', pDynamicInfoSet$);
            if (vField$.length !== 0) {
                vAttrValue = vField$.val();
            } else {
                vField$ = $('#' + vHtmlID, pDynamicInfoSet$);
                if (vField$.prop('tagName') === 'SELECT') {
                    // For DropDownLists get the text of the selected item(s)
                    vAttrValue = $('option:selected', vField$).text();
                } else {
                    if (vField$.prop('type') === 'checkbox') {
                        vAttrValue = vField$.prop('checked') ? $.WGK.Literals.booleanYes : $.WGK.Literals.booleanNo;
                    } else if (vField$.filter(':radio').length > 0) {
                        // RadioButtonList: get the text immediately following the selected button
                        vAttrValue = vField$.filter(':radio:checked').next('span').text();
                    } else {
                        // Default
                        vAttrValue = vField$.val();
                    }
                }
            }
            // Append attribute name/value to summary
            if (vDynamicInfoSummary.length != 0) {
                vDynamicInfoSummary += '\r\n';
            }
            vDynamicInfoSummary += vAttrName;
            vDynamicInfoSummary += ': ';
            vDynamicInfoSummary += vAttrValue;
        });
        return vDynamicInfoSummary;
    };
    //#endregion -- DynamicInfo functions

    //#region -- AutoComplete widget function
    // Remark 2013-09-20: pBlankLine parameter has been replaced by pAutoClearAndSelect parameter.
    $.WGK.autoComplete = function WGK$autoComplete(pTextBoxID, pValueID, pUrl, pMinLength, pSoftDeleted, pAutoClearAndSelect, pOnSelectCallback, pExtraData, pDataFunction) {
        // Store current value in data attribute on hidden field
        if (pValueID != null) {
            var vValueID$ = $('#' + pValueID);
            vValueID$.data('last_val', vValueID$.val());
        } else {
            $('#' + pTextBoxID).data('clear_val', false);
        }
        $('#' + pTextBoxID).autocomplete({
            source: function (pRequest, pResponse) {
                // Default data object for the ajax call
                var vAjaxData = {
                    pStartsWith: pRequest.term,
                    pSoftDeleted: pSoftDeleted
                };
                if (pExtraData != null) {
                    // Add properties from pExtraData to the data object for the ajax call
                    $.extend(vAjaxData, pExtraData);
                }
                if (pDataFunction != null) {
                    // Use a custom function to get the data object for the ajax call
                    vAjaxData = pDataFunction(vAjaxData);
                }
                $.ajax({
                    url: pUrl,
                    dataType: 'json',
                    data: vAjaxData,
                    success: function (pData) {
                        if ((pAutoClearAndSelect == true) && $.isArray(pData)) {
                            if (pData.length == 1 || (pData.length > 1 && pData[0].label.toLowerCase() == $('#' + pTextBoxID).val().toLowerCase())) {
                                // Automatically select item if there is only one match or if there is an exact text match
                                $.WGK.autoCompleteSelect(pData[0], pValueID, pTextBoxID, pOnSelectCallback);
                            } else {
                                // Clear selected value in hidden field. Postpone clearing the textbox until it loses focus.
                                $.WGK.autoCompleteClear(pValueID, pTextBoxID, pOnSelectCallback);
                            }
                        }
                        pResponse(pData);
                    },
                    error: function (pResp, pStatus, pStatusText) {
                        $('#ErrorMessage', $('#' + pTextBoxID).closest('form')).html(
                            $.WGK.ajaxErrorHtml(pResp, pStatus, pStatusText));
                    }
                });
            },
            minLength: pMinLength,
            open: function () {
                $(this).removeClass(' ').addClass('ui-corner-top');
            },
            close: function () {
                $(this).removeClass('ui-corner-top').addClass(' ');
            },
            focus: function (e, a) {
                $(this).val(a.item.label);
                return false;
            },
            search: function () {
                if (pAutoClearAndSelect == true) {
                    // Clear the selected value before starting the ajax call
                    $.WGK.autoCompleteClear(pValueID, pTextBoxID, pOnSelectCallback);
                }
            },
            change: function () {
                // This event is raised when the autocomplete widget loses focus
                if (pAutoClearAndSelect == true) {
                    // If textbox contains less chars then needed for an autocomplete search, then clear the selected value
                    if ($(this).val().length < pMinLength) {
                        $.WGK.autoCompleteClear(pValueID, pTextBoxID, pOnSelectCallback);
                    }
                    // Clear the autocomplete textbox if no value was selected
                    if (pValueID != null) {
                        var vValueID$ = $('#' + pValueID);
                        if (vValueID$.val() == null || vValueID$.val() == '') {
                            $(this).val('');
                        }
                    } else {
                        // If there is no hidden field, clear textbox if postponed-clear-flag is set
                        if ($(this).data('clear_val') == true) {
                            $(this).data('clear_val', false);
                            $(this).val('');
                        }
                    }
                }
                // Trigger the hidden value change event when the autocomplete textbox loses focus
                if (pValueID != null) {
                    var vValueID$ = $('#' + pValueID);
                    if (vValueID$.val() != vValueID$.data('last_val')) {
                        // Store new value in data attribute on hidden field before triggering the change event
                        vValueID$.data('last_val', vValueID$.val());
                        vValueID$.change();
                    }
                } else {
                    // Trigger textbox change event if there is no hidden field
                    $(this).change();
                }
            },
            select: function (e, a) {
                // Copy selected text and value to autocomplete textbox and hidden field
                $.WGK.autoCompleteSelect(a.item, pValueID, pTextBoxID, pOnSelectCallback);
                return false;
            }
        });
    };

    // AutoComplete helper function.
    // Copies selected text and value to autocomplete textbox and hidden field.
    $.WGK.autoCompleteSelect = function WGK$autoCompleteSelect(pItem, pValueID, pTextBoxID, pOnSelectCallback) {
        // Callback replaces default select event handling
        if (pOnSelectCallback != null) {
            if (pValueID == null) {
                // If there is no hidden field, clear the flag to clear textbox when it loses focus
                $('#' + pTextBoxID).data('clear_val', false);
            }
            // Selected item is parameter for callback function
            pOnSelectCallback(pItem, pTextBoxID, pValueID);
        } else {
            // Copy description to autocomplete textbox
            $('#' + pTextBoxID).val(pItem.label);
            if (pValueID != null) {
                // Copy value to hidden field
                $('#' + pValueID).val(pItem.value);
            } else {
                // If there is no hidden field, clear the flag to clear textbox when it loses focus
                $('#' + pTextBoxID).data('clear_val', false);
            }
        }
    };

    // AutoComplete helper function.
    // Clears selected value in hidden field. Postpone clearing the textbox until it loses focus.
    $.WGK.autoCompleteClear = function WGK$autoCompleteClear(pValueID, pTextBoxID, pOnSelectCallback) {
        if (pValueID != null) {
            // Clear value in hidden field
            $('#' + pValueID).val('');
        } else {
            // If there is no hidden field, set a flag in order to clear textbox when it loses focus
            $('#' + pTextBoxID).data('clear_val', true);
        }
        // Callback replaces default select event handling
        if (pOnSelectCallback != null) {
            // Passing null as parameter to callback function indicates that nothing is selected
            pOnSelectCallback(null, pTextBoxID, pValueID);
        }
    };
    //#endregion -- AutoComplete widget function

    //#region -- AutoComplete widget alternative function
    // This alternative implementation has jQuery object parameters instead of HTML IDs for textbox and hidden field.
    $.WGK.autoCompleteAlt = function WGK$autoCompleteAlt(pTextBox$, pValue$, pUrl, pMinLength, pSoftDeleted, pAutoClearAndSelect, pOnSelectCallback, pExtraData, pDataFunction) {
        // Store current value in data attribute on hidden field
        if (pValue$ != null) {
            pValue$.data('last_val', pValue$.val());
        } else {
            pTextBox$.data('clear_val', false);
        }
        pTextBox$.autocomplete({
            source: function (pRequest, pResponse) {
                // Default data object for the ajax call
                var vAjaxData = {
                    pStartsWith: pRequest.term,
                    pSoftDeleted: pSoftDeleted
                };
                if (pExtraData != null) {
                    // Add properties from pExtraData to the data object for the ajax call
                    $.extend(vAjaxData, pExtraData);
                }
                if (pDataFunction != null) {
                    // Use a custom function to get the data object for the ajax call
                    vAjaxData = pDataFunction(vAjaxData);
                }
                $.ajax({
                    url: pUrl,
                    dataType: 'json',
                    data: vAjaxData,
                    success: function (pData) {
                        if ((pAutoClearAndSelect == true) && $.isArray(pData)) {
                            if (pData.length == 1 || (pData.length > 1 && pData[0].label.toLowerCase() == pTextBox$.val().toLowerCase())) {
                                // Automatically select item if there is only one match or if there is an exact text match
                                $.WGK.autoCompleteSelectAlt(pData[0], pValue$, pTextBox$, pOnSelectCallback);
                            } else {
                                // Clear selected value in hidden field. Postpone clearing the textbox until it loses focus.
                                $.WGK.autoCompleteClearAlt(pValue$, pTextBox$, pOnSelectCallback);
                            }
                        }
                        pResponse(pData);
                    },
                    error: function (pResp, pStatus, pStatusText) {
                        $('#ErrorMessage', pTextBox$.closest('form')).html(
                            $.WGK.ajaxErrorHtml(pResp, pStatus, pStatusText));
                    }
                });
            },
            minLength: pMinLength,
            open: function () {
                $(this).removeClass(' ').addClass('ui-corner-top');
            },
            close: function () {
                $(this).removeClass('ui-corner-top').addClass(' ');
            },
            focus: function (e, a) {
                $(this).val(a.item.label);
                return false;
            },
            search: function () {
                if (pAutoClearAndSelect == true) {
                    // Clear the selected value before starting the ajax call
                    $.WGK.autoCompleteClearAlt(pValue$, pTextBox$, pOnSelectCallback);
                }
            },
            change: function () {
                // This event is raised when the autocomplete widget loses focus
                if (pAutoClearAndSelect == true) {
                    // If textbox contains less chars then needed for an autocomplete search, then clear the selected value
                    if ($(this).val().length < pMinLength) {
                        $.WGK.autoCompleteClearAlt(pValue$, pTextBox$, pOnSelectCallback);
                    }
                    // Clear the autocomplete textbox if no value was selected
                    if (pValue$ != null) {
                        if (pValue$.val() == null || pValue$.val() == '') {
                            $(this).val('');
                        }
                    } else {
                        // If there is no hidden field, clear textbox if postponed-clear-flag is set
                        if ($(this).data('clear_val') == true) {
                            $(this).data('clear_val', false);
                            $(this).val('');
                        }
                    }
                }
                // Trigger the hidden value change event when the autocomplete textbox loses focus
                if (pValue$ != null) {
                    if (pValue$.val() != pValue$.data('last_val')) {
                        // Store new value in data attribute on hidden field before triggering the change event
                        pValue$.data('last_val', pValue$.val());
                        pValue$.change();
                    }
                } else {
                    // Trigger textbox change event if there is no hidden field
                    $(this).change();
                }
            },
            select: function (e, a) {
                // Copy selected text and value to autocomplete textbox and hidden field
                $.WGK.autoCompleteSelectAlt(a.item, pValue$, pTextBox$, pOnSelectCallback);
                return false;
            }
        });
    };

    // AutoComplete helper alternative function with jQuery object parameters.
    // Copies selected text and value to autocomplete textbox and hidden field.
    $.WGK.autoCompleteSelectAlt = function WGK$autoCompleteSelectAlt(pItem, pValue$, pTextBox$, pOnSelectCallback) {
        // Callback replaces default select event handling
        if (pOnSelectCallback != null) {
            if (pValue$ == null) {
                // If there is no hidden field, clear the flag to clear textbox when it loses focus
                pTextBox$.data('clear_val', false);
            }
            // Selected item is parameter for callback function
            pOnSelectCallback(pItem, pTextBox$, pValue$);
        } else {
            // Copy description to autocomplete textbox
            pTextBox$.val(pItem.label);
            if (pValue$ != null) {
                // Copy value to hidden field
                pValue$.val(pItem.value);
            } else {
                // If there is no hidden field, clear the flag to clear textbox when it loses focus
                pTextBox$.data('clear_val', false);
            }
        }
    };

    // AutoComplete helper alternative function with jQuery object parameters.
    // Clears selected value in hidden field. Postpone clearing the textbox until it loses focus.
    $.WGK.autoCompleteClearAlt = function WGK$autoCompleteClearAlt(pValue$, pTextBox$, pOnSelectCallback) {
        if (pValue$ != null) {
            // Clear value in hidden field
            pValue$.val('');
        } else {
            // If there is no hidden field, set a flag in order to clear textbox when it loses focus
            pTextBox$.data('clear_val', true);
        }
        // Callback replaces default select event handling
        if (pOnSelectCallback != null) {
            // Passing null as parameter to callback function indicates that nothing is selected
            pOnSelectCallback(null, pTextBox$, pValue$);
        }
    };
    //#endregion -- AutoComplete widget alternative function

    //#region -- jQuery Grid utility functions and variables
    $.WGK.gridModus = [];
    $.WGK.gridDataSource = [];
    $.WGK.gridLastEditID = [];
    $.WGK.gridDelIDs = [];
    $.WGK.gridLastAddID = [];
    $.WGK.gridErrorIDs = [];
    $.WGK.gridSelectedIDs = [];
    $.WGK.gridOnSelectFunc = [];
    $.WGK.gridOnEditFunc = [];
    $.WGK.gridOnCloseFunc = [];
    $.WGK.gridBeforeDeleteFunc = [];
    $.WGK.gridOnDeleteFunc = [];
    $.WGK.gridOpenSearchFunc = [];
    $.WGK.gridOpenCustomFormFunc = [];
    $.WGK.gridOpenDetailUrl = [];
    $.WGK.gridOpenDetailColumnID = [];
    $.WGK.gridOpenDetailAsLink = [];
    
    $.WGK.GridModusEnum = {
        "view": 0,
        "editonly": 1,
        "select": 2,
        "deleteonly": 4,
        "editanddelete": 5,
    };

    $.WGK.gridLoadComplete = function (pGridID, pCallback) {
    	/// <summary>
    	/// Handler for the jqgrid LoadComplete event
    	/// </summary>
    	/// <param name="pGridID">HTML ID of the jqgrid widget</param>
        /// <param name="pCallback">Optional callback function to be called. pGridID is passed as parameter to the callback function</param>
        var vGrid = $("#" + pGridID);
        var vRowIDs = vGrid.getDataIDs();
        for (var i = 0; i < vRowIDs.length; i++) {
            var vRowID = vRowIDs[i];
            // Add buttons to row
            $.WGK.gridAddRowActionButtons(pGridID, vRowID);
            // Highlight rows with errors
            if ($.inArray(parseInt(vRowID), $.WGK.gridErrorIDs[pGridID]) !== -1) {
                $("#" + pGridID + " tr").filter("#" + vRowID).addClass("errorRow");
            }
        }
        if (pCallback != null) {
            pCallback(pGridID);
        }
    };

    $.WGK.gridEditRow = function (pGridID, pRowID, pCallbackFunc) {
        // Save current editable row (if any) and put specified row in edit mode. 
        // Call the pCallbackFunc if row can be put in edit mode or if row is already in edit mode.
        if (pRowID && pRowID != $.WGK.gridLastEditID[pGridID]) {
            $.WGK.gridSaveLastEditRow(pGridID, function (pGridID) {
                // This callback is called if previous editable row can be saved (i.e. validation succeeds) or if there is no editable row to save
                // Add cancel button to row
                ce = "<span title='" + $.WGK.Literals.cancelButton + "' style='float: left; cursor: hand' class='ui-icon ui-icon-cancel' onclick='$.WGK.gridRestoreRow(\"" + pGridID + "\",\"" + pRowID + "\");' />";
                var vGrid = $("#" + pGridID);
                vGrid.setRowData(pRowID, { sav: ce });
                // Put row in edit mode
                vGrid.editRow(pRowID, true,
                    function (id) { $.WGK.gridOnEdit(pGridID, id) },
                    null, null, null,
                    function (id) { $.WGK.gridAfterCloseRow(pGridID, id); },
                    null,
                    function (id) { $.WGK.gridAfterCloseRow(pGridID, id); });
                // Call optional callback function to signal row is in edit mode
                if (pCallbackFunc != null) {
                    pCallbackFunc(pGridID, pRowID);
                }
            });
            return;
        }
        // Call the specified pCallbackFunc, even if row is already in edit mode.
        if (pCallbackFunc != null) {
            pCallbackFunc(pGridID, pRowID);
        }
    };

    $.WGK.gridRestoreRow = function (pGridID, pRowID) {
        var vGrid = $("#" + pGridID);
        vGrid.restoreRow(pRowID);
        $.WGK.gridAfterCloseRow(pGridID, pRowID);
    };

    $.WGK.gridAfterCloseRow = function (pGridID, pRowID) {
        // Called after putting row from edit mode in read mode.
        // Must be called directly when saving row with ENTER key
        if (pRowID) {
            $.WGK.gridLastEditID[pGridID] = null;
            // remove cancel button from row
            var vGrid = $("#" + pGridID);
            vGrid.setRowData(pRowID, { sav: "" });
            //if ($.WGK.gridOpenDetailAsLink[pGridID]) {
            //    // We must update the open detail hyperlink
            //    $.WGK.gridAddRowActionButtons(pGridID, pRowID, true);
            //}
            // callback
            if ($.WGK.gridOnCloseFunc[pGridID]) {
                $.WGK.gridOnCloseFunc[pGridID](pRowID);
            }
        }
    };

    $.WGK.gridSaveLastEditRow = function (pGridID, pAfterSaveFunc) {
        // Accept changes in editable row. If validation succeeds put row in read mode and call the afterSaveFunc.
        if ($.WGK.gridLastEditID[pGridID] != null) {
            var vGrid = $("#" + pGridID);
            // Check if row is in edit mode
            if ($(vGrid.getInd($.WGK.gridLastEditID[pGridID], true)).attr("editable") === "1") {
                vGrid.saveRow($.WGK.gridLastEditID[pGridID], null, null, null, function (pLastEditID, pResponse) {
                    // This callback is called only if editable row can be saved (i.e. validation succeeds)
                    // Make sure cancel button is removed from previous row
                    $.WGK.gridAfterCloseRow(pGridID, pLastEditID);
                    if (pAfterSaveFunc != null) {
                        pAfterSaveFunc(pGridID);
                    }
                });
                return;
            }
        }
        // Call the specified afterSaveFunc, even if there is no editable row to save.
        if (pAfterSaveFunc != null) {
            pAfterSaveFunc(pGridID);
        }
    };

    // DELETED - When showing a jquery dialogbox enter key saves the current row. Instead use confirm function ...
    //    $.WGK.gridDeleteRow = function (pGridID, pRowID, pNoDialog) {
    //        // Delete specified row. Optionally show jquery confirmation dialog.
    //        if (pNoDialog) {
    //            // Delete the row without showing confirmation dialog
    //            $.WGK._gridDeleteRow(pGridID, pRowID);
    //        }
    //        else {
    //            // Show confirmation dialog
    //            var vButtons = {};
    //            vButtons[$.jgrid.del.bSubmit] = function () {
    //                // OK button handler
    //                $(this).dialog('destroy');
    //                // Go ahead and delete the row 
    //                $.WGK._gridDeleteRow(pGridID, pRowID);
    //            };
    //            vButtons[$.jgrid.del.bCancel] = function () {
    //                // Cancel button handler
    //                $(this).dialog('destroy');
    //            };

    //            $('<div>').dialog({
    //                resizable: false,
    //                modal: true,
    //                closeOnEscape: false,
    //                open: function (event, ui) {
    //                    $(this).text($.jgrid.del.msg);
    //                    // Hide close button
    //                    $(this).parent().children().children('.ui-dialog-titlebar-close').hide();
    //                },
    //                buttons: vButtons,
    //                title: $.jgrid.del.caption
    //            });
    //        }
    //    };

    //    $.WGK._gridDeleteRow = function (pGridID, pRowID) {
    //        // Private implementation. Must not be called from external code. 
    //        // Instead use $.WGK.gridDeleteRow public method.
    //        var vGrid = $("#" + pGridID);
    //        // Delete row from grid data
    //        vGrid.delRowData(pRowID);
    //        // For existing rows remember which ones are deleted
    //        if (pRowID > 0) {
    //            $.WGK.gridDelIDs[pGridID].push(pRowID);
    //        }
    //        // Call optional callback function
    //        if ($.WGK.gridOnDeleteFunc[pGridID]) {
    //            $.WGK.gridOnDeleteFunc[pGridID](pRowID);
    //        }
    //    };

    $.WGK._gridDeleteRow = function (pGridID, pRowID, pNoDialog) {
        // Private implementation. Must not be called from external code. 
        // Instead use $.WGK.gridDeleteRow public method.
        var vGrid = $("#" + pGridID);
        if (pNoDialog || confirm($.jgrid.del.msg)) {
            // Delete row from grid data
            vGrid.delRowData(pRowID);
            // For existing rows remember which ones are deleted
            $.WGK.gridDelIDs[pGridID].push(pRowID);
            // Call optional callback function
            if ($.WGK.gridOnDeleteFunc[pGridID]) {
                $.WGK.gridOnDeleteFunc[pGridID](pRowID);
            }
        }
    };

    $.WGK.gridDeleteRow = function (pGridID, pRowID, pNoDialog) {
        // Delete specified row
        if ($.WGK.gridBeforeDeleteFunc[pGridID]) {
            // Cancel delete if gridBeforeDeleteFunc returns false
            if ($.WGK.gridBeforeDeleteFunc[pGridID](pRowID) === true) {
                $.WGK._gridDeleteRow(pGridID, pRowID, pNoDialog);
            }
        } else {
            $.WGK._gridDeleteRow(pGridID, pRowID, pNoDialog);
        }
    };

    $.WGK.gridCancelAddRow = function (pGridID, pRowID) {
        // Delete specified row without showing a messagebox
        var vGrid = $("#" + pGridID);
        vGrid.delRowData(pRowID);
    };

    $.WGK.gridCreateRow = function (pGridID, pKeyCol, pNoEdit, pPositionNewRow, pAfterCreateFunc) {
        // Save current editable row (if any) and add a new row
        // Put the new row in edit mode unless pNoEdit is set to true
        // Pass pKeyCol value and pCallback on to the AfterSaveFunc through closure
        $.WGK.gridSaveLastEditRow(pGridID, function (pGridID) {
            // Add empty row
            $.WGK.gridLastAddID[pGridID]--;
            var vRowID = $.WGK.gridLastAddID[pGridID];
            var vDatarow = {};
            vDatarow[pKeyCol] = vRowID;
            var vGrid = $("#" + pGridID);
            vGrid.addRowData(vRowID, vDatarow, pPositionNewRow);

            // Add buttons
            $.WGK.gridAddRowActionButtons(pGridID, vRowID, true);
            if (!pNoEdit) {
                // Add a save/cancel buttons only if row is put in edit mode
                $.WGK.gridAddRowUpdateButtons(pGridID, vRowID);
            }

            // Call the callback function from within AfterSaveFunc BEFORE putting row in edit mode,
            // e.g. to fill in default data
            if (pAfterCreateFunc) {
                pAfterCreateFunc(vRowID);
            }

            // Put row in edit mode (default)
            if (!pNoEdit) {
                // Set the afterRestoreFunc to gridCancelAddRow so the added row is removed when changes are cancelled (ESC key)
                vGrid.editRow(vRowID, true,
                    function (id) { $.WGK.gridOnEdit(pGridID, id) },
                    null, null, null,
                    function (id) { $.WGK.gridAfterCloseRow(pGridID, id); },
                    null,
                    function (id) { $.WGK.gridCancelAddRow(pGridID, id); });
            }
        });
    };

    $.WGK.gridOnEdit = function (pGridID, pRowID) {
        // Row OnEdit event handler
        $.WGK.gridLastEditID[pGridID] = pRowID; // remember id of row that is in edit mode
        jQuery("#" + pGridID + " tr#" + pRowID).removeClass("errorRow");

        if ($.WGK.gridOnEditFunc[pGridID] != null) {
            $.WGK.gridOnEditFunc[pGridID](pRowID);
        }
    };

    $.WGK.gridSubmit = function (pGridID, pSource, pCallback) {
        $.WGK.gridSaveLastEditRow(pGridID, function (pGridID) {
            // Post grid data in hidden field
            var vGrid = $("#" + pGridID);
            var vGridData = vGrid.getRowData();
            // Remove non-data columns
            for (var i = 0; i < vGridData.length; i++) {
                var vRowData = vGridData[i];
                delete vRowData.del;
                delete vRowData.sav;
            }
            var jsonGrid = $.toJSON(vGridData);
            $("#" + pGridID + "_Data").val(jsonGrid);

            // Post grid deleted IDs in hidden field
            var jsonDel = $.toJSON($.WGK.gridDelIDs[pGridID]);
            $("#" + pGridID + "_DelIDs").val(jsonDel);

            // Post grid last added ID in hidden field
            $("#" + pGridID + "_LastAddID").val($.WGK.gridLastAddID[pGridID]);

            // Chain to next call if last edit row could be saved
            pCallback(pSource);
        });
    };

    $.WGK.gridSelectRow = function (pGridID, pRowID, pChecked) {
        // Event handler for the row selection checkbox button
        // Add or remove row from selected row IDs array
        if (pChecked === true) {
            // Add RowID to array
            if ($.inArray(pRowID, $.WGK.gridSelectedIDs[pGridID]) === -1) {
                $.WGK.gridSelectedIDs[pGridID].push(pRowID);
            }
        }
        else {
            // Remove RowID from array
            if ($.inArray(pRowID, $.WGK.gridSelectedIDs[pGridID]) > -1) {
                $.WGK.gridSelectedIDs[pGridID] = $.grep($.WGK.gridSelectedIDs[pGridID], function (pVal) {
                    return pVal !== pRowID;
                });
            }
        }
        // Callback function
        if ($.WGK.gridOnSelectFunc[pGridID] != null) {
            $.WGK.gridOnSelectFunc[pGridID].call(this);           
        }            
    };

    $.WGK.gridGetSelectedIDs = function (pGridID) {
        /// <summary>
        /// Gets the selected RowIDs
        /// </summary>
        /// <returns type="Array"></returns>
        return $.WGK.gridSelectedIDs[pGridID];
    };

    $.WGK.gridGetSelectedIDsWithDescription = function (pGridID, pDescriptionColumn) {
        /// <summary>
        /// Gets the selected RowIDs and corresponding Descriptions 
        /// </summary>
        /// <returns type="Array"></returns>
        var vGrid = $("#" + pGridID);
        return $.map($.WGK.gridSelectedIDs[pGridID], function (pRowID) {
            var vRowData = vGrid.getRowData(pRowID);
            var vDescription = (vRowData != null) ? vRowData[pDescriptionColumn] : '';
            return {
                id: pRowID,
                description: vDescription
            };
        });
    };

    $.WGK.gridSetOnSelectFunc = function(pGridID, pOnSelectFunc) {
        /// <summary>
        /// Sets the OnSelect callback function
        /// </summary>
        $.WGK.gridOnSelectFunc[pGridID] = pOnSelectFunc;
    };

    $.WGK.gridIsRowInEditMode = function (pGridID, pRowID) {
        /// <summary>
        /// Checks if a row is in edit mode
        /// </summary>
        return ($($("#" + pGridID).getInd(pRowID, true)).attr("editable") === "1");
    };

    $.WGK.gridAddRowActionButtons = function (pGridID, pRowID) {
        // Adds detail/delete/search action buttons to a row
        var vButtons = "";
        var vOpenUrl = $.WGK.gridOpenDetailUrl[pGridID];
        var vOpenColumnID = $.WGK.gridOpenDetailColumnID[pGridID];
        var vGridModus = $.WGK.gridModus[pGridID];
        if (vGridModus === $.WGK.GridModusEnum.select) {
            // Render checkbox to be able to select row
            vButtons += "<input type='checkbox' title='" + $.WGK.Literals.selectButton + "' style='float: left;' onclick='$.WGK.gridSelectRow.call(this, \"" + pGridID + "\",\"" + pRowID + "\", this.checked);' />";
        }
        // Don't render detail button in select modus as this is confusing for the user
        else if ((vOpenUrl != null) && (vOpenColumnID != null)) {
            if ($.WGK.gridOpenDetailAsLink[pGridID]) {
                // Render a static hyperlink
                var vOpenID = $("#" + pGridID).getCell(pRowID, vOpenColumnID);
                if (vOpenID) {
                    vButtons += "<a title='" + $.WGK.Literals.detailButton + "' style='float: left; cursor: hand' class='ui-icon ui-icon-extlink' target='_blank' href='" + vOpenUrl + vOpenID + "' />";
                }
            }
            else {
                vButtons += "<span title='" + $.WGK.Literals.detailButton + "' style='float: left; cursor: hand' class='ui-icon ui-icon-extlink' onclick='$.WGK.gridOpenDetail(\"" + pGridID + "\", \"" + pRowID + "\");' />";
            }
        }
        if (vGridModus & $.WGK.GridModusEnum.deleteonly) {
            vButtons += "<span title='" + $.WGK.Literals.deleteButton + "' style='float: left; cursor: hand' class='ui-icon ui-icon-trash' onclick='$.WGK.gridDeleteRow(\"" + pGridID + "\",\"" + pRowID + "\");' />";
        }
        if (vGridModus & $.WGK.GridModusEnum.editonly) {
            if ($.WGK.gridOpenSearchFunc[pGridID] != null) {
                // Edit row data through a search window
                vButtons += "<span title='" + $.WGK.Literals.searchButton + "' style='float: left; cursor: hand' class='ui-icon ui-icon-search' onclick='$.WGK.gridOpenSearch(\"" + pGridID + "\",\"" + pRowID + "\");' />";
            }
            if ($.WGK.gridOpenCustomFormFunc[pGridID] != null) {
                // Edit row detail in a custom form
                vButtons += "<span title='" + $.WGK.Literals.editButton + "' style='float: left; cursor: hand' class='ui-icon ui-icon-pencil' onclick='$.WGK.gridOpenCustomForm(\"" + pGridID + "\",\"" + pRowID + "\");' />";
            }
        }
        $("#" + pGridID).setRowData(pRowID, { del: vButtons });
    };

    $.WGK.gridAddRowUpdateButtons = function (pGridID, pRowID) {
        // Adds save/cancel buttons to a row
        var vButtons = "<span title='" + $.WGK.Literals.cancelButton + "' style='float: left; cursor: hand' class='ui-icon ui-icon-cancel' onclick='$.WGK.gridCancelAddRow(\"" + pGridID + "\",\"" + pRowID + "\");' />";
        $("#" + pGridID).setRowData(pRowID, { sav: vButtons });
    };
    
    $.WGK.gridOpenDetail = function (pGridID, pRowID) {
        // Navigate to the OpenDetailUrl for the grid (in a new tab)
        var vOpenUrl = $.WGK.gridOpenDetailUrl[pGridID];
        var vOpenColumnID = $.WGK.gridOpenDetailColumnID[pGridID];
        if (vOpenUrl != null && vOpenColumnID != null && !$.WGK.gridIsRowInEditMode(pGridID, pRowID)) {
            var vOpenID = $("#" + pGridID).getCell(pRowID, vOpenColumnID);
            if (vOpenID) {
                window.open(vOpenUrl + vOpenID);
            }
        }
    };

    $.WGK.gridOpenSearch = function (pGridID, pRowID) {
        // Invoke the OpenSearch function for the grid
        var vOpenSearchFunc = $.WGK.gridOpenSearchFunc[pGridID];
        if (vOpenSearchFunc != null) {
            vOpenSearchFunc(pGridID, pRowID);
        }
    };

    $.WGK.gridOpenCustomForm = function (pGridID, pRowID) {
        // Invoke the OpenCustomForm function for the grid
        var vOpenCustomFormFunc = $.WGK.gridOpenCustomFormFunc[pGridID];
        if (vOpenCustomFormFunc != null) {
            vOpenCustomFormFunc(pGridID, pRowID);
        }
    };

    $.WGK.gridReload = function (pGridID, pSearchCriteria) {
        // Reload grid data though ajax call
        $("#" + pGridID).setGridParam({ datatype: 'json', page: 1, postData: pSearchCriteria }).trigger('reloadGrid');
    };
    //#endregion -- END Grid utility functions 

    //#region -- jQuery Wrapper methods
    $.fn.disable = function () {
        /// <summary>
        /// Disables a group of form elements
        /// </summary>
        return this.each(function () {
            if (this.disabled == null) {
                this.disabled = true;
            }
        });
    };
    //#endregion -- jQuery Wrapper methods

    //#region -- jQuery validate plugin: Custom validation methods (validator rules)
    // IMPORTANT: Don't make the mistake of putting your custom validation functions and adapters in the
    // jQuery document ready event handler. This is too late in the process.

    // requiredUserCode rule
    $.validator.addMethod('requiredusercode', function (value, element, params) {
        /// <summary>
        /// Validator rule that checks that a UserCode value is filled in
        /// </summary>
        return !$.WGK.isNullOrEmptyOrBlankCode(value);
    });
    $.validator.unobtrusive.adapters.addBool('requiredusercode');

    // dayRange rule
    $.validator.addMethod('dayrange', function (value, element, param) {
        /// <summary>
        /// Validator rule that checks that a date value is in a valid range of days
        /// </summary>
       if (!value) {
            // No check needed if field is not filled in
            return true;
        }
        var valueDateParts = value.split(param.separator);
        var minDate = new Date();
        var maxDate = new Date();
        var now = new Date();

        // TODO: code beneden moet herwerkt worden want nu worden maand en dag omgewisseld !!!
        var dateValue = new Date(
            valueDateParts[2],
            (valueDateParts[1] - 1),
            valueDateParts[0],
            now.getHours(),
            now.getMinutes(),
            (now.getSeconds() + 5));

        minDate.setDate(minDate.getDate() - parseInt(param.min));
        maxDate.setDate(maxDate.getDate() + parseInt(param.max));

        return dateValue >= minDate && dateValue <= maxDate;
    });
    $.validator.unobtrusive.adapters.add('dayrange', ['min', 'max', 'dateseparator'], function (options) {
        var params = {
            min: options.params.min,
            max: options.params.max,
            separator: options.params.dateseparator
        };
        options.rules['dayrange'] = params;
        if (options.message) {
            options.messages['dayrange'] = options.message;
        }
    });

    // requiredEmptyIfRelatedFieldsEmpty rule
    $.validator.addMethod('requiredemptyifrelatedfieldsempty',
        function (value, element, params) {
            /// <summary>
            /// Validator rule that checks that if a field is filled in, then all related fields must also be filled in.
            /// </summary>
            
            // If no value in current field ==> no check is needed.
            // Note: optional returns true if no value provided.
            if (this.optional(element)) return true;

            // Getting the prefixed part, because MVC adds prefixes if the fields belong to a class used in the model.
            // E.g.IndexModel consists of:
            //          * Some fields
            //          * A SearchCriteria class consisting of
            //              - FieldA
            //              - FieldB
            //
            //  Then the element-id of FieldA = SearchCriteria_FieldA
            //  And the element-name of FieldA = SearchCriteria.FieldA
            var prefix = '';
            var splittedElementName = element.name.split('.');
            if (splittedElementName.length > 1) {
                // element-name has prefixes.
                // Removing the last array-element.
                var prefixedArray = splittedElementName.slice(0, splittedElementName.length - 1);
                prefix = prefixedArray.join('.');
            }

            // The related-parameter (passed as a string) into an array.
            var relatedFields = params.split(',');

            // Checking all fields above . None of these fields should be empty.
            //for (i = indexCurrentField - 1; i > -1; i--) {
            for (i = 0; i < relatedFields.length; i++) {
                var field = $('#' + prefix + '_' + relatedFields[i])
                if (field.val() == '') {
                    return false;
                }
            }
            return true;
        }
    );
    $.validator.unobtrusive.adapters.addSingleVal('requiredemptyifrelatedfieldsempty', 'relatedfields');    
    //
    //
    // requiredAndNotEqualTo - validation
    $.validator.addMethod("requiredandnotequalto",
        function (value, element, params) {

            // If no value in current field ==> error.
            if (this.optional(element)) return false;
            
            // The not-equal-to-value (passed as a string).
            var notEqualToValue = params;

            if (value == '') {
                return false;
            }

            if (value == notEqualToValue) {
                return false;
            }

            return true;
        }
    );
    $.validator.unobtrusive.adapters.addSingleVal("requiredandnotequalto", "notequaltovalue");
    
    // -- jQuery validate plugin
    // Replace validator methods with new culture aware implementations
    $.extend($.validator.methods, {
        date: function (pValue, pElement) {
            return this.optional(pElement) || (Globalize.parseDate(pValue) != null);
        },
        number: function (pValue, pElement) {
            return this.optional(pElement) || !isNaN(Globalize.parseFloat(pValue)); ;
        },
        range: function(pValue, pElement, pParams) {
            var vValue = Globalize.parseFloat(pValue);
            return this.optional(pElement) || (vValue >= pParams[0] && vValue <= pParams[1]);
        }
    });
    //#endregion -- jQuery validate plugin: Custom validation methods (validator rules)
    
    //#region -- jQuery plugins: Overwrite default configuration
    // IMPORTANT: Don't put plugin configuration in the jQuery document ready event handler. This is too late in the process.

    // -- Default settings for jqgrid plugin
    $.extend($.jgrid.defaults, {
        jsonReader: {repeatitems: false}, // Search elements in the json data by name 
        ajaxGridOptions: { async: false }, // Make sure grids use synchronous ajax calls!
        altRows: true,
        altclass: 'altRow',
        autowidth: true,
        height: '235', // corresponds to the height for displaying default the rowNum number of rows
        imgpath: '/Content/jqtheme/images',
        loadui: 'disable', // use loading indicator defined on master site
        multiboxonly: true,
        rowNum: 10, // override this value for grids that don't use paging
        rowList: [10, 20, 50], // set rowList: -1 to clear it
        viewrecords: true
    });

    // -- Default settings for multiselect listbox plugin
    $.extend($.ech.multiselect.prototype.options, {
        show: ['fade', 500],
        hide: ['blind', 250],
        selectedList: 4
    });

    // -- Default settings for numeric up/down plugin
    //$.extend($.ui.numeric.prototype.options, {
    //    format: '0',
    //    minWidth: 200,
    //    increment: 1,
    //    minValue: 0
    //});
    
    // -- Default settings for datepicker plugin
    $.datepicker.setDefaults({
        showOn: 'button'
    });

    // -- Default settings for dialog plugin
    $.extend($.ui.dialog.prototype.options, {
        closeOnEscape: false
    });

    // -- Default settings for mask plugin
    $.extend($.mask, {
        // Use a blank mask otherwise we get jquery validation errors on numeric fields
        placeholder: ' ',
    });
    //#endregion -- jQuery plugins: Overwrite default configuration

})(jQuery);
