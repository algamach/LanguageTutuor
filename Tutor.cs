using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LanguageTutuor
{
    internal class Tutor
    {
        private WordStorage _storage = new WordStorage();
        private Dictionary<string, string> _dic;

        Random _rand = new Random();

        public Tutor() 
        {
            _dic = _storage.GetAllWords();
        }

        public void AddWord(string eng, string rus)
        {
            if (!_dic.ContainsKey(eng))
            {
                _dic.Add(eng, rus);
                _storage.AddWord(eng, rus);
            }
        }
        public bool CheckWord(string eng, string rus)
        {
            try
            {
                var answer = _dic[eng];
                return answer.ToLower() == rus.ToLower();
            }
            catch (KeyNotFoundException ex)
            {                
                return false;
            }
        }
        public string Translate(string eng)
        {
            if (_dic.ContainsKey(eng))
            {
                return _dic[eng];
            }
            else
            {
                return null;
            }
        }
        public string GetRandomEngWord()
        {
            var r = _rand.Next(0,_dic.Count);
            var keys = new List<string>(_dic.Keys);
            return keys[r];
        }
    }
}
