using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlMr_CardBase
{
    internal class CardAlignCurve : CardAlignBase
    {
        [SerializeField] private float dTheta = 27;
        [SerializeField] private float maxLimAngleDeg = 30;
        [SerializeField] private float maxDiffAngleDeg = 6;
        [SerializeField] private float R = 7;
        [SerializeField] private float dz = 0.001f;

        internal override void Align()
        {
            if(ChildCount ==1)
            {
                Trn.GetChild(0).localPosition = GetPosition(0,0);
                Trn.GetChild(0).localRotation = Quaternion.identity;
                return;
            }

            float maxAngle = Mathf.Min(maxLimAngleDeg, dTheta * (ChildCount - 1));
            float diff = Mathf.Min(maxDiffAngleDeg,maxAngle / (ChildCount - 1));
            maxAngle = diff * (ChildCount - 1);

            for (int i = 0; i < Trn.childCount; i++)
            {
                float theta = diff * i - maxAngle/2;
                Trn.GetChild(i).localPosition = GetPosition(i,theta);
                Trn.GetChild(i).localRotation = Quaternion.Euler(0, theta, 0);
            }
        }

        private Vector3 GetPosition(int index,float theta)
        {
            return R * new Vector3((float)Mathf.Sin(theta * Mathf.PI / 180), index * dz, (float)Mathf.Cos(theta * Mathf.PI / 180)- 1);
        }
    }
}
