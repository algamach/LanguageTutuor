using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace LanguageTutuor
{
    internal class Tutor
    {
        private Dictionary<string, string> _dic = new Dictionary<string, string>();
        Random _rand = new Random();
        public void AddWord(string eng, string rus)
        {
            _dic.Add(eng, rus);
        }
        public bool CheckWord(string eng, string rus)
        {
            var answer = _dic[eng];
            return answer.ToLower() == rus.ToLower();
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
