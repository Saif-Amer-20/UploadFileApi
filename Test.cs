﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadsFile
{
    public class Test
    {
        public string ImgNam  { get; set; }
        [JsonConverter(typeof(Base64FileJsonConverter))]
        public byte[] ImgData { get; set; }
    }
}
