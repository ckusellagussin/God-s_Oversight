using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using God_s_Oversight;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.Sound;
using God_s_Oversight.Defs;
using UnityEngine;
using God_s_Oversight.Buildings;


namespace God_s_Oversight.Buildings.CreationChamber
{


    class Building_CreationChamber : Building_Casket
    {
        string[] isPhys = { "Bloodlust-0", "Brawler-0", "Tough-0", "Nimble-0", "SpeedOffset-2", "SpeedOffset-1", "Masochist-0", "QuickSleeper-0", "ShootingAccuracy-1", "ShootingAccuracy--1", "Immunity-1" };
        string[] isMent = { "TooSmart-0", "FastLearner-0", "Nerves-2", "Nerves--1", "NaturalMood-2", "NaturalMood--2", "Psychopath-0", "SlowLearner-0", "GreatMemory", "Industriousness-1", "Industriousness-2", "Nerves-1" };

        string[] plus20 = { "Masochist-0", "SpeedOffset-2", "DrugDesire-1", "Industriousness-1" };
        string[] plus30 = { "DrugDesire-2", "Industriousness-2", "NaturalMood-1", "Nerves-1" };
        string[] plus40 = { "NaturalMood-2", "Nerves-2", "Immunity-1" };
        string[] plus50 = { "Tough-0" };

        string[] minus20 = { "Industriousness--2", "Nerves--1", "Neurotic-1" };
        string[] minus30 = { "NaturalMood--1" };
        string[] minus40 = { "NaturalMood--2" };
        string[] minus50 = { "Wimp-0", "Nerves--2", "Neurotic-2" };
        string[] minus100 = { "Immunity-1" };

        public override bool TryAcceptThing(Thing thing, bool allowSpecialEffects = true)
        {
            if (base.TryAcceptThing(thing, allowSpecialEffects))
            {
                if (allowSpecialEffects)
                {
                    MyDefOf.CreationChamber_Accept.PlayOneShot(new TargetInfo(base.Position, base.Map));
                    Log.Message("You have entered the cage");

                }
                return true;
            }
            return false;
        }

        public override IEnumerable<FloatMenuOption> GetFloatMenuOptions(Pawn mypawn)
        {
            if (mypawn.IsQuestLodger())
            {
                yield return new FloatMenuOption("CannotUseReason".Translate("CreationChamberGuestsNotAllowed".Translate()), null);
                yield break;
            }

            foreach (FloatMenuOption floatMenuOption in base.GetFloatMenuOptions(mypawn))
            {
                yield return floatMenuOption;

            }
            if (innerContainer.Count != 0)
            {
                yield break;

            }
            if(!mypawn.CanReach(this, Verse.AI.PathEndMode.InteractionCell, Danger.Deadly))
            {
                yield return new FloatMenuOption("CannotUseNoPath".Translate(), null);
                yield break;


            }
            JobDef jobDef = MyDefOf.enterCreationChamber;
            string label = "EnterCreationChamber".Translate();
            Action action = delegate
            {
                Job job = JobMaker.MakeJob(jobDef, this);
                mypawn.jobs.TryTakeOrderedJob(job);

            };

            yield return FloatMenuUtility.DecoratePrioritizedTask(new FloatMenuOption(label, action), mypawn, this);

        }

        public override IEnumerable<Gizmo> GetGizmos()
        {

            Pawn actor = ContainedThing as Pawn;

            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;

            }

            if (base.Faction == Faction.OfPlayer && innerContainer.Count > 0 && this.GetComp<CompRefuelable>().IsFull && IsPowered(this, out _) == true)
            {

                Command_Action command_Action = new Command_Action();
                command_Action.action = () => creationSystem(actor);
                command_Action.defaultLabel = "StartEvolution".Translate();

                if (innerContainer.Count == 0)
                {
                    command_Action.Disable("Can'tEjectFromChamber".Translate());



                }
                command_Action.icon = ContentFinder<Texture2D>.Get("UI/Commands/PodEject");
                yield return command_Action;

            }

            if (base.Faction == Faction.OfPlayer && innerContainer.Count > 0 && def.building.isPlayerEjectable)
            {
                Command_Action command_Action = new Command_Action();
                command_Action.action = EjectContents;
                command_Action.defaultLabel = "CommandPodEject".Translate();

                if (innerContainer.Count == 0)
                {
                    command_Action.Disable("CommandPodEjectFailEmpty".Translate());



                }
                command_Action.icon = ContentFinder<Texture2D>.Get("UI/Commands/PodEject");
                yield return command_Action;
            }
            
            

        }

        public override void EjectContents()
        {
            
            foreach (Thing item in (IEnumerable<Thing>)innerContainer)
            {
                Pawn pawn = item as Pawn;
                if (pawn != null)
                {
                    PawnComponentsUtility.AddComponentsForSpawn(pawn);
                    if (pawn.RaceProps.IsFlesh)
                    {
                        pawn.health.AddHediff(MyDefOf.dnaideSickness);
                        
                    }

                }

            }

            if (!base.Destroyed)
            {
                MyDefOf.CreationChamber_Eject.PlayOneShot(SoundInfo.InMap(new TargetInfo(base.Position, base.Map)));
               

            }
            base.EjectContents();


            
        }


        public bool IsPowered(ThingWithComps thing, out bool usesPower)
        {
            var comp = thing.GetComp<CompPowerTrader>();
            usesPower = comp != null;
            
            return usesPower && comp.PowerOn;
        }

       
        
        
        
        private int successChance(Pawn actor)
        {

        int baseChance = 30;
       
        var traits = actor.story.traits.allTraits;
         
        string converted = string.Join(", ", traits);
         
            foreach (string traitsPawn in plus20)
            {


                if (converted.Contains(traitsPawn))
                {

                    baseChance += 20;


                }

            }

            foreach (string traitsPawn in plus30)
            {

                if (converted.Contains(traitsPawn))
                {

                    baseChance += 30;

                }


            }

            foreach (string traitsPawn in plus40)
            {

                if (converted.Contains(traitsPawn))
                {

                    baseChance += 40;

                }


            }

            foreach (string traitsPawn in plus50)
            {

                if (converted.Contains(traitsPawn))
                {

                    baseChance += 50;

                }


            }

            foreach (string traitsPawn in minus20)
            {

                if (converted.Contains(traitsPawn))
                {

                    baseChance -= 20;

                }


            }

            foreach (string traitsPawn in minus30)
            {

                if (converted.Contains(traitsPawn))
                {

                    baseChance -= 30;

                }


            }

            foreach (string traitsPawn in minus40)
            {

                if (converted.Contains(traitsPawn))
                {

                    baseChance -= 40;

                }


            }

            foreach (string traitsPawn in minus50)
            {

                if (converted.Contains(traitsPawn))
                {

                    baseChance -= 50;

                }


            }

            foreach (string traitsPawn in minus100)
            {

                if (converted.Contains(traitsPawn))
                {

                    baseChance -= 100;

                }


            }
           
            Log.Message("The chance of success for this pawn is " + baseChance + "%");
            return baseChance;
        }


        private int PhysicalTraits(Pawn actor)
        {
            var traits = actor.story.traits.allTraits;
            string converted = string.Join(", ", traits);

            int pMinded = 0;

            foreach (string traitsPawn in isPhys)
            {

                if (converted.Contains(traitsPawn))
                {

                    pMinded += 1;
                    Log.Message("This pawn has " + pMinded + " Physical traits");
                    
                    
                }

             }

            return pMinded;

        }


        private int MentalTraits(Pawn actor)
        {
            var traits = actor.story.traits.allTraits;
            string converted = string.Join(", ", traits);

            int mMinded = 0;


            foreach (string traitsPawn in isMent)
            {

                if (converted.Contains(traitsPawn))
                {
                    
                    mMinded += 1;
                    Log.Message("This pawn has"+ mMinded+" Mental traits");
                    
                    
                }
               

            }
               return mMinded;

        }

        private bool Mindedness(Pawn actor)
        {
            int physical = PhysicalTraits(actor);
            int mental = MentalTraits(actor);
            int sum = 0;
            

            if (physical >= mental)
            {
                sum = physical - mental;
                Log.Message("This pawn has more physical traits with " +sum+ " trait/s more than mental");
                return true;
                
            }
            sum = mental - physical;
            Log.Message("This pawn has more mental traits with " +sum+ " trait/s more than physical");
            return false;
                    
        }

 

        public void creationSystem(Pawn actor)
        {

           
            int physical = PhysicalTraits(actor);
            int mental = MentalTraits(actor);
            int totalSuccess = successChance(actor);          
             
            bool isPhysical = Mindedness(actor);


           foreach (Thing item in (IEnumerable<Thing>)innerContainer)
           {

               if (actor != null)
               {
                    PawnComponentsUtility.AddComponentsForSpawn(actor);
                    if (actor.RaceProps.IsFlesh)
                    {

                        if (isPhysical == true)
                        {
                            var fuelComp = this.GetComp<CompRefuelable>();
                            fuelComp.ConsumeFuel(1);
                            

                            if (totalSuccess <= 24)
                            {
                                HediffDef tier1 = Tier1P.RandomElementByWeight(x => x.Second).First;
                                actor.health.AddHediff(tier1);
                                Log.Message("This Pawn has been given " + tier1 + " from the Death Tier");

                            }

                            if (totalSuccess >= 25 && totalSuccess <= 49)
                            {
                                HediffDef tier2 = Tier2P.RandomElementByWeight(x => x.Second).First;
                                actor.health.AddHediff(tier2);
                                Log.Message("This Pawn has been given " + tier2 + " from Physical Tier 2");

                            }
                            if (totalSuccess >= 50 && totalSuccess <= 59)
                            {
                                HediffDef tier3 = Tier3P.RandomElementByWeight(x => x.Second).First;
                                actor.health.AddHediff(tier3);
                                Log.Message("This Pawn has been given " + tier3 + " from Physical Tier 3");

                            }
                            if (totalSuccess >= 60 && totalSuccess <= 79)
                            {
                                HediffDef tier4 = Tier4P.RandomElementByWeight(x => x.Second).First;
                                actor.health.AddHediff(tier4);
                                Log.Message("This Pawn has been given " + tier4 + " from Physical Tier 4");

                            }
                            if (totalSuccess >= 80 && totalSuccess <= 89)
                            {
                                HediffDef tier5 = Tier5P.RandomElementByWeight(x => x.Second).First;
                                actor.health.AddHediff(tier5);
                                Log.Message("This Pawn has been given " + tier5 + " from Physical Tier 5");

                            }

                            if (totalSuccess >= 90)
                            {
                                HediffDef tier6 = Tier6P.RandomElementByWeight(x => x.Second).First;
                                actor.health.AddHediff(tier6);
                                Log.Message("This Pawn has been given " + tier6 + " from Physical God Tier");

                            }

                        }

                        else if (isPhysical == false)
                        {
                            var fuelComp = this.GetComp<CompRefuelable>();
                            fuelComp.ConsumeFuel(1);


                            if (totalSuccess <= 24)
                            {
                                HediffDef tier1M = Tier1M.RandomElementByWeight(x => x.Second).First;
                                actor.health.AddHediff(tier1M);
                                Log.Message("This Pawn has been given " + tier1M + " from Death Tier");

                            }

                            if (totalSuccess >= 25 && totalSuccess <= 49)
                            {
                                HediffDef tier2M = Tier2M.RandomElementByWeight(x => x.Second).First;
                                actor.health.AddHediff(tier2M);
                                Log.Message("This Pawn has been given " + tier2M + " from Mental Tier 2");

                            }
                            if (totalSuccess >= 50 && totalSuccess <= 59)
                            {
                                HediffDef tier3M = Tier3P.RandomElementByWeight(x => x.Second).First;
                                actor.health.AddHediff(tier3M);
                                Log.Message("This Pawn has been given " + tier3M + " from Mental Tier 3");

                            }
                            if (totalSuccess >= 60 && totalSuccess <= 79)
                            {
                                HediffDef tier4M = Tier4P.RandomElementByWeight(x => x.Second).First;
                                actor.health.AddHediff(tier4M);
                                Log.Message("This Pawn has been given " + tier4M + " from Mental Tier 4");

                            }
                            if (totalSuccess >= 80 && totalSuccess <= 89)
                            {
                                HediffDef tier5M = Tier5P.RandomElementByWeight(x => x.Second).First;
                                actor.health.AddHediff(tier5M);
                                Log.Message("This Pawn has been given " + tier5M + " from Mental Tier 5");

                            }

                            if (totalSuccess >= 90)
                            {
                                HediffDef tier6M = Tier6P.RandomElementByWeight(x => x.Second).First;
                                actor.health.AddHediff(tier6M);
                                Log.Message("This Pawn has been given " + tier6M + " from Mental God Tier");

                            }


                        }

                    }

               }


           }
     

        }


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

            new Pair<HediffDef, int>(MyDefOf.extremeSpeed, 10), new Pair<HediffDef, int>(MyDefOf.BadBack, 15),new Pair<HediffDef, int>(MyDefOf.Frail, 25), new Pair<HediffDef, int>(MyDefOf.Blindness, 25),new Pair<HediffDef, int>(MyDefOf.HearingLoss,25)

        };

        public static List<Pair<HediffDef, int>> Tier2M = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.extremeSpeed, 10), new Pair<HediffDef, int>(MyDefOf.BadBack, 15),new Pair<HediffDef, int>(MyDefOf.extremeUnderstanding, 25), new Pair<HediffDef, int>(MyDefOf.Blindness, 25),new Pair<HediffDef, int>(MyDefOf.HearingLoss,25)

        };

        public static List<Pair<HediffDef, int>> Tier3M = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.extremeSpeed, 10), new Pair<HediffDef, int>(MyDefOf.extremeUnderstanding, 15),new Pair<HediffDef, int>(MyDefOf.Frail, 25), new Pair<HediffDef, int>(MyDefOf.Blindness, 25),new Pair<HediffDef, int>(MyDefOf.HearingLoss,25)

        };

        public static List<Pair<HediffDef, int>> Tier4M = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.extremeSpeed, 10), new Pair<HediffDef, int>(MyDefOf.BadBack, 15),new Pair<HediffDef, int>(MyDefOf.Frail, 25), new Pair<HediffDef, int>(MyDefOf.Blindness, 25),new Pair<HediffDef, int>(MyDefOf.HearingLoss,25)

        };

        public static List<Pair<HediffDef, int>> Tier5M = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.unwaveringMorale, 10), new Pair<HediffDef, int>(MyDefOf.extremeIntelligence, 40),new Pair<HediffDef, int>(MyDefOf.Frail, 25), new Pair<HediffDef, int>(MyDefOf.Blindness, 25),new Pair<HediffDef, int>(MyDefOf.HearingLoss,25)

        };

        public static List<Pair<HediffDef, int>> Tier6M = new List<Pair<HediffDef, int>>
        {

            new Pair<HediffDef, int>(MyDefOf.extremeSpeed, 10), new Pair<HediffDef, int>(MyDefOf.unwaveringMorale, 15),new Pair<HediffDef, int>(MyDefOf.Frail, 25), new Pair<HediffDef, int>(MyDefOf.Blindness, 25),new Pair<HediffDef, int>(MyDefOf.HearingLoss,25)

        };




    }



}

