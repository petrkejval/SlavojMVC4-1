
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
        [RequiredIf("JeClen", true, ErrorMessage = "Pokud Je člen zaškrtnuto, Rodné číslo musí být vyplněno.")]
        public string RodneCislo { get; set; }

        [Display(Name = "Datum narození")]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}")]
        public Nullable<System.DateTime> DatumNarozeni { get; set; }

        public Nullable<int> PohlaviId { get; set; }
        
        public Nullable<int> Vek { get; set; }

        //..............................................................................................
        //Adresa
        [Display(Name = "Ulice")]
        //[AdresaRequiredAttibute(PoleFieldName = "AdresaUlice",  JeClenFieldName = "JeClen", AdresaUliceFieldName = "AdresaUlice", AdresaCisloPopisneFieldName = "AdresaCisloPopisne", AdresaObecFieldName = "AdresaObec", AdresaPscFieldName = "AdresaPsc", ErrorMessage = "Ulice musí být vyplněna.")]
        [RequiredIf("JeClen", true, ErrorMessage = "Pokud Je člen zaškrtnuto, Ulice musí být vyplněna.")]
        [RequiredIfNotEmpty("AdresaCisloPopisne", ErrorMessage = "Pokud je vyplněno Číslo popisné, Ulice musí být vyplněna.")]
        [RequiredIfNotEmptya("AdresaObec", ErrorMessage = "Pokud je vyplněna Obec, Ulice musí být vyplněna.")]
        [RequiredIfNotEmptyb("AdresaPsc", ErrorMessage = "Pokud je vyplněno Psč, Ulice musí být vyplněna.")]
        public string AdresaUlice { get; set; }

        [Display(Name = "Číslo popisné")]
        //[AdresaRequiredAttibute(PoleFieldName = "AdresaCisloPopisne", JeClenFieldName = "JeClen", AdresaUliceFieldName = "AdresaUlice", AdresaCisloPopisneFieldName = "AdresaCisloPopisne", AdresaObecFieldName = "AdresaObec", AdresaPscFieldName = "AdresaPsc", ErrorMessage = "Číslo popisné musí být vyplněno.")]
        [RequiredIf("JeClen", true, ErrorMessage = "Pokud Je člen zaškrtnuto, Číslo popisné musí být vyplněno.")]
        [RequiredIfNotEmpty("AdresaUlice", ErrorMessage = "Pokud je vyplněna Ulice, Číslo popisné musí být vyplněno.")]
        [RequiredIfNotEmptya("AdresaObec", ErrorMessage = "Pokud je vyplněna Obec, Číslo popisné musí být vyplněno.")]
        [RequiredIfNotEmptyb("AdresaPsc", ErrorMessage = "Pokud je vyplněno Psč, Číslo popisné musí být vyplněno.")]
        public int? AdresaCisloPopisne { get; set; }

        [Display(Name = "Obec")]
        //[AdresaRequiredAttibute(PoleFieldName = "AdresaObec", JeClenFieldName = "JeClen", AdresaUliceFieldName = "AdresaUlice", AdresaCisloPopisneFieldName = "AdresaCisloPopisne", AdresaObecFieldName = "AdresaObec", AdresaPscFieldName = "AdresaPsc", ErrorMessage = "Obec musí být vyplněna.")]
        [RequiredIf("JeClen", true, ErrorMessage = "Pokud Je člen zaškrtnuto, Obec musí být vyplněna.")]
        [RequiredIfNotEmpty("AdresaUlice", ErrorMessage = "Pokud je vyplněna Ulice, Obec musí být vyplněna.")]
        [RequiredIfNotEmptya("AdresaCisloPopisne", ErrorMessage = "Pokud je vyplněno Číslo popisné, Obec musí být vyplněna.")]
        [RequiredIfNotEmptyb("AdresaPsc", ErrorMessage = "Pokud je vyplněno Psč, Obec musí být vyplněno.")]
        public string AdresaObec { get; set; }

        [Display(Name = "PSČ")]
        //[AdresaRequiredAttibute(PoleFieldName = "AdresaPsc", JeClenFieldName = "JeClen", AdresaUliceFieldName = "AdresaUlice", AdresaCisloPopisneFieldName = "AdresaCisloPopisne", AdresaObecFieldName = "AdresaObec", AdresaPscFieldName = "AdresaPsc", ErrorMessage = "PSČ musí být vyplněno.")]
        [RequiredIf("JeClen", true, ErrorMessage = "Pokud Je člen zaškrtnuto, Psč musí být vyplněno.")]
        [RequiredIfNotEmpty("AdresaUlice", ErrorMessage = "Pokud je vyplněna Ulice, Psč musí být vyplněno.")]
        [RequiredIfNotEmptya("AdresaCisloPopisne", ErrorMessage = "Pokud je vyplněno Číslo popisné, Psč musí být vyplněno.")]
        [RequiredIfNotEmptyb("AdresaObec", ErrorMessage = "Pokud je vyplněna Obec, Psč musí být vyplněno.")]
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
        [RequiredIfNotEmpty("RegistracePlatnaDo", ErrorMessage = "Pokud je vyplněno Platná do, Číslo registrace musí být vyplněno.")]
        public int? RegistraceCisloRegistrace { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Registrace: Platná do")]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}", ApplyFormatInEditMode = true)]
        [RequiredIfNotEmpty("RegistraceCisloRegistrace", ErrorMessage = "Pokud je vyplněno Číslo registraceo, Platná do musí být vyplněno.")]
        public System.DateTime? RegistracePlatnaDo { get; set; }
        //..............................................................................................
        //Rozhodčí
        [Display(Name = "Rozhodčí: Číslo registrace")]
        [RequiredIfNotEmpty("RozhodciPlatnaDo", ErrorMessage = "Pokud je vyplněno Platná do, Číslo registrace musí být vyplněno.")]
        [RequiredIfNotEmptya("RozhodciTrida", ErrorMessage = "Pokud je vyplněna Třída, Číslo registrace musí být vyplněno.")]
        public string RozhodciCisloRegistrace { get; set; }

        [Display(Name = "Rozhodčí: Třída")]
        //[RequiredIfNotEmpty1("RozhodciCisloRegistrace", ErrorMessage = "Pokud je vyplněno Číslo registrace, Třída musí být vyplněna.")]
        //[RequiredIfNotEmpty1("RozhodciPlatnaDo", ErrorMessage = "Pokud je vyplněno Platná do, Třída musí být vyplněna.")]
        public string RozhodciTrida { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Rozhodčí: Platná do")]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}", ApplyFormatInEditMode = true)]
        [RequiredIfNotEmpty("RozhodciCisloRegistrace", ErrorMessage = "Pokud je vyplněno Číslo registrace, Platná do musí být vyplněna.")]
        [RequiredIfNotEmptya("RozhodciTrida", ErrorMessage = "Pokud je vyplněna Třída, Platná do musí být vyplněna.")]
        public System.DateTime? RozhodciPlatnaDo { get; set; }
        //..............................................................................................
        //Trenér
        [Display(Name = "Trenér: Číslo registrace")]
        [RequiredIfNotEmpty("TrenerPlatnaDo", ErrorMessage = "Pokud je vyplněno Platná do, Číslo registrace musí být vyplněno.")]
        [RequiredIfNotEmptya("TrenerTrida", ErrorMessage = "Pokud je vyplněna Třída, Číslo registrace musí být vyplněno.")]
        public string TrenerCisloRegistrace { get; set; }

        [Display(Name = "Trenér: Třída")]
        [RequiredIfNotEmpty("TrenerCisloRegistrace", ErrorMessage = "Pokud je vyplněno Číslo registrace, Třída musí být vyplněna.")]
        [RequiredIfNotEmptya("TrenerPlatnaDo", ErrorMessage = "Pokud je vyplněno Platná do, Třída musí být vyplněna.")]
        public string TrenerTrida { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Trenér: Platná do")]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}", ApplyFormatInEditMode = true)]
        [RequiredIfNotEmpty("TrenerCisloRegistrace", ErrorMessage = "Pokud je vyplněno Číslo registrace, Platná do musí být vyplněna.")]
        [RequiredIfNotEmptya("TrenerTrida", ErrorMessage = "Pokud je vyplněna Třída, Platná do musí být vyplněna.")]
        public System.DateTime? TrenerPlatnaDo { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (IsClen())
        //    {
        //        if (RodneCislo == null)
        //        {
        //            yield return new ValidationResult("Pokud je člen, Rodné číslo musí být vyplněno.", new[] { "RodneCislo" });
        //        }
        //    }

        //    if (IsAdresa())
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

        //    if (IsRegistrace())
        //    {
        //        if (RegistraceCisloRegistrace == null)
        //        {
        //            yield return new ValidationResult("Číslo registracve musí být vyplněno.", new[] { "RegistraceCisloRegistrace" });
        //        }
        //        if (RegistracePlatnaDo == null)
        //        {
        //            yield return new ValidationResult("Datum platnosti registrace musí být vyplněno.", new[] { "RegistracePlatnaDo" });
        //        }

        //    }

        //    if (IsRozhodci())
        //    {
        //        if (RozhodciCisloRegistrace == null)
        //        {
        //            yield return new ValidationResult("Číslo registracve musí být vyplněno.", new[] { "RozhodciCisloRegistrace" });
        //        }
        //        if (RozhodciPlatnaDo == null)
        //        {
        //            yield return new ValidationResult("Datum platnosti registrace musí být vyplněno.", new[] { "RozhodciPlatnaDo" });
        //        }

        //    }
        //    if (IsTrener())
        //    {
        //        if (TrenerCisloRegistrace == null)
        //        {
        //            yield return new ValidationResult("Číslo registracve musí být vyplněno.", new[] { "TrenerCisloRegistrace" });
        //        }
        //        if (TrenerPlatnaDo == null)
        //        {
        //            yield return new ValidationResult("Datum platnosti registrace musí být vyplněno.", new[] { "TrenerPlatnaDo" });
        //        }

        //    }
        //}


        public bool IsClen()
        {
            return ((JeClen) || (RodneCislo != null));

        }

        public bool IsAdresa()
        {
            return ((JeClen) || (AdresaUlice != null) || (AdresaCisloPopisne != null) || (AdresaObec != null) || (AdresaPsc != null));

        }

        public bool IsKontakt()
        {
            return ((KontaktMail != null) || (KontaktTelefon != null) || (KontaktWWW != null));

        }

        public bool IsRegistrace()
        {
            return ((RegistraceCisloRegistrace != null) || (RegistracePlatnaDo != null));

        }

        public bool IsRozhodci()
        {
            return ((RozhodciCisloRegistrace != null) || (RozhodciPlatnaDo != null));

        }

        public bool IsTrener()
        {
            return ((TrenerCisloRegistrace != null) || (TrenerPlatnaDo != null));

        }


    }

    //[AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    //public class AdresaRequiredAttibute : ValidationAttribute, IClientValidatable
    //{
    //    int? MyNullableInt = null;
    //    public string PoleFieldName { get; set; }
    //    public string JeClenFieldName { get; set; }
    //    public string AdresaUliceFieldName { get; set; }
    //    public string AdresaCisloPopisneFieldName { get; set; }
    //    public string AdresaObecFieldName { get; set; }
    //    public string AdresaPscFieldName { get; set; }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var jeClenDataValue = validationContext.ObjectType.GetProperty(JeClenFieldName).GetValue(validationContext.ObjectInstance, null);
    //        bool jeClen = jeClenDataValue != null;

    //        var adresaUliceDataValue = validationContext.ObjectType.GetProperty(AdresaUliceFieldName).GetValue(validationContext.ObjectInstance, null);
    //        string adresaUlice = adresaUliceDataValue != null ? (string)adresaUliceDataValue : null;

    //        var adresaCisloPopisneDataValue = validationContext.ObjectType.GetProperty(AdresaCisloPopisneFieldName).GetValue(validationContext.ObjectInstance, null);
    //        int? adresaCisloPopisne = adresaCisloPopisneDataValue != null ? (int)adresaCisloPopisneDataValue : MyNullableInt;
            
    //        var adresaObecDataValue = validationContext.ObjectType.GetProperty(AdresaObecFieldName).GetValue(validationContext.ObjectInstance, null);
    //        string adresaObec = adresaObecDataValue != null ? (string)adresaObecDataValue : null;

    //        var adresaPscDataValue = validationContext.ObjectType.GetProperty(AdresaPscFieldName).GetValue(validationContext.ObjectInstance, null);
    //        int? adresaPsc = adresaPscDataValue != null ? (int)adresaPscDataValue : MyNullableInt;


    //        if ((jeClen) || (adresaUlice != null) || (adresaCisloPopisne != null) || (adresaObec != null) || (adresaPsc != null))
    //        {
    //            //if ((adresaUlice == null))
    //            //{
    //            //    return new ValidationResult(ErrorMessage);
    //            //}

    //            if ((adresaCisloPopisne == null))
    //            {
    //                return new ValidationResult(ErrorMessage);
    //            }

    //            if ((adresaObec == null))
    //            {
    //                return new ValidationResult(ErrorMessage);
    //            }

    //            if ((adresaPsc == null))
    //            {
    //                return new ValidationResult(ErrorMessage);
    //            }
    //        }

    //        return ValidationResult.Success;

    //    }

    //    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    //    {
            
    //        var rule = new ModelClientValidationRule
    //        {
    //            ErrorMessage = ErrorMessage,
    //            ValidationType = PoleFieldName.ToLower()+"a"
    //        };

    //        rule.ValidationParameters[PoleFieldName.ToLower() + "b"] = PoleFieldName;

    //        yield return rule;
    //    }
    //}

    //[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    //[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
    public class RequiredIfNotEmptyaAttribute : RequiredIfNotEmptyAttribute
    {

        public RequiredIfNotEmptyaAttribute(string dependentProperty)
            : base(dependentProperty) 
        {
            Register.Attribute(typeof(RequiredIfNotEmptyaAttribute));
        }

        public override bool IsValid(object value, object dependentValue, object container)
        {

            if (!string.IsNullOrEmpty((dependentValue ?? string.Empty).ToString().Trim()))
                return value != null && !string.IsNullOrEmpty(value.ToString().Trim());

            return true;
        }

        public override string DefaultErrorMessage
        {
            get { return "{0} is required due to {1} not being empty."; }
        }
    };
    public class RequiredIfNotEmptybAttribute : RequiredIfNotEmptyAttribute
    {

        public RequiredIfNotEmptybAttribute(string dependentProperty)
            : base(dependentProperty) 
        {
            Register.Attribute(typeof(RequiredIfNotEmptybAttribute));
        }

        public override bool IsValid(object value, object dependentValue, object container)
        {

            if (!string.IsNullOrEmpty((dependentValue ?? string.Empty).ToString().Trim()))
                return value != null && !string.IsNullOrEmpty(value.ToString().Trim());

            return true;
        }

        public override string DefaultErrorMessage
        {
            get { return "{0} is required due to {1} not being empty."; }
        }
    };

    //public class RequiredIfNotEmpty2Attribute : ContingentValidationAttribute
    //{
    //    public RequiredIfNotEmpty2Attribute(string dependentProperty)
    //        : base(dependentProperty) { }

    //    public override bool IsValid(object value, object dependentValue, object container)
    //    {

    //        if (!string.IsNullOrEmpty((dependentValue ?? string.Empty).ToString().Trim()))
    //            return value != null && !string.IsNullOrEmpty(value.ToString().Trim());

    //        return true;
    //    }

    //    public override string DefaultErrorMessage
    //    {
    //        get { return "{0} is required due to {1} not being empty."; }
    //    }
    //}

}