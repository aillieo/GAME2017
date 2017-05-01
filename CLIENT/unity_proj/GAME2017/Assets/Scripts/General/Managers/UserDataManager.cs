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

		public void Init(string _uid, string _code)
		{
			_userData.uid = _uid;
			_userData.code = _code;
		}

		public void RequestUserData()
		{
			ProtoBuf.C2S_UserData msg = new ProtoBuf.C2S_UserData ();
			msg.uid = _userData.uid;
			msg.code = _userData.code;
			GNetwork.CommunicationManager.Instance.SendMessage (GNetwork.MessageTypes.C2S_UserData,msg);
		}

		public void UpdateUserData(ProtoBuf.DAT_UserData userData)
		{
			_userData.SetData(userData);
		}
			
		public void AddNewHero(HeroData hd)
		{
			
		}
			
	}




}