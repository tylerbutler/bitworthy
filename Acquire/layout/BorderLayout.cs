namespace Layout
{
    using System;
	using System.Drawing;

    /// <summary>
    /// Uses cardinal directions to lay out components.
    /// </summary>
    /// <remarks>
    /// BorderLayout is able to hold 5 Layoutable objects.  It lays them out in such a way that
    /// the objects in the north and south parts of its space are as wide as possible but as short as possible,
    /// the objects in the east and west parts are as tall as possible but as skinny as possible, and so that
    /// the object in the center occupies the space left over from the sides.
    /// </remarks>
    /// <author>Matthew Johnson</author>
    /// <version>1.0</version>
    public class BorderLayout : AbstractLayoutManager
    {
		/// <summary>
		/// Creates a new BorderLayout with horizontal and vertical spacings of 0.
		/// </summary>
        public BorderLayout() : this(0, 0)
        {
        }

		/// <summary>
		/// Creates a new BorderLayout with the specified horizontal and vertical spacings.
		/// </summary>
		/// <param name="HGap"> </param>
		/// <param name="VGap"> </param>
		public BorderLayout(int HGap, int VGap)
		{
			this.HGap = HGap;
			this.VGap = VGap;
		}

		override public Size GetMinimumSize()
		{
			int width = 0;
			int height = 0;
			if(myNorth != null){
				Size s = myNorth.GetMinimumSize();
				width = Math.Max(width, s.Width);
				height += s.Height + VGap;
			}
			if(mySouth != null){
				Size s = mySouth.GetMinimumSize();
				width = Math.Max(width, s.Width);
				height += s.Height + VGap;
			}
			if(myCenter != null){
				Size s = myCenter.GetMinimumSize();
				height = Math.Max(height, s.Height);
				width += s.Width;
			}
			if(myEast != null){
				Size s = myEast.GetMinimumSize();
				width += s.Width;
				height = Math.Max(height, s.Height);
			}
			if(myWest != null){
				Size s = myWest.GetMinimumSize();
				width += s.Width;
				height = Math.Max(height, s.Height);
			}
			return new Size(width, height);
		}

		override public Size GetPreferredSize()
		{
			int width = 0;
			int height = 0;
			if(myNorth != null){
				Size s = myNorth.GetPreferredSize();
				width = Math.Max(width, s.Width);
				height += s.Height + VGap;
			}
			if(mySouth != null){
				Size s = mySouth.GetPreferredSize();
				width = Math.Max(width, s.Width);
				height += s.Height + VGap;
			}
			if(myCenter != null){
				Size s = myCenter.GetPreferredSize();
				height = Math.Max(height, s.Height);
				width += s.Width;
			}
			if(myEast != null){
				Size s = myEast.GetPreferredSize();
				width += s.Width;
				height = Math.Max(height, s.Height);
			}
			if(myWest != null){
				Size s = myWest.GetPreferredSize();
				width += s.Width;
				height = Math.Max(height, s.Height);
			}
			return new Size(width, height);			
		}

		override public void Layout()
		{
			int top = Location.X;
			int left = Location.Y;
			int right = Location.X + Size.Width;
			int bottom = Location.Y + Size.Height;

			if (myNorth != null) {
			    Size s = myNorth.GetPreferredSize();
				myNorth.SetSize(new Size(right-left, s.Height));
				myNorth.SetLocation(new Point(left, top));
			    top += s.Height + VGap;
				myNorth.Layout();
			}
			if(mySouth != null){
				Size s = mySouth.GetPreferredSize();
				mySouth.SetSize(new Size(right-left, s.Height));
				bottom -= s.Height;
				mySouth.SetLocation(new Point(left, bottom));
				bottom -= VGap;
				mySouth.Layout();
			}
			if(myWest != null){
				Size s = myWest.GetPreferredSize();
				myWest.SetSize(new Size(s.Width, bottom - top));
				myWest.SetLocation(new Point(left, top));
				left += s.Width + HGap;
				myWest.Layout();
			}
			if(myEast != null){
				Size s = myEast.GetPreferredSize();
				myEast.SetSize(new Size(s.Width, bottom - top));
				right -= s.Width;
				myEast.SetLocation(new Point(right, top));
				right -= HGap;
				myEast.Layout();
			}
			if(myCenter != null){
				myCenter.SetSize(new Size(right-left, bottom-top));
				myCenter.SetLocation(new Point(left, top));
				myCenter.Layout();
			}
		}
		/// <summary>
		/// When adding objects to the layout, one must specify the part of the layout as a member
		/// of the Direction enum.
		/// </summary>
		/// <param name="l">object to add </param>
		/// <param name="constraints">direction to add it at</param>
		override public void AddLayoutable(ILayoutable l, object constraints)
		{
			if(constraints == null)
			{
				AddLayoutable(Direction.Center, l);
			}
			if(constraints is Direction)
			{
				AddLayoutable((Direction)constraints, l);
			}
		}

		private void AddLayoutable(Direction area, ILayoutable l)
		{
			switch(area)
			{
				case Direction.North:
					myNorth = l;
					break;

				case Direction.South:
					mySouth = l;
					break;

				case Direction.East:
					myEast = l;
					break;

				case Direction.West:
					myWest = l;
					break;

				case Direction.Center:
					myCenter = l;
					break;

				default:
					break;
			}
		}

		override public void RemoveLayoutable(ILayoutable l)
		{
			if(l == myNorth)
				myNorth = null;
			else if(l == mySouth)
				mySouth = null;
			else if(l == myEast)
				myEast = null;
			else if(l == myWest)
				myWest = null;
			else if(l == myCenter)
				myCenter = null;
		}

		override public void RemoveAll()
		{
			myNorth = myWest = myEast = mySouth = myCenter = null;
		}
		/// <summary>
		/// The horizontal space between components.
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
		/// The Vertical space between components.
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

		private ILayoutable myNorth;
		private ILayoutable mySouth;
		private ILayoutable myEast;
		private ILayoutable myWest;
		private ILayoutable myCenter;

		private int myHGap;
		private int myVGap;

		/// <summary>
		/// The possible directions in which to add an object to the manager.
		/// </summary>
		public enum Direction
		{
			North,
			South,
			East,
			West,
			Center
		}
    }
}