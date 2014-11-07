namespace SlavojMVC4_1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Validation;

    public partial class SlavojDBContainer : DbContext
    {
        private static readonly Dictionary<int, string> _sqlErrorTextDict =
            new Dictionary<int, string>
        {
            {547,
             "This operation failed because another data entry uses this entry."},        
            {2601,
             "One of the properties is marked as Unique index and there is already an entry with that value."},
             {2627,
                 "Duplicitní hodnota."
             }
        };
        /// <summary>
        /// This decodes the DbUpdateException. If there are any errors it can
        /// handle then it returns a list of errors. Otherwise it returns null
        /// which means rethrow the error as it has not been handled
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>null if cannot handle errors, otherwise a list of errors</returns>
        IEnumerable<ValidationResult> TryDecodeDbUpdateException(DbUpdateException ex)
        {
            if (!(ex.InnerException is System.Data.Entity.Core.UpdateException) ||
                !(ex.InnerException.InnerException is System.Data.SqlClient.SqlException))
                return null;
            var sqlException =
                (System.Data.SqlClient.SqlException)ex.InnerException.InnerException;
            var result = new List<ValidationResult>();
            for (int i = 0; i < sqlException.Errors.Count; i++)
            {
                var errorNum = sqlException.Errors[i].Number;
                var errorMessage = sqlException.Errors[i].Message;
                var errorClass = sqlException.Errors[i].Class;
                var errorProcedure = sqlException.Errors[i].Procedure;
                //Duplicitní klíč

                if (errorNum == 2627)
                {
                    string[] propertyName = new string[2];
                    if (errorMessage.IndexOf("IX_Registrace_CisloRegistrace") > -1)
                    {
                        propertyName[0] = "CisloRegistrace";
                        propertyName[1] = "Registrace";
                        result.Add(new ValidationResult("Číslo registrace už existuje.", propertyName));
                        break;
                    }
                    else if (errorMessage.IndexOf("IX_Rozhodci_CisloRegistrace") > -1)
                    {
                        propertyName[0] = "CisloRegistrace";
                        propertyName[1] = "Rozhodci";
                        result.Add(new ValidationResult("Číslo registrace už existuje.", propertyName));
                        break;
                    }
                    else if (errorMessage.IndexOf("IX_Treneri_CisloRegistrace") > -1)
                    {
                        propertyName[0] = "CisloRegistrace";
                        propertyName[1] = "Trener";
                        result.Add(new ValidationResult("Číslo registrace už existuje.", propertyName));
                        break;
                    }
                    else if (errorMessage.IndexOf("IX_DruzstvaCleniDruzstvoIdClenId") > -1)
                    {
                        propertyName[0] = "ClenId";
                        result.Add(new ValidationResult("Zadaný člen už existuje.", propertyName));
                        break;
                    }
                    else if (errorMessage.IndexOf("IX_UserClen_UserId") > -1)
                    {
                        propertyName[0] = "UserId";
                        result.Add(new ValidationResult("Zadaný uživatel je už propojen s nějakým členem.", propertyName));
                        break;
                    }
                    else if (errorMessage.IndexOf("IX_UserClen_ClenId") > -1)
                    {
                        propertyName[0] = "ClenId";
                        result.Add(new ValidationResult("Zadaný člen je už propojen s nějakým uživatelem.", propertyName));
                        break;
                    }
                    else if (errorMessage.IndexOf("IX_webpages_UsersInRoles_UserId_RoleId") > -1)
                    {
                        propertyName[0] = "RoleId";
                        result.Add(new ValidationResult("Člen už má přidělenu tuto roli.", propertyName));
                        break;
                    }
                    else
                    {
                        result.Add(new ValidationResult(errorMessage));
                    }
                }
                else if (errorNum == 50000)
                {
                    if (errorProcedure == "tiuCleni")
                    {
                        result.Add(new ValidationResult(errorMessage, new[] { "RodneCislo" }));
                        break;

                    }
                    else
                    {
                        result.Add(new ValidationResult(errorMessage));
                    }
                }
                else
                {
                    result.Add(new ValidationResult(errorMessage));

                }

                //string errorText;
                //if (_sqlErrorTextDict.TryGetValue(errorNum, out errorText))
                //    result.Add(new ValidationResult(errorText));
            }
            return result.Any() ? result : null;
        }

        public EfStatus SaveChangesWithValidation()
        {
            var status = new EfStatus();
            try
            {

                SaveChanges(); //then update it
            }
            catch (DbEntityValidationException ex)
            {
                return status.SetErrors(ex.EntityValidationErrors);
            }
            catch (DbUpdateException ex)
            {
                var decodedErrors = TryDecodeDbUpdateException(ex);
                if (decodedErrors == null)
                    throw; //it isn't something we understand so rethrow
                return status.SetErrors(decodedErrors);

            }
            //else it isn't an exception we understand so it throws in the normal way
            return status;
        }

        public void SetModifyFields(object entity)
        {

            foreach (string n in this.Entry(entity).CurrentValues.PropertyNames)
            {
                if (!Equals(this.Entry(entity).OriginalValues[n], this.Entry(entity).CurrentValues[n]))
                {
                    //Nastaví, že je položka modifikována
                    ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity).SetModifiedProperty(n);
                }
            }
        }

    }

    //.................................................................................................................................................
    public class EfStatus
    {
        private System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult> _errors;
        /// <summary>
        /// If there are no errors then it is valid
        /// </summary>
        public bool IsValid { get { return _errors == null; } }
        public System.Collections.Generic.IReadOnlyList<System.ComponentModel.DataAnnotations.ValidationResult> EfErrors
        {
            get { return _errors ?? new System.Collections.Generic.List<System.ComponentModel.DataAnnotations.ValidationResult>(); }
        }
        /// <summary>
        /// This converts the Entity framework errors into Validation errors
        /// </summary>
        public EfStatus SetErrors(System.Collections.Generic.IEnumerable<System.Data.Entity.Validation.DbEntityValidationResult> errors)
        {
            _errors =
                errors.SelectMany(
                    x => x.ValidationErrors.Select(y =>
                          new System.ComponentModel.DataAnnotations.ValidationResult(y.ErrorMessage, new[] { y.PropertyName })))
                    .ToList();
            return this;
        }

        public EfStatus SetErrors(System.Collections.Generic.IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> errors)
        {

            _errors = errors.ToList();

            return this;
        }
    }
}