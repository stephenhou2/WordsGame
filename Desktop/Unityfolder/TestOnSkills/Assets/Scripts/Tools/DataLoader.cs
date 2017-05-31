using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//读取文件上数据类
public class DataLoader {

	public string LoadDataString(string filePath,string fileName){
		StreamReader sr = null;
		//读取文件
		try{
			sr = File.OpenText (filePath + "/" + fileName);
			string dataString = sr.ReadToEnd ();
			Debug.Log(dataString);
			return dataString;

		}catch(System.Exception e){
			Debug.Log (e.Message);
			return null;
		}
	}
}
