using System.ComponentModel.DataAnnotations.Schema;

namespace QTF.Data.Models
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public string Value { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public bool IsCorrect { get; set; }
    }
}