namespace Domain.Validations
{
    public class MinNumberValidation : NumberValidation
    {
        public override string GetError() => "Minimum is " + Minimum;

        private readonly int Minimum;

        public MinNumberValidation(int minimum)
        {
            Minimum = minimum;
        }

        public override bool Validate(object value)
        {
            return AsNumber(value) >= Minimum;
        }
    }
}
