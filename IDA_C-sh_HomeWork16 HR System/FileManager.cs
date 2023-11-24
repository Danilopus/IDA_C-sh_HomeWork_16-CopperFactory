using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace FileManger
{
    internal class FileManager
    {

        /////   METHODS   /////

/*        static public bool WriteToLogFile(PassSystemEventLogger.ValueEvent valueEvent, string filename)
        {
           if (valueEvent == null) return false;
            using (StreamWriter writer = new StreamWriter(filename, true))  
            {
                //JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
                // writer.WriteLine(JsonSerializer.Serialize(valueEvent, options));
                writer.WriteLine(valueEvent.ToString());        
            }
        return true;
        }*/
        static public List<string> ReadLogFile(string filename)
        {
            List<string> tmp_list = new List<string>();
            using (StreamReader reader = new StreamReader(filename, true))
            {
                while (!reader.EndOfStream)
                {
                //tmp_list.Add(JsonSerializer.Deserialize<ValueEvent>(reader.ReadToEnd()));
                //tmp_list = (JsonSerializer.Deserialize<List<ValueEvent>>(reader.ReadToEnd()));
                tmp_list.Add(reader.ReadLine());
                }
            }
            return tmp_list;
        }


        static public void RandomNameSurnameLoader(out string[] male_surnames, out string[] male_names, out string[] femmale_names)
        {            
            // "male_surnames_rus.txt";
            // male_names_rus.txt
            // female_names_rus.txt
            male_surnames = GetItems("male_surnames_rus.txt");
            male_names = GetItems("male_names_rus.txt");
            femmale_names = GetItems("female_names_rus.txt");  

        }
        static string[] GetItems(string file)
        {
            if (!File.Exists(file))
            {
                string project_path = Directory.GetParent(file).Parent.Parent.Parent.FullName + "\\";
                if (!File.Exists(project_path+file)) throw new Exception("couldnt find " + file);
                File.Copy(project_path + file, file, true);
            }
            using (StreamReader streamReader = new StreamReader(file))
            {
                return streamReader.ReadToEnd().Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries);
            }
        }
        static string[] GetItemsTranslited(string file)
        {
            //char[] chars_to_remove = new char[] { '\r' };
            using (StreamReader streamReader = new StreamReader(file))
            {
                 List<string> result = new List<string>();
                foreach (string item in streamReader.ReadToEnd().Replace("\r", "").Split("\n", StringSplitOptions.RemoveEmptyEntries))
                    result.Add(Translit(item));
                return result.ToArray();
            }
        }
        public static string Translit(string str)
        {
            string[] lat_up = { "A", "B", "V", "G", "D", "E", "Yo", "Zh", "Z", "I", "Y", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "F", "Kh", "Ts", "Ch", "Sh", "Shch", "\"", "Y", "'", "E", "Yu", "Ya" };
            string[] lat_low = { "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "shch", "\"", "y", "'", "e", "yu", "ya" };
            string[] rus_up = { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            string[] rus_low = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
            for (int i = 0; i <= 32; i++)
            {
                str = str.Replace(rus_up[i], lat_up[i]);
                str = str.Replace(rus_low[i], lat_low[i]);
            }
            return str;
        }
    }
}
