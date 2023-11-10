//$('#TRAININGDetail_NC_EVD_YN,#LocAndersSP,#NCoCGC,#TRAININGDetail.EV_ANDERS_YN,#TRAININGDetail_EV_PERS_YN,#TRAININGDetail_EV_ANDERS_YN, #TRAININGDetail_NC_KATZ_YN,#TRAININGDetail.EV_PERS_YN, #TRAININGDetail_NC_NOMC_YN,#TRAININGDetail_EV_WW_YN, #TRAININGDetail_NC_THUISZORG_YN, #TRAININGDetail_NC_VVD_YN, #TRAININGDetail_NC_ROL_YN').css("display", "none");

var TotaleTijd = 0;
var val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, val6 = 0, val7 = 0, val8 = 0, val9 = 0, val10 = 0, val11 = 0, val12 = 0;
var Opgebruikteuren = 0 + val1 + val2 + val3 + val4 + val5 + val6 + val7 + val8 + val9 + val10 + val11 + val12;
$("#NCoCG").hide();
$('#NCCGSwitch').css("display", "none");
$('#EBTSwitch').css("display", "none");


if (document.getElementById("TRAININGDetail_ID").value == 0 || document.getElementById("TRAININGDetail_ID").value == null || document.getElementById("TRAININGDetail_ID").value == '0') {
    $('#NCCGSwitch').css("display", "block");
    $('#EBTSwitch').css("display", "block");
    $('#TRAININGDetail_NOMENCL_CONV_YN').css("display", "none");

    $("#NCCGSwitch").change(function () {


        if ($(this).prop('checked') == true) {
            ProgressAanpassen(0);
            document.getElementById('TRAININGDetail_NOMENCL_CONV_YN').value = "J";
            $("#NCoCG").show();
          

        } else {

            val1 = 0, val2 = 0, val3 = 0, val4 = 0, val5 = 0, val6 = 0;

            ProgressAanpassen(0);
            $('.NCoCGC').find('input').val(0);
            $('.NCoCGC').find('input:text').val("");
            $("#NCoCG").hide();
        }

    })

   $('#TRAININGDetail_EV_YN').css("display", "none");

    $("#EBTSwitch").change(function () {


        if ($(this).prop('checked') == true) {
            document.getElementById('TRAININGDetail_EV_YN').value = "J";
            ProgressAanpassen(0);
            $("#WCHDV").show();
            $("#ZorgGerelateerd").show();

        } else {
            val7 = 0, val8 = 0, val9 = 0, val10 = 0, val11 = 0, val12 = 0;
            ProgressAanpassen(0);
            $('.WCHDVC').find('input:text').val('');
            $('.WCHDVC').find('input').val("");
            hideEVZGSRD();
            hideEVZGSRD();
            hideTDEVZG();
            $('.colapseZG').hide();
            $("#ZGDT").hide();
            $("#ZorgGerelateerd").hide();
            document.getElementById("TRAININGDetail_EV_ZG_YN").value = "N";
            document.getElementById("TRAININGDetail_EV_ZG_DURATION").value = 0;
            $("#WCHDV").hide();
            hideAWS();
        }

    })


    $('#TRAININGDetail_DURATION_OVERALL').on('change', function () {
        TotaleTijd = timeStringToFloat(document.getElementById("TRAININGDetail_DURATION_OVERALL").value);
    });

    $('#TRAININGDetail_TRAINER_INT_EXT').on('change', function () {
        if (this.value === 'EXT_INT_BOTH' || this.value === 'EXT_INT_EXT') {
            $("#TRAINER_INT_EXT_MAIL").show();
        } else {
            $("#TRAINER_INT_EXT_MAIL").hide();
            document.getElementById("TRAINER_INT_EXT_MAIL").value = "";
        }
    });

    $('#TRAININGDetail_NOMENCL_CONV_YN').on('change', function () {
        if (this.value === 'J') {
            $("#NCoCG").show();
            ProgressAanpassen(0);
        } else {
            ProgressAanpassen(0);
            $("NCoCG > input").val("");
            $("#NCoCG").hide();
        }
    });



    function timeStringToFloat(time) {
        var hoursMinutes = time.split(/[.:]/);
        var hours = parseInt(hoursMinutes[0], 10);
        var minutes = hoursMinutes[1] ? parseInt(hoursMinutes[1], 10) : 0;

        return Math.round(((hours + minutes / 60) + Number.EPSILON) * 100) / 100;
    }

    function Error(TextError) {
        document.getElementById("AlertBalk").innerHTML = TextError;
        window.scrollTo(0, 0);
        setTimeout(function () {
            $("#AlertBalk").hide();
        }, 10000);
        $("#AlertBalk").show();
    };
    function minTommss(minutes) {
        var sign = minutes < 0 ? "-" : "";
        var min = Math.floor(Math.abs(minutes));
        var sec = Math.floor((Math.abs(minutes) * 60) % 60);
        return sign + (min < 10 ? "0" : "") + min + ":" + (sec < 10 ? "0" : "") + sec;
    }
    function ProgressAanpassen(field) {
        Opgebruikteuren = 0 + val1 + val2 + val3 + val4 + val5 + val6 + val7 + val8 + val9 + val10 + val11 + val12;

        var elem = document.getElementById("myBar");
        var elem2 = document.getElementById("myBar2");


        var width = Math.round((((Opgebruikteuren / TotaleTijd) * 100) + Number.EPSILON) * 100) / 100;
        if (width <= 100) {
            elem.style.backgroundColor = "rgb(255, 217, 35)";
            elem2.style.backgroundColor = "rgb(255, 217, 35)";
            elem.style.width = width + "%";
            elem.innerHTML = minTommss(TotaleTijd - Opgebruikteuren) + ' uren beschikbaar';
            elem2.style.width = width + "%";
            elem2.innerHTML = minTommss(TotaleTijd - Opgebruikteuren) + ' uren beschikbaar';
        }
        else {
            if (field != 0) {
                document.getElementById(field).value = 0;
            }

            $('.collapseAlg').collapse('show')
            Error("Je hebt meer uren ingegeven dan je hebt aangevraagd (Zie Algemeen)!");
        //    document.getElementById("TRAININGDetail_DURATION_OVERALL").focus();
            elem.style.width = 100 + "%";
            elem.style.backgroundColor = "rgb(255, 0, 0)";
            elem.innerHTML = "";
            elem2.style.width = 0 + "%";
            elem2.style.backgroundColor = "rgb(255, 0, 0)";
            elem2.innerHTML = "";
        }
    }

    $('#TRAININGDetail_EV_ZG_YN').on('change', function () {

        if (this.value === 'J') {
            ProgressAanpassen(0);
            $("#ZGDT").show();
            $("#ZorgGerelateerd").show();
            $('.colapseZG').show();


        } else {
            ProgressAanpassen(0);
            document.getElementById("TRAININGDetail_EV_ZG_SUBJ").value = 0;
            hideEVZGSRD();
            hideEVZGSRD();
            hideTDEVZG();
            $('.colapseZG').hide();
            $("#ZGDT").hide();
            $("#ZorgGerelateerd").hide();
            document.getElementById("TRAININGDetail_EV_ZG_DURATION").value = 0;
        }
    });

    $('#TRAININGDetail_EV_PERS_DURATION').on('change', function () {

        if (this.value != null && this.value != "" && this.value != '00:00') {
            document.getElementById("TRAININGDetail_EV_PERS_YN").checked = true;
            val9 = timeStringToFloat(document.getElementById("TRAININGDetail_EV_PERS_DURATION").value);
            ProgressAanpassen("TRAININGDetail_EV_PERS_DURATION");
        } else {
            document.getElementById("TRAININGDetail_EV_PERS_YN").checked = false;
        }
    });

    /*Hidden checkbox aan of uitvinken als onderwerp of tijd is ingevuld*/

    $('#TRAININGDetail_LOCATION').on('change', function () {

        if (this.value === "LOC_ANDERS") {
            $("#LocAndersSP").show()
        }
        else {
            document.getElementById(" TRAININGDetail_LOCATION_ANDERS").value = "";

            $("#LocAndersSP").hide()
        }
    });
    $('#TRAININGDetail.EV_ANDERS_DURATION').on('change', function () {

        if (this.value != null && this.value != "" && this.value != '00:00') {
            document.getElementById("TRAININGDetail.EV_ANDERS_YN").checked = true;
            val8 = timeStringToFloat(document.getElementById("TRAININGDetail.EV_ANDERS_DURATION").value);
            ProgressAanpassen("TRAININGDetail.EV_ANDERS_DURATION");
        } else {
            document.getElementById("TRAININGDetail.EV_ANDERS_YN").checked = false;
        }
    });

    $('#TRAININGDetail_EV_ANDERS_DURATION').on('change', function () {

        if (this.value != null && this.value != "" && this.value != '00:00') {
            document.getElementById("TRAININGDetail_EV_ANDERS_YN").checked = true;
            val11 = timeStringToFloat(document.getElementById("TRAININGDetail_EV_ANDERS_DURATION").value);
            ProgressAanpassen("TRAININGDetail_EV_ANDERS_DURATION");
        } else {
            document.getElementById("TRAININGDetail_EV_ANDERS_YN").checked = false;
        }
    });
    $('#TRAININGDetail_EV_AWS_DURATION').on('change', function () {

        if (this.value != null && this.value != "" && this.value != '00:00') {
            // document.getElementById("TRAININGDetail_EV_AWS_YN").checked = true;
            val10 = timeStringToFloat(document.getElementById("TRAININGDetail_EV_AWS_DURATION").value);
            ProgressAanpassen("TRAININGDetail_EV_AWS_DURATION");
            $(".colapseAWSB").show();
            $("#AWSBOX").show();

        } else {
            // document.getElementById("TRAININGDetail_EV_AWS_YN").checked = false;
            document.getElementById("TRAININGDetail_EV_AWS_SUBJ").checked = false;
            hideICC();
            hideOPP();
            hideAWS();
        }
    });

    $('#TRAININGDetail_NC_EVD_DURATION').on('change', function () {

        if (this.value != null && this.value != "" && this.value != '00:00') {
            document.getElementById("TRAININGDetail_NC_EVD_YN").checked = true;
            val1 = timeStringToFloat(document.getElementById("TRAININGDetail_NC_EVD_DURATION").value);
            ProgressAanpassen("TRAININGDetail_NC_EVD_DURATION");
        } else {
            document.getElementById("TRAININGDetail_NC_EVD_YN").checked = false;
        }
    });

    $('#TRAININGDetail_EV_WW_DURATION').on('change', function () {

        if (this.value != null && this.value != "" && this.value != '00:00') {
            document.getElementById("TRAININGDetail_EV_WW_YN").checked = true;
            val7 = timeStringToFloat(document.getElementById("TRAININGDetail_EV_WW_DURATION").value);
            ProgressAanpassen("TRAININGDetail_EV_WW_DURATION");
        } else {
            document.getElementById("TRAININGDetail_EV_WW_YN").checked = false;
        }
    });

    $('#TRAININGDetail_NC_KATZ_DURATION').on('change', function () {

        if (this.value != null && this.value != "" && this.value != '00:00') {
            document.getElementById("TRAININGDetail_NC_KATZ_YN").checked = true;
            val2 = timeStringToFloat(document.getElementById("TRAININGDetail_NC_KATZ_DURATION").value);
            ProgressAanpassen("TRAININGDetail_NC_KATZ_DURATION");
        } else {
            document.getElementById("TRAININGDetail_NC_KATZ_YN").checked = false;
        }
    });

    $('#TRAININGDetail_NC_NOMC_DURATION').on('change', function () {

        if (this.value != null && this.value != "" && this.value != '00:00') {
            document.getElementById("TRAININGDetail_NC_NOMC_YN").checked = true;
            val3 = timeStringToFloat(document.getElementById("TRAININGDetail_NC_NOMC_DURATION").value);
            ProgressAanpassen("TRAININGDetail_NC_NOMC_DURATION");
        } else {
            document.getElementById("TRAININGDetail_NC_NOMC_YN").checked = false;
        }
    });

    $('#TRAININGDetail_NC_THUISZORG_DURATION').on('change', function () {

        if (this.value != null && this.value != "" && this.value != '00:00') {
            document.getElementById("TRAININGDetail_NC_THUISZORG_YN").checked = true;
            val4 = timeStringToFloat(document.getElementById("TRAININGDetail_NC_THUISZORG_DURATION").value);
            ProgressAanpassen("TRAININGDetail_NC_THUISZORG_DURATION");
        } else {
            document.getElementById("TRAININGDetail_NC_THUISZORG_YN").checked = false;
        }
    });

    $('#TRAININGDetail_NC_VVD_DURATION').on('change', function () {
        console.log(this.value);
        if (this.value != null && this.value != "" && this.value != '00:00') {
            document.getElementById("TRAININGDetail_NC_VVD_YN").checked = true;
            val5 = timeStringToFloat(document.getElementById("TRAININGDetail_NC_VVD_DURATION").value);
            ProgressAanpassen("TRAININGDetail_NC_VVD_DURATION");
        } else {
            document.getElementById("TRAININGDetail_NC_VVD_YN").checked = false;
        }
    });
    $('#TRAININGDetail_NC_ROL_DURATION').on('change', function () {

        if (this.value != null && this.value != "" && this.value != '00:00' && this.value != '00:00') {
            document.getElementById("TRAININGDetail_NC_ROL_YN").checked = true;
            val6 = timeStringToFloat(document.getElementById("TRAININGDetail_NC_ROL_DURATION").value);
            ProgressAanpassen("TRAININGDetail_NC_ROL_DURATION");
        } else {
            document.getElementById("TRAININGDetail_NC_ROL_YN").checked = false;
        }
    });

    $('#TRAININGDetail_EV_ZG_DURATION').on('change', function () {

        if (this.value != null && this.value != "" && this.value != '00:00') {
            //document.getElementById("TRAININGDetail_NC_ZG_YN").checked = true;
            val12 = timeStringToFloat(document.getElementById("TRAININGDetail_EV_ZG_DURATION").value);
            ProgressAanpassen("TRAININGDetail_EV_ZG_DURATION");
        } else {
            hideTDEVZG()
        }
    });

    var ElementRefDom = document.querySelectorAll("input[value=ZG_SUBJ_REFDOM]");
    $(ElementRefDom).on('change', function () {
        if ($(ElementRefDom).is(':checked')) {
            $("#EVZGSRD").show();
        } else {
            hideEVZGSRD();
        }
    });
    var ElementRefCOMPL = document.querySelectorAll("input[value=ZG_SUBJ_SAM]");
    $(ElementRefCOMPL).on('change', function () {
        if ($(ElementRefCOMPL).is(':checked')) {
            $("#EVZGSCOMPL").show();
        } else {
            hideEVZGSCOMPL();
        }
    });

    var ElementRefICC = document.querySelectorAll("input[value=AWS_INCO]");
    $(ElementRefICC).on('change', function () {
        if ($(ElementRefICC).is(':checked')) {
            $("#ICC").show();
        } else {
            hideICC();
        }
    });
    var ElementRefOPP = document.querySelectorAll("input[value=AWS_ONCO]");
    $(ElementRefOPP).on('change', function () {
        if ($(ElementRefOPP).is(':checked')) {
            $("#OPP").show();
        } else {
            hideOPP();
        }
    });
} else {
    $("#OPP").show();
    $("#ICC").show();
    $("#EVZGSCOMPL").show();
    $("#EVZGSRD").show();
    $("#AWSBOX").show();

    $("#WCHDV").show();
    $("#NCoCG").show();
    $("#TRAINER_INT_EXT_MAIL").show();
    $("#ZGDT").show();
    $("#ZorgGerelateerd").show();

}

function hideOPP() {
    $("#OPP").hide();
    $('#TRAININGDetail_EV_AWS_ONCO_SUBJ input:checkbox').prop('checked', false);
}

function hideICC() {
    $('#TRAININGDetail_EV_AWS_INCO_SUBJ input:checkbox').prop('checked', false);
    $("#ICC").hide();
}
function hideEVZGSCOMPL() {
    $('#TRAININGDetail_EV_ZG_COMPL input:checkbox').prop('checked', false);
    $("#EVZGSCOMPL").hide();
}
function hideEVZGSRD() {
    $('#TRAININGDetail_EV_ZG_REFDOM input:checkbox').prop('checked', false);
    $("#EVZGSRD").hide();
}
function hideTDEVZG() {
    $('#TRAININGDetail_EV_ZG_SUBJ input:checkbox').prop('checked', false);
    $('#TRAININGDetail_EV_ZG_REFDOM input:checkbox').prop('checked', false);
    $('#TRAININGDetail_EV_ZG_COMPL input:checkbox').prop('checked', false);
    //document.getElementById("TRAININGDetail_EV_ZG_YN").checked = false;
}
function hideAWS() {
    // document.getElementById("TRAININGDetail_EV_AWS_YN").checked = false;
    document.getElementById("TRAININGDetail_EV_AWS_SUBJ").checked = false;
    $('#TRAININGDetail_EV_AWS_SUBJ input:checkbox').prop('checked', false);
    $('#TRAININGDetail_EV_AWS_ONCO_SUBJ input:checkbox').prop('checked', false);
    $('#TRAININGDetail_EV_AWS_INCO_SUBJ input:checkbox').prop('checked', false);
    $(".colapseAWSB").hide();
    $("#AWSBOX").hide();
}