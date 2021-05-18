using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Between.Saving
{
    [Serializable]
    public class PlayerData
    {
        public bool SomeBoolValue;
        public int SomeIntValue;
        public string SomeStringValue;

        public PlayerData CreateDefault()
        {
            SomeBoolValue = false;
            SomeIntValue = 123;
            SomeStringValue = "Default";

            return this;
        }
    }
}