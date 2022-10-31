
namespace Animals
{
    public class Worm : Animal
    {
        private string Name { get; set; }
        public Worm(string name, int age) : base(age, TypeOfFood.Else)
        {
            this.Name = name;
        }

        public override string About()
        {
            string comment;
            if(Age >= 10)
            {
                comment = "Это волшебный червь?! Обычно они столько не живут =)";
            } else if(Age <= 5)
            {
                comment = "Похоже на правду";
            } else
            {
                comment = "Червячок - старичок";
            }

            return $"Name: {Name} \n Age: {Age} {comment} \n Type: Else";
        }

        public override void Eat()
        {
            Console.WriteLine("Кушою и копаю");
        }

        public override void Move()
        {
            Console.WriteLine("Ползу");
        }
    }
}
