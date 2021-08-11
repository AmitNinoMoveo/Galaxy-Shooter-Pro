namespace UnityEngine
{
    public class Life
    {
        private int _maxLife;
        private int _currentLife;
        private bool _isAlive = true;

        public Life(int maxLife = 1)
        {
            this._maxLife = maxLife;
            this._currentLife = maxLife;
        }
        public void decreseCurrentLife(int amount = 1)
        {
            if (amount >= this._currentLife)
            {
                this._isAlive = false;
                this._currentLife = 0;
            }
            else
            {
                this._currentLife -= amount;
            }
        }

        public void increaseCurrentLife(int amount)
        {
            if ((amount + this._currentLife) >= this._maxLife)
            {
                this._currentLife = this._maxLife;
            }
            else
            {
                this._currentLife += amount;
            }
        }

        public bool isAlive()
        {
            return _isAlive;
        }
    }
}