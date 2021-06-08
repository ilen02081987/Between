using Between.Teams;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Interfaces
{
    public interface IDamagable
    {
        Team Team { get; set; }
        void ApplyDamage(float damage);
    }
}