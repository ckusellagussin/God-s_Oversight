using System.Collections.Generic;
using Verse;
using God_s_Oversight.Defs;


namespace God_s_Oversight.Buildings.CreationChamber
{
    class LootTables
    {

        public static List<Pair<HediffDef, int>> Tier1P = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.extremeSpeed, 5),new Pair<HediffDef, int>(MyDefOf.extremePrecision, 5), new Pair<HediffDef, int>(MyDefOf.BadBack, 15),new Pair<HediffDef, int>(MyDefOf.Frail, 25), new Pair<HediffDef, int>(MyDefOf.Blindness, 25),new Pair<HediffDef, int>(MyDefOf.HearingLoss,25)

        };

        public static List<Pair<HediffDef, int>> Tier2P = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.extremeSpeed, 5), new Pair<HediffDef, int>(MyDefOf.BadBack, 30), new Pair<HediffDef, int>(MyDefOf.Frail, 25), new Pair<HediffDef, int>(MyDefOf.Blindness, 20), new Pair<HediffDef, int>(MyDefOf.HearingLoss,20)

        };

        public static List<Pair<HediffDef, int>> Tier3P = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.extremeSpeed, 15), new Pair<HediffDef, int>(MyDefOf.Frail, 25), new Pair<HediffDef, int>(MyDefOf.Blindness, 25), new Pair<HediffDef, int>(MyDefOf.HearingLoss, 45)

        };

        public static List<Pair<HediffDef, int>> Tier4P = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.extremeSpeed, 30), new Pair<HediffDef, int>(MyDefOf.extremePrecision, 30), new Pair<HediffDef, int>(MyDefOf.angryBrawler, 15), new Pair<HediffDef, int>(MyDefOf.Blindness, 10), new Pair<HediffDef, int>(MyDefOf.HearingLoss, 15)

        };

        public static List<Pair<HediffDef, int>> Tier5P = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.extremeSpeed, 20), new Pair<HediffDef, int>(MyDefOf.angryBrawler, 30), new Pair<HediffDef, int>(MyDefOf.extremePrecision, 20), new Pair<HediffDef, int>(MyDefOf.HearingLoss, 5)

        };

        public static List<Pair<HediffDef, float>> Tier6P = new List<Pair<HediffDef, float>>
        {

            new Pair<HediffDef, float>(MyDefOf.extremeSpeed, 16.67f), new Pair<HediffDef, float>(MyDefOf.rockSkin, 16.67f), new Pair<HediffDef, float>(MyDefOf.extremePrecision, 16.67f), new Pair<HediffDef, float>(MyDefOf.treeSkin, 16.67f), new Pair<HediffDef, float>(MyDefOf.angryBrawler, 16.67f)

        };


        public static List<Pair<HediffDef, int>> Tier1M = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.Insomnia, 5), new Pair<HediffDef, int>(MyDefOf.BadBack, 20),new Pair<HediffDef, int>(MyDefOf.Frail, 25), new Pair<HediffDef, int>(MyDefOf.Blindness, 25),new Pair<HediffDef, int>(MyDefOf.HearingLoss,25)

        };

        public static List<Pair<HediffDef, int>> Tier2M = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.Insomnia, 10), new Pair<HediffDef, int>(MyDefOf.BadBack, 30),new Pair<HediffDef, int>(MyDefOf.extremeUnderstanding, 5), new Pair<HediffDef, int>(MyDefOf.Blindness, 25),new Pair<HediffDef, int>(MyDefOf.HearingLoss,30)

        };

        public static List<Pair<HediffDef, int>> Tier3M = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.Insomnia, 10), new Pair<HediffDef, int>(MyDefOf.extremeUnderstanding, 15),new Pair<HediffDef, int>(MyDefOf.Frail, 25), new Pair<HediffDef, int>(MyDefOf.Blindness, 25),new Pair<HediffDef, int>(MyDefOf.HearingLoss,25)

        };

        public static List<Pair<HediffDef, int>> Tier4M = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.Insomnia, 15), new Pair<HediffDef, int>(MyDefOf.extremeIntelligence, 25),new Pair<HediffDef, int>(MyDefOf.unwaveringMorale, 15), new Pair<HediffDef, int>(MyDefOf.Blindness, 25),new Pair<HediffDef, int>(MyDefOf.HearingLoss,25)

        };

        public static List<Pair<HediffDef, int>> Tier5M = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.unwaveringMorale, 20), new Pair<HediffDef, int>(MyDefOf.extremeIntelligence, 25),new Pair<HediffDef, int>(MyDefOf.Insomnia, 30), new Pair<HediffDef, int>(MyDefOf.Blindness, 10),new Pair<HediffDef, int>(MyDefOf.HearingLoss,10)

        };

        public static List<Pair<HediffDef, int>> Tier6M = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.Insomnia, 15), new Pair<HediffDef, int>(MyDefOf.unwaveringMorale, 20),new Pair<HediffDef, int>(MyDefOf.extremeIntelligence, 30), new Pair<HediffDef, int>(MyDefOf.extremeUnderstanding, 30),new Pair<HediffDef, int>(MyDefOf.HearingLoss,5)

        };



    }
}
