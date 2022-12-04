using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FlMr_CardBase
{
    /// <summary>
    /// �V���A�����\�Ȏ����N���X
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [Serializable]
        private class DataCore
        {
            public DataCore(List<Pair> pair) => Pair = pair;
            public List<Pair> Pair;
        }

        [Serializable]
        private struct Pair
        {
            internal Pair(KeyValuePair<TKey, TValue> pair)
            {
                key = pair.Key;
                value = pair.Value;
            }
            public TKey key;
            public TValue value;
        }

        /// <summary>
        /// �V���A���������Key,Value�y�A
        /// </summary>
        [SerializeField] private DataCore dataCore;

        /// <summary>
        /// �f�V���A���C�Y�̃R�[���o�b�N
        /// </summary>
        public void OnAfterDeserialize()
        {
            // �����l�����ƂɎ����𕜌�
            dataCore.Pair.ForEach(x => Add(x.key, x.value));
        }

        /// <summary>
        /// �V���A���C�Y�̃R�[���o�b�N
        /// </summary>
        public void OnBeforeSerialize()
        {
            // �����̏������ƂɁA�V���A���C�Y�p��DataCore�N���X�̃C���X�^���X���쐬
            dataCore = new(this.ToList().ConvertAll(x => new Pair(x)));
        }
    }
}
