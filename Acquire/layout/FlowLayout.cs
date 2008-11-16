namespace Layout
{
    using System;
	using System.Drawing;
	using System.Collections;

    /// <summary>
    /// Lays out objects in rows with no Size alteration.
    /// </summary>
    /// <remarks>
    /// FlowLayout will layout out all objects added to it in lines.  It will add objects to one line until
    /// it runs out of horizontal room in its space and then it will start a new line.  The alignment of these
    /// lines is similar to that of blocks of text, mainly left, center, or right.  As of this version, leading
    /// and trailing options have not been implemented, and will simply default to either left or right.
    /// </remarks>
    /// <author>Matthew Johnson</author>
    /// <version>1.0</version>
    public class FlowLayout : AbstractLayoutManager
    {
		/// <summary>
		/// Creates a new FlowLayout with centered alignment and the default gaps of 5.
		/// </summary>
        public FlowLayout() : this(Alignment.Center, DEFAULT_GAP, DEFAULT_GAP)
        {
		}
		
		/// <summary>
		/// Creates a new FlowLayout with the specified alignment and the default gaps of 5.
		/// </summary>
		/// <param name="Align"> </param>
		public FlowLayout(Alignment Align) : this(Align, DEFAULT_GAP, DEFAULT_GAP)
		{	
		}

		/// <summary>
		/// Creates a new FlowLayout with the specified properties.
		/// </summary>
		/// <param name="Align">The alignment of the FlowLayout </param>
		/// <param name="HGap">The horizontal gap between objects </param>
		/// <param name="VGap">The vertical gap between objects </param>
		public FlowLayout(Alignment Align, int HGap, int VGap)
		{
			myObjects = new ArrayList();
			this.Size = new Size(0, 0);

			this.Align = Align;
			this.HGap = HGap;
			this.VGap = VGap;			
		}

		override public void AddLayoutable(ILayoutable l, object constraints)
		{
			myObjects.Add(l);
		}

		override public void RemoveLayoutable(ILayoutable l)
		{
			myObjects.Remove(l);
		}

		override public void RemoveAll()
		{
			myObjects = new ArrayList();
		}

		override public Size GetMinimumSize()
		{
			return new Size(GetMaxWidth(), GetMaxHeight());
		}
		//get max object width
		private int GetMaxWidth()
		{
			int maxWidth = 0;
			foreach(ILayoutable l in myObjects)
			{
				maxWidth = Math.Max(maxWidth, l.GetPreferredSize().Width);
			}
			return maxWidth;
		}
		//get max object height
		private int GetMaxHeight()
		{
			int maxHeight = 0;
			foreach(ILayoutable l in myObjects)
			{
				maxHeight = Math.Max(maxHeight, l.GetPreferredSize().Height);
			}
			return maxHeight;
		}

		override public Size GetPreferredSize()
		{
			int height = 0;
			int width = 0;
			foreach(ILayoutable c in myObjects)
			{
				Size s = c.GetPreferredSize();
				width += s.Width;
				height = Math.Max(height, s.Height);
			}
			width += (myObjects.Count + 1)*HGap;
			height += 2*VGap;
			return new Size(width, height);
		}

		override public void Layout()
		{
			ArrayList lines = new ArrayList();
			int currentWidth = HGap;
			Line currentLine = new Line(HGap);
			foreach(ILayoutable c in myObjects)
			{
				Size s = c.GetPreferredSize();
				currentWidth += s.Width + HGap;
				if(currentWidth > Size.Width)
				{
					currentWidth = s.Width + 2*HGap;
					lines.Add(currentLine);
					currentLine = new Line(HGap);
				}
				currentLine.Add(c);
			}
			lines.Add(currentLine);
			int totalHeight = 0;
			foreach(Line l in lines)
			{
				totalHeight += l.Size.Height;
			}
			totalHeight += VGap*(lines.Count - 1);
			int y = Location.Y + (Size.Height - totalHeight)/2;
			switch(Align)
			{
				case Alignment.Left:
					foreach(Line l in lines)
					{
						l.Location = new Point(Location.X + HGap, y);
						l.Layout();
						y += l.Size.Height + VGap;
					}
					break;
				case Alignment.Center:
					foreach(Line l in lines)
					{
						int x = Location.X + (Size.Width - l.Size.Width)/2;
						l.Location = new Point(x, y);
						l.Layout();
						y += l.Size.Height + VGap;
					}
					break;
				case Alignment.Right:
					foreach(Line l in lines)
					{
						int x = Location.X + Size.Width - l.Size.Width - HGap;
						l.Location = new Point(x, y);
						l.Layout();
						y += l.Size.Height + VGap;
					}
					break;

				case Alignment.Leading:
					goto case Alignment.Left;
					
				case Alignment.Trailing:
					goto case Alignment.Right;

				default:
					goto case Alignment.Center;
			}
			foreach(ILayoutable l in myObjects)
			{
				l.Layout();
			}
		}
		/// <summary>
		/// The horizontal gap between objects.
		/// </summary>
		/// <value>the new horizontal gap</value>
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
		/// The vertical gap between objects.
		/// </summary>
		/// <value>the new vertical gap</value>
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
		/// The alignment of the layout
		/// </summary>
		public Alignment Align
		{
			get
			{
				return myAlign;
			}
			set
			{
				myAlign = value;
			}
		}
		//The objects in the layout
		private ArrayList myObjects;
		private Alignment myAlign;
		private int myHGap;
		private int myVGap;
		/// <summary>
		/// The different alignments accepted by FlowLayout.
		/// </summary>
		public enum Alignment
		{
			Center,
			Left,
			Right,
			Leading,
			Trailing,
		}
		
		private const int DEFAULT_GAP = 5;
    }

	internal class Line
	{
		/// <summary>
		/// Constructs a new Line with the specified HGap
		/// </summary>
		/// <param name="HGap">The horizontal gap between components</param>
		public Line(int HGap)
		{
			myMembers = new ArrayList();
			this.HGap = HGap;
		}
		/// <summary>
		/// Adds a Layoutable object to the Line
		/// </summary>
		/// <param name="c">The Layoutable object to add</param>
		public void Add(ILayoutable c)
		{
			myMembers.Add(c);
			Recalc();
		}
		/// <summary>
		/// Removes a Layoutable object from the Line
		/// </summary>
		/// <param name="c">The Layoutable object to remove</param>
		public void remove(ILayoutable c)
		{
			myMembers.Remove(c);
			Recalc();
		}
		/// <summary>
		/// Sets the locations of the various Layoutable objects in the Line
		/// </summary>
		public void Layout()
		{
			Recalc();
			int x = Location.X + HGap;
			int y = Location.Y + (Size.Height / 2);
			foreach(ILayoutable c in myMembers)
			{
				Size s = c.GetPreferredSize();
				c.SetLocation(new Point(x, y-(s.Height/2)));
				x += s.Width + HGap;
			}
		}
		//recalculates the Size of the line
		private void Recalc()
		{
			int height = GetMaxHeight();
			int width = 0;
			foreach(ILayoutable c in myMembers)
			{
				Size s = c.GetPreferredSize();
				width += s.Width;
			}
			width += HGap * (myMembers.Count + 1);
			mySize = new Size(width, height);
		}
		//gets the maximum height
		private int GetMaxHeight()
		{
			int maxHeight = 0;
			foreach(ILayoutable c in myMembers)
			{
				maxHeight = Math.Max(maxHeight, c.GetPreferredSize().Height);
			}
			return maxHeight;
		}
		/// <summary>
		/// The horizontal gap for this Line.
		/// </summary>
		/// <value>the new horizontal gap</value>
		public int HGap
		{
			get
			{
				return myHGap;
			}
			set
			{
				myHGap = value;
				Recalc();
			}
		}
		/// <summary>
		/// The Location of the Line
		/// </summary>
		/// <value>the new  Location</value>
		public Point Location
		{
			get
			{
				return myLocation;
			}
			set
			{
				myLocation = value;
				Recalc();
			}
		}
		/// <summary>
		/// The Size of the Line, defined as the total width (object widths + horizontal spacing)
		/// and the maximum object height.
		/// </summary>
		public Size Size 
		{
			get
			{
				return mySize;
			}
		}

		private ArrayList myMembers;
		private Size mySize;
		private int myHGap;
		private Point myLocation;
	}
}
