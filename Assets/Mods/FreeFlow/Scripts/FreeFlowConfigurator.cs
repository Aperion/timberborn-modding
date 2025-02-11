using Bindito.Core;
using UnityEngine;

namespace Mods.FreeFlow.Scripts {
  [Context("Game")]
  [Context("MapEditor")]
  public class FreeFlowConfigurator : IConfigurator {

    public void Configure(IContainerDefinition containerDefinition) {
      Debug.Log("Aperion.FreeFlow: initializer");
      containerDefinition.Bind<FreeFlowInitializer>().AsSingleton();
    }

  }
}