using Timberborn.SingletonSystem;
using Timberborn.WaterBuildings;
using Timberborn.WaterObjects;
using Timberborn.WaterSourceSystem;
using Timberborn.WaterSystem;
using UnityEngine;

namespace Mods.FreeFlow.Scripts {
  public class FreeFlowInitializer : ILoadableSingleton {

    private readonly WaterSimulationSettings _waterSimulationSettings;

    public FreeFlowInitializer(WaterSimulationSettings waterSimulationSettings) {
      _waterSimulationSettings = waterSimulationSettings;
    }

    public void Load() {
      Debug.Log("Aperion.FreeFlow: Setting max water fall to infinity");
      _waterSimulationSettings.MaxWaterfallOutflow = float.PositiveInfinity;
    }

  }
}