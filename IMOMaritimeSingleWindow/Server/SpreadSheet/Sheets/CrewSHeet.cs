using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMOMaritimeSingleWindow.SpreadSheet.Sheets
{
    public class CrewSheet : PersonOnBoardSheet
    {
        public override string Sheetname { get => "Crew List"; set => base.Sheetname = value; }

        public int startRow = 19;
        public int endCol = 27;

        public int RankOrRatingAddress = 25;
        public string RankOrRatingIdentificator = "Rank_or_Rating";

        public int PlaceOfBirthAddress = 26;
        public string PlaceOfBirthIdentificator = "Place_of_Birth";

        public int EffectsCustomsAddress = 27;
        public string EffectsCustomsIdentificator = "Effects_Customs";
    }
}
