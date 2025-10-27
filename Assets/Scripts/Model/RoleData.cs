namespace Model
{
    using System;

    [Serializable]
    public class RoleData
    {
        public int id;
        public string name;
        public string avatarImagePath;
        public string describe;
        public int slot;
        public int record;
        public int unlock;
        public string unlockConditions;
    }
}