namespace Layout
{
    using System;
	using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Top level layout Area.
    /// </summary>
    /// <remarks>
    /// This top level layout component lays out Layoutables in the specified bounds inside a parent
    /// control.  This Area can be changed, but it will not change on its own.
    /// </remarks>
    /// <author>Matthew Johnson</author>
    /// <version>1.0</version>
    public class AreaPane : ContainerBox
    {
		/// <summary>
		/// Constructs a new AreaPane with the specified parent and a zero-sized Area.
		/// </summary>
		/// <param name="parent">The parent control for the Area </param>
        public AreaPane(Control parent) : this(parent, new Rectangle(0, 0, 0, 0))
        {
        }
		/// <summary>
		/// Constructs a new AreaPane with the specified parent, Area and a BorderLayout.
		/// </summary>
		/// <param name="parent">The parent control for the Area </param>
		/// <param name="Area">The Area in which to layout objects </param>
		public AreaPane(Control parent, Rectangle Area) : this(parent, Area, new BorderLayout())
		{
		}
		/// <summary>
		/// Constructs a new AreaPane with the specified parent, Area, and layout.
		/// </summary>
		/// <param name="parent">The parent control for the Area </param>
		/// <param name="Area">The Area in which to layout objects </param>
		/// <param name="layout">The layout manager to use </param>
		public AreaPane(Control parent, Rectangle Area, ILayoutManager layout) : base(parent)
		{
			this.Area = Area;
			this.LayoutManager = layout;
			BoxedControl.HandleCreated += new EventHandler(show);
		}

		private void show(object sender, EventArgs args)
		{
			Layout();
		}

		public new void Layout()
		{
			if(LayoutManager != null)
			{
				LayoutManager.SetLocation(new Point(Area.X, Area.Y));
				LayoutManager.SetSize(new Size(Area.Width, Area.Height));
				LayoutManager.Layout();
			}
		}
		/// <summary>
		/// The Area in which components are laid out.  Setting this Area to a new value fires a new layout.
		/// </summary>
		public Rectangle Area
		{
			get
			{
				return myArea;
			}
			set
			{
				myArea = value;
				Layout();
			}
		}

		private Rectangle myArea;
    }
}
