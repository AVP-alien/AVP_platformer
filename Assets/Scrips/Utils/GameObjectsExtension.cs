using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AVPplatformer.Utils
{ 
    public static class GameObjectsExtension
    {
        public static bool IsInlayer(this GameObject go, LayerMask layer)
        {
            return layer == (layer | 1 << go.layer);
        }

    }
 
}