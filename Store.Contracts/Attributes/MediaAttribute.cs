using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Contracts
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MediaAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MediaFileAttribute : Attribute
    {
        private readonly string _relatedProperty;
        public MediaFileAttribute(string relatedProperty)
        {
            _relatedProperty = relatedProperty;
        }

        public string RelatedProperty
        {
            get
            {
                return _relatedProperty;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MovieAttribute : Attribute
    {

    }
}
