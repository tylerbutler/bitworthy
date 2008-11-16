namespace Layout
{
    using System;
    using System.Windows.Forms;
    using System.Drawing;

    /// <summary>
    /// A Layoutable wrapper for containers
    /// </summary>
    /// <remarks>
    /// ContainerBox boxes a Control but adds the ability to add/remove members to that Control and use
    /// a layout manager to handle them.  It will not distinguish between language-specific containers 
    /// and non-containers, however, leaving that implementation up to the user.  Since it extends
    /// ControlBox, it can also be added to other ContainerBoxes, allowing for nested layout structures.
    /// </remarks>
    /// <example>
    /// Panel p = new Panel();
    /// ContainerBox cb = new ContainerBox(p);
    /// Button b = new Button();
    /// b.Text = "Press me";
    /// cb.add(b);
    /// </example>
    /// <author>Matthew Johnson</author>
    /// <version>1.0</version>
    public class ContainerBox : ControlBox, ILayoutable
    {
		/// <summary>
		/// Creates a new ContainerBox which boxes Control c and uses a FlowLayout.
		/// </summary>
		/// <param name="c">The Control to box. </param>
		public ContainerBox(Control c) : base(c)
		{
			LayoutManager = new FlowLayout();
		}

		/// <summary>
		/// Boxes the control and then adds it.
		/// </summary>
		/// <param name="c">The Control to box and then add </param>
		public void Add(Control c)
		{
			Add(c, null);
		}
		/// <summary>
		/// Adds the Box with null constraints.
		/// </summary>
		/// <param name="c">The ControlBox to add </param>
		public void Add(ControlBox c)
		{
			Add(c, null);
		}

		public new Size GetPreferredSize()
		{
			return LayoutManager.GetPreferredSize();
		}

		public new Size GetMinimumSize()
		{
			return LayoutManager.GetMinimumSize();
		}

		public new Size GetMaximumSize()
		{
			return LayoutManager.GetMaximumSize();
		}
		/// <summary>
		/// Boxes the control and then adds it with the specified constraints.
		/// </summary>
		/// <param name="c">The Control to box </param>
		/// <param name="constraints">The layout constraints </param>
		public void Add(Control c, object constraints)
		{
			Add(new ControlBox(c), constraints);
		}
		/// <summary>
		/// Adds the specified ControlBox to the layout manager and to the boxed container.
		/// </summary>
		/// <param name="c"> </param>
		/// <param name="constraints"> </param>
		public void Add(ControlBox c, object constraints)
		{
			BoxedControl.Controls.Add(c.BoxedControl);
			LayoutManager.AddLayoutable(c, constraints);
		}
		/// <summary>
		/// Adds the specified Layoutable object to the layout manager but not the boxed container.
		/// </summary>
		/// <param name="c">The Layoutable to add</param>
		public void Add(ILayoutable c)
		{
			Add(c, null);
		}
		/// <summary>
		/// Adds the specified Layoutable with the specified constraints to the layout manager
		/// but not the boxed container.
		/// </summary>
		/// <param name="c">The Layoutable to add </param>
		/// <param name="constraints">The constraints to use </param>
		public void Add(ILayoutable c, object constraints)
		{
			LayoutManager.AddLayoutable(c, constraints);
		}
		/// <summary>
		/// Removes the boxed control from both the boxed container and the layout manager.
		/// </summary>
		/// <param name="c">The ControlBox to remove </param>
		public void Remove(ControlBox c)
		{
			BoxedControl.Controls.Remove(c.BoxedControl);
			LayoutManager.RemoveLayoutable(c);
		}
		/// <summary>
		/// Removes the Layoutable object from the layout manager.
		/// </summary>
		/// <param name="c">The Layoutable object to remove. </param>
		public void Remove(ILayoutable c)
		{
			LayoutManager.RemoveLayoutable(c);
		}

		public new void Layout()
		{
			LayoutManager.Layout();
		}

		public new void SetSize(Size s)
		{
			BoxedControl.Size = s;
			LayoutManager.SetSize(s);
		}

		public new void SetLocation(Point l)
		{
			BoxedControl.Location = l;
		}
		/// <summary>
		/// The layout manager for the ContainerBox.
		/// </summary>
		public ILayoutManager LayoutManager
		{
			get
			{
				return myLayout;
			}
			set
			{
				myLayout = value;
			}
		}

		private ILayoutManager myLayout;
	}
}