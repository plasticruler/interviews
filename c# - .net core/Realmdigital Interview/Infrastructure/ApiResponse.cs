using System;
using System.Collections.Generic;

namespace Realmdigital_Interview.Infrastructure
{    
    public class ApiResponse<T>
    {
        T _content;
        private List<Error> _errors;
        private bool _hasContent;
        public ApiResponse()
        {
            _errors = new List<Error>();
            _content = default(T);
        }
        public T Content
        {
            get
            {
                return _content;
            }
        }
        public bool HasContent => _hasContent;
        public IEnumerable<Error> Errors => _errors.ToArray();
        public void SetContent(T content)
        {
            if (content == null)
                _content = default(T);//add default
            else{
                _content = content;
                _hasContent = true; //content was actually set
            }
        }
        public void AddError(Error error)
        {
            _errors.Add(error);
        }
    }
}