using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OriginL
{
    public class ResourceType{

        [SerializeField] int TotalAmount;

        public int GetValue() {
            int finalValue = TotalAmount;
            return finalValue;
        }

        public int Add(int amount) {
            TotalAmount += amount;
            return TotalAmount;
        }
        public int Remove(int amount) {
            TotalAmount -= amount;
            return TotalAmount;
        }

    }
}
