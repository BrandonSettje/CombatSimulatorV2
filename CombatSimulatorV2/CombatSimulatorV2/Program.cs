using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSimulatorV2
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Console.Title = "Al Gore vs Global Warming";
            Game game = new Game();
            game.PlayGame();
        }
    }
    /// <summary>
    /// The actor base that other classes inherit from
    /// </summary>
    public class Actor
    {
        public Random rng = new Random(DateTime.Now.Millisecond);
        public string Name { get; set; }
        public int HP { get; set; }
        public bool IsAlive
        {
            get
            {
                if (this.HP > 0)
                    return true;
                else
                    return false;

            }
        }
        public Actor(string name, int hp) 
        {
            this.Name = name;
            this.HP = hp;
        }

        public virtual void DoAttack(Actor Actor)
        {
         
        }
    }
    /// <summary>
    /// Enemy, taking properties from actor
    /// </summary>
    public class Enemy : Actor
    {
        public Enemy(string name, int hp) : base(name, hp)
        {
           
        }
        public override void DoAttack(Actor Player) 
        {
            int enemyDamage = 0;
            if (rng.NextDouble() >= .2)
           {
               enemyDamage = rng.Next(5, 16);
               Player.HP -= enemyDamage;
               Console.WriteLine("The Super Storm struck {0} with a lightning bolt!", Player.Name);
               Console.WriteLine("{0} been hit for {1} damage!", Player.Name, enemyDamage);
               
            }
            else
            {
                Console.Clear();
                Console.WriteLine("The lightning bolt missed you, and struck a tree! Al Gore sheds a single tear.");
                Console.WriteLine();
            }
        }
    }
    /// <summary>
    /// The object representing the Player
    /// </summary>
    public class Player : Actor
    {
        enum AttackType
        {
            icebergThrow = 1,
            spriteIgniteAttack,
            squirrelSacrifice,
            loseTurn
        }
        public Player(string name, int hp) : base(name, hp)
        {
        
        }
        public override void DoAttack(Actor Enemy)
        {
            int playerDamage = 0;
            int playerHeal = 0;
            switch (ChooseAttack())
            {
                case AttackType.icebergThrow:
                    if (rng.NextDouble() >= .3)
                    {
                        playerDamage = rng.Next(20, 36);
                        Enemy.HP -= playerDamage;
                        Console.Clear();
                        Console.WriteLine("{0} throws an Iceberg and hits {1} for {2} damage!", Name, Enemy.Name, playerDamage);
                        
                    }
                    break;
                case AttackType.spriteIgniteAttack:
                    
                        playerDamage = rng.Next(10, 16);
                        Enemy.HP -= playerDamage;
                        Console.Clear();
                        
                        break;
                    
                case AttackType.squirrelSacrifice:
                    playerHeal = rng.Next(10, 21);
                    HP += playerHeal;
                    Console.Clear();
                    break;
                case AttackType.loseTurn:
                    Console.WriteLine(@"{0} tried to perform a different action than the one {0} knows! {0} failed 
miserably!", Name);
                    break;
            }
        }
        private void PlayerChar()
        {
            
        }
        
        private AttackType ChooseAttack()
        {
            
            Console.SetCursorPosition(5, 10);
            PlayerChar();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Option 1: Throw an Iceberg at Kali");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"Option 2: Open a window into space above the storm. 
          Igniting a Sprite from the storm, it drains off some life into space!");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Option 3: A squirrel will sacrifice itself to heal you!");
            Console.WriteLine();
            Console.ResetColor();
            Console.Write("Choose 1, 2, or 3: ");
            switch(Console.ReadLine())
            {
                case "1": return AttackType.icebergThrow;
                case "2": return AttackType.spriteIgniteAttack;
                case "3": return AttackType.squirrelSacrifice;
                default:  return AttackType.loseTurn;
            }
            
        }

    }
    /// <summary>
    /// The game class, includes all the work to be done.
    /// </summary>
    class Game
    {
        public Enemy Enemy { get; set; }
        public Player Player { get; set; }


        public Game()
        {
            this.Enemy = new Enemy("Super Storm Kali", 200);
            this.Player = new Player("Al Gore", 100);
        }
        public void DisplayCombatInfo()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"{0} has {1} HP              {2} has {3} HP", Enemy.Name, Enemy.HP, Player.Name, Player.HP);
            Console.WriteLine();
            Console.ResetColor();
        }
        public void PlayGame()
        {
            Intro();
            char input = ' ';

                while (this.Player.IsAlive && this.Enemy.IsAlive)
                {
                    DisplayCombatInfo();
                    Console.WriteLine();
                    this.Player.DoAttack(this.Enemy);
                    this.Enemy.DoAttack(this.Player);
                    System.Threading.Thread.Sleep(4000);
                    Console.Clear();
                }
                if (this.Player.IsAlive)
                {
                    Console.WriteLine(@"{0} has beaten {1}! He knows another will form soon. He must go 
back to his Super Eco House and restore his energy!", Player.Name, Enemy.Name);
                }
                else if (this.Enemy.IsAlive)
                {
                    Console.WriteLine("{0} has beaten {1}! Global Warming will triumph at last!", Enemy.Name, Player.Name);
                    Console.Beep(300, 600);
                    Console.Beep(240, 500);
                    Console.Beep(120, 1500);
                   
                }
                System.Threading.Thread.Sleep(2000);
                Console.Write("Would you like to battle some more Global Warming? Y to play again N to exit.");
                input = char.ToLower(Console.ReadKey().KeyChar);
                if (input != 'n')
                {
                    Console.Clear();
                    Game game = new Game();
                    game.PlayGame();
                }
        }
        private void Intro()
        {
            
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine();
            Console.WriteLine("              Al Gore Vs Global Warming!");
            Console.WriteLine("         Press any key to begin the fight!");
            Console.ReadKey();
            Console.Clear();
        }

    }
}
