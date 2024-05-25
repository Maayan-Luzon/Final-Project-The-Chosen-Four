using DB_Project.Models;
using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChosenFour_GameProject.GameObjects.Heroes;
using TheChosenFour_GameProject.GameObjects.Lands;
using TheChosenFour_GameProject.GameObjects.Villains;
using Windows.System;

namespace TheChosenFour_GameProject.GameServices
{
    public class GameManager : Manager
    {
        private enum CombatState // the diffrent combat states
        {
            OutOfCombat,
            StartingCombat,
            InCombat,
            EnterFinishingCombat,
            FinishingCombat,
            ExitingCombat
        }

        private CombatState _combatState; // the combat state
        private GameObject _combatant1; // the first combatant in a combat
        private GameObject _combatant2; // the second combatant in a combat
        private GameScene _gameScene; // the game's scene

        public static GameUser Player = new GameUser(); // the user
        public static GameLevel Level = new GameLevel(); // the level
        public static AppGameEvents AppGameEvents = new AppGameEvents(); // all of the require gameEvents
        
        public GameManager(Scene scene) : base(scene)
        {
            AppGameEvents.OnEnterCombat += OnEnterCombat;
            AppGameEvents.OnFinishingCombat += OnFinishingCombat;
            AppGameEvents.OnExitCombat += OnExitCombat;
            AppGameEvents.OnFinishCombatInPlace += OnFinishCombatInPlace;

            _combatState = CombatState.OutOfCombat;

            scene.Ground = scene.ActualHeight;
            Player.CurrentLevel = Level;
            _gameScene = (GameScene)scene;
            Init();
        }

        /// <summary>
        /// this method run at the start of the game. it sets all the object in thier place
        /// in accordance to the wanted level. In addition it activates the Scene's Start action.
        /// </summary>
        public override void Start()
        {
            base.Start();
            switch (Level.LevelID)
            {
                case 1:
                    CreateLandFieldLevel1();
                    break;

                case 2: 
                    CreateLandFieldLevel2(); 
                    break;

                case 3:
                    CreateLandFieldLevel3();
                    break;

            }
            
            Scene.Start();
        }

        /// <summary>
        /// this method pause the game by changing the game state and activating the Scene's Pause action
        /// </summary>
        public override void Pause()
        {
            base.Pause();
            Scene.Pause();
        }

        /// <summary>
        /// this method resume the game by changing the game state and activating the Scene's Resume action
        /// </summary>
        public override void Resume()
        {
            base.Resume();
            Scene.Resume();
        }

        /// <summary>
        /// this action clean all the past objects, creates and place all the heroes in their place 
        /// </summary>
        public void Init()
        {
            Scene.RemoveAllObjects();
            
            Hero hM = new Musketeer(Scene, Hero.HeroState.Idle, 10, Scene.Ground - 250);
            Scene.AddObject(hM);
            Hero hK = new Knight(Scene, Hero.HeroState.Idle, 150, Scene.Ground - 250);
            Scene.AddObject(hK);
            Hero hA = new Archer(Scene, Hero.HeroState.Idle, 300, Scene.Ground - 250);
            Scene.AddObject(hA);
            Hero hW = new Wizard(Scene, Hero.HeroState.Idle, 450, Scene.Ground - 250);
            Scene.AddObject(hW);
        }

        // The methods to create the different levels. each one is harder then its previous
        private void CreateLandFieldLevel1()
        {
              Land land = new Land(Scene, 300, Scene.ActualWidth, Scene.Ground - 300, "greenStoneLand1.png");
              Scene.AddObject(land);
            Land land1_2 = new Land(Scene, 300, land.Rect.Right, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land1_2);
            Land land2 = new Land(Scene, 300, land.Rect.Right + 50, Scene.Ground - 500, "greenStoneLand1.png");
              Scene.AddObject(land2);
              Land land3 = new Land(Scene, 400, land2.Rect.Right + 30, Scene.Ground - 400, "greenStoneLand1.png");
              Scene.AddObject(land3);
              Land land4 = new Land(Scene, 400, land3.Rect.Right , Scene.Ground - 300, "greenStoneLand2.png");
              Scene.AddObject(land4);
              Land land5 = new Land(Scene, 250, land4.Rect.Right + 100, Scene.Ground - 100, "greenStoneLand1.png");
              Scene.AddObject(land5);
              Land land6 = new Land(Scene, 300, land3.Rect.Right + 70, Scene.Ground - 500, "greenStoneLand1.png");
              Scene.AddObject(land6);
              Land land7 = new Land(Scene, 350, land6.Rect.Right, Scene.Ground - 500, "greenStoneLand1.png");
              Scene.AddObject(land7);
              Land land8 = new Land(Scene, 400, land7.Rect.Right + 30, Scene.Ground - 400, "greenStoneLand1.png");
              Scene.AddObject(land8);
              Land land9 = new Land(Scene, 300, land7.Rect.Right + 30, Scene.Ground - 200, "greenStoneLand1.png");
              Scene.AddObject(land9);
            Land land10 = new Land(Scene, 300, land9.Rect.Right +30, Scene.Ground - 150, "greenStoneLand1.png");
            Scene.AddObject(land10);
            Land land11 = new Land(Scene, 250, land10.Rect.Right + 60, Scene.Ground - 250, "greenStoneLand1.png");
            Scene.AddObject(land11);
            Land land12 = new Land(Scene, 300, land11.Rect.Right, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land12);
            Land land13 = new Land(Scene, 300, land12.Rect.Right + 40, Scene.Ground - 400, "greenStoneLand1.png");
            Scene.AddObject(land13);
            Land land14 = new Land(Scene, 300, land12.Rect.Right + 40, Scene.Ground - 200, "greenStoneLand1.png");
            Scene.AddObject(land14); 
            Land land15 = new Land(Scene, 250, land14.Rect.Right + 70, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land15);
            Land land16 = new Land(Scene, 300, land15.Rect.Right + 100, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land16); 
            Land land17 = new Land(Scene, 350, land16.Rect.Right + 70, Scene.Ground - 400, "greenStoneLand1.png");
            Scene.AddObject(land17); 
            Land land18 = new Land(Scene, 300, land16.Rect.Right + 70, Scene.Ground - 200, "greenStoneLand1.png");
            Scene.AddObject(land18);
            Land land19 = new Land(Scene, 300, land17.Rect.Right + 40, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land19);
            Land land20 = new Land(Scene, 300, land19.Rect.Right + 40, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land20);


            Monster m = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, Scene.ActualWidth+150, Scene.Ground - 49 * 3, 2, 10);
            Scene.AddObject(m);
            Monster monster1 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land3.Rect.Left +20, land3.Rect.Top - 49 * 3, 2, 12);
            Scene.AddObject(monster1);
            Monster monster2 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land5.Rect.Left +20, land5.Rect.Top - 49 * 3, 3, 14);
            Scene.AddObject(monster2);
            Monster monster3 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land10.Rect.Left + 20, land10.Rect.Top - 49 * 3, 5, 16);
            Scene.AddObject(monster3); 
            Monster monster4 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land15.Rect.Left + 20, land15.Rect.Top - 49 * 3, 6, 18);
            Scene.AddObject(monster4);
            Monster monster5 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land17.Rect.Left + 20, land17.Rect.Top - 49 * 3, 7, 20);
            Scene.AddObject(monster5);
            Monster monster6 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land19.Rect.Left + 20, Scene.Ground - 49 * 3, 8, 22);
            Scene.AddObject(monster6);


        }
        private void CreateLandFieldLevel2()
        {
            Land land = new Land(Scene, 300, Scene.ActualWidth, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land);
            Land land1_2 = new Land(Scene, 300, land.Rect.Right, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land1_2);
            Land land2 = new Land(Scene, 300, land.Rect.Right + 50, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land2);
            Land land3 = new Land(Scene, 400, land2.Rect.Right + 30, Scene.Ground - 400, "greenStoneLand1.png");
            Scene.AddObject(land3);
            Land land4 = new Land(Scene, 400, land3.Rect.Right, Scene.Ground - 300, "greenStoneLand2.png");
            Scene.AddObject(land4);
            Land land5 = new Land(Scene, 250, land4.Rect.Right + 100, Scene.Ground - 100, "greenStoneLand1.png");
            Scene.AddObject(land5);
            Land land6 = new Land(Scene, 300, land3.Rect.Right + 70, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land6);
            Land land7 = new Land(Scene, 350, land6.Rect.Right, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land7);
            Land land8 = new Land(Scene, 400, land7.Rect.Right + 30, Scene.Ground - 400, "greenStoneLand1.png");
            Scene.AddObject(land8);
            Land land9 = new Land(Scene, 300, land7.Rect.Right + 30, Scene.Ground - 200, "greenStoneLand1.png");
            Scene.AddObject(land9);
            Land land10 = new Land(Scene, 300, land9.Rect.Right + 30, Scene.Ground - 150, "greenStoneLand1.png");
            Scene.AddObject(land10);
            Land land11 = new Land(Scene, 250, land10.Rect.Right + 60, Scene.Ground - 250, "greenStoneLand1.png");
            Scene.AddObject(land11);
            Land land12 = new Land(Scene, 300, land11.Rect.Right, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land12);
            Land land13 = new Land(Scene, 300, land12.Rect.Right + 40, Scene.Ground - 400, "greenStoneLand1.png");
            Scene.AddObject(land13);
            Land land14 = new Land(Scene, 300, land12.Rect.Right + 40, Scene.Ground - 200, "greenStoneLand1.png");
            Scene.AddObject(land14);
            Land land15 = new Land(Scene, 250, land14.Rect.Right + 70, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land15);
            Land land16 = new Land(Scene, 300, land15.Rect.Right + 100, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land16);
            Land land17 = new Land(Scene, 350, land16.Rect.Right + 70, Scene.Ground - 400, "greenStoneLand1.png");
            Scene.AddObject(land17);
            Land land18 = new Land(Scene, 300, land16.Rect.Right + 70, Scene.Ground - 200, "greenStoneLand1.png");
            Scene.AddObject(land18);
            Land land19 = new Land(Scene, 300, land17.Rect.Right + 40, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land19);
            Land land20 = new Land(Scene, 300, land19.Rect.Right + 40, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land20);
            Land land21 = new Land(Scene, 300, land19.Rect.Right + 40, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land21);
            Land land22 = new Land(Scene, 300, land20.Rect.Right + 40, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land22);
            Land land23 = new Land(Scene, 300, land21.Rect.Right + 40, Scene.Ground - 200, "greenStoneLand1.png");
            Scene.AddObject(land23);
            Land land24 = new Land(Scene, 300, land23.Rect.Right + 20, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land24);
            Land land25 = new Land(Scene, 300, land24.Rect.Right + 40, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land25);


            
            Monster m = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, Scene.ActualWidth + 150, Scene.Ground - 49 * 3, 6, 40);
            Scene.AddObject(m);
            Monster monster1 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land2.Rect.Left + 20, land2.Rect.Top - 49 * 3, 6, 44);
            Scene.AddObject(monster1);
            Monster monster2 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land4.Rect.Left + 20, land4.Rect.Top - 49 * 3, 7, 46);
            Scene.AddObject(monster2);
            Monster monster3 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land7.Rect.Left + 20, land7.Rect.Top - 49 * 3, 7, 48);
            Scene.AddObject(monster3);
            Monster monster4 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land12.Rect.Left + 20, land12.Rect.Top - 49 * 3, 8, 48);
            Scene.AddObject(monster4);
            Monster monster5 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land15.Rect.Left + 20, land15.Rect.Top - 49 * 3, 9, 50);
            Scene.AddObject(monster5);
            Monster monster6 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land17.Rect.Left, Scene.Ground - 49 * 3, 10, 52);
            Scene.AddObject(monster6);
            Monster monster7 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land20.Rect.Left + 20, land20.Rect.Top - 49 * 3, 15, 54);
            Scene.AddObject(monster7);
            Monster monster8 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land22.Rect.Left + 20, land22.Rect.Top - 49 * 3, 17, 56);
            Scene.AddObject(monster8);
            Monster monster9 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land25.Rect.Left + 20, Scene.Ground - 49 * 3, 20, 60);
            Scene.AddObject(monster9);

        }
        private void CreateLandFieldLevel3()
        {
            Land land = new Land(Scene, 300, Scene.ActualWidth, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land);
            Land land1_2 = new Land(Scene, 300, land.Rect.Right, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land1_2);
            Land land2 = new Land(Scene, 300, land.Rect.Right + 50, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land2);
            Land land3 = new Land(Scene, 400, land2.Rect.Right + 30, Scene.Ground - 400, "greenStoneLand1.png");
            Scene.AddObject(land3);
            Land land4 = new Land(Scene, 400, land3.Rect.Right, Scene.Ground - 300, "greenStoneLand2.png");
            Scene.AddObject(land4);
            Land land5 = new Land(Scene, 250, land4.Rect.Right + 100, Scene.Ground - 100, "greenStoneLand1.png");
            Scene.AddObject(land5);
            Land land6 = new Land(Scene, 300, land3.Rect.Right + 70, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land6);
            Land land7 = new Land(Scene, 350, land6.Rect.Right, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land7);
            Land land8 = new Land(Scene, 400, land7.Rect.Right + 30, Scene.Ground - 400, "greenStoneLand1.png");
            Scene.AddObject(land8);
            Land land9 = new Land(Scene, 300, land7.Rect.Right + 30, Scene.Ground - 200, "greenStoneLand1.png");
            Scene.AddObject(land9);
            Land land10 = new Land(Scene, 300, land9.Rect.Right + 30, Scene.Ground - 150, "greenStoneLand1.png");
            Scene.AddObject(land10);
            Land land11 = new Land(Scene, 250, land10.Rect.Right + 60, Scene.Ground - 250, "greenStoneLand1.png");
            Scene.AddObject(land11);
            Land land12 = new Land(Scene, 300, land11.Rect.Right, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land12);
            Land land13 = new Land(Scene, 300, land12.Rect.Right + 40, Scene.Ground - 400, "greenStoneLand1.png");
            Scene.AddObject(land13);
            Land land14 = new Land(Scene, 300, land12.Rect.Right + 40, Scene.Ground - 200, "greenStoneLand1.png");
            Scene.AddObject(land14);
            Land land15 = new Land(Scene, 250, land14.Rect.Right + 70, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land15);
            Land land16 = new Land(Scene, 300, land15.Rect.Right + 100, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land16);
            Land land17 = new Land(Scene, 350, land16.Rect.Right + 70, Scene.Ground - 400, "greenStoneLand1.png");
            Scene.AddObject(land17);
            Land land18 = new Land(Scene, 300, land16.Rect.Right + 70, Scene.Ground - 200, "greenStoneLand1.png");
            Scene.AddObject(land18);
            Land land19 = new Land(Scene, 300, land17.Rect.Right + 40, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land19);
            Land land20 = new Land(Scene, 300, land19.Rect.Right + 40, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land20);
            Land land21 = new Land(Scene, 300, land19.Rect.Right + 40, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land21);
            Land land22 = new Land(Scene, 300, land20.Rect.Right + 40, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land22);
            Land land23 = new Land(Scene, 300, land21.Rect.Right + 40, Scene.Ground - 200, "greenStoneLand1.png");
            Scene.AddObject(land23);
            Land land24 = new Land(Scene, 300, land23.Rect.Right + 20, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land24);
            Land land25 = new Land(Scene, 300, land24.Rect.Right + 40, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land25);
            Land land26 = new Land(Scene, 300, land25.Rect.Right + 40, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land26);
            Land land27 = new Land(Scene, 300, land25.Rect.Right + 40, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land27);
            Land land28 = new Land(Scene, 300, land27.Rect.Right + 40, Scene.Ground - 200, "greenStoneLand1.png");
            Scene.AddObject(land28);
            Land land29 = new Land(Scene, 300, land27.Rect.Right + 40, Scene.Ground - 500, "greenStoneLand1.png");
            Scene.AddObject(land29);
            Land land30 = new Land(Scene, 300, land29.Rect.Right + 40, Scene.Ground - 300, "greenStoneLand1.png");
            Scene.AddObject(land30);


            Monster m = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, Scene.ActualWidth + 150, Scene.Ground - 49 * 3, 10, 65);
            Scene.AddObject(m);
            Monster monster1 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land.Rect.Left + 20, land.Rect.Top - 49 * 3, 10, 66);
            Scene.AddObject(monster1);
            Monster monster2 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land5.Rect.Left + 20, land5.Rect.Top - 49 * 3, 10, 68);
            Scene.AddObject(monster2);
            Monster monster3 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land8.Rect.Left + 20, land8.Rect.Top - 49 * 3, 15, 70);
            Scene.AddObject(monster3);
            Monster monster4 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land12.Rect.Left + 20, land12.Rect.Top - 49 * 3, 15, 72);
            Scene.AddObject(monster4);
            Monster monster5 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land16.Rect.Left + 20, land16.Rect.Top - 49 * 3, 15, 74);
            Scene.AddObject(monster5);
            Monster monster6 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land17.Rect.Left, Scene.Ground - 49 * 3, 20, 76);
            Scene.AddObject(monster6);
            Monster monster7 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land20.Rect.Left + 20, land20.Rect.Top - 49 * 3, 25, 78);
            Scene.AddObject(monster7);
            Monster monster8 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land22.Rect.Left + 20, land22.Rect.Top - 49 * 3, 30, 80);
            Scene.AddObject(monster8);
            Monster monster9 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land25.Rect.Left + 20, land25.Rect.Top - 49 * 3, 35, 82);
            Scene.AddObject(monster9);
            Monster monster10 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land26.Rect.Left + 20, Scene.Ground - 49 * 3, 40, 84);
            Scene.AddObject(monster10);
            Monster monster11 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land28.Rect.Left + 20, land28.Rect.Top - 49 * 3, 50, 86);
            Scene.AddObject(monster11);
            Monster monster12 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land29.Rect.Left, Scene.Ground - 49 * 3, 55, 88);
            Scene.AddObject(monster12);
            Monster monster13 = new Monster(Scene, Monster.MonsterState.Idle, 49 * 3, land30.Rect.Left + 20, land20.Rect.Top - 49 * 3, 60, 90);
            Scene.AddObject(monster13);

        }

        /// <summary>
        /// this method changes the combat state in the game when need for every timer tick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void _runTimer_Tick(object sender, object e)
        {
            switch (_combatState)
            {
                case CombatState.StartingCombat:
                    Debug.WriteLine("Starting Combat");
                    _combatState = CombatState.InCombat;
                    _gameScene.EnterCombat(_combatant1, _combatant2);
                    break;
                case CombatState.EnterFinishingCombat:
                    Debug.WriteLine("Finishing Combat");
                    _combatState = CombatState.FinishingCombat;
                    _gameScene.FinishingCombat();
                    break;
                case CombatState.ExitingCombat:
                    Debug.WriteLine("ExitingCombat");
                    _combatState = CombatState.OutOfCombat;
                    _gameScene.ExitCombat();
                    break;
                default:
                    break;
            }
            base._runTimer_Tick(sender, e);
        }

        /// <summary>
        /// at the end of the game, this action will be called and clear the events connections.
        /// </summary>
        public override void UnsubscribeFunctions()
        {
            base.UnsubscribeFunctions();
            AppGameEvents.ClearEvent();
        }

        /// <summary>
        /// enters the game to combat mode and declaring on the combatants characters. 
        /// </summary>
        /// <param name="combatant1"> the first combatant</param>
        /// <param name="combatant2"> the second combatant</param>
        private void OnEnterCombat(GameObject combatant1, GameObject combatant2)
        {
            if (_combatState != CombatState.OutOfCombat) return;

            Debug.WriteLine("OnEnterCombat Called");
            _combatant1 = combatant1;
            _combatant2 = combatant2;
            _combatState = CombatState.StartingCombat;
        }

        /// <summary>
        /// enters the game to finishing combat mode. 
        /// </summary>
        private void OnFinishingCombat()
        {
            if (_combatState != CombatState.InCombat) return;

            Debug.WriteLine("OnFinishingCombat Called");
            _combatState = CombatState.EnterFinishingCombat;
        }

        /// <summary>
        /// enters the game to exit combat mode. 
        /// </summary>
        private void OnExitCombat()
        {
            if (_combatState != CombatState.FinishingCombat) return;

            Debug.WriteLine("OnExitCombat Called");
            _combatState = CombatState.ExitingCombat;
        }

        /// <summary>
        /// a hero declared that he have return to its place so manger inform
        /// the Scene about this. 
        /// </summary>
        private void OnFinishCombatInPlace()
        {
            if (_combatState != CombatState.FinishingCombat) return;

            Debug.WriteLine("OnFinishCombatInPlace Called");
            _gameScene.FinishCombatInPlace();
        }
    }
}
