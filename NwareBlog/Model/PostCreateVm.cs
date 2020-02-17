using NwareBlog.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Model
{
    public class PostCreateVm : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Publication Date is required")]
        public string PublicationDate { get; set; }

        public DateTime PublicationDateTime => GetPublishDate();


        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        private DateTime GetPublishDate()
        {
            return PublicationDate.ToDateTime(AppConsts.DateConsts.SystemDateTimeFormat, out bool isSucceeded);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            PublicationDate.ToDateTime(AppConsts.DateConsts.SystemDateTimeFormat, out bool isSucceeded);

            if(!isSucceeded)
            {
                yield return new ValidationResult($"Format specified for date published invalid. Correct format is {AppConsts.DateConsts.SystemDateTimeFormat}");
            }
        }
    }
}
