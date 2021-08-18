namespace UnityEngine
{
    public class GlobalInfo : MonoBehaviour
    {
        private float _yScreenBorder = 6f; // size is 6f
        public float YScreenBorder
        {
            get => _yScreenBorder;
        }
        private float _xScreenBorder = 11f; // size is 11f
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