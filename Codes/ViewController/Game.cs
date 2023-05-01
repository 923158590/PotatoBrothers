using QFramework;

namespace Game
{
    public class Game : Architecture<Game>
    {
        protected override void Init()
        {
            // 地图系统
            RegisterSystem<IGridNodeSystem>(new GridNodeSystem());
            RegisterSystem<IGridCreateSystem>(new GridCreateSystem());
            // 玩家系统
            RegisterSystem<IPlayerCreateSystem>(new PlayerCreateSystem());
            // 敌人系统
            RegisterSystem<IEnemyCreateSystem>(new EnemyCreateSystem());
            // 游戏系统
            RegisterSystem<IGameSystem>(new GameSystem());
            // 时间系统
            RegisterSystem<ITimeSystem>(new TimeSystem());
            // 金币系统
            RegisterSystem<ICoinSystem>(new CoinSystem());
            // 枪支系统
            RegisterSystem<IGunSystem>(new GunSystem());
            // 等级系统
            RegisterSystem<ILevelSystem>(new LevelSystem());
            // UI系统
            RegisterSystem<IUISystem>(new UISystem());
            // 商店系统
            RegisterSystem<IShopSystem>(new ShopSystem());
            // 玩家系统
            RegisterSystem<IPlayerSystem>(new PlayerSystem());
            // 显示日志系统
            RegisterSystem<ILogSystem>(new LogSystem());

            // 玩家Model
            RegisterModel<IPlayerModel>(new PlayerModel());
            // 敌人Model
            RegisterModel<IEnemyModel>(new EnemyModel());
            // 枪支Model
            RegisterModel<IGunModel>(new PistolModel());
            RegisterModel<IGunModel>(new RifleModel());
            RegisterModel<IGunModel>(new SubmachineGunModel());
            //RegisterSystem<IInputSystem>(new InputSystem());
        }
    }
}