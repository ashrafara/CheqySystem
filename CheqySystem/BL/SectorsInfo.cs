namespace CheqySystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SectorsInfo")]
    public partial class SectorsInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SectorsInfo()
        {
            DipsatchInfoes = new HashSet<DipsatchInfo>();
        }

        [Key]
        public int sectorId { get; set; }

        [StringLength(150)]
        public string sectorName { get; set; }

        [StringLength(150)]
        public string sectorAddress { get; set; }

        [StringLength(250)]
        public string sectorNote { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DipsatchInfo> DipsatchInfoes { get; set; }
    }
}
