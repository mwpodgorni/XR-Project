using System.Collections.Generic;

namespace UnityTemplateProjects
{
    [System.Serializable]
    public class Question
    {
        public string question;
        public Answer[] answers;

        // public Question(string question, List<Answer> answers)
        // {
        //     this.question = question;
        //     this.answers = answers;
        // }
        //
        // public string Question1
        // {
        //     get => question;
        //     set => question = value;
        // }
        //
        // public List<Answer> Answers
        // {
        //     get => answers;
        //     set => answers = value;
        // }
    }
}