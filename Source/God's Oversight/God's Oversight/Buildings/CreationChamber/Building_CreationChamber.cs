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

namespace God_s_Oversight.Buildings.CreationChamber
{
    class Building_CreationChamber : Building_Casket
    {
       

        public override bool TryAcceptThing(Thing thing, bool allowSpecialEffects = true)
        {
            if (base.TryAcceptThing(thing, allowSpecialEffects))
            {
                if (allowSpecialEffects)
                {
                    MyDefOf.CreationChamber_Accept.PlayOneShot(new TargetInfo(base.Position, base.Map));

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

      /*  public static Building_CreationChamber FindCreationChamberFor (Pawn p, Pawn traveler, bool ignoreOtherReservations = false)
        {


            foreach (DefOf item in DefDatabase<DefOf>.AllDefs.Where((DefOf def) => typeof(Building_CreationChamber).IsAssignableFrom(def.thingClass)))
            {




            }



            return null;
        }

        
      */

    }



}

