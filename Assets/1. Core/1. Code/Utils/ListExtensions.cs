using System.Collections.Generic;
using System.Linq;

public static class ListExtensions {
    public static bool HasSameContent<T>(this List<T> list1, List<T> list2) {
        var list1Count = list1.Count;
        if (list1Count != list2.Count) {
            return false;
        }

        for (var i = 0; i < list1Count; i++) {
            var index = list2.IndexOf(list1[i]);
            if (index == -1) {
                return false;
            }

            if (list2.Count(elem => elem.Equals(list1[i])) != list1.Count(elem => elem.Equals(list1[i]))) {
                return false;
            }
        }

        return true;
    }
}