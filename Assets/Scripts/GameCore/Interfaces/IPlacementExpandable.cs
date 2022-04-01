using UnityEngine;

namespace Game.Core
{
    public interface IPlacementExpandable
    {
        void AddPlacemenst(params Transform[] transforms);
    }
}
