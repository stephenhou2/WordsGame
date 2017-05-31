using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataInitializer{

	// 加载所有的技能效果
	public static EffectData[] LoadEffectsWithPath(string filePath,string fileName){
		
		DataLoader dataLoader = new DataLoader ();

		string jsonStr = dataLoader.LoadDataString (filePath, fileName);

		EffectData[] effectDataArray = null;

		//模型转换
		try{
			effectDataArray = JsonHelper.FromJson<EffectData> (jsonStr);
		}catch(System.Exception e){
			Debug.Log (e.Message);
		}
		return effectDataArray;
	}




}

