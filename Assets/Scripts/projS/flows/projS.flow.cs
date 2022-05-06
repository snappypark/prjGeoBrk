using nj;

namespace ProjS
{
    public abstract class Flow : Flow_Abs
    {
        public override int iType { get { return (int)Type; } }
        protected virtual eType Type { get { return eType.None; } }

        public enum eType
        {
            None = 0,
            Entry,
            Menu,
            Play,
            EditWave,
        }

        public const int iTypeEntry = 1;
        public const int iTypeMenu = 2;
        public const int iTypePlay = 3;
        public const int iTypeEditWave = 4;

    }

}