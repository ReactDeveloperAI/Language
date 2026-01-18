namespace MultilingualMenuApp.Models
{
    /// <summary>
    /// Represents a supported language in the system
    /// </summary>
    public class Language
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Language code (e.g., "en", "tr", "es")
        /// </summary>
        public string Code { get; set; } = string.Empty;
        
        /// <summary>
        /// Language display name (e.g., "English", "Türkçe")
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Indicates if this language is currently active
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
