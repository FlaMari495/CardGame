using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlMr_CardBase.Demo
{
    public class CardBaseTest : MonoBehaviour
    {
        [SerializeField] private DataManager manager;
        [SerializeField] private List<Position> positions;

        void Start()
        {
            manager.Initialize(positions.ConvertAll(p=>p.GetType()) );

            //�J�[�h��4���ǉ�
            manager.AddCard(typeof(Deck), 0, true); // �uDeck�v�́u0�v�Ԗڂ̈ʒu�Ɂu���v��id�����J�[�h��ǉ�
            manager.AddCard(typeof(Deck), 0, true);
            manager.AddCard(typeof(Deck), 0, true);
            manager.AddCard(typeof(Deck), 0, true);

            // �J�[�h��2���ړ�
            manager.MoveCard(2,typeof(Hand), null); // CardObjectId���u2�v�̃J�[�h���uHand�v�́u�Ō���v�Ɉړ�
            manager.MoveCard(3,typeof(Hand), null);

            // �󋵊m�F
            Debug.Log(manager.GetJsonData());
        }
    }
}