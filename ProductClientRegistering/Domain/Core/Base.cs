using Domain.Core.Interface;
using System;

namespace Domain.Core
{
    [Serializable]
    public class Base : IBase
    {
        public int Id { get; set; }
    }
}
