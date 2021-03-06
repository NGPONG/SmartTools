﻿using SmartTools.Common.Enum;
using System.Runtime.Serialization;

namespace SmartTools.Service.Module.Entity
{
    [DataContract]
    public class CustomMessage
    {
        [DataMember]
        public HttpStatus Status { get; set; }
        [DataMember]
        public object Message { get; set; }
    }
}
