using System;

namespace TrackerApi.Authorization {

    [AttributeUsage(AttributeTargets.Method)]
    public class AnonymousAttribute : Attribute { }

}