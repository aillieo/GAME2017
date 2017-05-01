using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using GNetwork;

namespace GAME2017
{
	public class PanelRoleInit : MonoBehaviour {

		int index = 0;
		RoleDataList _roleDataList = new RoleDataList();
		Toggle _toggle1, _toggle2, _toggle3 ;

		// Use this for initialization
		void Start () {

			_toggle1 = GameObject.Find ("Toggle1").GetComponent<Toggle>();
			_toggle2 = GameObject.Find ("Toggle2").GetComponent<Toggle>();
			_toggle3 = GameObject.Find ("Toggle3").GetComponent<Toggle>();

			LoadData ();

		}

		// Update is called once per frame
		void Update () {

		}


		public void OnButtonConfirmClick()
		{

			if (_toggle1.isOn) {
				index = 1;
			} else if (_toggle2.isOn) {
				index = 2;
			} else if (_toggle3.isOn) {
				index = 3;
			}
				
			if (index > 0) {
				
				Debug.Log ("ROLE INDEX " + _roleDataList.roleDataList[index - 1].RoleId);

				ProtoBuf.C2S_RoleInit msg = new ProtoBuf.C2S_RoleInit ();
				msg.roleID = _roleDataList.roleDataList[index - 1].RoleId;
				GNetwork.CommunicationManager.Instance.SendMessage (MessageTypes.C2S_RoleInit , msg);
				UIManager.Instance.ShowWaiting ();
			}
		}


		[Serializable]
		public class RoleDataList {
			public RoleDataStatic[] roleDataList; 
		}


		void LoadData ()
		{
			string filepath = Application.dataPath + "/Resources/JsonFiles/role.json";
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
			string json = sr.ReadToEnd();
			if (json.Length > 0) {
				_roleDataList = JsonUtility.FromJson<RoleDataList> (json);
			} 
			else {
				Debug.Log (filepath + "empty file");
			}
			//Debug.Log (_roleDataList.roleDataList.Length.ToString());
		}
			


	}
}