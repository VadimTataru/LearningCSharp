namespace Animals
{
    public abstract class Animal
    {
        protected int Age {
            get
            {
                return age;
            }
            set
            {
                
                if (value < 0)
                {
                    throw new AgeException("Возраст не может быть отрицательным!");
                }
                else
                    age = value;               
            } 
        }

        protected TypeOfFood Type { get; set; }

        protected int age;

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
            return $"Age: {age}, Type: {nameof(Type)}";
        }
    }
}
