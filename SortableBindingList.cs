using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class SortableBindingList<T> : BindingList<T>
    {
        protected override bool SupportsSortingCore => true;

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            var items = this.Items;
            var sorted = direction == ListSortDirection.Ascending
                ? items.OrderBy(x => property.GetValue(x)).ToList()
                : items.OrderByDescending(x => property.GetValue(x)).ToList();

            for (int i = 0; i < sorted.Count; i++)
                items[i] = sorted[i];

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }
}
