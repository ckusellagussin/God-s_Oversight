using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;
using RimWorld;
using God_s_Oversight.Buildings.CreationChamber;
using System.Diagnostics;
using God_s_Oversight.Creation_System;

namespace God_s_Oversight.Buildings
{

  
    class JobDriver_enterCreationChamber : JobDriver
    {
        string[] isPhys = { "Bloodlust", "Brawler", "Tough-0", "Nimble-0", "SpeedOffset-2", "SpeedOffset-1", "Masochist-0", "QuickSleeper-0", "ShootingAccuracy-1", "ShootingAccuracy--1", "Immunity-1"};
        string[] isMental = { "TooSmart-0", "FastLearner-0", "Nerves-2", "Nerves--1", "NaturalMood-2", "NaturalMood--2", "Psychopath-0", "SlowLearner-0", "GreatMemory", "Industriousness-1", "Industriousness-2", "Nerves-1"};

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(job.targetA, job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
           
            this.FailOnDespawnedOrNull(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.InteractionCell); //Pawn walks to creation chamber and stops walking when interaction starts
            Toil toil = Toils_General.Wait(500);
            toil.FailOnCannotTouch(TargetIndex.A, PathEndMode.InteractionCell);
            toil.WithProgressBarToilDelay(TargetIndex.A);
            yield return toil;
            Toil enter = new Toil();

            //Start action of pawn entering the chamber
            enter.initAction = delegate
            {
                
                Pawn actor = enter.actor;
                Building_CreationChamber chamber = (Building_CreationChamber)actor.CurJob.targetA.Thing;
                Action action = delegate
                {
                    actor.DeSpawn();                   
                    chamber.TryAcceptThing(actor);                   

                    traitSystem();
                    
                  

   

                };
               
                if (!chamber.def.building.isPlayerEjectable)
                {
                    if (base.Map.mapPawns.FreeColonistsSpawnedOrInPlayerEjectablePodsCount <= 1)
                    {
                        Find.WindowStack.Add(Dialog_MessageBox.CreateConfirmation("ChamberWarning".Translate(actor.Named("PAWN")).AdjustedFor(actor), action));

                    }
                    else
                    {

                        action();
                    }
                }
                else
                {
                    action();

                }

            };
            enter.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return enter;


        }




        public void traitSystem()
        {

            var traits = pawn.story.traits.allTraits;


            int pMinded = 0;
            int mMinded = 0;

            string converted = string.Join(", ", traits);

            Log.Message(converted);

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

                Log.Message("This pawn is more Physical Minded traits with" + pMinded + " physical traits");

            }
            else if (mMinded > pMinded)
            {

                Log.Message("This pawn is more Mental Minded traits with" + mMinded + " physical traits");

            }



        }






    }



    }




