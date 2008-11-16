namespace Layout
{
    using System;
	using System.Drawing;
	using System.Collections;

    /// <summary>
    /// Lays out objects in a Grid.
    /// </summary>
    /// <remarks>
    /// This layout uses a grid format to layout components.  The user specifies Rows, Columns, and the horizontal
    /// and vertical spacings and then adds components to fill the grid.
    /// </remarks>
    /// <author>Matthew Johnson</author>
    /// <version>1.0</version>
    public class GridLayout : AbstractLayoutManager
    {
		/// <summary>
		/// Constructs a new GridLayout with one row, no Columns and spacing of 0.
		/// </summary>
        public GridLayout() : this(1, 0, 0, 0)
        {
			
        }
		/// <summary>
		/// Constructs a new GridLayout with the specified Rows and Columns with spacing of 0.
		/// </summary>
		/// <param name="Rows">Number of Rows in the layout </param>
		/// <param name="Columns">Number of Columns in the layout </param>
		public GridLayout(int Rows, int Columns) : this(Rows, Columns, 0, 0)
		{
			
		}

		/// <summary>
		/// Constructs a new Grid Layout with the specified dimensions and spacings.
		/// </summary>
		/// <param name="Rows">Number of Rows in the layout </param>
		/// <param name="Columns">Number of Columns in the layout </param>
		/// <param name="HGap">Horizontal spacing </param>
		/// <param name="VGap">Vertical spacing </param>
		public GridLayout(int Rows, int Columns, int HGap, int VGap)
		{
			myMembers = new ArrayList();
			this.Rows = Rows;
			this.Columns = Columns;
			this.HGap = HGap;
			this.VGap = VGap;
		}

        override public Size GetMinimumSize()
		{
			Size grid = calculateGrid();
			int width = 0;
			int height = 0;
			foreach(ILayoutable c in myMembers)
			{
				Size s = c.GetMinimumSize();
				width = Math.Max(s.Width, width);
				height = Math.Max(s.Height, height);
			}
			return new Size(width * grid.Width + HGap * (grid.Width - 1), 
							height * grid.Height + VGap * (grid.Height - 1));
		}

		private Size calculateGrid()
		{
			int newRows = Rows;
			int newColumns = Columns;
			int components = myMembers.Count;
			if(newRows > 0)
			{
				newColumns = (components + newRows - 1)/newRows;
			}else
			{
				newRows = (components + newColumns - 1)/newColumns;
			}
			return new Size(newColumns, newRows);
		}

		override public Size GetPreferredSize()
		{
			Size grid = calculateGrid();
			int width = 0;
			int height = 0;
			foreach(ILayoutable c in myMembers)
			{
				Size s = c.GetPreferredSize();
				width = Math.Max(s.Width, width);
				height = Math.Max(s.Height, height);
			}
			return new Size(width * grid.Width + HGap * (grid.Width - 1), 
							height * grid.Height + VGap * (grid.Height - 1));

		}

		override public void Layout()
		{
			Size grid = calculateGrid();

			int width = Size.Width;
			int height = Size.Height;

			width = (width - (grid.Width - 1) * HGap) / grid.Width;
			height = (height - (grid.Height - 1) * VGap) / grid.Height;

			for(int c = 0, x = Location.X; c < grid.Width; c++, x += width + HGap)
			{
				for(int r = 0, y = Location.Y; r < grid.Height; r++, y += height + VGap)
				{
					int i = r * grid.Width + c;
					if(i < myMembers.Count)
					{
						ILayoutable member = (ILayoutable)myMembers[i];
						member.SetLocation(new Point(x, y));
						member.SetSize(new Size(width, height));
						member.Layout();
					}
				}
			}
		}

		override public void AddLayoutable(ILayoutable c, object constraints)
		{
			myMembers.Add(c);
		}

		override public void RemoveLayoutable(ILayoutable c)
		{
			myMembers.Remove(c);
		}

		override public void RemoveAll()
		{
			myMembers = new ArrayList();
		}
		/// <summary>
		/// Horizontal spacing.
		/// </summary>
		public int HGap
		{
			get
			{
				return myHGap;
			}
			set
			{
				myHGap = value;
			}
		}
		/// <summary>
		/// Vertical spacing
		/// </summary>
		public int VGap
		{
			get
			{
				return myVGap;
			}
			set
			{
				myVGap = value;
			}
		}
		/// <summary>
		/// Number of Rows
		/// </summary>
		public int Rows
		{
			get
			{
				return myRows;
			}
			set
			{
				myRows = value;
			}
		}
		/// <summary>
		/// Number of Columns
		/// </summary>
		public int Columns
		{
			get
			{
				return myColumns;
			}
			set
			{
				myColumns = value;
			}
		}

		private ArrayList myMembers;
		private int myRows;
		private int myColumns;
		private int myHGap;
		private int myVGap;
    }
}
