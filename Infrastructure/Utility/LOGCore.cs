using System;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Threading;
using System.Collections.Generic;
using System.Web;

namespace Infrastructure.Utility
{
    /// <summary>
    /// ����־�洢�ķ�װ
    /// </summary>
    public class LOGCore
    {
        static LOGCore()
        {
            if (HttpContext.Current != null)
                LOGCore.LogPath = HttpContext.Current.Server.MapPath(Config.LogAbsolutePath);
            else
                LOGCore.LogPath = System.Windows.Forms.Application.StartupPath + Config.LogRelativePath;


        }

        #region ˽�б���
        private static string LineBreak = new string('-', 100);
        #endregion

        #region ��������

        /// <summary>
        /// �洢·��
        /// </summary>
        public static string LogPath = Config.LogRelativePath;

        /// <summary>
        /// �ļ����ɹ���
        /// </summary>
        public static ST SaveType = ST.Day;

        /// <summary>
        /// ��־�ļ�Ĭ��ǰ׺
        /// </summary>
        public static string DefaultPrefix =Config.LogDefaultPrefix;
        #endregion

        #region ��������


        /// <summary>
        /// ���ݿ��¼����ķ���
        /// </summary>
        /// <param name="st"></param>
        /// <param name="prefix"></param>
        /// <param name="e"></param>
        /// <param name="funName"></param>
        /// <param name="sqlText_proName"></param>
        /// <param name="parms"></param>
        public static void DBExp(ST st, string prefix, Exception e, string funName, string sqlText_proName, SqlParameter[] parms)
        {
            string fp = GetFilePath(st, prefix, "DB");
            StringBuilder sb = new StringBuilder();
            sb.Append("ʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("�쳣��" + e.Message + "\r\n");
            sb.Append("�������ƣ�" + funName + "\r\n");
            sb.Append("SQL���&�洢���̣�" + sqlText_proName + "\r\n");
            if (parms != null)
            {
                sb.Append(LineBreak.Insert(50, "�����б�") + "\r\n\r\n");
                foreach (var parm in parms)
                {
                    sb.Append(string.Format("{0}��{1}\r\n", parm.ParameterName, parm.Value.ToString()));
                }
                sb.Append("\r\n");  
            }

            sb.Append(LineBreak + "\r\n\r\n");

            WriteFile(fp, sb.ToString());

        }


        /// <summary>
        /// ��¼��־��Ϣ
        /// </summary>
        /// <param name="st">�ļ����ɹ���</param>
        /// <param name="prefix">�ļ�ǰ׺</param>
        /// <param name="Info">�Զ�����Ϣ</param>
        public static void Trace(ST st, string prefix, string Info, Dictionary<string, string> param)
        {
            string fp = GetFilePath(st, prefix, "INFO");
            StringBuilder sb = new StringBuilder();
            sb.Append("ʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("��Ϣ��" + Info + "\r\n");

            if (param != null)
            {
                foreach (KeyValuePair<string, string> kv in param)
                {
                    sb.Append(string.Format("{0}��{1} \r\n", kv.Key, kv.Value));
                }
            }
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());
        }

        /// <summary>
        /// 3����������д
        /// </summary>
        /// <param name="st"></param>
        /// <param name="prefix"></param>
        /// <param name="Info"></param>
        public static void Trace(ST st, string prefix, string Info)
        {
            Trace(st, prefix, Info, null);
        }
        /// <summary>
        /// ������Ϣ��¼ ����
        /// </summary>
        /// <param name="st"></param>
        /// <param name="prefix"></param>
        /// <param name="info"></param>
        /// <param name="param"></param>
        public static void Error(ST st, string prefix, string info, Dictionary<string, string> param)
        {
            string fp = GetFilePath(st, prefix, "ERR");
            StringBuilder sb = new StringBuilder();
            sb.Append("ʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("����" + info + "\r\n");
            if (param != null)
            {
                foreach (KeyValuePair<string, string> kv in param)
                {
                    sb.Append(string.Format("{0}��{1} \r\n", kv.Key, kv.Value));
                }
            }

            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());
        }

        /// <summary>
        /// ��¼�ճ����⡣
        /// </summary>
        /// <param name="st"></param>
        /// <param name="prefix"></param>
        /// <param name="info"></param>
        /// <param name="param"></param>
        public static void Excep(ST st, string prefix, Exception e, string info, Dictionary<string, string> param)
        {
            string fp = GetFilePath(st, prefix, "EX");
            StringBuilder sb = new StringBuilder();
            sb.Append("ʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            sb.Append("λ�ã�" + info + "\r\n");
            sb.Append("�쳣��" + e.ToString() + "\r\n");
            if (param != null)
            {
                foreach (KeyValuePair<string, string> kv in param)
                {
                    sb.Append(string.Format("{0}��{1} \r\n", kv.Key, kv.Value));
                }
            }

            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());
        }




        /// <summary>
        /// ��д������Dic��ֵ����
        /// </summary>
        /// <param name="st">�ļ����ɹ���</param>
        /// <param name="prefix">�ļ�ǰ׺</param>
        /// <param name="Info">�Զ�����Ϣ</param>
        /// <param name="param">�洢�ı����б�</param>
        public static void Debug(ST st, string prefix, Dictionary<string, string> param)
        {
            string fp = GetFilePath(st, prefix, "DEB");
            StringBuilder sb = new StringBuilder();
            sb.Append("ʱ�䣺" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            if (param != null)
            {
                foreach (KeyValuePair<string, string> kv in param)
                {
                    sb.Append(string.Format("{0}��{1} \r\n", kv.Key, kv.Value));
                }
            }
            sb.Append(LineBreak + "\r\n\r\n");
            WriteFile(fp, sb.ToString());
        }




        #endregion

        #region ˽�к���

        /// <summary>
        /// �����ļ�����·��
        /// </summary>
        /// <param name="st">�ļ����ɹ���</param>
        /// <param name="prefix">�ļ�ǰ׺</param>
        /// <param name="type">�ļ���������</param>
        /// <returns>�ļ�����·��</returns>
        public static string GetFilePath(ST st, string prefix, string type)
        {
            string ext = ".log";


            string path = LogPath.Replace("/", "\\");

            if (path.Substring(path.Length - 1) != "\\")
            {
                path += "\\";
            }


            if (prefix.IndexOf(@"\") > -1)//���prefix �����ǵ���Ŀ¼ʹ�� [sss\111\222 ]
            {

                path = path + prefix;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += type + "_";
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += prefix + "_" + type;
            }


            switch (st)
            {
                case ST.Fixed:
                    path += ext;
                    break;
                case ST.Day:
                    path += DateTime.Now.ToString("yyyyMMdd") + ext;
                    break;
                case ST.Hour:
                    path += DateTime.Now.ToString("yyyyMMddHH") + ext;
                    break;
            }
            return (path);
        }

        /// <summary>
        /// ������object[]���ɵ��ַ���
        /// </summary>
        /// <param name="param">object����</param>
        /// <returns>���Ӻõ��ַ���</returns>
        private static string GetParamList(object[] param)
        {
            if (param == null) return string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (object p in param)
            {
                sb.Append("(" + ((p != null) ? p.ToString() : "null") + ")");
                if (p != param[param.Length - 1])
                {
                    sb.Append(",");
                }
            }
            return (sb.ToString());
        }

        /// <summary>
        /// ������־����д���ļ�
        /// </summary>
        /// <param name="FilePath">�ļ�·��</param>
        /// <param name="Content">��־����</param>
        private static void WriteFile(string FilePath, string Content)
        {
            Type type = typeof(LOGCore);
            try
            {
                if (!Monitor.TryEnter(type))
                {
                    Monitor.Enter(type);
                    return;
                }
                using (StreamWriter writer = new StreamWriter(FilePath, true, System.Text.Encoding.Default))
                {
                    writer.Write(Content);
                    writer.Close();
                }
            }
            catch (System.IO.IOException e)
            {
                throw (e);
            }
            catch { }
            finally
            {
                Monitor.Exit(type);
            }
        }

        #endregion

        #region ö��
        /// <summary>
        /// �ļ����ɹ���
        /// </summary>
        public enum ST : int
        {
            /// <summary>
            /// �̶��ļ���
            /// </summary>
            Fixed = 1,
            /// <summary>
            /// ���������µ��ļ�
            /// </summary>
            Day = 2,
            /// <summary>
            /// ��Сʱ�����µ��ļ�
            /// </summary>
            Hour = 3
        }
        #endregion

    }
}
