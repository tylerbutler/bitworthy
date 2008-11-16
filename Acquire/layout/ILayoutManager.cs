namespace Layout
{
	
    using System;
	using System.Drawing;

    /// <summary>
    ///	Lays out Layoutable objects in a pattern.
    /// </summary>
    /// <remarks>
    /// Classes which implement this interface are intended to use specific patterns or algorithms
    /// to give location and size to Layoutable objects.
    /// </remarks>
    /// <author>Matthew Johnson</author>
    ///	<version>1.0</version> 
    public interface ILayoutManager : ILayoutable
    {
		/// <summary>
		/// Adds a Layoutable object to the manager.
		/// </summary>
		/// <param name="l">The Layoutable object to add</param>
		/// <param name="constraints">
		/// These can be any constraints use to specify options about the layout of the specified
		/// Layoutable object
		/// </param>
        void AddLayoutable(ILayoutable l, object constraints);
		/// <summary>
		/// Removes a Layoutable object from the manager.
		/// </summary>
		/// <param name="l">The Layoutable object to remove</param>
		void RemoveLayoutable(ILayoutable l); 
		/// <summary>
		/// Removes all objects from the manager.
		/// </summary>
		void RemoveAll();
    }
}
