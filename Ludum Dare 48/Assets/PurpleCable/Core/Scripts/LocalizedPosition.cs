using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PurpleCable
{
    public class LocalizedPosition : MonoBehaviour
    {
        [SerializeField] Vector3 PositionFr = Vector3.zero;
        [SerializeField] Vector3 PositionEs = Vector3.zero;
        [SerializeField] Vector3 PositionPt = Vector3.zero;
        [SerializeField] Vector3 PositionDe = Vector3.zero;
        [SerializeField] Vector3 PositionNl = Vector3.zero;
        [SerializeField] Vector3 PositionTr = Vector3.zero;
        [SerializeField] Vector3 PositionRu = Vector3.zero;

        [SerializeField] bool IgnoreChildren = false;

        private void Start()
        {
            Vector3[] childPositions = null;

            if (IgnoreChildren)
            {
                childPositions = new Vector3[transform.childCount];

                for (int i = 0; i < transform.childCount; i++)
                {
                    childPositions[i] = transform.GetChild(i).position;
                }
            }

            switch (TextManager.CurrLanguage)
            {
                case "French":
                    transform.localPosition = PositionFr;
                    break;

                case "Spanish":
                    transform.localPosition = PositionEs;
                    break;

                case "Portuguese":
                    transform.localPosition = PositionPt;
                    break;

                case "German":
                    transform.localPosition = PositionDe;
                    break;

                case "Dutch":
                    transform.localPosition = PositionNl;
                    break;

                case "Turkish":
                    transform.localPosition = PositionTr;
                    break;

                case "Russian":
                    transform.localPosition = PositionRu;
                    break;
            }

            if (IgnoreChildren)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).position = childPositions[i];
                }
            }
        }
    }
}
