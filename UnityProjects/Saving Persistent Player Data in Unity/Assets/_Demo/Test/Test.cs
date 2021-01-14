using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int i;
    public float f;
    public string s;
    public Vector3 v;
    public Color c;
    public LayerMask l;
    public AnimationCurve a;

    public GameObject g;
    public Rigidbody r;
    public Transform t;

    public int[] ints;
    public float[] floats;

    public C cl;

    public S str;

    [Serializable]
    public class C
    {
        public int i;
        public float f;
    }

    [Serializable]
    public struct S
    {
        public int i;
        public float f;
    }
}
