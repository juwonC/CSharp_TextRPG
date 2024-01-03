using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace TextRPG
{
    internal class Program
    {
        enum EJob
        {
            NONE,
            KNIGHT,
            ARCHER,
            MAGE
        }

        struct SPlayer
        {
            public int hp;
            public int attack;
        }

        enum EMonsterType
        {
            NONE,
            SLIME,
            ORC,
            SKELETON,
            WOLF,
            GOBLIN
        }

        struct SMonster
        {
            public int hp;
            public int attack;
        }

        static EJob SelectJob()
        {
            EJob job = EJob.NONE;

            Console.WriteLine();
            Console.WriteLine("직업을 선택하세요!");
            Console.WriteLine("[1] 기사");
            Console.WriteLine("[2] 궁수");
            Console.WriteLine("[3] 마법사");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    job = EJob.KNIGHT;
                    Console.WriteLine("기사 선택!");
                    break;

                case "2":
                    job = EJob.ARCHER;
                    Console.WriteLine("궁수 선택!");
                    break;

                case "3":
                    job = EJob.MAGE;
                    Console.WriteLine("마법사 선택!");
                    break;

                default:
                    Console.WriteLine("올바른 숫자를 입력하세요.");
                    break;
            }

            return job;
        }

        static void CreatePlayer(EJob job, out SPlayer player)
        {
            if(job == EJob.KNIGHT)
            {
                player.hp = 100;
                player.attack = 10;
            }
            else if(job == EJob.ARCHER)
            {
                player.hp = 75;
                player.attack = 12;
            }
            else if( job == EJob.MAGE)
            {
                player.hp = 50;
                player.attack = 15;
            }
            else
            {
                player.hp = 0;
                player.attack = 0;
            }
        }

        static void CreateRandomMonster(out SMonster monster)
        {
            Random random = new Random();
            int randMonster = random.Next(1, 4);

            switch (randMonster)
            {
                case (int)EMonsterType.SLIME:
                    Console.WriteLine("슬라임이 스폰되었습니다.");
                    monster.hp = 20;
                    monster.attack = 2;
                    break;

                case (int)EMonsterType.ORC:
                    Console.WriteLine("오크가 스폰되었습니다.");
                    monster.hp = 50;
                    monster.attack = 5;
                    break;

                case (int)EMonsterType.SKELETON:
                    Console.WriteLine("스켈레톤이 스폰되었습니다.");
                    monster.hp = 30;
                    monster.attack = 4;
                    break;

                case (int)EMonsterType.WOLF:
                    Console.WriteLine("늑대가 스폰되었습니다.");
                    monster.hp = 25;
                    monster.attack = 3;
                    break;

                case (int)EMonsterType.GOBLIN:
                    Console.WriteLine("고블린이 스폰되었습니다.");
                    monster.hp = 10;
                    monster.attack = 1;
                    break;

                default:
                    monster.hp = 0;
                    monster.attack = 0;
                    break;
            }
        }

        static void Fight(ref SPlayer player, ref SMonster monster)
        {
            while(true)
            {
                // 플레이어가 몬스터 공격
                monster.hp -= player.attack;
                if(monster.hp <= 0)
                {
                    Console.WriteLine("몬스터를 물리쳤습니다!");
                    Console.WriteLine($"남은 체력: {player.hp}");
                    break;
                }

                // 몬스터 반격
                player.hp -= monster.attack;
                if(player.hp <= 0)
                {
                    Console.WriteLine("전투에서 패배했습니다.");
                    break;
                }
            }
        }

        static void EnterField(ref SPlayer player)
        {
            while(true)
            {
                Console.WriteLine();
                Console.WriteLine("필드에 접속했습니다!");

                // 랜덤으로 1~5 몬스터 중 하나를 스폰
                SMonster monster;
                CreateRandomMonster(out monster);

                Console.WriteLine("[1] 전투 모드로 돌입");
                Console.WriteLine("[2] 일정 확률로 마을로 도망");

                string input = Console.ReadLine();
                if(input == "1")
                {
                    Fight(ref player, ref monster);
                }
                else if(input == "2")
                {
                    // 마을로 도망갈 확률 33%
                    Random random = new Random();
                    int randValue = random.Next(0, 101);
                    if(randValue <= 33)
                    {
                        Console.WriteLine("성공적으로 도망쳤습니다!");
                        break;
                    }
                }
                else
                {
                    Fight(ref player, ref monster);
                }
            }
        }
        static void EnterGame(ref SPlayer player)
        {
            while(true)
            {
                Console.WriteLine();
                Console.WriteLine("마을에 접속했습니다!");
                Console.WriteLine("[1] 필드로 간다");
                Console.WriteLine("[2] 로비로 돌아가기");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        EnterField(ref player);
                        break;

                    case "2":
                        return;
                }
            }
        }
        static void Main(string[] args)
        {
            EJob playerJob = EJob.NONE;

            while(true)
            {
                playerJob = SelectJob();

                if (playerJob == EJob.NONE)
                {
                    continue;
                }

                SPlayer player;
                CreatePlayer(playerJob, out player);
                Console.WriteLine($"HP: {player.hp}, ATTACK:{player.attack}");

                EnterGame(ref player);
            }
        }
    }
}
