namespace MultilingualMenuApp.Models
{
    /// <summary>
    /// Represents a menu item with multilingual support
    /// </summary>
    public class Menu
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Default name (fallback when translation is not available)
        /// </summary>
        public string DefaultName { get; set; } = string.Empty;
        
        /// <summary>
        /// Default description (fallback when translation is not available)
        /// </summary>
        public string DefaultDescription { get; set; } = string.Empty;
        
        /// <summary>
        /// Collection of translations for this menu item
        /// </summary>
        public ICollection<MenuTranslation> Translations { get; set; } = new List<MenuTranslation>();
    }

    /// <summary>
    /// Represents a translation of a menu item in a specific language
    /// </summary>
    public class MenuTranslation
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Language code (e.g., "en", "tr", "es")
        /// </summary>
        public string Language { get; set; } = string.Empty;
        
        /// <summary>
        /// Translated name
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Translated description
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Foreign key to the Menu
        /// </summary>
        public int MenuId { get; set; }
        
        /// <summary>
        /// Navigation property to the Menu
        /// </summary>
        public Menu? Menu { get; set; }
    }
}