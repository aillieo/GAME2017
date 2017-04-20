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

	enum ElementType
	{
		air = 0,
		water = 1,
		fire = 2,
		earth = 3
	}

	public class HeroData
	{
		public string id;
	}

}