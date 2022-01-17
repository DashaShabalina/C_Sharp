using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldofWorms.Data;

namespace WorldOfWorms
{

    public interface IBehaviourService
    {
        public void GenerateBehavior(EnvironmentContext context, string name, IFoodGenerator foodGenerator);
        public IEnumerable<BehaviorInfo> GetBehavior(EnvironmentContext context, string name);
    }

    public class BehaviourService : IBehaviourService
    {
        public EnvironmentContext _context;
        public IFoodGenerator _foodGenerator;
        public List<BehaviorInfo> steps = new List<BehaviorInfo>();
        public Behavior behavior;
        public BehaviourService(EnvironmentContext context, IFoodGenerator foodGenerator)
        {
            _context = context;
            _foodGenerator = foodGenerator;
        }

        public void GenerateBehavior(EnvironmentContext context, string name, IFoodGenerator foodGenerator)
        {
            int maxId = 0;
            try { maxId = context.Behaviours.Max(p => p.Id); }
            catch { maxId = 0; }

            behavior = new Behavior() { Name = name, Id = maxId + 1 };

            /*Console.WriteLine("---------------------------------");
            Console.WriteLine(behavior.Id);
            Console.WriteLine("---------------------------------");*/
            BehaviorInfo behaviourInfo = new BehaviorInfo();
            for (int i = 0; i < 10; i++)
            {
                bool flag = false;
                Food curFood = foodGenerator.GetFood();
                //Console.WriteLine(curFood.X + " " + curFood.Y);
                if (i > 0)
                {

                    while (!flag)
                    {

                        if (i > 9)
                        {
                            int temp = 0;
                            for (int j = i - 9; j < i; j++)
                            {
                                if (steps[j].X == curFood.X && steps[j].Y == curFood.Y)
                                {
                                    curFood = foodGenerator.GetFood();
                                    break;
                                }

                                else
                                {
                                    temp++;
                                }
                            }
                            if (temp == steps.Count)
                            {
                                flag = true;
                                //Console.WriteLine(2);
                            }
                        }
                        else
                        {
                            int temp = 0;
                            foreach (var j in steps)
                            {
                                if (j.X == curFood.X && j.Y == curFood.Y)
                                {
                                    curFood = foodGenerator.GetFood();
                                    break;
                                    //Console.WriteLine(1);
                                }

                                else
                                {
                                    temp++;
                                }
                            }
                            if (temp == steps.Count)
                            {
                                flag = true;
                                //Console.WriteLine(2);
                            }
                        }
                    }
                }
                behaviourInfo = new BehaviorInfo() { X = curFood.X, Y = curFood.Y, Order = i, BehaviorId = behavior.Id, Behavior = behavior };
                steps.Add(behaviourInfo);
                context.BehaviourSteps.Add(behaviourInfo);
                /*Console.WriteLine(Convert.ToString(behaviourInfo.X) + " " + behaviourInfo.Y + " " + behaviourInfo.Order + " " + behaviourInfo.BehaviorId);*/
            }
            behavior.Steps = steps;
            context.Behaviours.Add(behavior);
            /*Console.ReadLine();*/
            context.SaveChanges();
        }

        public IEnumerable<BehaviorInfo> GetBehavior(EnvironmentContext context, string name)
        {
            var behaviourInfo = context.BehaviourSteps.Include(u => u.Behavior).ToList();
            var behaviour = from i in behaviourInfo
                            where i.Behavior.Name == name
                            select i;
            return behaviour;
            /* foreach (var j in behaviour)
             {
                 Console.WriteLine(j.X + " " + j.Y + " " + j.Order);
             }*/
        }

    }
}
