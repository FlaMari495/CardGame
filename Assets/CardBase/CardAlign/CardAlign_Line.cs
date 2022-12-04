using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlMr_CardBase
{
    public class CardAlign_Line : CardAlignBase
    {
        [SerializeField] private float param;

        internal override void Align()
        {
            var position = Trn.position;

            for (int i = 0; i < Trn.childCount; i++)
            {
                Trn.GetChild(i).position = position + Vector3.right * i * param;
            }
        }
    }
}
