﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CRM.Service.Utils
{
    public class FileSupport
    {
        public MemoryStream Stream { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }

    }
}
