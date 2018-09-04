using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace Infrastructure.Utility
{
    public class RegexCore
    {
        #region Replace

        public static MatchCollection Matches(string input, string pattern)
        {
            Regex rex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.ExplicitCapture);

            MatchCollection mcs = rex.Matches(input);

            return mcs;
        }

        /// <summary>
        /// �õ��ַ���
        /// </summary>
        /// <param name="input">Դ</param>
        /// <param name="regStart">ǰ�ַ�</param>
        /// <param name="regEnd">���ַ�</param>
        /// <returns></returns>
        public static string GetMatches(string input, string pattern)
        {
            MatchCollection matches = Matches(input, pattern);
            string results = "";
            if (matches.Count > 0)
            {
                results = matches[0].Groups["content"].Value;
            }
            else
            {
                results = "";
            }
            return results;
        }



        /// <summary>
        /// �õ��ַ���
        /// </summary>
        /// <param name="input">Դ</param>
        /// <param name="regStart">ǰ�ַ�</param>
        /// <param name="regEnd">���ַ�</param>
        /// <returns></returns>
        public static string GetMatches(string input, string regStart, string regEnd)
        {
            string regString = string.Format("{0}(?<getcontent>[\\s|\\S]+?){1}", regStart, regEnd);
            MatchCollection matches = Matches(input, regString);
            string results = "";
            if (matches.Count > 0)
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    results = results + "|" + matches[i].Groups["getcontent"].Value;
                }
                results = results.Substring(1);
            }
            else
            {
                results = "";
            }
            return results;
        }

        //�Ƿ�ƥ��
        public static Boolean IsMatch(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        public static string Replace(string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        /// <summary>
        /// �滻Html ��:��link���� ����link��
        /// </summary>
        /// <param name="label">��ǩ����,��:a,br,table,title��;</param>
        /// <param name="input">Դ�ִ�</param>
        /// <param name="replacement">�滻��Ŀ���ִ�</param>
        /// <returns></returns>
        public static string ReplaceHtml(string label, string input, string replacement)
        {
            string pattern = string.Format("(<{0}[^>]*>)|</[^>]*{0}>", label);

            string res = Replace(input, pattern, replacement);

            return res.Replace("&nbsp;", " ");
        }

        /// <summary>
        /// �滻HTML��ǩ��������ǩ������,��:��link��XXX����XX ����link��
        /// </summary>
        /// <param name="label">��ǩ����,��:a,br,table,title��;</param>
        /// <param name="input">Դ�ִ�</param>
        /// <param name="replacement">�滻��Ŀ���ִ�</param>
        /// <returns></returns>
        public static string ReplaceHtmlLabel(string label, string input, string replacement)
        {
            string pattern = string.Format("(<{0}[^>]*>).*?</[^>]*{0}>", label);

            string res = Replace(input, pattern, replacement);

            return res;
        }

        public static bool IsHtml(string input)
        {
            return Regex.IsMatch(input, "<[^>]+>");
        }
        #endregion

        #region MatchsHtml notes

        public static HtmlLinkLabel[] MatchLinks(string input)
        {
            string pattern = "<a[^>]+href=(\"(?<href>[^\"]*)\"|'(?<href>[^']*)'|(?<href>[^\\s\\n>]*))[^>]*>(?<innerText>.*?)</a>";

            MatchCollection mc = Matches(input, pattern);

            HtmlLinkLabel[] arrLinks = new HtmlLinkLabel[mc.Count];

            for (int i = 0; i < mc.Count; i++)
            {
                Match m = mc[i];

                HtmlLinkLabel link = new HtmlLinkLabel();

                link.Href = m.Groups["href"].Value;

                link.InnerText = m.Groups["innerText"].Value;

                link.LabelText = m.Value;

                arrLinks[i] = link;
            }

            return arrLinks;
        }

        #endregion
    }

    public class HtmlLinkLabel
    {
        public string LabelText = string.Empty;
        public string InnerText = string.Empty;
        public string Href = string.Empty;
    }

}
