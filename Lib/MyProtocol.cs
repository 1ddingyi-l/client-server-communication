using System;
using System.Text;

namespace Lib
{
    public class MyProtocol : IProtocol
    {
        public string GetResponse(string msgFromClient)
        {
            if (msgFromClient.StartsWith("start") && msgFromClient.EndsWith("end"))
            {
                var tmp = msgFromClient.Split(new string[] { "start", "end" }, StringSplitOptions.RemoveEmptyEntries);
                if (tmp.Length == 0)
                    return "invalid";
                var content = tmp[0];
                Random random = new Random(DateTime.Now.GetHashCode());
                if (content == "a")
                    return random.Next().ToString();
                else if (content == "b")
                    return random.NextDouble().ToString();
                else if (content == "c")
                    return "Your are beauity";
                else if (int.TryParse(content, out int num))
                {
                    var stringBuilder = new StringBuilder();
                    for (var idx = 0; idx < num; idx++)
                        stringBuilder.Append(random.Next().ToString() + '\n');
                    stringBuilder.Append("End" + '\n');
                    return stringBuilder.ToString();
                }
            }
            return "invalid";
        }
    }
}
