using Between.UserInput.Trackers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Spells
{
    public class ShieldSpell : BaseSpell
    {
        protected override BaseInputTracker tracker => _tracker;

        private CurveTracker _tracker = new CurveTracker();

        protected override void OnCanCompleteDraw()
        {
            Debug.Log("Can complete draw shield spell");
        }

        protected override void OnCompleteDraw()
        {
            Debug.Log("Complete draw shield spell");
        }

        protected override void OnDrawFailed()
        {
            Debug.Log("Draw shield spell failed");
        }
    }
}