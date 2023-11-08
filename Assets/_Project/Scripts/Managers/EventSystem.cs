using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldGeneration
{
    public static class EventSystem
    {
        public static Action OnCellGenerationComplete;
        public static void CallCellGenerationComplete() => OnCellGenerationComplete?.Invoke();

        public static  Action<Transform> OnPlayerSpawned;
        public static void CallPlayerSpawned(Transform _player) => OnPlayerSpawned?.Invoke(_player);

    }

}