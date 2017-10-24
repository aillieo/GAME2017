using System;
using System.Text;
using System.IO;


namespace fake_server
{
    class JsonLoader
    {
        public static string Load(string filename)
        {
            filename = "../../json/" + filename + ".json";
            string content = "";

            try
            {
                StreamReader sr = new StreamReader(filename, System.Text.Encoding.Default);
                content = sr.ReadToEnd();
                sr.Close();
            }
            catch
            {
                Console.WriteLine("json file error : {0}", filename);
            }
            finally
            {
                
            }
            return content;
		}
    }

}
