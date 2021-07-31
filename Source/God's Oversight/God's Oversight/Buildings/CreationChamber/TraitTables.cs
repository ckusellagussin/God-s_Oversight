using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace God_s_Oversight.Buildings.CreationChamber
{
     public static class TraitTables
    {

        public static readonly string[] isPhys = { "Bloodlust-0", "Brawler-0", "Tough-0", "Nimble-0", "SpeedOffset-2", "SpeedOffset-1", "Masochist-0", "QuickSleeper-0", "ShootingAccuracy-1", "ShootingAccuracy--1", "Immunity-1" };
        public static readonly string[] isMent = { "TooSmart-0", "FastLearner-0", "Nerves-2", "Nerves--1", "NaturalMood-2", "NaturalMood--2", "Psychopath-0", "SlowLearner-0", "GreatMemory", "Industriousness-1", "Industriousness-2", "Nerves-1" };

        public static readonly string[] plus20 = { "Masochist-0", "SpeedOffset-2", "DrugDesire-1", "Industriousness-1" };
        public static readonly string[] plus30 = { "DrugDesire-2", "Industriousness-2", "NaturalMood-1", "Nerves-1" };
        public static readonly string[] plus40 = { "NaturalMood-2", "Nerves-2", "Immunity-1" };
        public static readonly string[] plus50 = { "Tough-0" };

        public static readonly string[] minus20 = { "Industriousness--2", "Nerves--1", "Neurotic-1" };
        public static readonly string[] minus30 = { "NaturalMood--1" };
        public static readonly string[] minus40 = { "NaturalMood--2" };
        public static readonly string[] minus50 = { "Wimp-0", "Nerves--2", "Neurotic-2" };
        public static readonly string[] minus100 = { "Immunity-1" };


    }
}
