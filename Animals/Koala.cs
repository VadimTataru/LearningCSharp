
namespace Animals
{
    public class Koala: Animal
    {
        private string Name { get; set; }
        public Koala(string name, int age) : base(age, TypeOfFood.Herb)
        {
            this.Name = name;
        }

        public override string About()
        {
            string comment;
            if (Age >= 13)
            {
                comment = "Умер";
            }
            else if (Age <= 10)
            {
                comment = "Похоже на правду";
            }
            else
            {
                comment = "Средний возраст продолжительности жизни =/";
            }

            return $"Name: {Name} \n Age: {Age} {comment} \n Type: Травоядное";
        }

        public override void Eat()
        {
            Console.WriteLine("Люблю листья эвкалипта");
        }

        public override void Move()
        {
            Console.WriteLine("Карабкаюсь");
        }
    }
}
