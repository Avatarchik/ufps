
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class OutLog : MonoBehaviour
{
    static List<string> mLines = new List<string>();
    static List<string> mWriteTxt = new List<string>();
    private string outpath;

    GUIStyle style;

    void Awake()
    {
        //Application.persistentDataPath Unity��ֻ�����·���Ǽȿ��Զ�Ҳ����д�ġ�
        outpath = Application.persistentDataPath + "/outLog.txt";
        //ÿ�������ͻ���ɾ��֮ǰ�����Log
        if (System.IO.File.Exists(outpath))
        {
            File.Delete(outpath);
        }
        //��������һ��Log�ļ���
        //Application.RegisterLogCallback(HandleLog);
        Application.logMessageReceived += HandleLog;
        //һ�����
        //Debug.Log("xuanyusong");

        style = new GUIStyle();
        style.fontSize = 40;
        style.normal.textColor = new Color(1, 1, 1);
    }

    void Update()
    {
        //��Ϊд���ļ��Ĳ������������߳�����ɣ�������Update��Ŷ����д���ļ���
        if (mWriteTxt.Count > 0)
        {
            string[] temp = mWriteTxt.ToArray();
            foreach (string t in temp)
            {
                using (StreamWriter writer = new StreamWriter(outpath, true, Encoding.UTF8))
                {
                    writer.WriteLine(t);
                }
                mWriteTxt.Remove(t);
            }
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        mWriteTxt.Add(logString);
        //if (type == LogType.Error || type == LogType.Exception)
        {
            Log(logString);
            if (type == LogType.Error || type == LogType.Exception)
            {
                mWriteTxt.Add(stackTrace);
                Log(stackTrace);
            }
        }
    }

    //�����ҰѴ������Ϣ��������������������ֻ���Ļ��
    static public void Log(params object[] objs)
    {
        string text = "";
        for (int i = 0; i < objs.Length; ++i)
        {
            if (i == 0)
            {
                text += objs[i].ToString();
            }
            else
            {
                text += ", " + objs[i].ToString();
            }
        }
        if (Application.isPlaying)
        {
            if (mLines.Count > 20)
            {
                mLines.RemoveAt(0);
            }
            mLines.Add(text);

        }
    }

    void OnGUI()
    {
        //GUI.color = Color.red;
        for (int i = 0, imax = mLines.Count; i < imax; ++i)
        {
            GUILayout.Label(mLines[i], style);
        }
    }
}
