using System;

namespace ERPSystem.Models
{
    public interface IAuditable
    {
        DateTime CreateAt { get; set; }
        DateTime UpdateAt { get; set; }
    }
}
