using System.Globalization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ganss.Xss;

namespace OnlineBankingApp.Models
{
    public class FundTransfer : IValidatableObject
    {
        public int ID { get; set; }

        public int CustomerID { get; set; }

        [Display(Name = "Account From")]
        public int AccountFrom { get; set; }

        [Display(Name = "Account To")]
        public int AccountTo { get; set; }

        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        private string note;

        [StringLength(60)]
        [DataType(DataType.MultilineText)]
        public string Note
        {
            get => note;
            set => note = new HtmlSanitizer().Sanitize(value);
        }

        public string GetResendLink(){
            return $"/FundTransfers/Create?amount={Amount}&note={Note}";
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AccountFrom == AccountTo)
            {
                yield return new ValidationResult(
                    "The fund transfer destination must be different from the source"
                );
            }
        }
    }
}