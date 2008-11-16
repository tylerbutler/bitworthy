namespace Layout
{
    using System;
	using System.Drawing;

    /// <summary>
    /// All instances of classes implementing this interface are able to be "laid out" by a
    /// layout manager.
    /// </summary>
    /// <remarks>
    ///	Layoutable objects tend to be used in tandem with an instance of a class implementing
    ///	ILayoutManager, where one will add Layoutable objects
    ///	to the manager, which then lays them out accordingly.
    ///	</remarks>
    ///	<author>Matthew Johnson</author>
    ///	<version>1.0</version> 
    public interface ILayoutable
    {
		/// <summary>
		///	Returns the minimum size.
		/// </summary>
		/// <returns>the minimum size</returns>
        Size GetMinimumSize();
		/// <summary>
		///	Returns the maximum size.
		/// </summary>
		/// <returns>the maximum size</returns>
		Size GetMaximumSize();
		/// <summary>
		///	Returns the preferred size.
		/// </summary>
		/// <returns>the preferred size</returns>
		Size GetPreferredSize();
		/// <summary>
		/// Lays out this layoutable object.
		/// </summary>
		void Layout();
		/// <summary>
		/// Sets the size of this layoutable object.
		/// </summary>
		/// <param name="s">The desired new size</param>
		void SetSize(Size s);
		/// <summary>
		/// Sets the location of this layoutable object.
		/// </summary>
		/// <param name="p">The desired new location</param>
		void SetLocation(Point p);
    }
}
