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
        /// ‘SPosition
        /// </summary>
        [SerializeField] private List<Position> positions;

        private DataManager Manager { get; set; }

        private void Awake()
        {
            Manager = this.GetComponent<DataManager>();

            // DataManager‚Ì‰Šú‰»
            Manager.Initialize(positions.ToDictionary(p => p.GetType(), p => p.CardNumberLimit));

            // Position‚Ì‰Šú‰»
            foreach (var pos in positions)
            {
                pos.Initialize(Manager);
            }
        }
    }
}
