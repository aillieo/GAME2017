using System.Collections;
using System.Collections.Generic;

namespace GAME2017
{
	public class ElementProperty
	{
		public int air;
		public int water;
		public int fire;
		public int earth;
	}

	public enum ElementType
	{
		air = 0,
		water = 1,
		fire = 2,
		earth = 3
	}

	public class UserData
	{
		public string uid;
		public string code;
		public string username;
		public string nickname;
		public int lv;
		public int gold;
		public int gem;
		public int experience;
		public int roleId;
		public int strength;
		public int magic;
		public int agility;
		public ElementProperty eleProperty;
		public ElementType eleType;
		public int chest;
		public ElementProperty keys;
		public List<HeroData> heroes;
	}

	public class HeroData
	{
		public string id;
		// ...
	}

}