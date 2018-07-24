using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace ElectionDb
{
    public class ElectionDb
    {
        private static OthersEntities db;
        private IQueryable<Elector> currentResult;
        private string currentSql;
        public ElectionDb()
        {
            if (db == null) db = new OthersEntities();
            // db.Configuration.AutoDetectChangesEnabled = false;
            db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s, "Anas");

        }
        private bool IsWherAdded = false;
        private List<object> parameters;
        public void FilterElctorByName(string name, bool continueSavedSearch = false)
        {
            string[] nameParts = name.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            string nn = nameParts[0];
            bool useSql = true;

            if (useSql)
            {
                string command = "SELECT [NationalNo],[Name],[Gender],[BirthDate],[GovernateName],[MunicipalName],[ElectionDepartmentName],[ElectionCenterName],";
                command += "[GovernateID],[MunicipalID],[ElectionDepartmentID],[ElectionCenterID],[Age],[Voted],[VotedCount],[Photo] FROM [Others].[dbo].[Electors]";
                if (IsWherAdded)
                {
                    for (int i = 0; i < nameParts.Length; i++)
                    {
                        command += " AND [Name] Like @param" + i.ToString() + " ";
                        parameters.Add(new SqlParameter("param" + i.ToString(), "%" + nameParts[i] + "%") { SqlDbType = SqlDbType.NVarChar });
                    }
                }
                else
                {
                    IsWherAdded = true;
                    parameters = new List<object>();
                    if (nameParts.Length > 0)
                    {
                        command += " Where ";
                        command += " [Name] Like @param0";
                        parameters.Add(new SqlParameter("param0", "%" + nameParts[0] + "%") { SqlDbType = SqlDbType.NVarChar });
                    }
                    for (int i = 1; i < nameParts.Length; i++)
                    {
                        command += " AND [Name] Like @param" + i.ToString();
                        parameters.Add(new SqlParameter("param" + i.ToString(), "%" + nameParts[i] + "%") { SqlDbType = SqlDbType.NVarChar });
                    }


                }

                currentResult = db.Database.SqlQuery<Elector>(command, parameters.ToArray()).AsQueryable<Elector>();

            }
            else
            {
                currentResult = continueSavedSearch ? currentResult.Where(e => nameParts.All(n => e.Name.Contains(n))) : db.Electors.AsNoTracking().Where(e => nameParts.All(n => e.Name.Contains(n)));
            }
            currentSql = currentResult.ToString();
        }


        public void FilterByElectorByGovernate(String governates, bool continueSavedSearch = true)
        {
            string id = db.Governates.First(g => g.Name == governates).ID;
            currentResult = continueSavedSearch ? currentResult.Where(e => e.GovernateID == id) : db.Electors.AsNoTracking().Where(e => e.GovernateID == id);
        }
        public void FilterByGender(string gender, bool continueSavedSearch = true)
        {
            currentResult = continueSavedSearch ? currentResult.Where(e => e.Gender == gender) : db.Electors.AsNoTracking().Where(e => e.Gender == gender);
        }
        public ObservableCollection<Person> GetResults()
        {
            var results = currentResult.Select(e => new Person
            {
                image = e.Photo,
                dob = e.BirthDate,
                NationalNumber = e.NationalNo,
                Name = e.Name,
                MunicipalName = e.MunicipalName,
            });
            return new ObservableCollection<Person>(results);

        }

        public void SortByName(bool ascending = true)
        {
            currentResult = ascending ? currentResult.OrderBy(e => e.Name) : currentResult.OrderByDescending(e => e.Name);
        }
        public void SortByAge(bool ascending = true)
        {
            currentResult = ascending ? currentResult.OrderBy(e => e.BirthDate) : currentResult.OrderByDescending(e => e.BirthDate);
        }


    }







}
