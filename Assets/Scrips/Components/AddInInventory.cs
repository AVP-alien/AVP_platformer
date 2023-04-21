using AVPplatformer.Creatures;
using AVPplatformer.Model.Definitions;
using UnityEngine;

namespace AVPplatformer.Components
{
    public class AddInInventory : MonoBehaviour
    {
      [InventoryId]  [SerializeField] private string _id;
        [SerializeField] private int _count;


        public void Add(GameObject go)
        {
           var hero = go.GetComponent<HERO>();
            if (hero != null ) 
            { 
                hero.AddInInventory(_id, _count);
            }
        }
    }
}

