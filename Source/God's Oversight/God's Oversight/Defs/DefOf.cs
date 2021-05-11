using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using RimWorld;

namespace God_s_Oversight.Defs
{
    [DefOf]
    public static class MyDefOf
    {

        //ThingDef
        public static ThingDef creationChamber;

        //Abilities and other HeDif
        public static HediffDef dnaideSickness;
        public static HediffDef extremeSpeed;
        public static HediffDef BadBack;
        public static HediffDef Frail;
        public static HediffDef Blindness;
        public static HediffDef HearingLoss;
        public static HediffDef Dementia;
        public static HediffDef extremePrecision;
        public static HediffDef extremeIntelligence;
        public static HediffDef extremeUnderstanding;
        public static HediffDef rockSkin;
        public static HediffDef treeSkin;
        public static HediffDef angryBrawler;
        public static HediffDef unwaveringMorale;

        //JobDef
        public static JobDef enterCreationChamber;

        //SoundDef
        public static SoundDef CreationChamber_Accept;
        public static SoundDef CreationChamber_Eject;


        static MyDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MyDefOf));
        }
    }
}


