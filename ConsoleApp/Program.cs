using Newtonsoft.Json;
using OpenCalais.Converters;
using OpenCalais.Objects;
using OpenCalais.Objects.Relations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("JSON Entry : ");
            var entry = File.ReadAllText(@"C:\Users\AMA3694\Documents\oc - Copy - Copy.json");
            var doc = OpenCalais.Parsers.OCParser.Parse(entry);

            var companies = doc.Where((i) =>
            {
                if(i.Value is Entity e)
                {
                    return e.Type == ObjectType.Company;
                }
                return false;
            });

            var persons = doc.Where((i) =>
            {
                if (i.Value is Entity e)
                {
                    return e.Type == ObjectType.Person;
                }
                return false;
            });

            var technologies = doc.Where((i) =>
            {
                if (i.Value is Entity e)
                {
                    return e.Type == ObjectType.Technology;
                }
                return false;
            });

            var tags = doc.Where(i => i.Value is SocialTag);

            Console.WriteLine($"Companies in this document :");
            foreach (var item in companies)
            {
                var company = item.Value as Entity;
                Console.WriteLine($"Name : {company.Name}");
            }

            Console.WriteLine($"TAG in this documents :");
            foreach(var t in tags)
            {
                var tag = t.Value as SocialTag;
                Console.WriteLine($"Tag value : {tag.Name}");
            }


            Console.WriteLine($"Technologies term in this document :");
            foreach (var item in technologies)
            {
                var techno = item.Value as Entity;
                Console.WriteLine($"Name : {techno.Name}");
            }

            Console.WriteLine($"Persons mentionned in this document :");
            foreach (var item in persons)
            {
                var person = item.Value as Entity;


                Entity email = null;
                if (doc.Select(e => e.Value).FirstOrDefault((i) => i is PersonEmailAddress association && association.PersonId == item.Key) is PersonEmailAddress emailAssociation)
                {
                    email = doc.FirstOrDefault((i) =>
                    {
                        return i.Value is Entity e
                            && e.Type == ObjectType.EmailAddress
                            && i.Key == emailAssociation.EmailAddressId;
                    }).Value as Entity;
                }

                Console.WriteLine($"Name : {person.Name}");
                if(email != null)
                    Console.WriteLine($"Email Address : {email.Name}");
            }

            Console.ReadLine();
        }
    }
}
