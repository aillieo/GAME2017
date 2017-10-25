using System;
using System.Collections.Generic;
using ProtoBuf;

namespace GAME2017
{
    class MsgLoginHandler
	{
        public static void HandleMsg(int index, int type, S2C_Login resp)
        {
            Messenger.Broadcast<S2C_Login>("S2C_Login", resp);
        }
	}


}
