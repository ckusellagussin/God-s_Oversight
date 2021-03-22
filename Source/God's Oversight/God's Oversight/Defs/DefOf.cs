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
        public static ThingDef creationChamber;
        public static HediffDef dnaideSickness;
        public static JobDef enterCreationChamber;
        public static SoundDef CreationChamber_Accept;
        public static SoundDef CreationChamber_Reject;


        static MyDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MyDefOf));
        }
    }
}


