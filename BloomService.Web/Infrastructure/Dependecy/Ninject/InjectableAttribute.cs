using System;

namespace BloomService.Web.Infrastructure.Dependecy.Ninject
{
    public class InjectableAttribute : Attribute
    {
        public InjectableAttribute()
        {
            Singleton = true;
        }

        public bool Singleton { get; set; }
    }
}