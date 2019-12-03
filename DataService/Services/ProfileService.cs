using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services.Interfaces;

namespace rawdata_portfolioproject_2.Services
{
    public class ProfileService : IProfileService
    {
        public Profile CreateProfile(string email, string salt, string hash)
        {
            using var db = new StackOverflowContext();
            var profile = new Profile();
            profile.ProfileId = NextProfileId(db);
            profile.Email = email;
            profile.Salt = salt;
            profile.Hash = hash;

            try
            {
                db.Add(profile);
                db.SaveChanges();
                return profile;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private int NextProfileId(StackOverflowContext db)
        {
            int id = 1;
            var ids = db.Profiles.Select(x => x.ProfileId);

            while (ids.Contains(id))
                id = id + 1;

            return id;
        }
    }
}