using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;

namespace MVVM
{
    internal class JsonFileService : IFileService
    {
        public List<Phone> Open(string filename)
        {
            var phones = new List<Phone>();
            var json = new DataContractJsonSerializer(typeof(List<Phone>));
            using (var fs = new FileStream(filename, FileMode.OpenOrCreate))
                phones = json.ReadObject(fs) as List<Phone>;
            return phones;
        }
        public void Save(string filename, List<Phone> phones)
        {
            var json = new DataContractJsonSerializer(typeof(List<Phone>));
            using(var fs = new FileStream(filename, FileMode.Create))
                json.WriteObject(fs, phones);
        }
    }
}
