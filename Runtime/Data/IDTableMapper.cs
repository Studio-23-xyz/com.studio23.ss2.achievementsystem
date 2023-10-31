using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Data
{
    public class IDTableMapper : ScriptableObject
    {
        public List<IDMap> IDMaps;
    }


    [Serializable]
    public class IDMap
    {
        public string Key;
        public string Value;
    }


}
