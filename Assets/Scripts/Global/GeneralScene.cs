namespace UnityEngine
{
    public class GeneralScene
    {
        private static readonly GeneralScene _instance = new GeneralScene();
        private GeneralScene() { }
        public static GeneralScene Instance
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