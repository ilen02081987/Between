using System;
using Between.SpellRecognition;

namespace Between.Spells
{
    public class FailedTrackerSpell : SvmBasedSpell
    {
        protected override SpellFigure _figure { get; } = SpellFigure.None;
        protected override float _manaCoefficient => 0f;

        protected override void OnCompleteSpell()
        {
            InvokeNotRecognizeEvent();
        }
    }
}