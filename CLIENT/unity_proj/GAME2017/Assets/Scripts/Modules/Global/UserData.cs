using System.Collections;
using System.Collections.Generic;
using GAME2017;

namespace GAME2017
{
	public class UserData : Singleton<UserData> {

		string uid;
		string code;
		string username;
		int lv;
		int gold;
		int gem;
		int experience;
		int roleId;
		int strength;
		int magic;
		int agility;
		ElementProperty eleProperty;
		ElementType eleType;
		int chest;
		ElementProperty keys;
		List<HeroData> heroes;


		bool inited = false;

		public bool IsInited()
		{
			return inited;
		}


		public void Init(string _uid, string _code)
		{
			GNetwork.MessageDispatcher.Instance.AddHandler (GNetwork.MessageTypes.S2C_UserData,InitWithData);
			uid = _uid;
			code = _code;
			Init ();
		}

		public void Init()
		{
			ProtoBuf.C2S_UserData msg = new ProtoBuf.C2S_UserData ();
			msg.uid = uid;
			msg.code = code;
			GNetwork.CommunicationManager.Instance.SendMessage (GNetwork.MessageTypes.C2S_UserData,msg);
		}

		public void InitWithData(object msg)
		{
			inited = true;
			// ... 
		}
	}

}