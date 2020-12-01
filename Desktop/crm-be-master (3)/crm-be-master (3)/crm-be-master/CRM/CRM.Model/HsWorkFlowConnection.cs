using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRM.Model
{
    public class HsWorkFlowConnection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Type { get; set; }
        public Guid FromInstanceId { get; set; }
        public Guid ToInstanceId { get; set; }
        public string Command { get; set; }
        public bool IsDeleted { get; set; }

        //[ForeignKey("FromInstanceId"), Column(Order = 0)]
        public virtual HsWorkFlowInstance FromInstance { get; set; }
        //[ForeignKey("ToInstanceId"), Column(Order = 1)]
        public virtual HsWorkFlowInstance ToInstance { get; set; }
    }
}
