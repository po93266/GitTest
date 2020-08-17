
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
        /// SVN更新
        /// </summary>
        [MenuItem("GIT/Update %g", false, 1)]
        public static void GitUpdate()
        {
            ProcessCommand("TortoiseGitProc.exe", "/command:update /path:\"" + GIT_BASE + "Assets" + "\"");
        }

        /// <summary>
        /// GIT提交
        /// </summary>
        [MenuItem("GIT/Commit", false, 2)]
        public static void GitCommit()
        {
            ProcessCommand("TortoiseGitProc.exe", "/command:commit /path:\"" + GIT_BASE + "Assets" + "\"");
        }

        /// <summary>
        /// SVN选择并提交
        /// </summary>
        [MenuItem("GIT/CommitSelect", false, 3)]
        public static void GitCommitSelect()
        {
            if (Selection.GetFiltered(typeof(object), SelectionMode.Assets).Length > 0)
            {
                string selectionPath = string.Empty;
                for (int i = 0; i < Selection.GetFiltered(typeof(object), SelectionMode.Assets).Length; i++)
                {
                    if (i > 0)
                    {
                        selectionPath = selectionPath + "*" + GIT_BASE + AssetDatabase.GetAssetPath(Selection.GetFiltered(typeof(object), SelectionMode.Assets)[i]).Replace("/", "\\");
                        selectionPath = selectionPath + "*" + GIT_BASE + MetaFile(AssetDatabase.GetAssetPath(Selection.GetFiltered(typeof(object), SelectionMode.Assets)[i])).Replace("/", "\\");
                    }
                    else
                    {
                        selectionPath = GIT_BASE + AssetDatabase.GetAssetPath(Selection.GetFiltered(typeof(object), SelectionMode.Assets)[i]).Replace("/", "\\");
                        selectionPath = selectionPath + "*" + GIT_BASE + MetaFile(AssetDatabase.GetAssetPath(Selection.GetFiltered(typeof(object), SelectionMode.Assets)[i])).Replace("/", "\\");
                    }
                }
                ProcessCommand("TortoiseGitProc.exe", "/command:commit /path:\"" + selectionPath + "\"");
            }
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