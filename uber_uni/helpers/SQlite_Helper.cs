using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace uber_uni
{

    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Trip>().Wait();
            _database.CreateTableAsync<Compnaion>().Wait();
        }

        
        public async Task<List<User>> GetAllUsers()
        {
            return await _database.Table<User>().ToListAsync();
        }


        //retreive spesific user using id number
        public Task<User> GetSpesificUser(int id)
        {
            return _database.Table<User>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }


        //get companions from local db
        public Task<List<Compnaion>> geAllCompanions()
        {
            return _database.Table<Compnaion>().ToListAsync();
        }


        //get trip from local db
        public Task<List<Trip>> geAllTrips()
        {
            return _database.Table<Trip>().ToListAsync();
        }



        //save new trip to local db
        public Task<int> saveNewTrip(Trip trip)
        {
            return _database.InsertAsync(trip);
        }

        //save new companion to local db
        public async Task<int> saveNewCompanion(Compnaion comp)
        {
            return await _database.InsertAsync(comp);
        }

        //save new user to local db
        public Task<int> SaveNewUser(User user)
        {
            if (user.ID != 0)
            {
                return _database.UpdateAsync(user);
            }
            else
            {
                return _database.InsertAsync(user);
            }
        }


    }


 


}
