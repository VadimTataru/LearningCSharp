using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public class Owl: Animal
    {
        private string Name { get; set; }
        public Owl(string name, int age) : base(age, TypeOfFood.Predator)
        {
            this.Name = name;
        }

        public override string About()
        {
            string comment;
            if (Age >= 15)
            {
                comment = "Ну это байки! Обычно они столько не живут =)";
            }
            else if (Age <= 5)
            {
                comment = "Похоже на правду";
            }
            else
            {
                comment = "Средний возраст продолжительности жизни =/";
            }

            return $"Name: {Name} \n Age: {Age} {comment} \n Type: Predator";
        }

        public override void Eat()
        {
            Console.WriteLine("Ем мышов и ещё что-нибудь");
        }

        public override void Move()
        {
            Console.WriteLine("Лечу");
        }
    }
}
