using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GAME2017;

namespace GAME2017
{
	public class SpriteResourceManager : Singleton<SpriteResourceManager> {


		public void LoadResources()
		{
			
		}


		public Texture GetRoleHeadByID(string roleID)
		{
			return Resources.Load<Sprite> ("Sprites/Roles/R_128_" + roleID).texture;
		}

		public Texture GetRoleAvatarByID(string roleID)
		{
			return Resources.Load<Sprite> ("Sprites/Roles/R_128_" + roleID).texture;
		}

	}

}