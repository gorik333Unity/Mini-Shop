using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public abstract class Item : MonoBehaviour
    {
        public float Height { get; private set; }

        private void Start()
        {
            DetermineHeight();
        }

        private void DetermineHeight()
        {
            var meshFilter = GetComponent<MeshFilter>();

            if (meshFilter == null)
                meshFilter = GetComponentInChildren<MeshFilter>();

            if (meshFilter == null)
                throw new ArgumentException("No mesh filter on the object.");

            Height = meshFilter.sharedMesh.bounds.size.y;
        }
    }
}