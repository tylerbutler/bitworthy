namespace Layout
{
    using System;
	using System.Drawing;

    /// <summary>
    /// Layout Manager abstraction.
    /// </summary>
    /// <remarks>
    /// This class abstracts away some of the more common functions of a LayoutManager, such as
    /// its Size and Location.
    /// </remarks>
    /// <author>Matthew Johnson</author>
    ///	<version>1.0</version> 
    public abstract class AbstractLayoutManager : ILayoutManager
    {
		/// <summary>
		/// Constructor.
		/// </summary>
        public AbstractLayoutManager()
        {
            Location = new Point(0, 0);
			Size = new Size(0, 0);
        }

		public abstract void AddLayoutable(ILayoutable l, object constraints);
		public abstract void RemoveLayoutable(ILayoutable l); 
        public abstract Size GetMinimumSize();
		public abstract Size GetPreferredSize();
		public abstract void Layout();
		public abstract void RemoveAll();

		public void SetLocation(Point p)
		{
			Location = p;
		}

		public void SetSize(Size s)
		{
			Size = s;
		}
		/// <summary>
		/// Returns the largest Size possible.
		/// </summary>
		public Size GetMaximumSize()
		{
			return new Size(Int32.MaxValue, Int32.MaxValue);
		}
		/// <summary>
		/// The Location of the manager.
		/// </summary>
		public Point Location
		{
			get
			{
				return myLocation;
			}
			set
			{
				myLocation = value;
			}
		}
		/// <summary>
		/// The Size of the manager.
		/// </summary>
		public Size Size
		{
			get
			{
				return mySize;
			}
			set
			{
				mySize = value;
			}
		}

		private Point myLocation;
		private Size mySize;
    }
}
