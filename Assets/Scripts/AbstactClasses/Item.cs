using System;
using UnityEngine;

namespace Game.Core
{
    public abstract class Item : MonoBehaviour
    {
        public int Price { get; private set; }

        public float MeshHeight { get; private set; }

        protected void SetPrice(int price)
        {
            Price = price;
        }

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

            MeshHeight = meshFilter.sharedMesh.bounds.size.y * meshFilter.transform.localScale.y;
        }

        protected abstract void SetUpItem();
    }
}