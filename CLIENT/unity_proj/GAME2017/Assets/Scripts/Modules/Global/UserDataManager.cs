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

		bool _inited = false;

		public bool IsInited()
		{
			return _inited;
		}

		public bool IsNewUser { set ; get;}

		public void Init(string _uid, string _code)
		{
			GNetwork.MessageDispatcher.Instance.AddHandler (GNetwork.MessageTypes.S2C_UserData,InitWithData);
			_userData.uid = _uid;
			_userData.code = _code;
			Init ();
		}

		public void Init()
		{
			ProtoBuf.C2S_UserData msg = new ProtoBuf.C2S_UserData ();
			msg.uid = _userData.uid;
			msg.code = _userData.code;
			GNetwork.CommunicationManager.Instance.SendMessage (GNetwork.MessageTypes.C2S_UserData,msg);
		}

		public void InitWithData(object msg)
		{
			_inited = true;
			ProtoBuf.S2C_UserData _msg = (ProtoBuf.S2C_UserData)msg;
			IsNewUser = _msg.newUser;
			if (IsNewUser) 
			{
				// ...
				// Init role
			} 
			else 
			{
				_userData.SetData(_msg.userData);
			}
		}
			
	}




}