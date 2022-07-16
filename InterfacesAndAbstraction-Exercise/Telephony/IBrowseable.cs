using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony
{
    public interface IBrowseable
    {
        string Website { get; set; }

        void BrowseWeb();
    }
}
