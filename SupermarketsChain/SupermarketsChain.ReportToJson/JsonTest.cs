namespace SupermarketsChain.ReportToJson
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class JsonTest
    {
        public static void Main()
        {
            List<List<string>> _data = new List<List<string>>();
            _data.Add(new List<string>()
            {
                "dsadsa", "dsdsa", "dsassdas"
            });
            string json = JsonConvert.SerializeObject(_data.ToArray());

            //write string to file
            System.IO.File.WriteAllText(@"D:\path.json", json);
            Console.WriteLine("yes");
            Console.ReadLine();
        }
    }
}
