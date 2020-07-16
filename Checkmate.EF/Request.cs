using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkmate.EF
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
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

    }
}
