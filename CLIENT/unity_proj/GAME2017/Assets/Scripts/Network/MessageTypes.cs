using System;

namespace GNetwork
{
    public class MessageTypes
    {

        public const int C2S_Login = 1001;
        public const int C2S_UserInit = 1002;
        public const int C2S_RoleInit = 1003;
        public const int C2S_NewHero = 1004;
        public const int C2S_GetHeroData = 1005;
        public const int C2S_UpdateHeroData = 1006;
        public const int C2S_GetTeamData = 1007;
        public const int C2S_UpdateTeamData = 1008;
        public const int C2S_BattleInit = 1010;

        //------------

        public const int S2C_Login = 2001;
        public const int S2C_UserInit = 2002;
        public const int S2C_RoleInit = 2003;
        public const int S2C_NewHero = 2004;
        public const int S2C_GetHeroData = 2005;
        public const int S2C_UpdateHeroData = 2006;
        public const int S2C_GetTeamData = 2007;
        public const int S2C_UpdateTeamData = 2008;
        public const int S2C_BattleInit = 2010;


    }
}
