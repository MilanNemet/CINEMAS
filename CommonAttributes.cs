#define DEBUG
//#undef DEBUG

namespace Cinemas
{
    abstract class CommonAttributes
    {
        public string Name { get; private set; }
        public CommonAttributes(string Name)
        {
            #region debug message
#if DEBUG
            Program.LogItsCaller();
#endif
            #endregion
            this.Name = Name;
        }
    }
}
