using AVPplatformer.Model;
using UnityEngine;
using UnityEngine.Events;

namespace AVPplatformer.Components
{
    public class RequireItemComponent : MonoBehaviour
    {
        [SerializeField] private InventoryItemData[] _recuired;

        [SerializeField] private bool _removeAfterUse;

        [SerializeField] private UnityEvent _onSuccess;
        [SerializeField] private UnityEvent _onFail;
        public void Check()
        {
            var session = FindObjectOfType<GameSession>();
            var areAllRequirementsMet = true;
            foreach (var item in _recuired)
            {
                var numItems = session.Data.Inventory.Count(item.Id);
                if (numItems < item.Value)
                {
                    areAllRequirementsMet = false;
                }
            }

            if (areAllRequirementsMet)
            {
                if (_removeAfterUse)
                {
                    foreach (var item in _recuired)
                        session.Data.Inventory.Remove(item.Id, item.Value);
                }
                _onSuccess?.Invoke();
            }
            else
            {
                _onFail?.Invoke();
            }
        }



    }
}
