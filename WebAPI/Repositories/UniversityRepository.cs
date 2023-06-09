﻿using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.ViewModels.Universities;

namespace WebAPI.Repositories;

public class UniversityRepository : GenericRepository<University>, IUniversityRepository
{
    // panggil context dari bookingroomdbcontext
    public UniversityRepository(BookingRoomsDBContext context) : base(context)
    { 
    }

    public University CreateWithValidate(University university)
    {
        try
        {
            var existingUniversityWithCode = _context.Universities.FirstOrDefault(u => u.Code == university.Code);
            var existingUniversityWithName = _context.Universities.FirstOrDefault(u => u.Name == university.Name);

            if (existingUniversityWithCode != null & existingUniversityWithName != null)
            {
                university.Guid = existingUniversityWithCode.Guid;

                _context.SaveChanges();

            }
            else
            {
                _context.Universities.Add(university);
                _context.SaveChanges();
            }

            return university;

        }
        catch
        {
            return null;
        }
    }



    /*public IEnumerable<UniversityVM> GetByName(string name)
    {
        return (IEnumerable<UniversityVM>)_context.Set<University>().Where(u => u.Name.Contains(name));
    }*/

}
