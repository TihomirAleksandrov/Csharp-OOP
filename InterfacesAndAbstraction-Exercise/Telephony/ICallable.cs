﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Telephony
{
    public interface ICallable
    {
        string PhoneNumber { get; set; }
        void Call();
    }
}
