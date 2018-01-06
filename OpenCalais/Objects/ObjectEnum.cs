using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCalais.Objects
{
    public enum ObjectGroupType
    {
        None,
        Topics,
        Entities,
        Relations,
        SocialTag,
        Language,
        Industry,
        Versions
    }

    public enum ObjectType
    {
        None,
        PersonCareer,
        PersonEmailAddress,
        ContactDetails,
        Person,
        Company,
        Organization,
        Url,
        IndustryTerm,
        Position,
        Technology,
        PhoneNumber,
        EmailAddress,
        Facility,
        ProgrammingLanguage,
        Country,
        OperatingSystem,
        Continent,
        City,
        Currency,
        ProvinceOrState
    }
}
