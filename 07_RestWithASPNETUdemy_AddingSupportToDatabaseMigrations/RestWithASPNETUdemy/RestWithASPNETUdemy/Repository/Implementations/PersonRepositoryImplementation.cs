﻿using RestWithASPNETUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using RestWithASPNETUdemy.Model.Context;

namespace RestWithASPNETUdemy.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MySqlContext _context;

        public PersonRepositoryImplementation(MySqlContext context)
        {
            _context = context;

        }

        public List<Person> FindAll()
        {
            return _context.Person.ToList();
        }

        public Person FindByID(long id)
        {
            return _context.Person.SingleOrDefault((System.Linq.Expressions.Expression<Func<Person, bool>>)(p => (bool)p.Id.Equals(id)));
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                throw e;
            }
            return person;
        }
        public Person Update(Person person)
        {
            if (!Exists(person.Id))
            {
                return null;
            }

            var result = _context.Person.SingleOrDefault((System.Linq.Expressions.Expression<Func<Person, bool>>)(p => (bool)p.Id.Equals((long)person.Id)));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();

                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return person;
        }
        public void Delete(long id)
        {
            var result = _context.Person.SingleOrDefault((System.Linq.Expressions.Expression<Func<Person, bool>>)(p => (bool)p.Id.Equals(id)));
            if (result != null)
            {
                try
                {
                    _context.Person.Remove(result);
                    _context.SaveChanges();

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public bool Exists(long id)
        {
            return _context.Person.Any((System.Linq.Expressions.Expression<Func<Person, bool>>)(p => (bool)p.Id.Equals(id)));
        }
    }
}
