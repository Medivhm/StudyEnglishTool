using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Meaning
    {
        private string partOfSpeech;
        private string meaning;

        public void SetProps(string partOfSpeech, string meaning)
        {
            this.partOfSpeech = partOfSpeech;
            this.meaning = meaning;
        }

        public void SetPartOfSpeech(string partOfSpeech)
        {
            this.partOfSpeech = partOfSpeech;
        }

        public string GetPartOfSpeech()
        {
            return this.partOfSpeech;
        }

        public void SetMeaning(string meaning)
        {
            this.meaning = meaning;
        }

        public string GetMeaning()
        {
            return this.meaning;
        }

        public override string ToString()
        {
            return this.partOfSpeech + meaning;
        }
    }
}
