using System;

namespace FlowOrchestrator.Domain
{
    /// <summary>
    /// Represents version information for the FlowOrchestrator system.
    /// </summary>
    public class VersionInfo
    {
        /// <summary>
        /// Gets or sets the version number.
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// Gets or sets the build number.
        /// </summary>
        public int Build { get; set; }
        
        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the release notes.
        /// </summary>
        public string ReleaseNotes { get; set; }
    }
}
