using System.Collections.Generic;
using UnityEngine;

namespace Zenvin.UI.Layout {
	public static class LayoutUtility {

		public static void CalculateRowHeights (List<RowDefinition> rows, Vector2 totalSize, ref float[] values, float scale = 1f) {
			InitializeArray (ref values, Mathf.Max (rows?.Count ?? 0, 1));

			if (rows == null || rows.Count <= 1) {
				values[0] = totalSize.y;
				return;
			}

			int relativeRows = 0;
			float relativeTotal = 0f;

			for (int i = 0; i < rows.Count; i++) {
				switch (rows[i].Unit) {
					case CellSizeUnit.Fixed:
						totalSize.y -= rows[i].Height * scale;
						values[i] = rows[i].Height * scale;
						break;
					case CellSizeUnit.Remaining:
						relativeRows++;
						relativeTotal += rows[i].Height;
						break;
				}
			}

			if (relativeRows == 0) {
				return;
			}

			for (int i = 0; i < rows.Count; i++) {
				if (rows[i].Unit == CellSizeUnit.Remaining) {
					float height = rows[i].Height / relativeTotal;
					values[i] = height * totalSize.y;
				}
			}
		}

		public static void CalculateColumnWidths (List<ColumnDefinition> columns, Vector2 totalSize, ref float[] values, float scale = 1f) {
			InitializeArray (ref values, Mathf.Max (columns?.Count ?? 0, 1));

			if (columns == null || columns.Count <= 1) {
				values[0] = totalSize.x;
				return;
			}

			int relativeColumns = 0;
			float relativeTotal = 0f;

			for (int i = 0; i < columns.Count; i++) {
				switch (columns[i].Unit) {
					case CellSizeUnit.Fixed:
						totalSize.x -= columns[i].Width * scale;
						values[i] = columns[i].Width * scale;
						break;
					case CellSizeUnit.Remaining:
						relativeColumns++;
						relativeTotal += columns[i].Width;
						break;
				}
			}

			if (relativeColumns == 0) {
				return;
			}

			for (int i = 0; i < columns.Count; i++) {
				if (columns[i].Unit == CellSizeUnit.Remaining) {
					float width = columns[i].Width / relativeTotal;
					values[i] = width * totalSize.x;
				}
			}
		}

		private static void InitializeArray<T> (ref T[] arr, int size) {
			if (arr != null && arr.Length == size || size < 0) {
				return;
			}
			arr = new T[size];
		}

	}
}