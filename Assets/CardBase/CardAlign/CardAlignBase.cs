using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlMr_CardBase {
    public abstract class CardAlignBase : MonoBehaviour
    {
        public bool IsRunning { get; set; } = true;

        protected Transform Trn { get; private set; }

        protected int ChildCount { get; set; }

        private void OnDrawGizmos()
        {
            if(Trn == null)
            {
                Trn = transform;
            }
            Update();
        }



        private void Start()
        {
            Trn = transform;
        }

        private void Update()
        {
            if (!IsRunning) return;

            int currentChildCount = Trn.childCount;

#if !UNITY_EDITOR
            if (ChildCount == currentChildCount) return;
#endif

            ChildCount = currentChildCount;
            if (ChildCount > 0)
            {
                Align();
            }



        }

        internal abstract void Align();
    }
}
