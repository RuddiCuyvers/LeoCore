

document.addEventListener('DOMContentLoaded', function () {
    
   

    let sr1 = document.querySelectorAll('select');
    
    for (var i = 0; i < sr1.length; i++) {
        RatingKruisjes("Person_QuestionnaireDetail_PERSON_QUESTIONNAIRE_ANSWERDetails_"+i+"__ANSWER_TEXT");
    }



   

}());

function RatingKruisjes(element) {
    if ($('#' + element).is(':enabled')) {
       
   
    //How many crosses do we need
        var len = $('#' + element + ' option').length;
        if (len >3) {
            var html = "";
            //Dynamic create Crosses
            for (var y = 1; y <= len - 1; y++) {
                html += "<i class=\"" + element + "-cross cross-" + element + "" + y + "\" data-cross=\"" + y + "\"><\/i>"
            }
            //place crosses before the dropdowns
            document.getElementById(element).insertAdjacentHTML("beforebegin", html)
            //select all crosses for making event listner
            let sr = document.querySelectorAll('.' + element + '-cross');
            let i = 0;
            //Make dropdown hidden
             $('#' + element).css("display", "none");

            //loop through crosss
            while (i < len - 1) {

                //attach click event
                sr[i].addEventListener('click', function () {
                    //current cross
                    let cs = parseInt(this.getAttribute("data-cross"));

                    //output current clicked cross value
                    if (len <= 6) {
                        document.querySelector('#' + element).value = "SCORE5/" + cs;
                    }
                    if (len >= 9) {
                        document.querySelector('#' + element).value = "SCORE10/" + cs;
                    }

                    /*our first loop to set the class on preceding cross elements*/
                    let pre = cs; //set the current cross value
                    //loop through and set the active class on preceding crosss
                    while (1 <= pre) {
                        //check if the classlist contains the active class, if not, add the class
                        if (!document.querySelector('.cross-' + element + pre).classList.contains('is-active')) {
                            document.querySelector('.cross-' + element + pre).classList.add('is-active');
                        }
                        //decrement our current index
                        --pre;
                    }//end of first loop

                    /*our second loop to unset the class on succeeding cross elements*/
                    //loop through and unset the active class, skipping the current cross
                    let succ = cs + 1;
                    while (len - 1 >= succ) {
                        //check if the classlist contains the active class, if yes, remove the class
                        if (document.querySelector('.cross-' + element + succ).classList.contains('is-active')) {
                            document.querySelector('.cross-' + element + succ).classList.remove('is-active');
                        }
                        //increment current index
                        ++succ;
                    }

                })//end of click event
                i++;
            }//end of while loop
        }
   
    }
} 






