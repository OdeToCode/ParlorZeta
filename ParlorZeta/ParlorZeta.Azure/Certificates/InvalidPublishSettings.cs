using System;

namespace ParlorZeta.Azure.Certificates
{
    public class InvalidPublishSettings : Exception
    {
        public InvalidPublishSettings(Exception inner)
            :base("The publish settings file seems to be invalid", inner)
        {
            
        }
    }
}
