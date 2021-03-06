﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Fox : Animal
    {
        #region Static Members
        /// <summary>
        /// Time (in Days) between each 2 Multiplication Seasons
        /// </summary>
        public static int MultiplicationInterval { get; private set; }

        /// <summary>
        /// The Max Distance a Rabbit May Travel
        /// </summary>
        public static int TravellingDistance { get; private set; }

        /// <summary>
        /// Expected Age of Foxes
        /// </summary>
        public static int MedianAge { get; private set; }

        /// <summary>
        /// The number of Foxes born per couple based on Number of Foxes
        /// And the Number of Rabbits per fox in the current context
        /// </summary>
        public static Distribution<int, float, int> BirthRate { get; private set; }

        /// <summary>
        /// Deterimines the Level of Vegetation above which finding rabbits becomes Hard
        /// </summary>
        public static float PlantationCoverRabbits { get; private set; }

        /// <summary>
        /// Ratio of Foxes that travel to Neighbouring Cells
        /// </summary>
        public static float RateOfTravel { get; private set; }

        /// <summary>
        /// porpabilty of Rabbit being Eaten when the vegetation cover is low
        /// </summary>
        public static float MinFoodProp { get; private set; }

        /// <summary>
        /// porpabilty of Rabbit being Eaten when the vegetation cover is High
        /// </summary>
        public static float MaxFoodProp { get; private set; }

        /// <summary>
        /// Number of Rabbits of which The Fox can't eat anymore when there's enough Cover
        /// </summary>
        public static int MaxFoodAllowedWeekly { get; private set; }

        /// <summary>
        /// Number of Rabbits of which The Fox can't eat anymore when there's NOT enough Cover
        /// </summary>
        public static int MinFoodAllowedWeekly { get; private set; }

        /// <summary>
        /// The Food Required for a fox weekly so it does NOT become hungry
        /// </summary>
        public static int RequiredWeekly { get; private set; }

        /// <summary>
        /// The Probabilty of Death by Hunger
        /// </summary>
        public static float HungerDeathPropabilty { get; private set; }

        /// <summary>
        /// Initialize The static Members of Class Fox
        /// </summary>
        /// <param name="MultiInterval">Time (in Days) between each 2 Multiplication Seasons</param>
        /// <param name="Median">Expected Age of Foxes</param>
        /// <param name="BRate">The number of Foxes born per couple based on Number of Foxes
        /// And the Number of Rabbits per fox in the current context</param>
        /// <param name="plantsCoverRabbits"> Deterimines the Level of Vegetation above which finding rabbits becomes Hard</param>
        /// <param name="minpropfood">porpabilty of Rabbit being Eaten when the vegetation cover is high</param>
        /// <param name="maxpropfood">porpabilty of Rabbit being Eaten when the vegetation cover is low</param>
        /// <param name="minAllowed">Number of Rabbits of which The Fox can't eat anymore when there's NOT enough Cover</param>
        /// <param name="maxAllowed">Number of Rabbits of which The Fox can't eat anymore when there's enough Cover</param>
        /// <param name="required">The Food Required for a fox weekly so it does NOT become hungry</param>
        /// <param name="trRate"> Ratio Of Foxes which travel to neighbouring Cells</param>
        /// <param name="trDist">the Distance a Rabbit Can travel</param>
        /// <param name="famineDeathProp">The Probabilty of Death by Hunger</param>
        public static void init(int MultiInterval, int Median, Distribution<int, float, int> BRate, float plantsCoverRabbits, float minpropfood, float maxpropfood, int minAllowed, int maxAllowed, int required, float famineDeathProp, float trRate, int trDist)
        {
            TravellingDistance = trDist;
            MultiplicationInterval = MultiInterval;
            MedianAge = Median;
            BirthRate = BRate;
            PlantationCoverRabbits = plantsCoverRabbits;
            minAllowed = MinFoodAllowedWeekly = minAllowed;
            maxAllowed = MaxFoodAllowedWeekly = maxAllowed;
            MinFoodProp = minpropfood;
            MaxFoodProp = maxpropfood;
            RequiredWeekly = required;
            HungerDeathPropabilty = famineDeathProp;
            RateOfTravel = trRate;
        }

        #endregion

        /// <summary>
        /// if Hungery the Fox would be more vulneraible to Death
        /// </summary>
        public bool Hungry => RequiredWeekly > EatenThisWeek;

        /// <summary>
        /// Number of Rabbits The Fox have Eaten each day in the Past Week
        /// </summary>
        public int[] EatenEachDay { get; private set; }

        /// <summary>
        /// Sum of all the Rabbits that the fox have eaten the past week
        /// </summary>
        public int EatenThisWeek { get; private set; }

        /// <summary>
        /// Simulates the satisfaction by eating <paramref name="num"/> Rabbits at the <paramref name="Day"/>
        /// </summary>
        /// <param name="num">number of rabbits to eat</param>
        /// <param name="Day">date on which the rabbits are being eaten</param>
        public void Eat(int num, int Day)
        {
            int x = EatenEachDay[Day % 7];
            EatenEachDay[Day % 7] = num;
            EatenThisWeek += num - x;
        }

        public Fox() : base()
        {
            EatenEachDay = new int[7];
            EatenThisWeek = 0;
        }
        public override object Clone()
        {
            return new Fox(this);
        }

        public Fox(Fox c) : base(c)
        {
            EatenThisWeek = c.EatenThisWeek;
            for (int i = 0; i < 7; i++)
                EatenEachDay[i] = c.EatenEachDay[i];
        }
    }
}
