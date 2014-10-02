
namespace SlavojMVC4_1.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Web.Mvc;
    using Foolproof;

    public class EditableClen //: IValidatableObject
    {
        //..............................................................................................
        [ScaffoldColumn(false)]//nebude nikde zobrazen
        public int ClenId { get; set; }

        [Required]
        [Display(Name = "Jméno")]
        public string Jmeno { get; set; }

        [Required]
        [Display(Name = "Přijmení")]
        public string Prijmeni { get; set; }

        [Display(Name = "Titul před")]
        public string TitulPred { get; set; }

        [Display(Name = "Titul za")]
        public string TitulZa { get; set; }

        [DefaultValue(true)]
        [Display(Name = "Je člen")]
        public bool JeClen { get; set; }

        [Display(Name = "Rodné číslo")]
        public string RodneCislo { get; set; }

        [Display(Name = "Datum narození")]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}")]
        public Nullable<System.DateTime> DatumNarozeni { get; set; }

        public Nullable<int> PohlaviId { get; set; }
        
        public Nullable<int> Vek { get; set; }

        //..............................................................................................
        //Adresa
        //[Required]
        [Display(Name = "Ulice")]
        //[AdresaRequiredAttibute(PoleFieldName = "AdresaUlice",  JeClenFieldName = "JeClen", AdresaUliceFieldName = "AdresaUlice", AdresaCisloPopisneFieldName = "AdresaCisloPopisne", AdresaObecFieldName = "AdresaObec", AdresaPscFieldName = "AdresaPsc", ErrorMessage = "Ulice musí být vyplněna.")]
        [RequiredIf("JeClen", true)]
        public string AdresaUlice { get; set; }

        //[Required]
        [Display(Name = "Číslo popisné")]
        //[AdresaRequiredAttibute(PoleFieldName = "AdresaCisloPopisne", JeClenFieldName = "JeClen", AdresaUliceFieldName = "AdresaUlice", AdresaCisloPopisneFieldName = "AdresaCisloPopisne", AdresaObecFieldName = "AdresaObec", AdresaPscFieldName = "AdresaPsc", ErrorMessage = "Číslo popisné musí být vyplněno.")]
        public int? AdresaCisloPopisne { get; set; }

        //[Required]
        [Display(Name = "Obec")]
        //[AdresaRequiredAttibute(PoleFieldName = "AdresaObec", JeClenFieldName = "JeClen", AdresaUliceFieldName = "AdresaUlice", AdresaCisloPopisneFieldName = "AdresaCisloPopisne", AdresaObecFieldName = "AdresaObec", AdresaPscFieldName = "AdresaPsc", ErrorMessage = "Obec musí být vyplněna.")]
        [RequiredIfNotEmpty("AdresaUlice")]
        public string AdresaObec { get; set; }

        //[Required]
        [Display(Name = "PSČ")]
        //[AdresaRequiredAttibute(PoleFieldName = "AdresaPsc", JeClenFieldName = "JeClen", AdresaUliceFieldName = "AdresaUlice", AdresaCisloPopisneFieldName = "AdresaCisloPopisne", AdresaObecFieldName = "AdresaObec", AdresaPscFieldName = "AdresaPsc", ErrorMessage = "PSČ musí být vyplněno.")]
        public int? AdresaPsc { get; set; }

        //..............................................................................................
        //Kontakt
        [Display(Name = "Telefon")]
        public string KontaktTelefon { get; set; }

        [Display(Name = "E-mail")]
        public string KontaktMail { get; set; }

        [Display(Name = "WWW")]
        public string KontaktWWW { get; set; }
        //..............................................................................................
        //Registrace
        [Display(Name = "Registrace: Číslo registrace")]
        public int? RegistraceCisloRegistrace { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Registrace: Platná do")]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? RegistracePlatnaDo { get; set; }
        //..............................................................................................
        //Rozhodčí
        [Display(Name = "Rozhodčí: Číslo registrace")]
        public string RozhodciCisloRegistrace { get; set; }

        [Display(Name = "Rozhodčí: Třída")]
        public string RozhodciTrida { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Rozhodčí: Platná do")]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? RozhodciPlatnaDo { get; set; }
        //..............................................................................................
        //Trenér
        [Display(Name = "Trenér: Číslo registrace")]
        public string TrenerCisloRegistrace { get; set; }

        [Display(Name = "Trenér: Třída")]
        public string TrenerTrida { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Trenér: Platná do")]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? TrenerPlatnaDo { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (IsAdresa(this))
        //    {
        //        if (AdresaUlice == null)
        //        {
        //            yield return new ValidationResult("Ulice musí být vyplněna", new[] { "AdresaUlice" });
        //        }
        //        if (AdresaCisloPopisne == null)
        //        {
        //            yield return new ValidationResult("Číslo popisné musí být vyplněno.", new[] { "AdresaCisloPopisne" });
        //        }
        //        if (AdresaObec == null)
        //        {
        //            yield return new ValidationResult("Obec musí být vyplněna.", new[] { "AdresaObec" });
        //        }
        //        if (AdresaPsc == null)
        //        {
        //            yield return new ValidationResult("Psč musí být vyplněno.", new[] { "AdresaPsc" });
        //        }

        //    }
        //}
        public bool IsAdresa(EditableClen clen)
        {
            return ((JeClen) || (AdresaUlice != null) || (AdresaCisloPopisne != null) || (AdresaObec != null) || (AdresaPsc != null));

        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class AdresaRequiredAttibute : ValidationAttribute, IClientValidatable
    {
        int? MyNullableInt = null;
        public string PoleFieldName { get; set; }
        public string JeClenFieldName { get; set; }
        public string AdresaUliceFieldName { get; set; }
        public string AdresaCisloPopisneFieldName { get; set; }
        public string AdresaObecFieldName { get; set; }
        public string AdresaPscFieldName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var jeClenDataValue = validationContext.ObjectType.GetProperty(JeClenFieldName).GetValue(validationContext.ObjectInstance, null);
            bool jeClen = jeClenDataValue != null;

            var adresaUliceDataValue = validationContext.ObjectType.GetProperty(AdresaUliceFieldName).GetValue(validationContext.ObjectInstance, null);
            string adresaUlice = adresaUliceDataValue != null ? (string)adresaUliceDataValue : null;

            var adresaCisloPopisneDataValue = validationContext.ObjectType.GetProperty(AdresaCisloPopisneFieldName).GetValue(validationContext.ObjectInstance, null);
            int? adresaCisloPopisne = adresaCisloPopisneDataValue != null ? (int)adresaCisloPopisneDataValue : MyNullableInt;
            
            var adresaObecDataValue = validationContext.ObjectType.GetProperty(AdresaObecFieldName).GetValue(validationContext.ObjectInstance, null);
            string adresaObec = adresaObecDataValue != null ? (string)adresaObecDataValue : null;

            var adresaPscDataValue = validationContext.ObjectType.GetProperty(AdresaPscFieldName).GetValue(validationContext.ObjectInstance, null);
            int? adresaPsc = adresaPscDataValue != null ? (int)adresaPscDataValue : MyNullableInt;


            if ((jeClen) || (adresaUlice != null) || (adresaCisloPopisne != null) || (adresaObec != null) || (adresaPsc != null))
            {
                //if ((adresaUlice == null))
                //{
                //    return new ValidationResult(ErrorMessage);
                //}

                if ((adresaCisloPopisne == null))
                {
                    return new ValidationResult(ErrorMessage);
                }

                if ((adresaObec == null))
                {
                    return new ValidationResult(ErrorMessage);
                }

                if ((adresaPsc == null))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;

        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessage,
                ValidationType = PoleFieldName.ToLower()+"a"
            };

            rule.ValidationParameters[PoleFieldName.ToLower() + "b"] = PoleFieldName;

            yield return rule;
        }
    }
}