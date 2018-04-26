using System;
using System.Collections.Generic;

namespace DocumentsApp
{
    public class DocumentInfo
    {
        public string DocumentTheme { get; set; }
        public string Creator { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Signs { get; set; }

        public void InsertSigns(ICollection<Sign> signs)
        {
            foreach (var sign in signs)
            {
                Signs = string.Concat($"{Signs}{sign.Signer.FullName}, ");
            }

            // Удаляю последнюю запятую
            if (Signs != null)
                Signs = Signs.TrimEnd(',', ' ');
        }
    }
}
