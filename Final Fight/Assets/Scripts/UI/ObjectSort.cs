using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class ObjectSort
{
    public static void SortChildren(this GameObject obj)
    {
        List<Transform> children = obj.GetComponentsInChildren<Transform>(true).ToList();
        children.Remove(obj.transform);
        children.Sort(Compare);
        for (int i = 0; i < children.Count; i++)
            children[i].SetSiblingIndex(i);
    }

    private static int Compare(Transform lhs, Transform rhs)
    {
        if (lhs == rhs) return 0;
        int test = rhs.gameObject.activeInHierarchy.CompareTo(lhs.gameObject.activeInHierarchy);
        if (test != 0) return test;
        if (lhs.localPosition.z < rhs.localPosition.z) return 1;
        if (lhs.localPosition.z > rhs.localPosition.z) return -1;
        return 0;
    }
}

// Source:
// http://forum.unity3d.com/threads/ui-image-how-to-change-render-order-between-parent-and-child.268340/
// Post #4
