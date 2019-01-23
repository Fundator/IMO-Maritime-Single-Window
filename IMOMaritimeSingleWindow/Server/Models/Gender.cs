using System;
using System.Collections.Generic;

namespace IMOMaritimeSingleWindow.Models
{
    public enum GENDER_TYPES
    {
        MALE,
        FEMALE,
        OTHER
    }
    public partial class Gender
    {
        public Gender()
        {
            PersonOnBoard = new HashSet<PersonOnBoard>();
        }
        public int GenderId { get; set; }
        public string Description { get; set; }
        public string EnumValue { get; set; }

        public ICollection<PersonOnBoard> PersonOnBoard { get; set; }
    }
}
