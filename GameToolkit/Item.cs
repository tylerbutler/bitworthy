
namespace TylerButler.GameToolkit
{
    public abstract class Item : GameComponent, Valuable, Carryable
    {
        private float value, size, weight;

        public float Value
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                this.value = value;
            }
        }

        #region Carryable Members

        public float Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        public float Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
            }
        }

        #endregion
    }
}
