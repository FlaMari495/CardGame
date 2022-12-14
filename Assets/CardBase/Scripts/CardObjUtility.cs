using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FlMr_CardBase
{

    [RequireComponent(typeof(DataManager))]
    public class CardObjUtility : MonoBehaviour
    {
        /// <summary>
        /// 全Position
        /// </summary>
        [SerializeField] private List<Position> positions;

        private DataManager Manager { get; set; }

        private void Awake()
        {
            Manager = this.GetComponent<DataManager>();

            // DataManagerの初期化
            Manager.Initialize(positions.ToDictionary(p => p.GetType(), p => p.CardNumberLimit));

            // Positionの初期化
            foreach (var pos in positions)
            {
                pos.Initialize(Manager);
            }
        }
    }
}
