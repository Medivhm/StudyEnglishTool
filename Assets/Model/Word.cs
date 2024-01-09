using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Word
    {
        private string word;
        private List<Meaning> meanings;

        public void SetWord(string word)
        {
            this.word = word;
        }

        public string GetWord()
        {
            return this.word;
        }

        public void SetMeaning(Meaning meaning)
        {
            if (this.meanings == null)
            {
                this.meanings = new List<Meaning>();
            }
            this.meanings.Add(meaning);
        }

        public void SetMeanings(List<Meaning> meanings)
        {
            if (this.meanings == null)
            {
                this.meanings = new List<Meaning>();
            }
            this.meanings.AddRange(meanings);
        }

        public List<Meaning> GetMeanings()
        {
            return this.meanings;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.word);
            foreach(Meaning meaning in this.meanings)
            {
                sb.AppendLine(meaning.ToString());
            }
            return sb.ToString();
        }
    }
}
