using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.User
{
    public class Validation
    {
        public ICollection<ValidationResult> Errors { get; set; } = null;
        public bool IsValid => Errors.Count != 0;
    }
}
