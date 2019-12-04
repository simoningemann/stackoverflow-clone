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
                db.Profiles.Add(profile);
                db.SaveChanges();
                return profile;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Profile GetProfile(string email)
        {
            using var db = new StackOverflowContext();
            var profile = db.Profiles.Where(x => x.Email == email).Select(x => x).FirstOrDefault();

            return profile;
        }

        public Profile DeleteProfile(string email)
        {
            using var db = new StackOverflowContext();
            var profile = db.Profiles.Where(x => x.Email == email).Select(x => x).FirstOrDefault();
            
            try
            {
                db.Profiles.Remove(profile);
                db.SaveChanges();
                return profile;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Profile UpdateProfilePassword(string email, string newHash)
        {
            using var db = new StackOverflowContext();
            var profile = db.Profiles.Where(x => x.Email == email).Select(x => x).FirstOrDefault();
            
            try
            {
                profile.Hash = newHash;
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