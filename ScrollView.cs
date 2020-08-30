using System;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;
using Rune = System.Rune;

namespace Course_Sorter {

	/// <summary>
	/// ScrollBarViews are views that display a 1-character scrollbar, either horizontal or vertical
	/// </summary>
	/// <remarks>
	/// <para>
	///   The scrollbar is drawn to be a representation of the Size, assuming that the 
	///   scroll position is set at Position.
	/// </para>
	/// <para>
	///   If the region to display the scrollbar is larger than three characters, 
	///   arrow indicators are drawn.
	/// </para>
	/// </remarks>
	public class ScrollBarView : View {
		bool vertical;
		int size, position;

		/// <summary>
		/// The length of the scrollbar
		/// </summary>
		public int Size {
			get => size;
			set {
				size = value;
				SetNeedsDisplay();
			}
		}

		/// <summary>
		/// This event is raised when the position on the scrollbar has changed.
		/// </summary>
		public event Action ChangedPosition;

		/// <summary>
		/// The position that the scroll bar is scrolled at.
		/// </summary>
		public int Position {
			get => position;
			set {
				position = value;
				SetNeedsDisplay();
			}
		}
		/// <summary>
		/// Sets the position and invokes the <see cref="ChangedPosition"/> event.
		/// </summary>
		void SetPosition(int newPos) {
			Position = newPos;
			ChangedPosition?.Invoke();
		}

		/// <summary>
		/// Initializes a scroll bar.
		/// </summary>
		/// <param name="rect">Frame for the scrollbar.</param>
		/// <param name="size">The size of the space represented by this scrollbar.</param>
		/// <param name="position">The scrolled position of this scrollbar.</param>
		/// <param name="isVertical">If set to <c>true</c> this is a vertical scrollbar, otherwize, the scrollbar is horizontal.</param>
		public ScrollBarView(Rect rect, int size, int position, bool isVertical) : base(rect) {
			vertical = isVertical;
			this.position = position;
			this.size = size;
		}
		/// <summary>
		/// Gets the start and end positions of the clickable handle relative to the open space area on the scrollbar
		/// </summary>
		/// <param name="start">The point where the handle starts (top side if vertical, left side if horizontal)</param>
		/// <param name="end">The point where the handle starts (bottom side if vertical, right side if horizontal)</param>
		public void GetHandleBounds(out int start, out int end, out int length) {
			int barLength = GetBarLength();
			start = position * barLength / Size;
			end = (position + barLength) * barLength / Size;
			length = end - start;
		}
		int GetScrollableSize() => Size - GetBarLength();
		int GetBarLength() {
			return vertical ? Bounds.Height : Bounds.Width; //Complete length, including the buttons
		}
		/// <summary>
		/// Decrements the position, automatically stopping at the start of the scrollbar. Scrolls up if vertical, left if horizontal
		/// </summary>
		/// <param name="spaces">The maximum number of units to decrement the position by.</param>
		/// <returns><c>true</c> if the scroll bar was able to scroll back; <c>false</c> otherwise</returns>
		public bool ScrollBack(int spaces) {
			int dest = position - spaces;
			//Minimum is zero
			if (dest < 0)
				dest = 0;
			if (position != dest) {
				SetPosition(dest);
				return true;
			}
			return false;
		}
		/// <summary>
		/// Increments the position, automatically stopping at the start of the scrollbar. Scrolls down if vertical, right if horizontal
		/// </summary>
		/// <param name="spaces">The maximum number of units to decrement the position by.</param>
		/// <returns><c>true</c> if the scroll bar was able to scroll back; <c>false</c> otherwise</returns>
		public bool ScrollForward(int spaces) {
			int barLength = GetBarLength();
			//We might have buttons
			if (barLength > 3)
				barLength -= 2;
			int dest = position + spaces;

			//Upper limit includes length of bar
			if (dest + barLength >= Size)
				dest = Size - 1;
			if (position != dest) {
				SetPosition(dest);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Redraw the scrollbar
		/// </summary>
		/// <param name="region">Region to be redrawn.</param>
		public override void Redraw(Rect region) {
			Driver.SetAttribute(ColorScheme.Normal);

			if (vertical) {
				//Vertical scroller
				if (region.Right < Bounds.Width - 1)
					return;

				var col = Bounds.Width - 1;
				var height = Bounds.Height;
				Rune special;

				if (height < 4) {
					var handleTop = position * height / Size;
					var handleBottom = (position + height) * height / Size;

					for (int y = 0; y < height; y++) {
						Move(col, y);
						if (y < handleTop || y > handleBottom)
							special = Driver.Stipple;
						else
							special = Driver.Diamond;
						Driver.AddRune(special);
					}
				} else {
					height -= 2;
					var handleTop = position * height / Size;
					var handleBottom = (position + height) * height / Size;


					Move(col, 0);
					Driver.AddRune('^');
					Move(col, Bounds.Height - 1);
					Driver.AddRune('v');
					for (int y = 0; y < height; y++) {
						Move(col, y + 1);

						if (y < handleTop || y > handleBottom)
							special = Driver.Stipple;
						else {
							if (handleBottom - handleTop == 0)
								special = Driver.Diamond;
							else {
								if (y == handleTop)
									special = Driver.TopTee;
								else if (y == handleBottom)
									special = Driver.BottomTee;
								else
									special = Driver.VLine;
							}
						}
						Driver.AddRune(special);
					}
				}
			} else {
				if (region.Bottom < Bounds.Height - 1)
					return;

				var row = Bounds.Height - 1;
				var width = Bounds.Width;
				Rune special;

				if (width < 4) {
					var handleLeft = position * width / Size;
					var handleRight = (position + width) * width / Size;

					for (int x = 0; x < width; x++) {
						Move(0, x);
						if (x < handleLeft || x > handleRight)
							special = Driver.Stipple;
						else
							special = Driver.Diamond;
						Driver.AddRune(special);
					}
				} else {
					width -= 2;
					var handleLeft = position * width / Size;
					var handleRight = (position + width) * width / Size;

					Move(0, row);
					Driver.AddRune('<');

					for (int x = 0; x < width; x++) {

						if (x < handleLeft || x > handleRight) {
							special = Driver.Stipple;
						} else {
							if (handleRight - handleLeft == 0)
								special = Driver.Diamond;
							else {
								if (x == handleLeft)
									special = Driver.LeftTee;
								else if (x == handleRight)
									special = Driver.RightTee;
								else
									special = Driver.HLine;
							}
						}
						Driver.AddRune(special);
					}
					Driver.AddRune('>');
				}
			}
		}
		public override bool MouseEvent(MouseEvent me) {
			if (me.Flags != MouseFlags.Button1Pressed)
				return false;

			int location = vertical ? me.Y : me.X;
			int barLength = GetBarLength();

			if (barLength < 4) {
				// Handle scrollbars with no buttons
				//Console.WriteLine ("TODO at ScrollBarView2");
				//Location clicked is somewhere along the scrollbar length

				//Old implementation ignored handle bounds
				//SetPosition((int) (((Size - 1) - barsize) * (1f * location / barsize)));
				GetHandleBounds(out int start, out int end, out int handleLength);
				if (location < start) {
					ScrollBack(barLength / 2);
				} else if (location > end) {
					ScrollForward(barLength / 2);
				}
				/*
				if(location - 1 < start)
					SetPosition((int) (Size * 1f * location / barLength));
				else if(location + 1 > end)
					SetPosition((int) (Size * 1f * (location + length/2) / barLength));
				*/
				/*
				if (location < start) {
					int delta = (int)(Size * ((double)(start - location) / barLength));
					ScrollBack(delta);
				} else if (location > end) {
					int delta = (int)(Size * ((double)(location - end) / barLength));
					ScrollForward(delta);
				} else {
					//TODO: Implement dragging
				}
				*/
			} else {
				// Handle scrollbars with arrow buttons
				int spaceSize = barLength - 2;
				var pos = Position;
				if (location == 0) {
					//Lower limit is 0
					if (pos > 0)
						//SetPosition(pos - 1);
						ScrollBack(1);
				} else if (location == barLength - 1) {
					//Upper limit is Size - 1
					if (pos + barLength < Size - 1)
						//SetPosition(pos + 1);
						ScrollForward(1);
				} else {
					//Location clicked is somewhere along the scrollbar length
					//Old implementation ignored handle
					//SetPosition((int) (((Size - 1) - barsize) * (1f * location / barsize)));

					GetHandleBounds(out int start, out int end, out int handleLength);
					if (location < start) {
						ScrollBack(barLength / 2);
					} else if (location > end) {
						ScrollForward(barLength / 2);
					}
					/*
					if(location - 1 < start)
						SetPosition((int) (Size * 1f * location / barLength));
					else if(location + 1 > end)
						SetPosition((int) (Size * 1f * location + length/2) / barLength);
					*/
					/*
					if (location - 1 < start) {
						int delta = (int)(Size * ((double)(1 + start - location) / barLength));
						ScrollBack(delta);
					} else if (location + 1 > end) {
						int delta = (int)(Size * ((double)(1 + location - end) / barLength));
						ScrollForward(delta);
					} else {
						//TODO: Implement dragging
					}
					*/
				}
			}

			return true;
		}
	}

	/// <summary>
	/// Scrollviews are views that present a window into a virtual space where children views are added.  Similar to the iOS UIScrollView.
	/// </summary>
	/// <remarks>
	/// <para>
	///   The subviews that are added to this scrollview are offset by the
	///   ContentOffset property.   The view itself is a window into the 
	///   space represented by the ContentSize.
	/// </para>
	/// <para>
	///   
	/// </para>
	/// </remarks>
	public class ScrollView : View {
		View contentView;
		ScrollBarView vertical, horizontal;

		public ScrollView(Rect frame) : base(frame) {
			contentView = new View(frame);
			vertical = new ScrollBarView(new Rect(frame.Width - 1, 0, 1, frame.Height), frame.Height, 0, isVertical: true);
			vertical.ChangedPosition += delegate {
				ContentOffset = new Point(ContentOffset.X, vertical.Position);
			};
			horizontal = new ScrollBarView(new Rect(0, frame.Height - 1, frame.Width - 1, 1), frame.Width - 1, 0, isVertical: false);
			horizontal.ChangedPosition += delegate {
				ContentOffset = new Point(horizontal.Position, ContentOffset.Y);
			};
			base.Add(contentView);
			CanFocus = true;
			allowHorizontal = true;
			allowVertical = true;
		}

		public ScrollView(Rect frame, Rect subframe) : base(frame) {
			contentView = new View(subframe);
			vertical = new ScrollBarView(new Rect(frame.Width - 1, 0, 1, frame.Height), subframe.Height, 0, isVertical: true);
			vertical.ChangedPosition += delegate {
				ContentOffset = new Point(ContentOffset.X, vertical.Position);
			};
			horizontal = new ScrollBarView(new Rect(0, frame.Height - 1, frame.Width - 1, 1), subframe.Width - 1, 0, isVertical: false);
			horizontal.ChangedPosition += delegate {
				ContentOffset = new Point(horizontal.Position, ContentOffset.Y);
			};
			base.Add(contentView);
			CanFocus = true;
			allowHorizontal = true;
			allowVertical = true;
		}

		Size contentSize => contentView.Frame.Size;
		Point contentOffset;
		bool showHorizontalScrollIndicator;
		bool showVerticalScrollIndicator;
		/// <summary>
		/// If true, we allow horizontal scrolling with the keyboard
		/// </summary>
		bool allowHorizontal;
		/// <summary>
		/// If true, we allow vertical scrolling with the keyboard
		/// </summary>
		bool allowVertical;

		/// <summary>
		/// The position of the top left corner displayed by the scrollview. Used to implement scrolling.
		/// </summary>
		/// <value>The offset from the subview's top left corner.</value>
		public Point ContentOffset {
			get {
				return contentOffset;
			}
			set {
				contentOffset = new Point(-Math.Abs(value.X), -Math.Abs(value.Y));
				contentView.Frame = new Rect(contentOffset, contentSize);
				vertical.Position = Math.Max(0, -contentOffset.Y);
				horizontal.Position = Math.Max(0, -contentOffset.X);
				Scrolled?.Invoke(this);
				SetNeedsDisplay();
			}
		}

		/// <summary>
		/// Adds the view to the scrollview.
		/// </summary>
		/// <param name="view">The view to add to the scrollview.</param>
		public override void Add(View view) {
			contentView.Add(view);
		}

		/// <summary>
		/// Gets or sets the visibility for the horizontal scroll indicator.
		/// </summary>
		/// <value><c>true</c> if the vertical scroll indicator is visible; <c>false</c> otherwise.</value>
		public bool ShowHorizontalScrollIndicator {
			get => showHorizontalScrollIndicator;
			set {
				if (value == showHorizontalScrollIndicator)
					return;

				showHorizontalScrollIndicator = value;
				SetNeedsDisplay();
				if (value)
					base.Add(horizontal);
				else
					Remove(horizontal);
			}
		}

		/// <summary>
		///   Removes all widgets from this container.
		/// </summary>
		public override void RemoveAll() {
			contentView.RemoveAll();
		}

		/// <summary>
		/// /// Gets or sets the visibility for the vertical scroll indicator.
		/// </summary>
		/// <value><c>true</c> if the vertical scroll indicator is visible; <c>false</c> otherwise.</value>
		public bool ShowVerticalScrollIndicator {
			get => showVerticalScrollIndicator;
			set {
				if (value == showVerticalScrollIndicator)
					return;

				showVerticalScrollIndicator = value;
				SetNeedsDisplay();
				if (value)
					base.Add(vertical);
				else
					Remove(vertical);
			}
		}

		/// <summary>
		/// This event is raised when the contents have scrolled
		/// </summary>
		public event Action<ScrollView> Scrolled;

		public override void Redraw(Rect region) {
			var oldClip = ClipToBounds();
			Driver.SetAttribute(ColorScheme.Normal);
			Clear();
			base.Redraw(region);
			Driver.Clip = oldClip;
			Driver.SetAttribute(ColorScheme.Normal);
		}
		/// <summary>
		/// Moves the cursor to the top right corner if the view is empty. Otherwise calls base behavior.
		/// </summary>
		public override void PositionCursor() {
			if (Subviews.Count == 0)
				Driver.Move(0, 0);
			else
				base.PositionCursor();
		}

		/// <summary>
		/// Scrolls the view up. Does not check whether vertical scrolling is enabled.
		/// </summary>
		/// <returns><c>true</c> if the view was able to scroll; <c>false</c> if the view is already scrolled all the way.</returns>
		/// <param name="lines">Number of lines to scroll.</param>
		public bool ScrollUp(int lines) {
			if (contentOffset.Y < 0) {
				ContentOffset = new Point(contentOffset.X, Math.Min(contentOffset.Y + lines, 0));
				return true;
			}
			return false;
		}

		/// <summary>
		/// Scrolls the view left. Does not check whether horizontal scrolling is enabled.
		/// </summary>
		/// <returns><c>true</c> if the view was able to scroll; <c>false</c> if the view is already scrolled all the way.</returns>
		/// <param name="cols">Number of columns to scroll by.</param>
		public bool ScrollLeft(int cols) {
			if (contentOffset.X < 0) {
				ContentOffset = new Point(Math.Min(contentOffset.X + cols, 0), contentOffset.Y);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Scrolls the view down. Does not check whether vertical scrolling is enabled.
		/// </summary>
		/// <returns><c>true</c> if the view was able to scroll; <c>false</c> if the view is already scrolled all the way.</returns>
		/// <param name="lines">Number of lines to scroll.</param>
		public bool ScrollDown(int lines) {
			var ny = Math.Max(-contentSize.Height, contentOffset.Y - lines);
			if (ny == contentOffset.Y)
				return false;
			ContentOffset = new Point(contentOffset.X, ny);
			return true;
		}

		/// <summary>
		/// Scrolls the view right. Does not check whether horizontal scrolling is enabled.
		/// </summary>
		/// <returns><c>true</c> if the view was able to scroll; <c>false</c> if the view is already scrolled all the way.</returns>
		/// <param name="cols">Number of columns to scroll by.</param>
		public bool ScrollRight(int cols) {
			var nx = Math.Max(-contentSize.Width, contentOffset.X - cols);
			if (nx == contentOffset.X)
				return false;

			ContentOffset = new Point(nx, contentOffset.Y);
			return true;
		}
		/// <summary>
		/// Receives a key event. First passes the event to the focused view. If the event is not handled by then, this view implements scrolling functionality (if enabled) with the arrow keys and PageUp / PageDown.
		/// </summary>
		/// <returns><c>true</c>, if right was scrolled, <c>false</c> otherwise.</returns>
		/// <param name="kb">The key event to handle.</param>
		public override bool ProcessKey(KeyEvent kb) {
			if (base.ProcessKey(kb))
				return true;

			switch (kb.Key) {
				case Key.CursorUp:
					if (allowVertical)
						return ScrollUp(1);
					break;
				case (Key)'v' | Key.AltMask:
				case Key.PageUp:
					if (allowVertical)
						return ScrollUp(Bounds.Height);
					break;

				case Key.ControlV:
				case Key.PageDown:
					if (allowVertical)
						return ScrollDown(Bounds.Height);
					break;

				case Key.CursorDown:
					if (allowVertical)
						return ScrollDown(1);
					break;

				case Key.CursorLeft:
					if (allowHorizontal)
						return ScrollLeft(1);
					break;

				case Key.CursorRight:
					if (allowHorizontal)
						return ScrollRight(1);
					break;

			}
			return false;
		}
	}
}
