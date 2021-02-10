using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Data.Enums
{
    public static class Enums
    {
        public enum RequestStatus
        {
            Draft = 1,
            Submitted = 2,
            Approved = 3,
            Ignored = 4
        }

        public enum SectionMediaType
        {
            None = 1,
            Image = 2,
            Video = 3
        }
    }
}
