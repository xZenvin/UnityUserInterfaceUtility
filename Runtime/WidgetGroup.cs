﻿using System.Collections.Generic;
using System.Collections;

namespace Zenvin.UI {
	/// <summary>
	/// A helper class for grouping <see cref="UserInterfaceWidget"/>s with the same identifier.
	/// </summary>
	public class WidgetGroup : IEnumerable<UserInterfaceWidget> {

		private readonly List<UserInterfaceWidget> widgets = new List<UserInterfaceWidget> ();

		public string Identifier { get; internal set; }
		public int Count => widgets.Count;
		public UserInterfaceWidget this[int index] => index >= 0 && index < Count ? widgets[index] : null;
		public UserInterfaceWidget First => widgets.Count > 0 ? widgets[0] : null;


		private WidgetGroup () { }

		internal WidgetGroup (string identifier) {
			Identifier = identifier;
		}

		internal WidgetGroup (string identifier, params UserInterfaceWidget[] widgets) : this (identifier) {
			this.widgets = new List<UserInterfaceWidget> (widgets);
			this.widgets.Sort ();
		}

		public bool TryGetFirst (out UserInterfaceWidget widget) {
			widget = First;
			return widget != null;
		}

		public bool TryGet (int index, out UserInterfaceWidget widget) {
			widget = this[index];
			return widget != null;
		}

		internal bool Add (UserInterfaceWidget widget) {
			if (widget == null || widgets.Contains (widget)) {
				return false;
			}
			widgets.Add (widget);
			widgets.Sort ();
			return true;
		}

		internal bool Remove (UserInterfaceWidget widget, out bool empty) {
			bool rem = widgets.Remove (widget);
			empty = Count == 0;
			return rem;
		}

		public IEnumerator<UserInterfaceWidget> GetEnumerator () {
			return widgets.GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator () {
			return GetEnumerator ();
		}

		public static implicit operator UserInterfaceWidget (WidgetGroup group) {
			if (group == null) {
				return null;
			}
			return group[0];
		}

	}
}