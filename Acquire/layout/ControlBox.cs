namespace Layout
{
    using System;
	using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// A Layoutable wrapper for Controls
    /// </summary>
    /// <remarks>
    /// ControlBox is a wraps Controls so that they can be laid out by a layout manager.
    /// </remarks>
    /// <author>Matthew Johnson</author>
    /// <version>1.0</version>
    public class ControlBox : ILayoutable
    {
		/// <summary>
		/// Constructs a new ControlBox to hold the specified control.
		/// </summary>
		/// <param name="control">The control to box </param>
        public ControlBox(Control control)
        {
			this.BoxedControl = control;
			this.MinimumSize = new Size(10, 10);
        }

        public Size GetMinimumSize()
		{
			return MinimumSize;
		}

		public Size GetMaximumSize()
		{
			return BoxedControl.Parent.Size;
		}

		public Size GetPreferredSize()
		{
			return PreferredSize;
		}

		public void SetSize(Size s)
		{
			BoxedControl.Size = s;
		}

		public void SetLocation(Point p)
		{
			BoxedControl.Location = p;
		}

		public void Layout()
		{
		}
		/// <summary>
		/// The boxed control.
		/// </summary>
		public Control BoxedControl
		{
			get
			{
				return myControl;
			}
			set
			{
				myControl = value;
				PreferredSize = myControl.ClientSize;
			}
		}
		
		private Size PreferredSize
		{
			get
			{
				return myPreferredSize;
			}
			set
			{
				myPreferredSize = value;
			}
		}

		private Size MinimumSize
		{
			get
			{
				return myMinimumSize;
			}
			set
			{
				myMinimumSize = value;
			}
		}

		private Control myControl;
		private Size myPreferredSize;
		private Size myMinimumSize;
    }
}
