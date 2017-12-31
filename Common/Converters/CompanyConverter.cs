using Common.Dtos;
using OC = OpenCalais.Objects;
using System.Collections.Generic;

namespace Common.Converters
{
    public class CompanyConverter
    {
        public static IEnumerable<Company> Convert(IEnumerable<OC.Entity> companies)
        {
            var companiesEntities = new List<Company>();

            foreach(var company in companies)
            {
                string name = string.Empty;
                string permLink = string.Empty;

                if (company.Resolutions is List<OC.Resolution> resolutions && resolutions.Count > 0)
                {
                    var res = resolutions[0];
                    name = res.CommonName;
                    permLink = res.Id;
                }
                else
                {
                    name = company.Name;
                }

                companiesEntities.Add(new Company
                {
                    Name = name,
                    PermIdLink = permLink
                });
            }

            return companiesEntities;
        }
    }
}
