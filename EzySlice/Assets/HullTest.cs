﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EzySlice;

public class HullTest : MonoBehaviour {

    public List<GameObject> pts = new List<GameObject>();

    public bool drawPoints = true;
    public bool drawTriangles = true;
    public bool run = false;

    void OnDrawGizmos() {
        if (!run) {
            return;
        }

        if (pts == null || pts.Count == 0) {
            return;
        }

        List<Vector3> points = new List<Vector3>();
        List<int> indices = new List<int>();

        foreach (GameObject obj in pts) {
            if (obj == null) {
                continue;
            }

            points.Add(obj.transform.position);
        }

        if (drawPoints) {
            Gizmos.color = Color.yellow;

            foreach (Vector3 point in points) {
                Gizmos.DrawWireCube(point, Vector3.one);
            }
        }

        Triangulator.TriangulateHullPt(ref points, ref indices);

        if (drawPoints) {
            Gizmos.color = Color.blue;

            foreach (Vector3 point in points) {
                Gizmos.DrawWireCube(point, Vector3.one / 2.0f);
            }
        }

        if (drawTriangles) {
            Gizmos.color = Color.green;

            for (int i = 0; i < indices.Count; i += 3) {
                Gizmos.DrawLine(points[indices[i]], points[indices[i + 1]]);
                Gizmos.DrawLine(points[indices[i + 1]], points[indices[i + 2]]);
                Gizmos.DrawLine(points[indices[i + 2]], points[indices[i]]);
            }
        }
    }
}
