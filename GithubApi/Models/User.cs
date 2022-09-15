using System;
using System.Collections.Generic;

namespace GithubApi.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Bio { get; set; }
        public List<Repo> listRepo { get; set; }
        public IEnumerable<LanguageSize> languageSize { get; set; }
        
    }
}
