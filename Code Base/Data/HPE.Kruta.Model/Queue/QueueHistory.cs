namespace HPE.Kruta.Model
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;


    [Serializable]
    [Table("QueueHistory")]
    public partial class QueueHistory
    {
        public int QueueHistoryID { get; set; }

        public int? QueueID { get; set; }

        public int? RoutedToDepartmentID { get; set; }

        public int? RoutedFromDepartmentID { get; set; }

        public int? AssignedFromEmployeeID { get; set; }

        public DateTime? EventDatetime { get; set; }

        [JsonIgnore]
        public virtual Department Department { get; set; }

        [JsonIgnore]
        public virtual Department Department1 { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }

        [JsonIgnore]
        public virtual Queue Queue { get; set; }
    }
}
