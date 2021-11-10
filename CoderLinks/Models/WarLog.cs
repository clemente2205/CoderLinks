using System;
using System.Collections.Generic;

#nullable disable

namespace CoderLinks.Models
{
    public partial class WarLog
    {
        public int IdGame { get; set; }
        public string WinPlayer { get; set; }
        public int? RoundsToWin { get; set; }
    }
}
