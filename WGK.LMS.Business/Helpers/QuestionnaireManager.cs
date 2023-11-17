using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Business.Interfaces.Questionnaires;
using WGK.LMS.Data.Interfaces;

namespace WGK.LMS.Business.Helpers
{
    public class QuestionnaireManager : IQuestionnaireManager
    {

        private readonly WGK.LMS.Data.Interfaces.IQuestionnaireRepository iQuestionnaireRepository;

        public QuestionnaireManager(IQuestionnaireRepository pQuestionnaireRepository)
        {
            iQuestionnaireRepository = pQuestionnaireRepository;
        }

        

    }
}
