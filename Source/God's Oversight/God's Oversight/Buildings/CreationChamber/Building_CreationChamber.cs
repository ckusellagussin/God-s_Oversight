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
        string[] isMental = { "TooSmart-0", "FastLearner-0", "Nerves-2", "Nerves--1", "NaturalMood-2", "NaturalMood--2", "Psychopath-0", "SlowLearner-0", "GreatMemory", "Industriousness-1", "Industriousness-2", "Nerves-1" };

        string[] plus20 = { "Masochist-0", "SpeedOffset-2", "DrugDesire-1", "Industriousness-1" };
        string[] plus30 = { "DrugDesire-2", "Industriousness-2", "NaturalMood-1", "Nerves-1" };
        string[] plus40 = { "NaturalMood-2", "Nerves-2", "Immunity-1" };
        string[] plus50 = { "Tough-0" };

        string[] minus20 = { "Industriousness--2", "Nerves--1", "Neurotic-1" };
        string[] minus30 = { "NaturalMood--1" };
        string[] minus40 = { "NaturalMood--2" };
        string[] minus50 = { "Wimp-0", "Nerves--2", "Neurotic-2" };
        string[] minus100 = { "Immunity-1" };

        public int baseChance = 30;
        

        public HediffDef[] Tier1p =
        {

        };

        

        public HediffDef[] Tier1m =
        {

        };
 

        public HediffDef[] Tier2p =
        {

        };
       
        public HediffDef[] Tier2m =
        {

        };

        public HediffDef[] Tier3p =
        {


        };

        public HediffDef[] Tier3m =
       {


       };

        public HediffDef[] Tier4p =
        {


        };

        public HediffDef[] Tier4m =
        {


        };

        public HediffDef[] Tier5p =
        {

        };

        public HediffDef[] Tier5m =
        {

        };

        public HediffDef[] Tier6p =
        {


        };

        public HediffDef[] Tier6m =
        {


        };







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

        foreach(Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;

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
                MyDefOf.CreationChamber_Reject.PlayOneShot(SoundInfo.InMap(new TargetInfo(base.Position, base.Map)));
               

            }
            base.EjectContents();


            
        }

        public void creationSystem()
        {
            //Need value of pMinded or mMinded depending on what is more
            //Need to know what is more as well so a bool saying isPhys or isMental true
            //Need to get success chance from pawn too


            //foreach (Thing item in (IEnumerable<Thing>)innerContainer)
            //{
            //    actor = item as Pawn;
            //    if(actor != null)
            //    {
            //        PawnComponentsUtility.AddComponentsForSpawn(pawn); 
            //        if(pawn.RaceProps.IsFlesh)
            //        {
                        





            //        }

                    

            //    }


            //}




        }

        public void successChance(Pawn actor)
        {
         var traits = actor.story.traits.allTraits;
         string converted = string.Join(", ", traits);

            Log.Message(converted);

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

        }

        public void traitSystem(Pawn actor)
        {

            var traits = actor.story.traits.allTraits;
            

            int pMinded = 0;
            int mMinded = 0;
            

            string converted = string.Join(", ", traits);



            foreach (string traitsPawn in isPhys)
            {

                if (converted.Contains(traitsPawn))
                {

                    pMinded += 1;

                }

            }

            foreach (string traitsPawn in isMental)
            {

                if (converted.Contains(traitsPawn))
                {

                    mMinded += 1;

                }

            }

            if (pMinded >= mMinded)
            {

                Log.Message("This pawn is more Physical Minded with " + pMinded + " physical traits");

            }
            else if (mMinded > pMinded)
            {

                Log.Message("This pawn is more Mental Minded with " + mMinded + " mental traits");

            }

        }



    }



}

