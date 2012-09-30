using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Tweetinvi.Utils
{
    public static class ObservableCollectionExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerableList)
        {
            if (enumerableList != null)
            {
                var observableCollection = new ObservableCollection<T>();

                foreach (var item in enumerableList)
                {
                    observableCollection.Add(item);
                }

                return observableCollection;
            }

            return null;
        }
    }
}
