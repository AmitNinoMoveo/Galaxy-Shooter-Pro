namespace UnityEngine
{
    public class GlobalInfo
    {
        private static readonly GlobalInfo _instance = new GlobalInfo();
        private GlobalInfo() { }
        public static GlobalInfo Instance
        {
            get => _instance;
        }
        private static readonly float _yScreenBorder = 6f;
        private static readonly float _xScreenBorder = 11f;
        public float YScreenBorder
        {
            get => _yScreenBorder;
        }
        public float XScreenBorder
        {
            get => _xScreenBorder;
        }
    }
}