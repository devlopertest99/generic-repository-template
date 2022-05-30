using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Model
{
    /// <summary>
    /// This class is the signature of all entities in the application.
    /// </summary>
    public class EntityBase
    {
        /// <summary>
        /// Unique Id of the entity or table.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Timestamp when is the entity is processed(Create or Updated).
        /// </summary>
        [Timestamp]
        public byte[]? TimeStamp { get; set; }
    }
}
