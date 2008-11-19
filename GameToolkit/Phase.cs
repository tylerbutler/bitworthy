
namespace TylerButler.GameToolkit
{
    public abstract class Phase
    {
        private string title, description;

        #region Phase Members

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public abstract Phase Execute();

        #endregion
    }
}
