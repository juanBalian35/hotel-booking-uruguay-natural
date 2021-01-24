namespace Domain.Validations
{
    public class MaxNumberValidation : NumberValidation
    {
        public override string GetError() => "Maximum is " + Maximum;

        private readonly int Maximum;

        public MaxNumberValidation(int maximum)
        {
            Maximum = maximum;
        }

        public override bool Validate(object value)
        {
            return AsNumber(value) <= Maximum;
        }
    }
}
