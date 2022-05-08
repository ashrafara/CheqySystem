namespace CheqySystem
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DipsatchInfo")]
    public partial class DipsatchInfo
    {
        [Key]
        public int dispId { get; set; }

        public int? sectorId { get; set; }

        public double? dispamount { get; set; }

        [StringLength(250)]
        public string displetter { get; set; }

        public double? dispcheque { get; set; }

        [StringLength(250)]
        public string dispnote { get; set; }

        public virtual SectorsInfo SectorsInfo { get; set; }
    }
}
