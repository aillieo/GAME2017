using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace GAME2017
{

	public class LanguageManager : Singleton<LanguageManager> {

		Dictionary<string,string> _textDict = new Dictionary<string,string>();

		public void Init()
		{
			string filepath = Application.dataPath + "/Resources/TextFiles/CN.txt";
			if (!File.Exists(filepath))
			{
				Debug.Log (filepath + "do not exist");
				return;
			}
			StreamReader sr = new StreamReader(filepath);
			if (sr == null)
			{
				Debug.Log (filepath + "read failed");
				return;
			}

			string lineStr;
			while ((lineStr = sr.ReadLine ()) != null) 
			{
				if (lineStr.Contains ("|")) 
				{
					string[] pair = lineStr.Split ('|');
					_textDict [pair [0]] = pair [1];
				}
			}


		}


		public string GetTextByKey(string key)
		{
			if (_textDict.ContainsKey (key)) 
			{
				return _textDict [key];
			} 
			else 
			{
				return key;
			}
		}

		public string GetErrorMessage(int errorCode)
		{
			string key = "E" + errorCode.ToString ("D4");
			return GetTextByKey (key);
		}



	}
}