using System.Collections;
using System.Collections.Generic;
using System;

namespace GAME2017
{
	[Serializable]
	public class ElementProperty
	{
		public int air;
		public int water;
		public int fire;
		public int earth;

		public void SetData(ProtoBuf.DAT_ElementProperty ele)
		{
			air = ele.air;
			water = ele.water;
			fire = ele.fire;
			earth = ele.earth;
		}

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
		//public string username;
		public string nickname;
		public int lv;
		public int gold;
		public int gem;
		public int experience;
		public string roleId;
		public int strength;
		public int magic;
		public int agility;
		public ElementProperty eleProperty = new ElementProperty();
		public ElementProperty keys = new ElementProperty();
		public List<string> heroes;

		public void SetData(ProtoBuf.DAT_UserData dat)
		{
			nickname = dat.nickname;
			lv = dat.lv;
			experience = dat.experience;
			uid = dat.uid;
			gold = dat.gold;
			gem = dat.gem;
			roleId = dat.roleId;
			strength = dat.strength;
			magic = dat.magic;
			agility = dat.agility;
			eleProperty.SetData(dat.elementProperty);
			keys.SetData(dat.keys);
			heroes = dat.heroes;
		}
	}

	public class HeroData
	{
		public string id;
		public string uid;
		public int lv;
		public int experience;
		public int pAtk;
		public int pDef;
		public int mAtk;
		public int mDef;
		public int aAtk;
		public int elemType;
		public float speed;
		public int move;
		public int atkRange;
		public int hp;
		public List<string> skills;

		public void SetData(ProtoBuf.DAT_HeroData dat)
		{
			id = dat.id;
			uid = dat.uid;
			lv = dat.lv;
			experience = dat.experience;
			pAtk = dat.physicalAttack;
			pDef = dat.physicalDefence;
			mAtk = dat.magicalAttack;
			mDef = dat.magicalDefence;
			aAtk = dat.absoluteAttack;
			//elemType = 
			speed = dat.speed;
			move = dat.move;
			atkRange = dat.attackRange;
			hp = dat.hp;
			skills = dat.skills;
		}
	}


	[Serializable]
	public class HeroDataStatic
	{
		public string HeroId;
		public int pAtk;
		public int pDef;
		public int mAtk;
		public int mDef;
		public int aAtk;
		public int speed;
		public int move;
		public int atkRange;
		public int hp;
		public string[] skill;
	}

	[Serializable]
	public class RoleDataStatic
	{
		public string RoleId;
		public int Strength;
		public int Magic;
		public int Agility;
		public ElementProperty ElemProperty;
	}

}