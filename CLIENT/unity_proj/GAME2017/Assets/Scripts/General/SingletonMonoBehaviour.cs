using UnityEngine;
using System.Collections;
using System;


public class SingletonMonoBehaviour<T> : MonoBehaviour where T: SingletonMonoBehaviour<T>
{
	private static T _instance;
	public static T Instance
	{
		get
		{
			return _instance;
		}
	}

	public static bool Exists
	{
		get;
		private set; 
	}


	protected void Awake()
	{
		if(_instance == null)
		{
			_instance = (T)this;
			Exists = true;
		}
		else if(_instance != this)
		{
			throw new InvalidOperationException("Can't have two instances of a view");
		}
	}

	/*
        protected static GameObject prefabGO;

 		public static void OpenView(string path)
        {
            if (Instance == null)
            {
                GameObject go = Resources.Load(path) as GameObject;
				prefabGO = GameObject.Instantiate(go);
				prefabGO.AddComponent<T>();
            }
            else
            {
                if (Instance.gameObject.activeSelf)
                {
                    Instance.gameObject.SetActive(true);
                }
            }
        }
*/

}


