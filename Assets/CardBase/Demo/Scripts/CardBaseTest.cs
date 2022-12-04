using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlMr_CardBase.Demo
{
    public class CardBaseTest : MonoBehaviour
    {
        [SerializeField] private DataManager manager;
        [SerializeField] private Position position;

        void Start()
        {
            manager.Initialize(new() { position.GetType() });
            Debug.Log(manager.GetJsonData());
        }
    }
}