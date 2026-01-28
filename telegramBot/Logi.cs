using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace telegramBot
{
    internal class Logi
    {
        public static void Record(UserLog userLog, string File_logi, string file)
        {
            var serializer = new DataContractJsonSerializer(typeof(UserLog));


            if (!Directory.Exists(File_logi))
            {
                Directory.CreateDirectory(File_logi); 
            }

            using (var writeStream = System.IO.File.Open(file, FileMode.Append, FileAccess.Write))
            {

                serializer.WriteObject(writeStream, userLog);

                writeStream.WriteByte((byte)'\n');
            }
        }
    }
}
