using System.Collections;
using System.Collections.Generic;
using GAME2017;

namespace GAME2017
{
	public class UserDataManager : Singleton<UserDataManager> {

		UserData _userData = new UserData ();

		public UserData GetUserData()
		{
			return _userData;
		}

		bool _hasUserData = false;

		public bool HasUserData()
		{
			return _hasUserData;
		}

		public bool IsNewUser { set ; get;}

		public void Init(string _uid, string _code)
		{
			_userData.uid = _uid;
			_userData.code = _code;
		}

		public void InitHandler()
		{
			GNetwork.MessageDispatcher.Instance.AddHandler (GNetwork.MessageTypes.S2C_UserData,OnInitBack);
		}

		public void RequestUserData()
		{
			ProtoBuf.C2S_UserData msg = new ProtoBuf.C2S_UserData ();
			msg.uid = _userData.uid;
			msg.code = _userData.code;
			GNetwork.CommunicationManager.Instance.SendMessage (GNetwork.MessageTypes.C2S_UserData,msg);
		}

		public void InitWithData(ProtoBuf.DAT_UserData userData)
		{
			_userData.SetData(userData);
		}

		public void OnInitBack(object msg)
		{
			ProtoBuf.S2C_UserData _msg = (ProtoBuf.S2C_UserData)msg;
			IsNewUser = _msg.newUser;
			if (IsNewUser) 
			{
				// ...
				// Init role
			} 
			else 
			{
				InitWithData (_msg.userData);
			}
			_hasUserData = true;
		}
			
	}




}