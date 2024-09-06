using Bindito.Core;

namespace Mods.FreeFlow.Scripts {
  [Context("Game")]
  [Context("MapEditor")]
  public class FreeFlowConfigurator : IConfigurator {

    public void Configure(IContainerDefinition containerDefinition) {
      containerDefinition.Bind<FreeFlowInitializer>().AsSingleton();
    }

  }
}