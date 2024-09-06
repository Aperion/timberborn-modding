using HarmonyLib;
using System.Reflection;
using Timberborn.SingletonSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Mods.FreeFlow.Scripts {
  public class FreeFlowInitializer : ILoadableSingleton {

    private readonly WaterSimulationSettings _waterSimulationSettings;
    private Harmony _harmony = new("Aperion.Mods.FreeFlow");

    public FreeFlowInitializer(WaterSimulationSettings waterSimulationSettings) {
      _waterSimulationSettings = waterSimulationSettings;
      
      var type = AccessTools.TypeByName("Timberborn.WaterSystem.WaterSimulator");
      var wcType = AccessTools.TypeByName("Timberborn.WaterSystem.WaterColumn");
      Debug.unityLogger.Log($"Aperion.FreeFlow: {type}");
      var _getOutFlow = type.GetMethod("GetOutflow",
                                       BindingFlags.Instance | BindingFlags.NonPublic,
                                       null,
                                       CallingConventions.Any,
                                       new [] {
                                           wcType.MakeByRefType(),
                                           typeof(int),
                                           typeof(int),
                                           wcType.MakeByRefType(),
                                           typeof(int),
                                           typeof(int)
                                       },
                                       null);
      Debug.unityLogger.Log($"Aperion.FreeFlow: {_getOutFlow}");
      var methodInfo = _harmony.Patch(_getOutFlow, 
                                      transpiler: new (typeof(Patch), nameof(Patch.PatchGetOutFlowTranspiler)));
    }

    public void Load() {
      Debug.Log("Aperion.FreeFlow: Setting max water fall to infinity");
      // _waterSimulationSettings.MaxWaterfallOutflow = float.PositiveInfinity;
    }
  }
}