using System;

int heroLife = 10;
int monsterLive = 10;

Random rand = new Random();
int attacks = rand.Next(1, 11);

bool gamePlayed = true;

do
{
    //hero attack
    monsterLive -= attacks = rand.Next(1, 11);
    Console.WriteLine($"Monster was damaged and lost {attacks} health and now has {monsterLive} health.");
    if (monsterLive <= 0)
    {
        Console.WriteLine("Hero wins!");
        gamePlayed = false;
        break;
    }
    //monster attack
    heroLife -= attacks = rand.Next(1, 11);
    Console.WriteLine($"Hero  was damaged and lost {attacks} health and now has {heroLife} health.");
    if (heroLife <= 0)
    {
        Console.WriteLine("Monster wins!");
        gamePlayed = false;
    }
}
while (gamePlayed == true);
