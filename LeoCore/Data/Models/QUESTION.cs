namespace LeoCore.Data.Models
{
    public class QUESTION
    {
        public int ID { get; set; }

        public string TEXT { get; set; }

        public string TYPE_ANSWER { get; set; }

        public int? VALUE_MIN { get; set; }

        public int? VALUE_MAX { get; set; }

        public string INFO { get; set; }

        public DateOnly DATE_VALID_START { get; set; }

        public DateOnly? DATE_VALID_END { get; set; }

        public virtual ICollection<PERSON_QUESTIONNAIRE_ANSWER> PERSON_QUESTIONNAIRE_ANSWERs { get; set; } = new List<PERSON_QUESTIONNAIRE_ANSWER>();

        public virtual ICollection<QUESTIONNAIRE_QUESTION> QUESTIONNAIRE_QUESTIONs { get; set; } = new List<QUESTIONNAIRE_QUESTION>();
    }
}
