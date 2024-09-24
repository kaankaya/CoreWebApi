using System.ComponentModel.DataAnnotations;

namespace GalacticTourAgency.Attributes
{
    public class GalacticElementCompositionAttribute:ValidationAttribute
    {
        private readonly string[] _validElement = new[]
        {
            "Hidrojen","Karbon","Oksijen","Silikon","Helyum","Neon"
        };
        public int MinElements { get; }
        public int MaxElements { get; }

        public GalacticElementCompositionAttribute(int minElements = 1, int maxElements = 5)
        {
            MinElements = minElements;
            MaxElements = maxElements;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
           if(value is string composition)
            {
                var elements = composition.Split(',').Select(e => e.Trim()).ToList();
                if(elements.Count < MinElements || elements.Count > MaxElements)
                {
                    return new ValidationResult($"Bileşen içerisik sayısı {MinElements} ile {MaxElements} arasında olmalıdır");
                }

                var invalidElements = elements.Except(_validElement,StringComparer.OrdinalIgnoreCase).ToList();

                if (invalidElements.Any())
                {
                    return new ValidationResult($"Geçersiz Element Saptandı {string.Join(",",invalidElements)}.Geçerli elementler : {string.Join(",",_validElement)}");
                }
            }
            else
            {
                return new ValidationResult("Geçersiz Value.Virgüller ile ayrılmış string değer girmelisiniz");
            }
           return ValidationResult.Success;
        }
    }
}
