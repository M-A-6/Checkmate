using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Checkmate.Model
{
    public class Request
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string PrinterBranchID { get; set; }
        public string PrinterClientID { get; set; }
        public string RequestID { get; set; }        
        public string OperatorName { get; set; }
        public int RequestType { get; set; }
        public string BookletAccountNumber { get; set; }
        public string BookletStyleName { get; set; }
        public int BookletSize { get; set; }
        public string BookletAccountName { get; set; }
    }
}
