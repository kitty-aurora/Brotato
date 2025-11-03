using System;

namespace Model
{
    [Serializable]
    public class WeaponData
    {
        public float damage;
        public int isLong;
        public float range;
        public float critical_strikes_multiple;
        public float critical_strikes_probability;
        public float cooling;
        public int repel;
    }
}