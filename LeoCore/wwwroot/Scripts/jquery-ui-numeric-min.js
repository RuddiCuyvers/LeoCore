/////*
////* jQuery UI Numeric Up/Down v1.4.1
////*
////* Copyright 2011, Tony Kramer
////* Dual licensed under the MIT or GPL Version 2 licenses.
////* https://github.com/flamewave/jquery-ui-numeric/raw/master/GPL-LICENSE.txt
////* https://github.com/flamewave/jquery-ui-numeric/raw/master/MIT-LICENSE.txt
////*/
////(function (a) {
////    a.widget("ui.numeric", {
////        version: "1.4.1",
////        options: {
////            disabled: false,
////            keyboard: true,
////            showCurrency: false,
////            currencySymbol: null,
////            title: null,
////            buttons: true,
////            upButtonIcon: "ui-icon-triangle-1-n",
////            upButtonTitle: null,
////            downButtonIcon: "ui-icon-triangle-1-s",
////            downButtonTitle: null,
////            emptyValue: 0,
////            minValue: false,
////            maxValue: false,
////            smallIncrement: 1,
////            increment: 5,
////            largeIncrement: 10,
////            calc: null,
////            format: null
////        },
////        _adjustmentFlag: false,
////        _keyDownFlag: false,
////        _timer: null,
////        _name: "numeric",
////        _value: 0,
////        _create: function () {
////            function timeStringToFloat(time) {
////                var hoursMinutes = time.split(/[.:]/);
////                var hours = parseInt(hoursMinutes[0], 10);
////                var minutes = hoursMinutes[1] ? parseInt(hoursMinutes[1], 10) : 0;
////                console.log(hours + minutes / 60);
////                return hours + minutes / 60;

////            }
////            var k = this.element.attr("type").toLowerCase();
////            console.log(k);
////            if (k !== "time" && k !== "number") {
////                throw "numeric widget can only be applied to text and number inputs."
////            }
////            this._checkFormat();
////            this._name = this.element.attr("id") || this.element.attr("name");
////            this._value = this._getInputValue(this.element.attr("value"), true);
////            //if (this.options.minValue !== false && this._value < this.options.minValue) {
////            //    this._value = this.options.minValue
////            //}
////            //if (this.options.maxValue !== false && this._value > this.options.maxValue) {
////            //    this._value = this.options.maxValue
////            //}
////            //this.element.attr("value", this._format(this._value));
////            //this.element.attr("title", this.options.title || a.ui.numeric.globalization.defaultTooltip).wrap(a('<div class="   -content   ui-numeric" />'));
////            //if (this.options.showCurrency) {
////            //    this._createCurrency()
////            //}
////            //if (this.options.buttons) {
////            //    this._createButtons()
////            //}
////            //var j = this;
////            //this.element.bind({
////            //    keydown: function (l) {
////            //        return j._onKeyDown(l)
////            //    },
////            //    keyup: function (l) {
////            //        return j._onKeyUp(l)
////            //    },
////            //    change: function (l) {
////            //        return j._onChange(l)
////            //    }
////            //});
////            if (this.options.disabled || this.element.attr("disabled")) {
////                this._setOption("disabled", true)
////            }
////            a(window).bind("unload", function () {
////                j.destroy()
////            })
////        },
////        destroy: function () {
////            var j = this;
////            this.element.unbind({
////                keydown: function (k) {
////                    return j._onKeyDown(k)
////                },
////                keyup: function (k) {
////                    return j._onKeyUp(k)
////                },
////                change: function (k) {
////                    return j._onChange(k)
////                }
////            });
////        //    if (this.options.showCurrency) {
////        //        a("#" + this._name + "_currency").remove()
////        //    }
////        //    if (this.options.buttons) {
////        //        a("#" + this._name + "_buttons").remove()
////        //    }
////        //    this.element.unwrap();
////        //    a.Widget.prototype.destroy.call(this);
////        //    a(window).unbind("unload", function () {
////        //        j.destroy()
////        //    })
////      },
////        //_createCurrency: function () {
////        //    this.element.before(a("<div/>").attr("id", this._name + "_currency").addClass("ui-numeric-currency").html(this.options.currencySymbol || Number.globalization.defaultCurrencyFormat.symbol))
////        //},
////        //_createButtons: function () {
////        //    var k = a('<button type="button"></button>').attr("title", this.options.upButtonTitle || a.ui.numeric.globalization.defaultUpTooltip).bind({
////        //        keydown: function (p) {
////        //            m(p, false)
////        //        },
////        //        keyup: function () {
////        //            o()
////        //        },
////        //        mousedown: function (p) {
////        //            l(p, false)
////        //        },
////        //        mouseup: function () {
////        //            o()
////        //        }
////        //    }).button({
////        //        text: false,
////        //        label: "U",
////        //        icons: {
////        //            primary: this.options.upButtonIcon
////        //        }
////        //    });
////        //    var j = a('<button type="button"></button>').attr("title", this.options.downButtonTitle || a.ui.numeric.globalization.defaultDownTooltip).bind({
////        //        keydown: function (p) {
////        //            m(p, true)
////        //        },
////        //        keyup: function () {
////        //            o()
////        //        },
////        //        mousedown: function (p) {
////        //            l(p, true)
////        //        },
////        //        mouseup: function () {
////        //            o()
////        //        }
////        //    }).button({
////        //        text: false,
////        //        label: "D",
////        //        icons: {
////        //            primary: this.options.downButtonIcon
////        //        }
////        //    });
////        //    this._addButtons(k, j);
////        //    var n = this;

////        //    function m(p, q) {
////        //        if (p.which == 32 || p.which == 13) {
////        //            l(p, q);
////        //            p.target.focus()
////        //        }
////        //    }

////        //    function l(p, r) {
////        //        (r ? k : j).blur();
////        //        var q = n._getIncrement(p.ctrlKey, p.shiftKey);
////        //        n._adjustValueRecursive(r ? -q.value : q.value, q.type)
////        //    }

////        //    function o() {
////        //        clearTimeout(n._timer)
////        //    }
////        //},
////        //_addButtons: function (k, j) {
////        //    this.element.after(a("<div/>").attr("id", this._name + "_buttons").addClass("ui-numeric-buttons").append(k).append(j))
////        //},
////        //_setOption: function (j, k) {
////        //    switch (j) {
////        //        case "disabled":
////        //            this.element.parent()[k ? "addClass" : "removeClass"]("ui-numeric-disabled ui-state-disabled").attr("aria-disabled", k);
////        //            this._adjustmentFlag = true;
////        //            if (k) {
////        //                this.element.attr({
////        //                    disabled: "disabled",
////        //                    value: ""
////        //                })
////        //            } else {
////        //                this.element.removeAttr("disabled").attr("value", this._format(this._value))
////        //            }
////        //            this._adjustmentFlag = false;
////        //            if (this.options.buttons) {
////        //                a("#" + this._name + "_buttons button").button(k ? "disable" : "enable")
////        //            }
////        //            break;
////        //        case "emptyValue":
////        //            this.options.emptyValue = k;
////        //            this._setValue(this._value);
////        //            break;
////        //        case "minValue":
////        //            this.options.minValue = k === false ? false : b(k);
////        //            if (this.options.minValue !== false && this._value < this.options.minValue) {
////        //                this._setValue(this.options.minValue)
////        //            }
////        //            break;
////        //        case "maxValue":
////        //            this.options.maxValue = k === false ? false : b(k);
////        //            if (this.options.maxValue !== false && this._value > this.options.maxValue) {
////        //                this._setValue(this.options.maxValue)
////        //            }
////        //            break;
////        //        case "format":
////        //            this.options.format = k;
////        //            this._checkFormat();
////        //            this._setValue(this._value);
////        //            break;
////        //        case "title":
////        //            this.options.title = k || a.ui.numeric.globalization.defaultTooltip;
////        //            this.element.attr("title", this.options.title);
////        //            break;
////        //        case "showCurrency":
////        //            if (k && !this.options.showCurrency) {
////        //                this._createCurrency()
////        //            } else {
////        //                if (!k && this.options.showCurrency) {
////        //                    a("#" + this._name + "_currency").remove()
////        //                }
////        //            }
////        //            this.options.showCurrency = k;
////        //            break;
////        //        case "currencySymbol":
////        //            this.options.currencySymbol = k || Number.globalization.defaultCurrencyFormat.symbol;
////        //            if (this.options.showCurrency) {
////        //                a("#" + this._name + "_currency").html(this.options.currencySymbol)
////        //            }
////        //            break;
////        //        case "buttons":
////        //            if (k && !this.options.buttons) {
////        //                this._createButtons()
////        //            } else {
////        //                if (!k && this.options.buttons) {
////        //                    a("#" + this._name + "_buttons").remove()
////        //                }
////        //            }
////        //            this.options.buttons = k;
////        //            break;
////        //        case "upButtonIcon":
////        //            this.options.upButtonIcon = k;
////        //            if (this.options.buttons) {
////        //                a("#" + this._name + "_buttons").find("button:eq(0)").button("option", "icons", {
////        //                    primary: k
////        //                })
////        //            }
////        //            break;
////        //        case "upButtonTitle":
////        //            this.options.upButtonTitle = k || a.ui.numeric.globalization.defaultUpTooltip;
////        //            if (this.options.buttons) {
////        //                a("#" + this._name + "_buttons").find("button:eq(0)").attr("title", this.options.upButtonTitle)
////        //            }
////        //            break;
////        //        case "downButtonIcon":
////        //            this.options.downButtonIcon = k;
////        //            if (this.options.buttons) {
////        //                a("#" + this._name + "_buttons").find("button:eq(1)").button("option", "icons", {
////        //                    primary: k
////        //                })
////        //            }
////        //            break;
////        //        case "downButtonTitle":
////        //            this.options.downButtonTitle = k || a.ui.numeric.globalization.defaultDownTooltip;
////        //            if (this.options.buttons) {
////        //                a("#" + this._name + "_buttons").find("button:eq(1)").attr("title", this.options.downButtonTitle)
////        //            }
////        //            break;
////        //        default:
////        //            a.Widget.prototype._setOption.call(this, j, k);
////        //            break
////        //    }
////        //    return this
////        //},
            
////        _checkFormat: function () {
////            this.options.format = a.extend({}, Number.globalization.defaultFormat, typeof this.options.format === "string" ? {
////                format: this.options.format
////            } : this.options.format)
////        },
////        _getInputValue: function (k, j) {
////            k = k.replace(new RegExp(h(this.options.format.thousandsChar), "g"), "");
////            if (this.options.format.decimalChar !== ".") {
////                k = k.replace(new RegExp(h(this.options.format.decimalChar), "g"), ".")
////            }
////            return j ? b(k) : k
////        },
////        _setInputValue: function (j) {
////            this._adjustmentFlag = true;
////            this.element.attr("value", this._format(j)).change();
////            this._adjustmentFlag = false
////        },
////        _setValue: function (j) {
////            j = b(j);
////            if (this.options.minValue !== false && j < this.options.minValue) {
////                j = this.options.minValue
////            }
////            if (this.options.maxValue !== false && j > this.options.maxValue) {
////                j = this.options.maxValue
////            }
////            this._value = j;
////            if (!this.options.disabled) {
////                this._setInputValue(j)
////            }
////        },
////        _format: function (j) {
////            return isNaN(j) || (this.options.emptyValue !== false && j === this.options.emptyValue) ? "" : a.formatNumber(j, this.options.format)
////        },
////        _getIncrement: function (j, k) {
////            if (j) {
////                return {
////                    value: this.options.smallIncrement,
////                    type: 2
////                }
////            } else {
////                if (k) {
////                    return {
////                        value: this.options.largeIncrement,
////                        type: 3
////                    }
////                }
////            }
////            return {
////                value: this.options.increment,
////                type: 1
////            }
////        },
////        _adjustValue: function (j, k) {
////            if (this.options.disabled) {
////                return
////            }
////            this._setValue(a.isFunction(this.options.calc) ? this.options.calc(this._value, k, j < 0 ? 2 : 1) : this._value + j);
////            this.select()
////        },
////        _adjustValueRecursive: function (j, k) {
////            a.ui.numeric._current = this;
////            a.ui.numeric._timerCallback(j, k, true)
////        },
////        _onKeyDown: function (j) {
////            if (this.options.disabled) {
////                return
////            }
////            var l = String.fromCharCode(a.keyCodeToCharCode(j.which, j.shiftKey)).toLowerCase();
////            if (l === this.options.format.decimalChar.toLowerCase() || l == this.options.format.thousandsChar.toLowerCase()) {
////                this._keyDownFlag = true;
////                return
////            }
////            switch (j.which) {
////                case 109:
////                    this._keyDownFlag = true;
////                    return;
////                case 38:
////                case 40:
////                    if (this.options.keyboard) {
////                        var k = this._getIncrement(j.ctrlKey, j.shiftKey);
////                        this._adjustValue(j.which == 40 ? -k.value : k.value, k.type)
////                    }
////                    return;
////                case 33:
////                    if (this.options.keyboard) {
////                        this._adjustValue(this.options.largeIncrement, 3)
////                    }
////                    return;
////                case 34:
////                    if (this.options.keyboard) {
////                        this._adjustValue(-this.options.largeIncrement, 3)
////                    }
////                    return;
////                case 65:
////                case 67:
////                case 86:
////                case 88:
////                case 89:
////                case 90:
////                    if (j.ctrlKey) {
////                        return
////                    }
////                    break
////            }
////            if (c(j.which)) {
////                return
////            }
////            if (!d(j.which)) {
////                j.preventDefault();
////                j.stopPropagation();
////                return
////            }
////        },
////        _onKeyUp: function () {
////            var j = parseFloat(this._getInputValue(this.element.attr("value"), false));
////            if (!isNaN(j)) {
////                this._value = j
////            }
////            this._keyDownFlag = false
////        },
////        _onChange: function (j) {
////            if (!this._adjustmentFlag && !this._keyDownFlag) {
////                this._setValue(this._getInputValue(j.target.value), true)
////            }
////        },
////        value: function (j) {
////            if (j === undefined) {
////                return this._value
////            }
////            this._setValue(j);
////            return this
////        },
////        select: function () {
////            if (!this.options.disabled) {
////                this.element.select()
////            }
////            return this
////        }
////    });
////    a.ui.numeric.globalization = {
////        defaultTooltip: "Type a new value or use the buttons or keyboard arrow keys to change the value. Hold Ctrl or Shift for a smaller or larger increment, respectively.",
////        defaultUpTooltip: "Increment the value. Hold Ctrl or Shift for a smaller or larger increment, respectively.",
////        defaultDownTooltip: "Decrement the value. Hold Ctrl or Shift for a smaller or larger increment, respectively."
////    };
////    a.ui = a.ui || {};
////    a.ui.numeric._current = null;
////    a.ui.numeric._timerCallback = function (j, l, k) {
////        clearTimeout(a.ui.numeric._current._timer);
////        a.ui.numeric._current._adjustValue(j, l);
////        a.ui.numeric._current._timer = setTimeout("jQuery.ui.numeric._timerCallback(" + j + "," + l + ",false)", k ? 1000 : 50)
////    };
////    var f = {
////        48: 41,
////        49: 33,
////        50: 64,
////        51: 35,
////        52: 36,
////        53: 37,
////        54: 94,
////        55: 38,
////        56: 42,
////        57: 40,
////        59: 58,
////        61: 43,
////        186: 58,
////        187: 43,
////        188: 60,
////        109: 95,
////        189: 95,
////        190: 62,
////        191: 63,
////        192: 126,
////        219: 123,
////        220: 124,
////        221: 125,
////        222: 34
////    };
////    var e = {
////        59: 59,
////        61: 61,
////        96: 48,
////        97: 49,
////        98: 50,
////        99: 51,
////        100: 52,
////        101: 53,
////        102: 54,
////        103: 55,
////        104: 56,
////        105: 57,
////        106: 42,
////        107: 43,
////        109: 45,
////        110: 46,
////        111: 47,
////        186: 59,
////        187: 61,
////        188: 44,
////        109: 45,
////        189: 45,
////        190: 46,
////        191: 47,
////        192: 96,
////        219: 91,
////        220: 92,
////        221: 93,
////        222: 39
////    };
////    a.keyCodeToCharCode = function (j, k) {
////        if (j >= 48 && j <= 57 && !k) {
////            return j
////        }
////        if (j >= 65 && j <= 90) {
////            return k ? j : j + 32
////        }
////        if (j === 9 || j === 32) {
////            return j
////        }
////        return k ? f[j] : e[j]
////    };

////    function c(j) {
////        return (j <= 47 && j != 32) || (j >= 91 && j <= 95) || (j >= 112 && [188, 190, 191, 192, 219, 220, 221, 222].indexOf(j) == -1)
////    }

////    function d(j) {
////        return (j >= 48 && j <= 57) || (j >= 96 && j <= 105)
////    }

////    function b(j) {
////        if (typeof j !== "number") {
////            j = Number(j)
////        }
////        if (isNaN(j)) {
////            return 0
////        }
////        return j
////    }

////    function i(l, j) {
////        var m = "";
////        for (var k = 0; k < j; k++) {
////            m += l
////        }
////        return m
////    }
////    var g = new RegExp("(\\" + ["/", ".", "*", "+", "?", "|", "(", ")", "[", "]", "{", "}", "\\"].join("|\\") + ")", "g");

////    function h(j) {
////        return j.replace(g, "\\$1")
////    }
////    a.formatNumber = function (q, r) {
////        r = a.extend({}, Number.globalization.defaultFormat, typeof r === "string" ? {
////            format: r
////        } : r);
////        r.decimalChar = typeof r.decimalChar !== "string" || r.decimalChar.length <= 0 ? Number.globalization.defaultFormat.decimalChar : r.decimalChar;
////        r.thousandsChar = typeof r.thousandsChar !== "string" ? Number.globalization.defaultFormat.thousandsChar : r.thousandsChar;
////        if (r.decimalChar.length > 1) {
////            throw "NumberFormatException: Can not have multiple characters as the decimal character."
////        }
////        if (r.thousandsChar.length > 1) {
////            throw "NumberFormatException: Can not have multiple characters as the thousands separator."
////        }
////        if (typeof q !== "number") {
////            if (typeof q === "string") {
////                q = q.replace(new RegExp(h(r.thousandsChar), "g"), "");
////                if (r.decimalChar !== ".") {
////                    q = q.replace(new RegExp(h(r.decimalChar), "g"), ".")
////                }
////            }
////            q = Number(q);
////            if (isNaN(q)) {
////                q = 0
////            }
////        }
////        if (typeof r.format !== "string" || r.format.length <= 0) {
////            return r.decimalChar === "." ? q.toString() : q.toString().replace(/\./g, r.decimalChar)
////        }
////        var t = r.format.indexOf(".");
////        if (t >= 0 && r.format.indexOf(".", t + 1) >= 0) {
////            throw "NumberFormatException: Format string has multiple decimal characters."
////        }
////        var z = q.toString().replace(/-/g, ""),
////            s = r.format.replace(new RegExp("[^0#.]", "g"), ""),
////            A = z.indexOf(".") < 0 ? [z, ""] : z.split("."),
////            w = t < 0 ? [s, ""] : s.split(".");
////        if (parseInt(q) === 0) {
////            A[0] = w[0].indexOf("0") >= 0 ? Number.globalization.padChar : ""
////        }
////        var k;
////        if (r.format.indexOf(",") >= 0 && A[0].length > 3) {
////            var y = [],
////                l = A[0].length,
////                o = Math.floor(l / 3),
////                p = A[0].length % 3 || 3;
////            for (k = 0; k < l; k += p) {
////                if (k != 0) {
////                    p = 3
////                }
////                y[y.length] = A[0].substr(k, p);
////                o -= 1
////            }
////            A[0] = y.join(r.thousandsChar)
////        }
////        if (w[1].length > 0) {
////            if (A[1].length > 0) {
////                var v = w[1].split(""),
////                    u = A[1].split("");
////                for (k = 0; k < u.length; k++) {
////                    if (k >= v.length) {
////                        break
////                    }
////                    v[k] = u[k]
////                }
////                A[1] = v.join("").replace(/#/g, "")
////            } else {
////                var x = 0;
////                A[1] = "";
////                while (w[1].charAt(x) === "0") {
////                    A[1] += Number.globalization.padChar;
////                    x++
////                }
////            }
////        } else {
////            A[1] = ""
////        }
////        var B = (A[1].length <= 0) ? A[0] : A[0] + r.decimalChar + A[1];
////        if (q < 0) {
////            B = "-" + B
////        }
////        return r.format.replace(new RegExp("[0|#|,|.]+"), B)
////    };
////    Number.prototype.roundRight = function (j) {
////        j = b(j);
////        var k = Math.pow(10, j);
////        return Math.round(this * k) / k
////    };
////    Number.prototype.pad = function (k) {
////        k = b(k);
////        var m = String(this.valueOf()),
////            l = m.length;
////        if (l < k) {
////            for (var j = l; j < k; j++) {
////                m = Number.globalization.padChar + m
////            }
////        }
////        return m
////    };
////    Number.prototype.padRight = function (k) {
////        k = b(k);
////        var m = String(this.valueOf()),
////            l = m.length - (this % 1 > 0 ? 1 : 0);
////        if (l < k) {
////            for (var j = l; j < k; j++) {
////                m = m + Number.globalization.padChar
////            }
////        }
////        return m
////    };
////    Number.prototype.padDecimals = function (j) {
////        j = b(j);
////        var k = String(this.valueOf()).split(".");
////        if (k.length <= 0) {
////            return j > 0 ? Number.globalization.padChar + "." + i(Number.globalization.padChar, j) : Number.globalization.padChar
////        }
////        if (k.length == 1) {
////            return k[0] + (j > 0 ? "." + i(Number.globalization.padChar, j) : "")
////        }
////        return k[0] + (j > 0 ? "." + parseInt(k[1]).padRight(j) : "")
////    };
////    Number.prototype.getOrdinal = function () {
////        if (this > 100) {
////            return (this % 100).getOrdinal()
////        }
////        if (this >= 11 && this <= 19) {
////            return Number.globalization.ordinals.th
////        }
////        switch (this % 10) {
////            case 1:
////                return Number.globalization.ordinals.st;
////            case 2:
////                return Number.globalization.ordinals.nd;
////            case 3:
////                return Number.globalization.ordinals.rd
////        }
////        return Number.globalization.ordinals.th
////    };
////    Number.prototype.format = function (j) {
////        a.formatNumber(this, j)
////    };
////    Number.prototype.formatCurrency = function (j) {
////        j = a.extend({}, Number.globalization.defaultCurrencyFormat, j);
////        var k = Math.abs(this).format(j);
////        if (this < 0) {
////            return j.noParens ? "-" + j.symbol + k : "(" + j.symbol + k + ")"
////        }
////        return k
////    };
////    Number.globalization = {
////        defaultFormat: {
////            format: null,
////            decimalChar: ".",
////            thousandsChar: ","
////        },
////        defaultCurrencyFormat: {
////            symbol: "$",
////            noParens: false,
////            format: "#,##0.00",
////            decimalChar: ".",
////            thousandsChar: ","
////        },
////        padChar: "0",
////        ordinals: {
////            th: "th",
////            st: "st",
////            nd: "nd",
////            rd: "rd"
////        }
////    }
////})(jQuery);