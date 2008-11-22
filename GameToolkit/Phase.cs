
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

        /// <summary>
        /// All of the logic that occurs when the phase is invoked.
        /// </summary>
        /// <returns>The next phase to run.</returns>
        public abstract Phase Execute();

        #endregion
    }
}
