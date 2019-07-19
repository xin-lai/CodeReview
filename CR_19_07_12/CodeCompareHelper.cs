using CR_19_07_12.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CR_19_07_12
{
    public class CodeCompareHelper
    {
        /// <summary>
        /// 执行Demo
        /// </summary>
        public void ExecDemoCode()
        {
            #region 选项拆分代码比较
            //批量生成选项测试数据
            var dataList = Enumerable.Range(1, 10000).Select(p => $"A.选项{p} B.选项{p + 1} C.选项{p + 2} D.选项{p + 3}").ToList();
            var code = new DemoCode();
            //执行代码比较
            CodeCompare(() =>
            {
                foreach (var item in dataList)
                {
                    code.Old_SpliteChoiceOption(item);
                }
                return "1.原始代码";
            },
            () =>
            {
                foreach (var item in dataList)
                {
                    code.New_SpliteChoiceOption(item);
                }
                return "1.新代码";
            }
            );
            #endregion

            //执行代码比较
            CodeCompare(() =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    var list = new List<QuestionOptionlnfo>();
                    for (int j = 0; j < 4; j++)
                    {
                        list.Add(new QuestionOptionlnfo());
                    }
                }
                return "2.不指定长度";
            },
            () =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    var list = new List<QuestionOptionlnfo>(4);
                    for (int j = 0; j < 4; j++)
                    {
                        list.Add(new QuestionOptionlnfo());
                    }
                }
                return "2.指定长度";
            }
            );

            var data = Enumerable.Range(1, 10000000);
            IList<int> demoList = null;
            HashSet<int> demoHashset = null;
            //执行代码比较
            CodeCompare(() =>
            {
                demoList = data.ToList();
                return "3.ToList";
            },
            () =>
            {
                demoHashset = data.ToHashSet();
                return "3.ToHashSet";
            }
            );

            //执行代码比较
            CodeCompare(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    demoList.FirstOrDefault(p => p == i);
                }
                return "4.List FirstOrDefault";
            },
            () =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    demoList.Where(p => p == i).FirstOrDefault();
                }
                return "4.List Where FirstOrDefault";
            }
            );
        }
        public void CodeCompare(params Func<string>[] funcs)
        {
            foreach (var func in funcs)
            {
                var sw = Stopwatch.StartNew();
                var msg = func();
                sw.Stop();
                Console.WriteLine($"【{msg}】执行时间：{sw.ElapsedMilliseconds}毫秒");
            }
        }
    }
}
