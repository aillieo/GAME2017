using System.Collections;
using System.Collections.Generic;
using GAME2017;
using ProtoBuf;

namespace GAME2017
{
	public class UserDataManager : Singleton<UserDataManager> {

        DAT_UserData _userData = new DAT_UserData();
        Dictionary<string, DAT_HeroData> _heroes = new Dictionary<string, DAT_HeroData>();
        private string _uid;
        private string _code;

        public DAT_UserData GetUserData()
		{
			return _userData;
		}

        public DAT_HeroData GetHeroData(string heroUid)
		{
			if (_heroes.ContainsKey (heroUid)) {
				return _heroes [heroUid];
			}
				
			return null;
		}

		public void Init(string uid, string code)
		{
			_uid = uid;
			_code = code;
		}

		public void RequestUserData()
		{
			ProtoBuf.C2S_UserInit msg = new ProtoBuf.C2S_UserInit ();
			msg.uid = _uid;
			msg.code = _code;
			GNetwork.CommunicationManager.Instance.SendMessage (GNetwork.MessageTypes.C2S_UserInit,msg);
		}

		public void UpdateUserData(ProtoBuf.DAT_UserData userData)
		{
			_userData = userData;
		}
			
		public void AddNewHero(DAT_HeroData hd)
		{
			_userData.heroes.Add (hd.uid);
			_heroes [hd.uid] = hd;
		}

        public void SetHeroData(DAT_HeroData hd)
		{
			if(_heroes.ContainsKey(hd.uid))
			{
				_heroes [hd.uid] = hd;
			}
		}
			
	}




}