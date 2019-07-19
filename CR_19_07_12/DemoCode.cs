using CR_19_07_12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CR_19_07_12
{
    public class DemoCode
    {
        public IList<QuestionOptionlnfo> Old_SpliteChoiceOption(string cellValue)
        {
            List<QuestionOptionlnfo> ltQuestionOption = new List<QuestionOptionlnfo>();
            var code = new string[8] { "A", "B", "C", "D", "E", "F", "G", "H" };
            var reg = @"(?<=[A-Z].)\S+(?=[A-Z].)?";
            var data = Regex.Matches(cellValue, reg);
            for (int i = 0; i < data.Count; i++)
            {
                QuestionOptionlnfo questionlnfo = new QuestionOptionlnfo();
                questionlnfo.Code = code[i];
                questionlnfo.Content = data[i].Value;
                ltQuestionOption.Add(questionlnfo);
            }
            return ltQuestionOption;
        }

       
        public IEnumerable<QuestionOptionlnfo> New_SpliteChoiceOption(string cellValue)
        {
            var list = cellValue?.Trim().Split(' ')
                      .Where(p => !string.IsNullOrWhiteSpace(p) && Regex.IsMatch(p, @"[A-Z].")).Select(p => new QuestionOptionlnfo()
                      {
                          Code = p.Substring(0, 1),
                          Content = p.Substring(2)
                      }).ToList();
            if (list == null || list.Count == 0)
            {
                throw new UserFriendlyException(message: $"选项不符合规范:{cellValue}");
            }
            return list;
        }
    }
}
