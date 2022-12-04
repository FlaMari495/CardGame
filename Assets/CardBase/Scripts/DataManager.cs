using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UniRx;

namespace FlMr_CardBase
{
    /// <summary>
    /// �J�[�h�̈ʒu����ێ��A�X�V����N���X
    /// </summary>
    internal class DataManager : MonoBehaviour
    {
        /// <summary>
        /// �J�[�h�̈ʒu���
        /// </summary>
        private CardObjectData Data { get; set; }

        /// <summary>
        /// �SPosition�������Ƃɏ���������
        /// </summary>
        /// <param name="positions"></param>
        internal void Initialize(List<Type> positions)
        {
            Data = new CardObjectData(positions);
        }

        /// <summary>
        /// �J�[�h�̏�񂷂ׂĂ�Json�`���ɕϊ�����
        /// </summary>
        /// <returns></returns>
        internal string GetJsonData() => JsonUtility.ToJson(Data);

        /// <summary>
        /// �J�[�h�̈ʒu����ێ�����N���X
        /// </summary>
        [Serializable]
        private class CardObjectData
        {
            /// <summary>
            /// �ǂ̈ʒu�ɂǂ̃J�[�h�����݂��邩��ێ�����ϐ�
            /// </summary>
            [SerializeField] private SerializableDictionary<string, List<int>> dataCore;

            internal CardObjectData(List<Type> positions)
            {
                dataCore = new SerializableDictionary<string, List<int>>();

                foreach (var posInfo in positions)
                {
                    dataCore.Add(posInfo.ToString(), new List<int>());
                }

            }
        }
    }

}