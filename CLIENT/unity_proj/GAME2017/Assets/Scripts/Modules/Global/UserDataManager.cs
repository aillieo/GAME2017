using System.Collections;
using System.Collections.Generic;
using GAME2017;

namespace GAME2017
{
	public class UserDataManager : Singleton<UserDataManager> {

		UserData userData = new UserData ();

		bool inited = false;

		public bool IsInited()
		{
			return inited;
		}


		public void Init(string _uid, string _code)
		{
			GNetwork.MessageDispatcher.Instance.AddHandler (GNetwork.MessageTypes.S2C_UserData,InitWithData);
			userData.uid = _uid;
			userData.code = _code;
			Init ();
		}

		public void Init()
		{
			ProtoBuf.C2S_UserData msg = new ProtoBuf.C2S_UserData ();
			msg.uid = userData.uid;
			msg.code = userData.code;
			GNetwork.CommunicationManager.Instance.SendMessage (GNetwork.MessageTypes.C2S_UserData,msg);
		}

		public void InitWithData(object msg)
		{
			inited = true;
			ProtoBuf.S2C_UserData _msg = (ProtoBuf.S2C_UserData)msg;
			 
			userData.lv = _msg.lv;
			// ...
		}
	}

}