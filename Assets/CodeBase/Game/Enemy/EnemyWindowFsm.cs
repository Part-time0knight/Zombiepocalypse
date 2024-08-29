using Core.MVVM.Windows;
using Presentation.View;
using Zenject;

namespace Game.Enemy
{
    public class EnemyWindowFsm : WindowFsm, IInitializable
    {
        public void Initialize()
        {
            ResolveWindows();
        }

        private void ResolveWindows()
        {
            CleanUp();
            Set<EnemyHealthView>();
        }
    }
}