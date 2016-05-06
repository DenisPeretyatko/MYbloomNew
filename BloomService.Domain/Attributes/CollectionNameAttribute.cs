namespace BloomService.Domain.Attributes
{
    using System;

    public class CollectionNameAttribute : Attribute
    {
        public CollectionNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}