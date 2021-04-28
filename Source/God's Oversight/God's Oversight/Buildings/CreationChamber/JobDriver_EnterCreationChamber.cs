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

namespace God_s_Oversight.Buildings
{

  
    class JobDriver_enterCreationChamber : JobDriver
    {


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
                    if (chamber is Building_CreationChamber creation)
                    {
                      //  creation.creationSystem(actor);

                       
                    }



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


    }


}




