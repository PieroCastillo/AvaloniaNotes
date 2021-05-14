using System.Drawing;
using Avalonia.Media;

namespace AvaloniaNotes.Models
{
    public class NotesDataBase
    {
        public static NotesDataBase Current { get; private set; }

        public NotesDataBase()
        {
            Current = this;
        }

        public void Load()
        {

        }

        public void Save()
        {
            
        }
    }
}