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
        public static HediffDef superSpeed;

        //JobDef
        public static JobDef enterCreationChamber;

        //SoundDef
        public static SoundDef CreationChamber_Accept;
        public static SoundDef CreationChamber_Reject;


        static MyDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MyDefOf));
        }
    }
}


