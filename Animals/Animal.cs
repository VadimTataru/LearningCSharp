namespace Animals
{
    public abstract class Animal
    {
        protected int Age { get; set; }
        protected TypeOfFood Type { get; set; }

        protected Animal(int age, TypeOfFood type = TypeOfFood.Else)
        {
            this.Age = age;
            this.Type = type;
        }

        public abstract string About();

        public abstract void Move();

        public abstract void Eat();

        public override string ToString()
        {
            string type;
            if (Type == TypeOfFood.Herb)
                type = "Herbivore";
            else if (Type == TypeOfFood.Predator)
                type = "Predator";
            else
                type = "Else";

            return $"Age: {Age}, Type: {type}";
        }
    }
}
