using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NwareBlog.Model
{
    public class ResultModel<T>
    {
        public List<string> ErrorMessages { get; private set; } = new List<string>();

        public string Message { get; set; }

        public T Data { get; set; } = default;

        public bool NotFound { get; set; }

        public bool HasError
        {
            get
            {
                if (ErrorMessages.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }

        public void AddError(string error)
        {
            ErrorMessages.Add(error);
        }

        public void AddError(IEnumerable<string> errors)
        {
            ErrorMessages.AddRange(errors);
        }
    }
}
