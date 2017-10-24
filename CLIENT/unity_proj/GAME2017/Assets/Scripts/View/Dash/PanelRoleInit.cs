using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using GNetwork;
using ProtoBuf;

namespace GAME2017
{
	public class PanelRoleInit : MonoBehaviour {

		RoleDataList _roleDataList = new RoleDataList();
		public Toggle _toggle1, _toggle2, _toggle3 ;

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		void OnEnable()
		{
			LoadData ();
		}

		public void OnButtonConfirmClick()
		{
			int index = 0;

			if (_toggle1.isOn) {
				index = 1;
			} else if (_toggle2.isOn) {
				index = 2;
			} else if (_toggle3.isOn) {
				index = 3;
			}
				
			if (index > 0) {
				
				//Debug.Log ("ROLE ID " + _roleDataList.roleDataList[index - 1].RoleId);

				ProtoBuf.C2S_RoleInit msg = new ProtoBuf.C2S_RoleInit ();
				msg.roleID = _roleDataList.roleDataList[index - 1].roleId;
				GNetwork.CommunicationManager.Instance.SendMessage (MessageTypes.C2S_RoleInit , msg);
				UIManager.Instance.ShowWaiting ();
			}
		}


		public class RoleDataList {
            public DAT_RoleDataStatic[] roleDataList; 
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