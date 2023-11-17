using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEO.Business.Interfaces.Questionnaires;
using LeoCore.Data;

namespace LEO.Business.Helpers
{
    public class QuestionnaireManager : IQuestionnaireManager
    {

        private readonly LeoCore.Data.IQuestionnaireRepository iQuestionnaireRepository;

        public QuestionnaireManager(IQuestionnaireRepository pQuestionnaireRepository)
        {
            iQuestionnaireRepository = pQuestionnaireRepository;
        }

        

    }
}
