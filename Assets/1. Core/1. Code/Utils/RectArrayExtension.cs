using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Logic;

public static class RectArrayExtension {
    public static void ForEach<T>(this T[,] array, Action<T> toDo) {
        foreach (var elem in array) {
            toDo(elem);
        }
    }
    public static bool Any<T>(this T[,] array, Predicate<T> predicate) {
        foreach (var elem in array) {
            if (predicate(elem)) {
                return true;
            }
        }

        return false;
    }

    public static void FillWith<T>(this T[,] array, Func<T> elemToFill) {
        for (var i = 0; i < array.GetLength(0); i++)
        for (var j = 0; j < array.GetLength(1); j++) {
            array[i, j] = elemToFill.Invoke();
        }
    }

    public static List<T> ToList<T>(this T[,] array) {
        return array.Cast<T>().ToList();
    }

    public static T ByListIndex<T>(this T[,] array, int index) {
        if (index >= array.Length) {
            throw new Exception($"{index} out of range");
        }

        var point = array.PointByListIndex(index);
        return array[point.Y, point.X];
    }

    public static Point PointByListIndex<T>(this T[,] array, int index) {
        if (index >= array.Length) {
            throw new Exception($"{index} out of range");
        }

        var xLen = array.GetLength(1);
        var y = index / xLen;
        var x = index % xLen;
        return new Point(y, x);
    }

    public static int GetListIndexOf<T>(this T[,] array, T elem) {
        var index = 0;
        var yLen = array.GetLength(0);
        var xLen = array.GetLength(1);
        for (var y = 0; y < yLen; y++) {
            for (var x = 0; x < xLen; x++, index++) {
                if (array[y, x].Equals(elem)) {
                    return index;
                }
            }
        }

        return -1;
    }

    public static int GetListIndexOf<T>(this T[,] array, Point point) {
        var xLen = array.GetLength(1);
        var index = point.Y * xLen + point.X;
        if (index >= array.Length) {
            throw new Exception($"{point} out of range");
        }

        return index;
    }

    public static ref T GetRef<T>(this T[,] array, Point point) => ref array[point.Y, point.X];
    public static T Get<T>(this T[,] array, Point point) => array[point.Y, point.X];
}