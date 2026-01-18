using System.Collections.Generic;

namespace MultilingualMenuApp.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string DefaultName { get; set; } // Varsayılan isim
        public string DefaultDescription { get; set; } // Varsayılan açıklama
        public ICollection<MenuTranslation>? Translations { get; set; } // Çeviriler
    }

    public class MenuTranslation
    {
        public int Id { get; set; }
        public string Language { get; set; } // Dil Kodu ("en", "tr", vb.)
        public string Name { get; set; } // Çevrilmiş isim
        public string Description { get; set; } // Çevrilmiş açıklama
        public int MenuId { get; set; } // Menu Foreign Key
        public Menu? Menu { get; set; }
    }
}