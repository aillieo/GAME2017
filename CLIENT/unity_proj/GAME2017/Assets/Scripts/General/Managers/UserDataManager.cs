using System.Collections;
using System.Collections.Generic;
using GAME2017;

namespace GAME2017
{
	public class UserDataManager : Singleton<UserDataManager> {

		UserData _userData = new UserData ();
		Dictionary<string,HeroData> _heroes = new Dictionary<string,HeroData>();

		public UserData GetUserData()
		{
			return _userData;
		}

		public HeroData GetHeroData(string heroUid)
		{
			if (_heroes.ContainsKey (heroUid)) {
				return _heroes [heroUid];
			}
				
			return null;
		}

		public void Init(string _uid, string _code)
		{
			_userData.uid = _uid;
			_userData.code = _code;
		}

		public void RequestUserData()
		{
			ProtoBuf.C2S_UserInit msg = new ProtoBuf.C2S_UserInit ();
			msg.uid = _userData.uid;
			msg.code = _userData.code;
			GNetwork.CommunicationManager.Instance.SendMessage (GNetwork.MessageTypes.C2S_UserInit,msg);
		}

		public void UpdateUserData(ProtoBuf.DAT_UserData userData)
		{
			_userData.SetData(userData);

		}
			
		public void AddNewHero(HeroData hd)
		{
			_userData.heroes.Add (hd.uid);
			_heroes [hd.uid] = hd;
		}

		public void SetHeroData(HeroData hd)
		{
			if(_heroes.ContainsKey(hd.uid))
			{
				_heroes [hd.uid] = hd;
			}
		}
			
	}




}