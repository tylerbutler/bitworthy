namespace Layout
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;

    /// <summary>
    /// A top level pane which resizes along with its parent Control.
    /// </summary>
    /// <remarks>
    /// This class behaves exactly like an AreaPane, except that its Area
    /// changes to be the same as its parent component at all times.
    /// </remarks>
    /// <author>Matthew Johnson</author>
    /// <version>1.0</version>
    public class ResizeablePane : AreaPane
    {
		/// <summary>
		/// Constructs a new ResizeablePane with the specified parent and a zero-sized Area.
		/// </summary>
		/// <param name="parent">The parent control for the Area </param>
        public ResizeablePane(Control parent) : this(parent, parent.ClientRectangle)
        {
        }
		/// <summary>
		/// Constructs a new ResizeablePane with the specified parent, Area and a BorderLayout.
		/// </summary>
		/// <param name="parent">The parent control for the Area </param>
		/// <param name="Area">The Area in which to layout objects </param>
		public ResizeablePane(Control parent, Rectangle Area) : this(parent, Area, new BorderLayout())
		{
		}
		/// <summary>
		/// Constructs a new ResizeablePane with the specified parent, Area, and layout.
		/// </summary>
		/// <param name="parent">The parent control for the Area </param>
		/// <param name="Area">The Area in which to layout objects </param>
		/// <param name="layout">The layout manager to use </param>
		public ResizeablePane(Control parent, Rectangle Area, ILayoutManager layout) : base(parent, Area, layout)
		{
			BoxedControl.Resize += new EventHandler(resize);
		}

		private void resize(object sender, EventArgs args)
		{
			Area = BoxedControl.ClientRectangle;
		}
    }
}
