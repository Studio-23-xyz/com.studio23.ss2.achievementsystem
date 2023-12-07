using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Studio23.SS2.AchievementSystem.Data
{
    public class IDTableMapper : ScriptableObject
    {
        public List<IDMap> IDMaps;
        public string GetMappedID(string id)
        {
           return  IDMaps.First(r => r.Key == id).Value;
        }
    }


    [Serializable]
    public class IDMap
    {
        public string Key;
        public string Value;
    }


}
