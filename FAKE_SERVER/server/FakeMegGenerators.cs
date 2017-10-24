using System;
using System.Collections.Generic;
using ProtoBuf;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace fake_server
{
    class FakeMegGenerators
    {
        public static S2C_Login GenMsg(C2S_Login req)
        {
            string desJson = "{\"ret\":\"0\",\"uid\":\"1000\",\"code\":\"1000\"}";
            JavaScriptSerializer js = new JavaScriptSerializer();
            S2C_Login resp = js.Deserialize<S2C_Login>(desJson);
            string message = string.Format("ret={0},uid={1},code={2}", resp.ret, resp.uid, resp.code);
            Console.WriteLine(message);
 
            return resp;
		}

        public static S2C_UserInit GenMsg(C2S_UserInit req)
        {
            ProtoBuf.S2C_UserInit resp = new ProtoBuf.S2C_UserInit();
            resp.ret = 0;
            resp.userData = new ProtoBuf.DAT_UserData();

            resp.userData.nickname = "PAPAPA";

            return resp;
        }

        public static S2C_RoleInit GenMsg(C2S_RoleInit req)
        {
            ProtoBuf.S2C_RoleInit resp = new ProtoBuf.S2C_RoleInit();
            resp.ret = 0;


            return resp;
        }

        public static S2C_NewHero GenMsg(C2S_NewHero req)
        {
            ProtoBuf.S2C_NewHero resp = new ProtoBuf.S2C_NewHero();
            resp.ret = 0;








            return resp;
        }
    }


}
