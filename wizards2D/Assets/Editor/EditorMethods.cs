using UnityEngine;
using System.Collections;
using UnityEditor;

public static class EditorMethods {

	public static Object getPrefab(string file){
		return AssetDatabase.LoadAssetAtPath(file, typeof(GameObject));
	}
}
