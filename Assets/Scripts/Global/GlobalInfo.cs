namespace UnityEngine
{
    public class GlobalInfo : MonoBehaviour
    {
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
        public Vector3 getRandomVectorEnemy()
        {
            float randomX = Random.Range(-1 * (XScreenBorder - 1), (XScreenBorder - 1));
            return new Vector3(randomX, 7.5f, 0);
        }
    }
}