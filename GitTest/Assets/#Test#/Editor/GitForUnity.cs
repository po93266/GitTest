
/*****************************************************
* 版权声明：上海卓越睿新数码科技有限公司，保留所有版权
* 文件名称：GitForUnity.cs
* 文件版本：1.0
* 创建时间：2020/08/17 03:06:31
* 作者名称：WangGuanNan
* 文件描述：

*****************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;

namespace WisdomTree.WangGuanNan.Function
{
    /// <summary>
    /// 
    /// </summary>
    public class GitForUnity
    {
        static string GIT_BASE = Application.dataPath.Replace("/", "\\").Remove(Application.dataPath.Replace("/", "\\").Length - 6, 6);

        /// <summary>
        /// GIT提交
        /// </summary>
        [MenuItem("GIT/Commit", false, 2)]
        public static void SvnCommit()
        {
            ProcessCommand("TortoiseGitProc.exe", "/command:commit /path:\"" + GIT_BASE + "Assets" + "\"");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="argument"></param>
        private static void ProcessCommand(string command, string argument)
        {
            ProcessStartInfo start = new ProcessStartInfo(command)
            {
                Arguments = argument,
                CreateNoWindow = false,
                ErrorDialog = true,
                UseShellExecute = true
            };

            if (start.UseShellExecute)
            {
                start.RedirectStandardOutput = false;
                start.RedirectStandardError = false;
                start.RedirectStandardInput = false;
            }
            else
            {
                start.RedirectStandardOutput = true;
                start.RedirectStandardError = true;
                start.RedirectStandardInput = true;
                start.StandardOutputEncoding = System.Text.Encoding.UTF8;
                start.StandardErrorEncoding = System.Text.Encoding.UTF8;
            }

            Process p = Process.Start(start);

            p.WaitForExit();
            p.Close();
        }

        static string MetaFile(string str)
        {
            return str + ".meta";
        }
    }
}