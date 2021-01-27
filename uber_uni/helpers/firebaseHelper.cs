using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using System.Collections.ObjectModel;
using System.Reactive;


namespace uber_uni
{

    public class firebaseHelper
    {

        FirebaseClient firebase = new FirebaseClient("https://test-89c21.firebaseio.com/");

        public async Task<int> addNewPerson(User user)
        {
            try
            {
                {
                 await firebase
                        .Child("Users")
                        .Child(user.email.Replace("@" ,"_").Replace(".", "_") + user.phone)
                        .PutAsync(user);

                    return 1;
                } 
            }
            catch (Exception ex)
            {
                return 0;
            }
        }



        public ObservableCollection<User> GetAllPersons()
        {
            try
            {
                var list = firebase
                 .Child("Users")
                 .AsObservable<User>().AsObservableCollection();
                return list;
            } catch ( Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ObservableCollection<User>();
            }
        
        }

        public async Task removeUser(User user)
        {
            try
            {
              await firebase.Child("Users").Child(user.email.Replace("@", "_").Replace(".", "_") + user.phone).DeleteAsync();
            } catch (Exception ex )
            {
              Console.WriteLine(ex.Message);
            }
        }

    }
}


