using Slots.Game.Events;
using Slots.Game.Utils;
using UnityEngine;

namespace Slots.Game.UI
{
    public class LinePopups : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _lines;

        public void Initialize(Transform[] transforms)
        {
            _lines = transforms;
        }

        public void ShowLinePopup(IGameEventData gameEventData)
        {
            ShowLinePopup(gameEventData as ILinePopupData);
        }

        public void ShowLinePopup(ILinePopupData linePopupData)
        {
            var lineIndex = linePopupData.LineIndex;
            _lines[lineIndex].gameObject.SetActive(true);
            StartCoroutine(ObjectDisabler.DisableGOAfterDelay(_lines[lineIndex].gameObject));
        }
    }
}